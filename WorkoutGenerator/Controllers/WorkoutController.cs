using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutGenerator.Data;
using WorkoutGenerator.Extensions;
using WorkoutGenerator.Models;
using WorkoutGenerator.Services;

namespace WorkoutGenerator.Controllers
{
    [Authorize]
    public class WorkoutController : Controller
    {
        private readonly ApplicationDbContext _db;
        private ProgramService _programService;

        public WorkoutController(ApplicationDbContext db, ProgramService programService)
        {
            _db = db;
            _programService = programService;
        }

        public IActionResult ProgressPartialAjax(int id)
        {
            var workout = _db.Workouts.Single(x => x.Id == id);
            var lastWorkoutHistory = workout.WorkoutHistories.Last();
            WorkoutHistory progressWorkoutHistory = ProgressWorkout(lastWorkoutHistory, workout);
            workout.WorkoutHistories.Add(progressWorkoutHistory);

            _db.SaveChanges();
            var workoutHistoryVm = GetWorkoutHistoryViewModel(lastWorkoutHistory, progressWorkoutHistory);
            return new JsonResult(new
            {
                partial = this.RenderViewAsync("Program/_WorkoutHistoryPartial",
                    workoutHistoryVm, true),
                name = workout.Name,
                count = workout.WorkoutHistories.Count
            });
        }

        public IActionResult WorkoutHistoryAjax(int workoutHistoryId)
        {
            WorkoutHistory workoutHistory = _db.WorkoutHistories.Single(x => x.Id == workoutHistoryId);
            return new JsonResult(new
            {
                partial = this.RenderViewAsync("Program/_WorkoutHistoryPartial",
                    new WorkoutHistoryViewModel(workoutHistory), true),
                name = workoutHistory.Workout.Name
            });
        }

        private WorkoutHistoryViewModel GetWorkoutHistoryViewModel(WorkoutHistory before, WorkoutHistory after)
        {
            var afterVm = new WorkoutHistoryViewModel(after);


            for (int i = 0; i < afterVm.MuscleExerciseViewModels.Count; i++)
            {
                for (int j = 0; j < afterVm.MuscleExerciseViewModels[i].Exercises.Count; j++)
                {
                    var afterExercise = afterVm.MuscleExerciseViewModels[i].Exercises[j];
                    if (j == before.MuscleExercises[i].Exercises.Count)
                    {
                         afterExercise.AddedExercise = true;
                         break;
                    }

                    var beforeExercise = before.MuscleExercises[i].Exercises[j];
                    for (int k = 0; k < afterExercise.SetViewModels.Count; k++)
                    {
                        var afterSet = afterExercise.SetViewModels[k];
                        if (k == beforeExercise.Sets.Count)
                        {
                            afterSet.SetAdded = true;
                            break;
                        }

                        var beforeSet = beforeExercise.Sets[k];

                        if (afterSet.Reps != beforeSet.Reps)
                        {
                            afterSet.RepsChanged = true;
                        }

                        if (afterSet.Rest != beforeSet.Rest)
                        {
                            afterSet.RestChanged = true;
                        }

                    }
                }
            }

            return afterVm;
        }


        private WorkoutHistory ProgressWorkout(WorkoutHistory wh, Workout w)
        {
            var workoutHistory = CloneWorkoutHistory(wh);
            var r = new Random();
            foreach (var workoutHistoryMuscleExercise in workoutHistory.MuscleExercises)
            {
                var exercises = workoutHistoryMuscleExercise.Exercises;
                var mechanicalRepsCount = exercises.SelectMany(x => x.Sets).Count(x => x.Reps < 10);
                var stressRepsCount = exercises.SelectMany(x => x.Sets).Count(x => x.Reps >= 10);
                int numOfExercisesToProgress = (int) Math.Ceiling((decimal) exercises.Count / 2);
                Dictionary<int, WorkoutExercise> selectedExercises;
                VolumeType vt;
                RepsType[] neededReps;

                if (mechanicalRepsCount == stressRepsCount)
                {
                    var volumeTypes = new[] {VolumeType.MetabolicStress, VolumeType.MechanicalTension};
                    vt = volumeTypes[r.Next(volumeTypes.Length)];
                }

                else if (stressRepsCount < mechanicalRepsCount)
                {
                    vt = VolumeType.MetabolicStress;
                }
                else
                {
                    vt = VolumeType.MechanicalTension;
                }

                switch (vt)
                {
                    case VolumeType.MetabolicStress:
                        selectedExercises = exercises.Where(x => x.Sets.Any(s => s.Reps < 10)).Select((e,i)=> new {i,e}).ToDictionary(x=> x.i, x=> x.e);
                        neededReps = new[] {RepsType.HighAdvanced, RepsType.HighInter, RepsType.HighNovice};
                        break;
                    case VolumeType.MechanicalTension:

                        selectedExercises = exercises.Where(x => x.Sets.Any(s => s.Reps >= 10)).Select((e, i) => new { i, e }).ToDictionary(x => x.i, x => x.e);
                        neededReps = new[] {RepsType.Low, RepsType.MedAdvanced, RepsType.MedInter, RepsType.MedNovice};
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                var modifyFuncs = new ProgramService.ModifyExerciseDelegate[]
                {
                    _programService.ModifyExerciseChangeReps,
                    _programService.ModifyExerciseChangeRest,
                    _programService.ModifyExerciseChangeSet
                };

                selectedExercises = selectedExercises.OrderBy(x=> r.Next()).Take(numOfExercisesToProgress).ToDictionary(x=>x.Key,x=>x.Value);

                foreach (var selectedExercise in selectedExercises)
                {
                    var exerciseSettings = _programService.GetRelevantExerciseData(selectedExercise.Key,
                        w.Template.TrainerLevelType, workoutHistoryMuscleExercise.MuscleType);

                    var exerciseSetting = exerciseSettings.Where(x => neededReps.Any(y => y == x.Key))
                        .OrderBy(x => r.Next())
                        .Take(1).Select(x => x.Value).Single();

                    modifyFuncs = modifyFuncs.OrderBy(x => r.Next()).ToArray();
                    foreach (var modifyExerciseDelegate in modifyFuncs)
                    {
                        if (modifyExerciseDelegate.Invoke(selectedExercise.Value, exerciseSetting)) break;
                    }
                }
            }

            return workoutHistory;
        }

        private WorkoutHistory CloneWorkoutHistory(WorkoutHistory wh)
        {
            var workoutHistory = new WorkoutHistory();
            foreach (var m in wh.MuscleExercises)
            {
                var me = new MuscleExercises {MuscleType = m.MuscleType};
                foreach (var e in m.Exercises)
                {
                    var ne = new WorkoutExercise {Name = e.Name};
                    foreach (var s in e.Sets)
                    {
                        ne.Sets.Add(new Set() {Reps = s.Reps, Rest = s.Rest});
                    }

                    me.Exercises.Add(ne);
                }

                workoutHistory.MuscleExercises.Add(me);
            }

            return workoutHistory;
        }
    }
}
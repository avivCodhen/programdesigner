using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutGenerator.Data;
using WorkoutGenerator.Extensions;
using WorkoutGenerator.Models;

namespace WorkoutGenerator.Controllers
{
    [Authorize]
    public class WorkoutController : Controller
    {
        private readonly ApplicationDbContext _db;

        public WorkoutController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult ProgressPartialAjax(int id)
        {
            var workout = _db.Workouts.Single(x => x.Id == id);
            var lastWorkoutHistory = workout.WorkoutHistories.Last();
            WorkoutHistory progressWorkoutHistory = ProgressWorkout(lastWorkoutHistory);
            workout.WorkoutHistories.Add(progressWorkoutHistory);

            _db.SaveChanges();
            return new JsonResult(new
            {
                partial = this.RenderViewAsync("Program/_WorkoutHistoryPartial",
                    new WorkoutHistoryViewModel(progressWorkoutHistory), true),
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

        private WorkoutHistory ProgressWorkout(WorkoutHistory wh)
        {
            var volume = new Dictionary<VolumeType, VolumeSettings>()
            {
                {
                    VolumeType.MetabolicStress, new VolumeSettings()
                    {
                        VolumeProgressTypes = new[]
                        {
                            VolumeProgressType.AddHighReps,
                            VolumeProgressType.ChangeToHighReps,
                            VolumeProgressType.ShortRest
                        }
                    }
                },
                {
                    VolumeType.MechanicalTension, new VolumeSettings()
                    {
                        VolumeProgressTypes = new[]
                        {
                            VolumeProgressType.AddMidLowReps,
                            VolumeProgressType.ChangeToLowReps
                        }
                    }
                },
            };
            var workoutHistory = CloneWorkoutHistory(wh);
            var exercises = workoutHistory.MuscleExercises.SelectMany(x => x.Exercises).ToList();
            var mechanicalRepsCount = exercises.SelectMany(x => x.Sets).Select(x => x.Reps <= 10).Count();
            var stressRepsCount = exercises.SelectMany(x => x.Sets).Select(x => x.Reps > 10).Count();

            if (mechanicalRepsCount > stressRepsCount)
            {
                //should add stress
            }
            else if (stressRepsCount > mechanicalRepsCount)
            {
                //should add mechanical
            }
            else
            {
                //random on which to add
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
                        ne.Sets.Add(new Set() {NumberOfSets = s.NumberOfSets, Reps = s.Reps, Rest = s.Rest});
                    }

                    me.Exercises.Add(ne);
                }

                workoutHistory.MuscleExercises.Add(me);
            }

            return workoutHistory;
        }
    }
}
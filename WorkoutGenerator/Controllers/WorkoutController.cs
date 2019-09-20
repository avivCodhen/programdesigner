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
                partial = this.RenderViewAsync("Program/_WorkoutTitlePartial", new WorkoutViewModel(workout), true),
                name = workout.Name
            });
        }

        private WorkoutHistory ProgressWorkout(WorkoutHistory wh)
        {
            var workoutHistory = CloneWorkoutHistory(wh);
            foreach (var me in workoutHistory.MuscleExercises)
            {
                foreach (var workoutExercise in me.Exercises)
                {
                    workoutExercise.Name = Guid.NewGuid().ToString();
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
                        ne.Sets.Add(new Set(){NumberOfSets = s.NumberOfSets, Reps = s.Reps, Rest = s.Rest});
                    }

                    me.Exercises.Add(ne);
                }
                workoutHistory.MuscleExercises.Add(me);
            }

            return workoutHistory;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutGenerator.Data;
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
            (WorkoutHistory progressWorkoutHistory, int[] progressedExerciseIds) = ProgressWorkout(lastWorkoutHistory);
            var vm = new WorkoutViewModel()
            {
                Name = workout.Name,
                WorkoutHistoryViewModels = workout.WorkoutHistories.Select(x => new WorkoutHistoryViewModel()
                {
                    Id = x.Id,
                    MuscleExerciseViewModels = x.MuscleExercises.Select(m => new MuscleExerciseViewModel(m)).ToList()
                }).ToList()
            };
            return PartialView("Program/_WorkoutTitlePartial", );
        }

        private Tuple<WorkoutHistory, int[]> ProgressWorkout(WorkoutHistory workoutHistory)
        {
            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutGenerator.Data;

namespace WorkoutGenerator.Controllers
{
    public class ExerciseController : Controller
    {
        private ApplicationDbContext _db;

        public ExerciseController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult ChangeExercise([FromBody]ChangeExerciseModel model)
        {
            var exercise = _db.WorkoutExercises.Single(x=>x.Id == model.ExerciseId);
            var link = _db.YoutubeVideoQueries.Single(x => x.Query == model.ExerciseName);
            exercise.Name = model.ExerciseName;
            _db.SaveChanges();
            return new JsonResult(new
            {
                name = model.ExerciseName, link.LinkId
            });
        }

        
    }

    public class ChangeExerciseModel
    {
        public string ExerciseName { get; set; }
        public int ExerciseId { get; set; }
    }
}
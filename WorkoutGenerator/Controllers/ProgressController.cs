using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WorkoutGenerator.Controllers
{
    [Authorize]
    public class ProgressController : Controller
    {
        public IActionResult WorkoutProgress(int workoutId)
        {
            return View();
        }
    }
}
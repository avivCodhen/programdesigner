using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkoutGenerator.Data;

namespace WorkoutGenerator.Controllers
{
    public class ProgramController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;

        public ProgramController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> SaveProgramToUser(int programId)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            return RedirectToAction("Index", "Home");
        }
    }
}
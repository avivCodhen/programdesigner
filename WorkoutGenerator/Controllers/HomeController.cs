using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using WorkoutGenerator.Data;
using WorkoutGenerator.Extensions;
using WorkoutGenerator.Extentions;
using WorkoutGenerator.Factories;
using WorkoutGenerator.Models;

using Activity = System.Diagnostics.Activity;

namespace WorkoutGenerator.Controllers
{
    public class HomeController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;
        private ApplicationDbContext _db;
        private readonly HtmlEncoder _htmlEncoder;

        public HomeController(ApplicationDbContext db, HtmlEncoder htmlEncoder,
            SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _htmlEncoder = htmlEncoder;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var vm = new IndexViewModel();
            return View(vm);
        }

        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            var userPrograms = _db.Programs
                .Where(x => x.ApplicationUserId == user.Id)
                .Select(x => new DashboardProgramItemViewModel
                    {
                        Level = x.Template.TrainerLevelType.Description(),
                        Type = x.Template.TemplateType.Description(),
                        Days = x.Template.DaysType.Description(),
                        Created = x.Created,
                        ProgramId = x.Id
                    }
                ).OrderByDescending(x=>x.Created).ToList();
            return View(new DashboardViewModel() {Programs = userPrograms});
        }

        [HttpPost]
        public IActionResult Index(IndexViewModel indexViewModel)
        {
            if (!ModelState.IsValid) return BadRequest();
            return RedirectToAction("GenerateProgram", "Program",
                new
                {
                    level = indexViewModel.TrainerLevelType, days = indexViewModel.DaysType,
                    TemplateType = indexViewModel.TemplateType
                });
        }




        [HttpPost]
        public IActionResult ProgramAjax(string feedback)
        {
            _db.FeedBacks.Add(new FeedBack {Text = _htmlEncoder.Encode(feedback)});
            _db.SaveChanges();
            return Ok();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

    }
}
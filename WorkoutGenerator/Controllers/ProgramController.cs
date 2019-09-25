using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkoutGenerator.Data;
using WorkoutGenerator.Extensions;
using WorkoutGenerator.Factories;
using WorkoutGenerator.Models;
using WorkoutGenerator.Services;
using static WorkoutGenerator.Data.ExerciseEquipmentType;
using static WorkoutGenerator.Data.ExerciseType;

namespace WorkoutGenerator.Controllers
{
    public class ProgramController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private ApplicationDbContext _db;
        public const string ProgramSessionPrefix = "program_";
        private ProgramService _programService;

        public ProgramController(SignInManager<ApplicationUser> signInManager, ApplicationDbContext db,
            ProgramService programService)
        {
            _signInManager = signInManager;
            _db = db;
            _programService = programService;
        }


        public IActionResult Index(int id)
        {
            var program = _db.Programs.Single(x => x.Id == id);
            var vm = GetProgramViewModel(program);
            return View("Program", vm);
        }

        public IActionResult GenerateProgram(TrainerLevelType level, DaysType days, string templateType)
        {
            var template = GetTemplate(level, days, templateType);
            var program = new FitnessProgram {Template = template};
            HttpContext.Session.Set($"{ProgramSessionPrefix}{program.Created}", program);

            var vm = GetProgramViewModel(program);
            return View("Program", vm);
        }

        public IActionResult ProgramJson(TrainerLevelType level = TrainerLevelType.Intermediate,
            DaysType days = DaysType.FourDays, TemplateType templateType = TemplateType.ABC)
        {
            var template = GetTemplate(level, days, templateType.ToString());
            var program = new FitnessProgram {Template = template};
            return new JsonResult(GetProgramViewModel(program));
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveProgramToUser(string created)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            var program = HttpContext.Session.Get<FitnessProgram>($"{ProgramSessionPrefix}{created}");
            program.ApplicationUserId = user.Id;
            _db.Programs.Add(program);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", new {program.Id});
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProgram(int id)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            var program = _db.Programs.Single(x => x.Id == id && user.Id == x.ApplicationUserId);

            _db.Remove(program);
            await _db.SaveChangesAsync();

            return RedirectToAction("Dashboard", "Home");
        }

        private ProgramViewModel GetProgramViewModel(FitnessProgram program)
        {
            var vm = new ProgramViewModel
            {
                TemplateViewModel = new TemplateViewModel(program.Template),
                Id = program.Id,
                Created = program.Created,
                ApplicationIdNull = program.ApplicationUserId == null
            };
            foreach (WorkoutViewModel workoutViewModel in vm.TemplateViewModel.Workouts)
            {
                foreach (MuscleExerciseViewModel muscleExerciseViewModel in workoutViewModel.WorkoutHistoryViewModel
                    .MuscleExerciseViewModels)
                {
                    foreach (ExerciseViewModel exerciseViewModel in muscleExerciseViewModel.Exercises)
                    {
                        var linkId = _db.YoutubeVideoQueries.SingleOrDefault(x => x.Query == exerciseViewModel.Name)
                            ?.LinkId;
                        exerciseViewModel.YoutubeVideoId = linkId;
                    }
                }
            }

            return vm;
        }

        private Template GetTemplate(TrainerLevelType level, DaysType days, string templateType)
        {
            Template template = null;
            TemplateFactory templateFactory = new TemplateFactory(level);
            if (templateType != "DecideForMe")
            {
                template = templateFactory.CreateBasicTemplate(Enum.Parse<TemplateType>(templateType));
            }
            else
            {
                Random random;
                if (days <= DaysType.TwoDays)
                {
                    //FBW
                    template = templateFactory.CreateBasicTemplate(TemplateType.FBW);
                }

                if (days == DaysType.ThreeDays)
                {
                    if (level == TrainerLevelType.Novice)
                    {
                        //FBW
                        template = templateFactory.CreateBasicTemplate(TemplateType.FBW);
                    }

                    if (level == TrainerLevelType.Intermediate)
                    {
                        //FBW/AB (random)
                        var list = new TemplateType[] {TemplateType.AB};
                        random = new Random();
                        template = templateFactory.CreateBasicTemplate(list[random.Next(list.Length)]);
                    }

                    if (level >= TrainerLevelType.Advanced)
                    {
                        //AB
                        template = templateFactory.CreateBasicTemplate(TemplateType.AB);
                    }
                }

                if (days == DaysType.FourDays)
                {
                    if (level <= TrainerLevelType.Intermediate)
                    {
                        //AB
                        template = templateFactory.CreateBasicTemplate(TemplateType.AB);
                    }

                    if (level >= TrainerLevelType.Advanced)
                    {
                        // AB, ABC, ABCD (random)
                        var list = new TemplateType[] {TemplateType.ABCD, TemplateType.AB, TemplateType.ABC};
                        random = new Random();
                        template = templateFactory.CreateBasicTemplate(list[random.Next(list.Length)]);
                    }
                }

                if (days == DaysType.FiveDays)
                {
                    if (level <= TrainerLevelType.Advanced)
                    {
                        //ABC
                        template = templateFactory.CreateBasicTemplate(TemplateType.ABC);
                    }
                    else
                    {
                        //ABC, ABCD, ABCDE
                        var list = new TemplateType[] {TemplateType.ABCD, TemplateType.ABC};
                        random = new Random(list.Length);
                        template = templateFactory.CreateBasicTemplate(list[random.Next(list.Length)]);
                    }
                }

                if (days == DaysType.SixDays || days == DaysType.SevenDays)
                {
                    if (level <= TrainerLevelType.Advanced)
                    {
                        //ABC
                        template = templateFactory.CreateBasicTemplate(TemplateType.ABC);
                    }
                    else
                    {
                        //ABC, ABCD, ABCDE, ABCDEF
                        var list = new[] {TemplateType.AB, TemplateType.ABC, TemplateType.ABCD};
                        random = new Random(list.Length);
                        template = templateFactory.CreateBasicTemplate(list[random.Next(list.Length)]);
                    }
                }

                if (template == null)
                {
                    throw new Exception("could not instantiate template");
                }
            }


            template.DaysType = days;
            template.TrainerLevelType = level;
            foreach (Workout templateWorkout in template.Workouts)
            {
                foreach (MuscleExercises templateWorkoutMuscleExercise in templateWorkout.WorkoutHistories.First()
                    .MuscleExercises)
                {
                    var exercisesOfMuscle = _db.Exercises.Where(x =>
                        x.MuscleType == templateWorkoutMuscleExercise.MuscleType).ToList();

                    for (int i = 0; i < templateWorkoutMuscleExercise.Exercises.Count; i++)
                    {
                        Random r = new Random();
                        var exerciseSettings =
                            _programService.GetRelevantExerciseData(i, level, templateWorkoutMuscleExercise.MuscleType);
                        var exerciseSetting = exerciseSettings[exerciseSettings.Select(x=>x.Key).ToArray()[r.Next(exerciseSettings.Count)]];

                        var workoutExercise = templateWorkoutMuscleExercise.Exercises[i];

                        var exerciseChose = _programService.PickExercise(i, exerciseSetting, exercisesOfMuscle);
                        workoutExercise.Name = exerciseChose.Name;

                        var numOfSets = exerciseSetting.Sets[r.Next(exerciseSetting.Sets.Length)];
                        var reps = exerciseSetting.Reps[r.Next(exerciseSetting.Reps.Length)];
                        var rest = exerciseSetting.Rest[r.Next(exerciseSetting.Rest.Length)];
                        for (int j = 0; j < numOfSets; j++)
                        {
                            var set = new Set() {Reps = reps, Rest = rest};
                            workoutExercise.Sets.Add(set);
                        }

                        exercisesOfMuscle.Remove(exerciseChose);
                    }
                }
            }

            return template;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using WorkoutGenerator.Data;
using WorkoutGenerator.Extensions;
using WorkoutGenerator.Factories;
using WorkoutGenerator.Models;
using static WorkoutGenerator.Data.ExerciseEquipmentType;
using static WorkoutGenerator.Data.ExerciseType;

namespace WorkoutGenerator.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var vm = new IndexViewModel();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(IndexViewModel indexViewModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            var level = indexViewModel.TrainerLevelType;
            var days = indexViewModel.DaysType;
            Template template = null;
            TemplateFactory templateFactory = new TemplateFactory(level);
            if (indexViewModel.TemplateType != "DecideForMe")
            {
                template = templateFactory.CreateBasicTemplate(Enum.Parse<TemplateType>(indexViewModel.TemplateType));
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

            var exerciseSettings = new Dictionary<RepsType, ExerciseSettings>()
            {
                {
                    RepsType.Low, new ExerciseSettings()
                    {
                        AllowedTrainerLevel = new[] {TrainerLevelType.Advanced},
                        ExerciseEquipments = new[] {Barbell.ToString(), Dumbbell.ToString()},
                        ExerciseTypes = new[] {Compound},
                        Reps = new[] {"3", "4", "5"},
                        Sets = new[] {"3,4,5"},
                        Rest = new[] {"1 minute", "1.5 minute", "2 minute", "2.5 minute" }
                    }
                },
                {
                    RepsType.Med, new ExerciseSettings()
                    {
                        AllowedTrainerLevel = new[] {TrainerLevelType.Advanced, TrainerLevelType.Intermediate},
                        ExerciseEquipments = new[] {Barbell.ToString(), Dumbbell.ToString(), Cable.ToString()},
                        ExerciseTypes = new[] {Compound, Isolate},
                        Reps = new[] {"6", "8"},
                        Sets = new[] {"3","4"},
                        Rest = new[] {"1 minute", "45 seconds", "1.5 minute", "2 minute"}
                    }
                },
                {
                    RepsType.High, new ExerciseSettings()
                    {
                        AllowedTrainerLevel = new[]
                            {TrainerLevelType.Advanced, TrainerLevelType.Intermediate, TrainerLevelType.Novice},
                        ExerciseEquipments = new[]
                            {Barbell.ToString(), Dumbbell.ToString(), Cable.ToString(), Machine.ToString()},
                        ExerciseTypes = new[] {Compound, Isolate},
                        Reps = new[] {"10", "12", "15"},
                        Sets = new[] {"3"},
                        Rest = new[] { "30 seconds", "45 seconds", "1 minute", "1.5 minute"}
                    }
                }
            };


            foreach (Workout templateWorkout in template.Workouts)
            {
                foreach (MuscleExercises templateWorkoutMuscleExercise in templateWorkout.MuscleExercises)
                {

                    var exercisesOfMuscle = _db.Exercises.Where(x =>
                        x.MuscleType == templateWorkoutMuscleExercise.MuscleType).ToList();

                    for (int i = 0; i < templateWorkoutMuscleExercise.Exercises.Count; i++)
                    {
                        Random r;
                        var workoutExercise = templateWorkoutMuscleExercise.Exercises[i];
                        ExerciseSettings exerciseSetting;
                        if (i == 0)
                        {
                            if (level == TrainerLevelType.Advanced)
                            {
                                var exSettings = exerciseSettings.Where(x => x.Key == RepsType.Low || x.Key == RepsType.Med)
                                    .ToDictionary(x => x.Key, y => y.Value);
                                r = new Random();
                                exerciseSetting = exSettings[new[] {RepsType.Low, RepsType.Med}[r.Next(exSettings.Count)]];
                            }
                            else
                            {
                                var exSettings = exerciseSettings
                                    .Where(x => x.Key == RepsType.Med)
                                    .ToDictionary(x => x.Key, y => y.Value);
                                r = new Random();
                                exerciseSetting = exSettings[new[] {RepsType.Med}[r.Next(exSettings.Count)]];
                            }
                        }
                        else
                        {
                            var exSettings = exerciseSettings
                                .Where(x => x.Key == RepsType.Med || x.Key == RepsType.High)
                                .ToDictionary(x => x.Key, y => y.Value);
                            r = new Random();
                            exerciseSetting = exSettings[new[] {RepsType.High, RepsType.Med}[r.Next(exSettings.Count)]];
                        }

                        var rRep = new Random();
                        var rRest = new Random();
                        var rSet = new Random();

                        var exercisesToChoose = exercisesOfMuscle.Where(x =>
                            x.Name.ContainsAny(exerciseSetting.ExerciseEquipments)
                            &&
                            exerciseSetting.ExerciseTypes.Any(m => m == x.ExerciseType)
                        ).ToList();
                        
                        var rExercise = new Random();
                        int num = rExercise.Next(exercisesToChoose.Count);
                        var exerciseChoosen = exercisesToChoose[num];
                        workoutExercise.Exercise = exerciseChoosen;
                        workoutExercise.Reps = exerciseSetting.Reps[rRep.Next(exerciseSetting.Reps.Length)];
                        workoutExercise.Rest = exerciseSetting.Rest[rRest.Next(exerciseSetting.Rest.Length)];
                        workoutExercise.Sets = exerciseSetting.Sets[rSet.Next(exerciseSetting.Sets.Length)];
                        exercisesOfMuscle.Remove(exerciseChoosen);
                    }

                }

               
            }

            var program = new BodyBuildingProgram() { Template = template };
            _db.Programs.Add(program);
            _db.SaveChanges();
            return RedirectToAction("Program", new {id = program.Id});
        }

        public IActionResult Program(int id)
        {
            var program = _db.Programs.Single(x=>x.Id == id);

            var vm = new ProgramViewModel()
            {
             TemplateViewModel   = new TemplateViewModel(program.Template)
            };
            return View(vm);
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
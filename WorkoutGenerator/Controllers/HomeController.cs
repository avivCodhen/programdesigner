using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using WorkoutGenerator.Data;
using WorkoutGenerator.Extensions;
using WorkoutGenerator.Factories;
using WorkoutGenerator.Models;
using static WorkoutGenerator.Data.ExerciseEquipmentType;
using static WorkoutGenerator.Data.ExerciseType;
using Activity = System.Diagnostics.Activity;

namespace WorkoutGenerator.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _db;
        private readonly HtmlEncoder _htmlEncoder;

        public HomeController(ApplicationDbContext db, HtmlEncoder htmlEncoder)
        {
            _db = db;
            _htmlEncoder = htmlEncoder;
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
                        Sets = new[] {"3", "4", "5"},
                        Rest = new[] {"1.5 minute", "2 minute", "2.5 minute"}
                    }
                },
                {
                    RepsType.Med, new ExerciseSettings()
                    {
                        AllowedTrainerLevel = new[] {TrainerLevelType.Advanced, TrainerLevelType.Intermediate},
                        ExerciseEquipments = new[] {Barbell.ToString(), Dumbbell.ToString(), Cable.ToString()},
                        ExerciseTypes = new[] {Compound},
                        Reps = new[] {"6", "8"},
                        Sets = new[] {"3", "4"},
                        Rest = new[] {"1 minute", "45 seconds", "1.5 minute", "2 minute"}
                    }
                },


                {
                    RepsType.MedNovice, new ExerciseSettings()
                    {
                        ExcludeExercises = new []{"Front", "Decline", "Incline"},
                        AllowedTrainerLevel = new[] {TrainerLevelType.Advanced, TrainerLevelType.Intermediate},
                        ExerciseEquipments = new[] {Dumbbell.ToString(), Cable.ToString(), Machine.ToString()},
                        ExerciseTypes = new[] {Compound},
                        Reps = new[] {"6", "8"},
                        Sets = new[] {"3", "4"},
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
                        Rest = new[] {"30 seconds", "45 seconds", "1 minute", "1.5 minute"}
                    }
                },
                {
                    RepsType.HighNovice, new ExerciseSettings()
                    {
                        ExcludeExercises = new []{"Front", "Decline"},
                        AllowedTrainerLevel = new[]
                            {TrainerLevelType.Advanced, TrainerLevelType.Intermediate, TrainerLevelType.Novice},
                        ExerciseEquipments = new[]
                            {Dumbbell.ToString(), Cable.ToString(), Machine.ToString()},
                        ExerciseTypes = new[] {Compound, Isolate},
                        Reps = new[] {"10", "12", "15"},
                        Sets = new[] {"3"},
                        Rest = new[] {"45 seconds", "1 minute", "1.5 minute"}
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
                        Random r = new Random();
                        var workoutExercise = templateWorkoutMuscleExercise.Exercises[i];
                        ExerciseSettings exerciseSetting;
                        RepsType[] repTypes;
                        if (i == 0)
                        {
                            switch (level)
                            {
                                case TrainerLevelType.Novice:
                                    repTypes = new[] {RepsType.HighNovice, RepsType.MedNovice};
                                    break;
                                case TrainerLevelType.Intermediate:
                                    if (!templateWorkoutMuscleExercise.MuscleType.IsSmallExercise())
                                    {
                                        repTypes = new[] { RepsType.Med, RepsType.High, RepsType.Low };
                                    }
                                    else
                                    {
                                        repTypes = new[] { RepsType.Med, RepsType.High};

                                    }
                                    break;
                                case TrainerLevelType.Advanced:
                                    repTypes = new[] {RepsType.Med, RepsType.Low};
                                    if (!templateWorkoutMuscleExercise.MuscleType.IsSmallExercise())
                                    {
                                        repTypes = new[] { RepsType.Med, RepsType.Low };
                                    }
                                    else
                                    {
                                        repTypes = new[] { RepsType.Med, RepsType.High };

                                    }
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }

                            exerciseSetting = ExerciseSettings(exerciseSettings, repTypes);
                        }

                        else
                        {
                            switch (level)
                            {
                                case TrainerLevelType.Novice:
                                    repTypes = new[] {RepsType.HighNovice, RepsType.MedNovice};
                                    break;
                                case TrainerLevelType.Intermediate:
                                case TrainerLevelType.Advanced:
                                    repTypes = new[] {RepsType.Med, RepsType.High};
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }

                            exerciseSetting = ExerciseSettings(exerciseSettings, repTypes);
                        }

                        var exercisesToChoose = exercisesOfMuscle.Where(x =>
                            x.Name.ContainsAny(exerciseSetting.ExerciseEquipments)
                            &&
                            exerciseSetting.ExerciseTypes.Any(m => m == x.ExerciseType)
                        ).ToList();

                        if (exerciseSetting.ExcludeExercises != null)
                        {
                            exercisesToChoose = exercisesToChoose
                                .Where(x => !x.Name.ContainsAny(exerciseSetting.ExcludeExercises)).ToList();
                        }
                        if (i == 0  )
                        {
                            exercisesToChoose = exercisesToChoose.Where(x => x.Utility == UtilityType.Basic).ToList();
                        }
                        
                        var rExercise = new Random();
                        int num = rExercise.Next(exercisesToChoose.Count);
                        var exerciseChose = exercisesToChoose[num];
                        workoutExercise.Name = exerciseChose.Name;
                        workoutExercise.Reps = exerciseSetting.Reps[r.Next(exerciseSetting.Reps.Length)];
                        workoutExercise.Rest = exerciseSetting.Rest[r.Next(exerciseSetting.Rest.Length)];
                        workoutExercise.Sets = exerciseSetting.Sets[r.Next(exerciseSetting.Sets.Length)];
                        exercisesOfMuscle.Remove(exerciseChose);
                    }
                }
            }

            var program = new BodyBuildingProgram() {Template = template};
            _db.Programs.Add(program);
            _db.SaveChanges();
            return RedirectToAction("Program", new {id = program.Id});
        }

        private static ExerciseSettings ExerciseSettings(Dictionary<RepsType, ExerciseSettings> exerciseSettings,
            RepsType[] repTypes)
        {
            Random r;
            ExerciseSettings exerciseSetting;
            var relevantExerciseSettings = exerciseSettings
                .Where(x => repTypes.Any(rep => x.Key == rep))
                .ToDictionary(x => x.Key, y => y.Value);
            r = new Random();
            exerciseSetting = relevantExerciseSettings[repTypes[r.Next(relevantExerciseSettings.Count)]];
            return exerciseSetting;
        }

        public IActionResult Program(int id, bool feedback = false)
        {
            var program = _db.Programs.Single(x => x.Id == id);

            var vm = new ProgramViewModel()
            {
                TemplateViewModel = new TemplateViewModel(program.Template),
                FeedBack =  feedback
            };

            foreach (WorkoutViewModel workoutViewModel in vm.TemplateViewModel.Workouts)
            {
                foreach (MuscleExerciseViewModel muscleExerciseViewModel in workoutViewModel.MuscleExerciseViewModels)
                {
                    foreach (ExerciseViewModel exerciseViewModel in muscleExerciseViewModel.Exercises)
                    {
                        var linkId = _db.YoutubeVideoQueries.Single(x => x.Query == exerciseViewModel.Name).LinkId;
                        exerciseViewModel.YoutubeVideoId = linkId;
                    }
                }
            }
            return View(vm);
        }
        [HttpPost]
        public IActionResult Program(int id, string feedback)
        {
            _db.FeedBacks.Add(new FeedBack {Text = _htmlEncoder.Encode(feedback)});
            _db.SaveChanges();
            return Program(id, true);
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
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
using static WorkoutGenerator.Data.ExerciseEquipmentType;
using static WorkoutGenerator.Data.ExerciseType;

namespace WorkoutGenerator.Controllers
{
    public class ProgramController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private ApplicationDbContext _db;
        public const string ProgramSessionPrefix = "program_";

        public ProgramController(SignInManager<ApplicationUser> signInManager, ApplicationDbContext db)
        {
            _signInManager = signInManager;
            _db = db;
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
        public async Task<IActionResult> SaveProgramToUser(DateTime created)
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
                foreach (MuscleExerciseViewModel muscleExerciseViewModel in workoutViewModel.MuscleExerciseViewModels)
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
            var exerciseSettings = new Dictionary<RepsType, ExerciseSettings>()
            {
                {
                    RepsType.Low, new ExerciseSettings()
                    {
                        AllowedTrainerLevel = new[] {TrainerLevelType.Advanced},
                        UtilityType = new UtilityType[]{UtilityType.Basic},
                        ExerciseTypes = new[] {Compound},
                        Reps = new[] {"3", "4", "5"},
                        Sets = new[] {3, 4, 5},
                        Rest = new[] {1.5, 2, 2.5}
                    }
                },
                {
                    RepsType.MedInter, new ExerciseSettings()
                    {
                        AllowedTrainerLevel = new[] {TrainerLevelType.Advanced, TrainerLevelType.Intermediate},
                        UtilityType = new UtilityType[]{UtilityType.Basic,UtilityType.AuxiliaryOrBasic},
                        ExerciseTypes = new[] {Compound},
                        Reps = new[] {"6", "8"},
                        Sets = new[] {3, 4},
                        Rest = new[] {1, 1.5, 2}
                    }
                },
                {
                    RepsType.MedAdvanced, new ExerciseSettings()
                    {
                        AllowedTrainerLevel = new[] {TrainerLevelType.Advanced, TrainerLevelType.Intermediate},
                        UtilityType = new UtilityType[]{UtilityType.Basic,UtilityType.AuxiliaryOrBasic},
                        ExerciseTypes = new[] {Compound},
                        Reps = new[] {"6", "8"},
                        Sets = new[] {3, 4},
                        Rest = new[] {1, 0.45, 1.5,}
                    }
                },

                {
                    RepsType.MedNovice, new ExerciseSettings()
                    {
                        ExcludeExercises = new[] {"Front", "Decline", "Incline"},
                        AllowedTrainerLevel = new[] {TrainerLevelType.Advanced, TrainerLevelType.Intermediate},
                        UtilityType = new UtilityType[]{UtilityType.Basic, UtilityType.AuxiliaryOrBasic},
                        ExerciseTypes = new[] {Compound},
                        Reps = new[] {"6", "8"},
                        Sets = new[] {3, 4},
                        Rest = new[] {1.5, 2}
                    }
                },
                {
                    RepsType.HighInter, new ExerciseSettings()
                    {
                        AllowedTrainerLevel = new[]
                            {TrainerLevelType.Advanced, TrainerLevelType.Intermediate, TrainerLevelType.Novice},
                        UtilityType = new UtilityType[]{UtilityType.Basic, UtilityType.AuxiliaryOrBasic, UtilityType.Auxiliary},
                        ExerciseTypes = new[] {Compound, Isolate},
                        Reps = new[] {"10", "12", "15"},
                        Sets = new[] {3},
                        Rest = new[] {0.45, 1, 1.5}
                    }
                },
                {
                    RepsType.HighAdvanced, new ExerciseSettings()
                    {
                        AllowedTrainerLevel = new[]
                            {TrainerLevelType.Advanced, TrainerLevelType.Intermediate, TrainerLevelType.Novice},
                        ExerciseTypes = new[] {Compound, Isolate},
                        UtilityType = new UtilityType[]{UtilityType.Basic, UtilityType.AuxiliaryOrBasic, UtilityType.Auxiliary},
                        Reps = new[] {"10", "12", "15"},
                        Sets = new[] {3},
                        Rest = new[] {0.35, 1, 0.45}
                    }
                },
                {
                    RepsType.HighNovice, new ExerciseSettings()
                    {
                        ExcludeExercises = new[] {"Front", "Decline"},
                        AllowedTrainerLevel = new[]
                            {TrainerLevelType.Advanced, TrainerLevelType.Intermediate, TrainerLevelType.Novice},
                        ExerciseTypes = new[] {Compound, Isolate},
                        UtilityType = new UtilityType[]{UtilityType.Basic, UtilityType.AuxiliaryOrBasic, UtilityType.Auxiliary},
                        Reps = new[] {"10", "12", "15"},
                        Sets = new[] {3},
                        Rest = new[] {1, 1.5}
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
                                        repTypes = new[] {RepsType.MedInter, RepsType.HighInter, RepsType.Low};
                                    }
                                    else
                                    {
                                        repTypes = new[] {RepsType.MedInter, RepsType.HighInter};
                                    }

                                    break;
                                case TrainerLevelType.Advanced:
                                    repTypes = new[] {RepsType.MedAdvanced, RepsType.Low};
                                    if (!templateWorkoutMuscleExercise.MuscleType.IsSmallExercise())
                                    {
                                        repTypes = new[] {RepsType.MedAdvanced, RepsType.Low};
                                    }
                                    else
                                    {
                                        repTypes = new[] {RepsType.MedAdvanced, RepsType.HighAdvanced};
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
                                    repTypes = new[] {RepsType.MedInter, RepsType.HighInter};
                                    break;
                                case TrainerLevelType.Advanced:
                                    repTypes = new[] {RepsType.MedAdvanced, RepsType.HighAdvanced};
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }

                            exerciseSetting = ExerciseSettings(exerciseSettings, repTypes);
                        }

                        var exercisesToChoose = exercisesOfMuscle.Where(x =>
                            exerciseSetting.UtilityType.Any(u => u == x.Utility)
                            &&
                            exerciseSetting.ExerciseTypes.Any(m => m == x.ExerciseType)
                        ).ToList();

                        if (exerciseSetting.ExcludeExercises != null)
                        {
                            exercisesToChoose = exercisesToChoose
                                .Where(x => !x.Name.ContainsAny(exerciseSetting.ExcludeExercises)).ToList();
                        }

                        if (i == 0)
                        {
                            exercisesToChoose = exercisesToChoose.Where(x => x.Utility == UtilityType.Basic).ToList();
                        }

                        var rExercise = new Random();
                        int num = rExercise.Next(exercisesToChoose.Count);
                        var exerciseChose = exercisesToChoose[num];
                        workoutExercise.Name = exerciseChose.Name;

                        var sets = exerciseSetting.Sets[r.Next(exerciseSetting.Sets.Length)];
                        var reps = exerciseSetting.Reps[r.Next(exerciseSetting.Reps.Length)];
                        var rest = exerciseSetting.Rest[r.Next(exerciseSetting.Rest.Length)];
                        for (int j = 0; j < sets; j++)
                        {
                            var set = new Set {NumberOfSets = j + 1, Reps = reps, Rest = rest};

                            workoutExercise.Sets.Add(set);
                        }

                        exercisesOfMuscle.Remove(exerciseChose);
                    }
                }
            }

            return template;
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
    }
}
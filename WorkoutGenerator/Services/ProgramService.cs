using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WorkoutGenerator.Data;
using WorkoutGenerator.Extensions;
using WorkoutGenerator.Models;
using static WorkoutGenerator.Data.ExerciseType;

namespace WorkoutGenerator.Services
{
    public class ProgramService
    {
        public delegate bool ModifyExerciseDelegate(WorkoutExercise exercise, int reps, double rest);

        private readonly IServiceScopeFactory _serviceScopeFactory;
        public Dictionary<RepsType, ExerciseSettings> ExerciseData { get; set; }

        public ProgramService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            ExerciseData = new Dictionary<RepsType, ExerciseSettings>()
            {
                {
                    RepsType.Low, new ExerciseSettings()
                    {
                        AllowedTrainerLevel = new[] {TrainerLevelType.Advanced},
                        UtilityType = new UtilityType[] {UtilityType.Basic},
                        ExerciseTypes = new[] {Compound},
                        Reps = new[] {3, 4, 5},
                        Sets = new[] {3, 4, 5},
                        Rest = new[] {1.5, 2, 2.5}
                    }
                },
                {
                    RepsType.MedInter, new ExerciseSettings()
                    {
                        AllowedTrainerLevel = new[] {TrainerLevelType.Advanced, TrainerLevelType.Intermediate},
                        UtilityType = new UtilityType[] {UtilityType.Basic, UtilityType.AuxiliaryOrBasic},
                        ExerciseTypes = new[] {Compound},
                        Reps = new[] {6, 7, 8},
                        Sets = new[] {3, 4},
                        Rest = new[] {1, 1.5, 2}
                    }
                },
                {
                    RepsType.MedAdvanced, new ExerciseSettings()
                    {
                        AllowedTrainerLevel = new[] {TrainerLevelType.Advanced, TrainerLevelType.Intermediate},
                        UtilityType = new UtilityType[] {UtilityType.Basic, UtilityType.AuxiliaryOrBasic},
                        ExerciseTypes = new[] {Compound},
                        Reps = new[] {6, 7, 8},
                        Sets = new[] {3, 4},
                        Rest = new[] {1, 0.45, 1.5,}
                    }
                },

                {
                    RepsType.MedNovice, new ExerciseSettings()
                    {
                        ExcludeExercises = new[] {"Front", "Decline", "Incline"},
                        AllowedTrainerLevel = new[] {TrainerLevelType.Advanced, TrainerLevelType.Intermediate},
                        UtilityType = new UtilityType[] {UtilityType.Basic, UtilityType.AuxiliaryOrBasic},
                        ExerciseTypes = new[] {Compound},
                        Reps = new[] {6, 7, 8},
                        Sets = new[] {3, 4},
                        Rest = new[] {1.5, 2}
                    }
                },
                {
                    RepsType.HighInter, new ExerciseSettings()
                    {
                        AllowedTrainerLevel = new[]
                            {TrainerLevelType.Advanced, TrainerLevelType.Intermediate, TrainerLevelType.Novice},
                        UtilityType = new UtilityType[]
                            {UtilityType.Basic, UtilityType.AuxiliaryOrBasic, UtilityType.Auxiliary},
                        ExerciseTypes = new[] {Compound, Isolate},
                        Reps = new[] {10, 11, 12, 13, 14, 15},
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
                        UtilityType = new UtilityType[]
                            {UtilityType.Basic, UtilityType.AuxiliaryOrBasic, UtilityType.Auxiliary},
                        Reps = new[] {10, 11, 12, 13, 14, 15},
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
                        UtilityType = new UtilityType[]
                            {UtilityType.Basic, UtilityType.AuxiliaryOrBasic, UtilityType.Auxiliary},
                        Reps = new[] {10, 11, 12, 13, 14, 15},
                        Sets = new[] {3},
                        Rest = new[] {1, 1.5}
                    }
                }
            };
        }

        public Dictionary<RepsType, ExerciseSettings> GetRelevantExerciseData(int exercisePosition,
            TrainerLevelType level,
            MuscleType muscleType)
        {
            RepsType[] repTypes;
            if (exercisePosition == 0)
            {
                switch (level)
                {
                    case TrainerLevelType.Novice:
                        repTypes = new[] {RepsType.HighNovice, RepsType.MedNovice};
                        break;
                    case TrainerLevelType.Intermediate:
                        if (!muscleType.IsSmallExercise())
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
                        if (!muscleType.IsSmallExercise())
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
            }

            var relevantExerciseSettings = ExerciseData
                .Where(x => repTypes.Any(rep => x.Key == rep))
                .ToDictionary(x => x.Key, y => y.Value);

            return relevantExerciseSettings;
        }

        public Exercise PickExercise(int position, ExerciseSettings exerciseSetting, List<Exercise> exercisesOfMuscle)
        {
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

            if (position == 0)
            {
                exercisesToChoose = exercisesToChoose.Where(x => x.Utility == UtilityType.Basic).ToList();
            }

            var rExercise = new Random();
            int num = rExercise.Next(exercisesToChoose.Count);
            return exercisesToChoose[num];
        }

        /*
        public bool ModifyExerciseAddSet(WorkoutExercise e, int reps, double rest)
        {
            if (e.Sets.Count > 3)
            {
                return false;
            }

            var set = new Set() {Reps = reps, Rest = rest};
            if (reps < 10)
                e.Sets.Insert(0, set);
            else
                e.Sets.Add(set);

            return true;
        }

*/
        public bool ModifyExerciseChangeSet(WorkoutExercise e, int reps, double rest)
        {
            Set set = null;
            if (reps > 10)
            {
                if (e.Sets.All(x => x.Reps >= 10))
                    return false;
                set = e.Sets.Where(x => x.Reps < 10).Take(1).Single();
            }

            else if (reps <= 10)
            {
                if (e.Sets.All(x => x.Reps < 10)) return false;
                set = e.Sets.Where(x => x.Reps >= 10).Take(1).Single();
            }

            if (set == null) return false;
            set.Reps = reps;
            set.Rest = rest;

            return true;
        }

        public bool ModifyExerciseChangeRest(WorkoutExercise e, int reps, double rest)
        {
            e.Sets.Select(x =>
            {
                x.Rest = rest;
                return x;
            }).ToList();

            return true;
        }

        public bool ModifyExerciseChangeReps(WorkoutExercise e, int reps, double rest)
        {
            if (reps > 10)
            {
                if (e.Sets.All(x => x.Reps >= reps))
                {
                    return false;
                }
            }
            else if (reps <= 10)
            {
                if (e.Sets.All(x => x.Reps < 10))
                {
                    return false;
                }
            }

            e.Sets.Select(x =>
            {
                x.Reps = reps;
                return x;
            }).ToList();
            return true;
        }
    }
}
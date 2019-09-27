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
        public delegate bool ModifyExerciseDelegate(WorkoutExercise e, ExerciseSetting exerciseSetting);

        private readonly IServiceScopeFactory _serviceScopeFactory;
        public Dictionary<RepsType, ExerciseSetting> ExerciseData { get; set; }

        public ProgramService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            ExerciseData = new Dictionary<RepsType, ExerciseSetting>()
            {
                {
                    RepsType.Low, new ExerciseSetting()
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
                    RepsType.MedInter, new ExerciseSetting()
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
                    RepsType.MedAdvanced, new ExerciseSetting()
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
                    RepsType.MedNovice, new ExerciseSetting()
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
                    RepsType.HighInter, new ExerciseSetting()
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
                    RepsType.HighAdvanced, new ExerciseSetting()
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
                    RepsType.HighNovice, new ExerciseSetting()
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

        public Dictionary<RepsType, ExerciseSetting> GetRelevantExerciseData(int exercisePosition,
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

        public ExerciseSetting PickExerciseSetting(Dictionary<RepsType, ExerciseSetting> es, RepsType[] arr = null)
        {
            return es[arr?[new Random().Next(arr.Length)] ?? es.Select(x => x.Key).ToArray()[new Random().Next(es.Count)]];
        }
        public Exercise PickExercise(int position, ExerciseSetting exerciseSetting, List<Exercise> exercisesOfMuscle)
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

        public int PickReps(ExerciseSetting exerciseSetting)
        {
            return exerciseSetting.Reps[new Random().Next(exerciseSetting.Reps.Length)];

        }

        public double PickRest(ExerciseSetting exerciseSetting)
        {
            return exerciseSetting.Rest[new Random().Next(exerciseSetting.Rest.Length)];

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
        public bool ModifyExerciseChangeSet(WorkoutExercise e, ExerciseSetting exerciseSetting)
        {
            Set set = null;
            var reps = PickReps(exerciseSetting);
            var rest = PickRest(exerciseSetting);
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

        public bool ModifyExerciseChangeRest(WorkoutExercise e, ExerciseSetting exerciseSetting)
        {
            var rest = PickRest(exerciseSetting);
            e.Sets.Select(x =>
            {
                x.Rest = rest;
                return x;
            }).ToList();

            return true;
        }

        public bool ModifyExerciseChangeReps(WorkoutExercise e, ExerciseSetting exerciseSetting)
        {
            var reps = PickReps(exerciseSetting);

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
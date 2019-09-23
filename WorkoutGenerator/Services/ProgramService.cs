using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WorkoutGenerator.Data;
using WorkoutGenerator.Models;
using static WorkoutGenerator.Data.ExerciseType;

namespace WorkoutGenerator.Services
{
    public class ProgramService
    {
        public Dictionary<RepsType, ExerciseSettings> Data { get; set; }

        public ProgramService()
        {
            Data = new Dictionary<RepsType, ExerciseSettings>()
            {
                {
                    RepsType.Low, new ExerciseSettings()
                    {
                        AllowedTrainerLevel = new[] {TrainerLevelType.Advanced},
                        UtilityType = new UtilityType[]{UtilityType.Basic},
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
                        UtilityType = new UtilityType[]{UtilityType.Basic,UtilityType.AuxiliaryOrBasic},
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
                        UtilityType = new UtilityType[]{UtilityType.Basic,UtilityType.AuxiliaryOrBasic},
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
                        UtilityType = new UtilityType[]{UtilityType.Basic, UtilityType.AuxiliaryOrBasic},
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
                        UtilityType = new UtilityType[]{UtilityType.Basic, UtilityType.AuxiliaryOrBasic, UtilityType.Auxiliary},
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
                        UtilityType = new UtilityType[]{UtilityType.Basic, UtilityType.AuxiliaryOrBasic, UtilityType.Auxiliary},
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
                        UtilityType = new UtilityType[]{UtilityType.Basic, UtilityType.AuxiliaryOrBasic, UtilityType.Auxiliary},
                        Reps = new[] {10, 11, 12, 13, 14, 15},
                        Sets = new[] {3},
                        Rest = new[] {1, 1.5}
                    }
                }
            };

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutGenerator.Data;

namespace WorkoutGenerator.Factories
{
    public class TemplateFactory
    {
        private TrainerLevelType TrainerLevelType { get; set; }
        public TemplateFactory(TrainerLevelType level)
        {
            TrainerLevelType = level;
        }

        public Template CreateBasicTemplate(TemplateType templateType)
        {
            List<Workout> list;
            int bigMuscle;
            int smallMuscle;
            switch (templateType)
            {
                case TemplateType.FBW:
                    bigMuscle = TrainerLevelType == TrainerLevelType.Novice ? 1 : 2;
                    smallMuscle = TrainerLevelType == TrainerLevelType.Novice ? 1 : 2;
                    list = new List<Workout>()
                    {
                        new Workout()
                        {
                            MuscleExercises = new List<MuscleExercises>()
                            {
                                new MuscleExercises{MuscleType = MuscleType.Chest, Exercises = GetExercises(bigMuscle)},
                                new MuscleExercises() {MuscleType = MuscleType.Back, Exercises = GetExercises(bigMuscle)},
                                new MuscleExercises() {MuscleType = MuscleType.Shoulders, Exercises = GetExercises(smallMuscle)},
                                new MuscleExercises() {MuscleType = MuscleType.Legs, Exercises = GetExercises(bigMuscle)},
                                new MuscleExercises() {MuscleType = MuscleType.Biceps, Exercises = GetExercises(smallMuscle)},
                                new MuscleExercises() {MuscleType = MuscleType.Triceps, Exercises = GetExercises(smallMuscle)},
                            }
                        }
                    };
                    break;
                case TemplateType.AB:
                    bigMuscle = TrainerLevelType == TrainerLevelType.Novice ? 2 : 3;
                    smallMuscle = TrainerLevelType == TrainerLevelType.Novice ? 1 : 2;
                    list = new List<Workout>()
                    {
                        new Workout()
                        {
                            MuscleExercises = new List<MuscleExercises>()
                            {
                                new MuscleExercises() {MuscleType = MuscleType.Chest, Exercises = GetExercises(bigMuscle)},
                                new MuscleExercises() {MuscleType = MuscleType.Shoulders, Exercises = GetExercises(bigMuscle)},
                                new MuscleExercises() {MuscleType = MuscleType.Triceps, Exercises = GetExercises(smallMuscle)}
                            }
                        },
                        new Workout()
                        {
                            MuscleExercises = new List<MuscleExercises>()
                            {
                                new MuscleExercises() {MuscleType = MuscleType.Legs, Exercises = GetExercises(bigMuscle)},
                                new MuscleExercises() {MuscleType = MuscleType.Back, Exercises = GetExercises(bigMuscle)},
                                new MuscleExercises() {MuscleType = MuscleType.Biceps, Exercises = GetExercises(smallMuscle)}
                            }
                        }
                    };
                    break;
                case TemplateType.ABC:
                    switch (TrainerLevelType)
                    {
                        case TrainerLevelType.Novice:
                            bigMuscle = 3;
                            smallMuscle = 2;
                            break;
                        case TrainerLevelType.Intermediate:
                            bigMuscle = 4;
                            smallMuscle = 2;
                            break;
                        case TrainerLevelType.Advanced:
                            bigMuscle = 4;
                            smallMuscle = 3;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    list = new List<Workout>()
                    {
                        new Workout()
                        {
                            MuscleExercises = new List<MuscleExercises>()
                            {
                                new MuscleExercises() {MuscleType = MuscleType.Chest, Exercises = GetExercises(bigMuscle)},
                                new MuscleExercises() {MuscleType = MuscleType.Shoulders, Exercises = GetExercises(bigMuscle)},
                                new MuscleExercises() {MuscleType = MuscleType.Triceps, Exercises = GetExercises(smallMuscle)}
                            }
                        },
                        new Workout()
                        {
                            MuscleExercises = new List<MuscleExercises>()
                            {
                                new MuscleExercises() {MuscleType = MuscleType.Back, Exercises = GetExercises(bigMuscle)},
                                new MuscleExercises() {MuscleType = MuscleType.Biceps, Exercises = GetExercises(smallMuscle)}
                            }
                        },

                        new Workout()
                        {
                            MuscleExercises = new List<MuscleExercises>()
                            {
                                new MuscleExercises() {MuscleType = MuscleType.Legs, Exercises = GetExercises(bigMuscle+1 /*add a little more to legs*/)}
                            }
                        }
                    };

                    break;
                case TemplateType.ABCD:
                    switch (TrainerLevelType)
                    {
                        case TrainerLevelType.Novice:
                            bigMuscle = 4;
                            smallMuscle = 2;
                            break;
                        case TrainerLevelType.Intermediate:
                            bigMuscle = 4;
                            smallMuscle = 3;
                            break;
                        case TrainerLevelType.Advanced:
                            bigMuscle = 5;
                            smallMuscle = 4;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    list = new List<Workout>()
                    {
                        new Workout()
                        {
                            MuscleExercises = new List<MuscleExercises>()
                            {
                                new MuscleExercises() {MuscleType = MuscleType.Chest, Exercises = GetExercises(bigMuscle)},
                                new MuscleExercises() {MuscleType = MuscleType.Triceps, Exercises = GetExercises(smallMuscle)}
                            }
                        },
                        new Workout()
                        {
                            MuscleExercises = new List<MuscleExercises>()
                            {
                                new MuscleExercises() {MuscleType = MuscleType.Back, Exercises = GetExercises(bigMuscle)},
                                new MuscleExercises() {MuscleType = MuscleType.Biceps, Exercises = GetExercises(smallMuscle)},
                            }
                        },

                        new Workout()
                        {
                            MuscleExercises = new List<MuscleExercises>()
                            {
                                new MuscleExercises() {MuscleType = MuscleType.Shoulders, Exercises = GetExercises(bigMuscle)}
                            }
                        },
                        new Workout()
                        {
                            MuscleExercises = new List<MuscleExercises>()
                            {
                                new MuscleExercises() {MuscleType = MuscleType.Legs, Exercises = GetExercises(bigMuscle)}
                            }
                        }
                    };

                    break;
                /*case TemplateType.ABCDE:

                    list = new List<Workout>()
                    {
                        new Workout()
                        {
                            MuscleExercises = new List<MuscleExercises>()
                            {
                                new MuscleExercises() {MuscleType = MuscleType.Chest},
                            }
                        },
                        new Workout()
                        {
                            MuscleExercises = new List<MuscleExercises>()
                            {
                                new MuscleExercises() {MuscleType = MuscleType.Back},
                            }
                        },

                        new Workout()
                        {
                            MuscleExercises = new List<MuscleExercises>()
                            {
                                new MuscleExercises() {MuscleType = MuscleType.Shoulders}
                            }
                        },
                        new Workout()
                        {
                            MuscleExercises = new List<MuscleExercises>()
                            {
                                new MuscleExercises() {MuscleType = MuscleType.Triceps},
                                new MuscleExercises() {MuscleType = MuscleType.Biceps}
                            }
                        },
                        new Workout()
                        {
                            MuscleExercises = new List<MuscleExercises>()
                            {
                                new MuscleExercises() {MuscleType = MuscleType.Legs}
                            }
                        }
                    };

                    break;
                case TemplateType.ABCDEF:

                    list = new List<Workout>()
                    {
                        new Workout()
                        {
                            MuscleExercises = new List<MuscleExercises>()
                            {
                                new MuscleExercises() {MuscleType = MuscleType.Chest},
                            }
                        },
                        new Workout()
                        {
                            MuscleExercises = new List<MuscleExercises>()
                            {
                                new MuscleExercises() {MuscleType = MuscleType.Back},
                            }
                        },

                        new Workout()
                        {
                            MuscleExercises = new List<MuscleExercises>()
                            {
                                new MuscleExercises() {MuscleType = MuscleType.Shoulders}
                            }
                        },
                        new Workout()
                        {
                            MuscleExercises = new List<MuscleExercises>()
                            {
                                new MuscleExercises() {MuscleType = MuscleType.Triceps},
                            }
                        },

                        new Workout()
                        {
                            MuscleExercises = new List<MuscleExercises>()
                            {
                                new MuscleExercises() {MuscleType = MuscleType.Biceps},
                            }
                        },

                        new Workout()
                        {
                            MuscleExercises = new List<MuscleExercises>()
                            {
                                new MuscleExercises() {MuscleType = MuscleType.Legs}
                            }
                        }
                    };
                    break;*/
                default:
                    throw new ArgumentOutOfRangeException(nameof(templateType), templateType, null);
            }

            return new Template(list);
        }

        private List<WorkoutExercise> GetExercises(int length)
        {
            var list = new List<WorkoutExercise>();
            for (int i = 0; i < length; i++)
            {
                list.Add(new WorkoutExercise());
            }

            return list;
        }
    }
}
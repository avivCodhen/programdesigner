using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutGenerator.Data;

namespace WorkoutGenerator.Models
{
    public class ExerciseSettings
    {
        public string[] Reps { get; set; }
        public int[] Sets { get; set; }
        public double[] Rest { get; set; }
        public ExerciseType[] ExerciseTypes { get; set; }
        public UtilityType[] UtilityType { get; set; }
        public TrainerLevelType[] AllowedTrainerLevel { get; set; }
        public string[] ExcludeExercises { get; set; }
    }
}
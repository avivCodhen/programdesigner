using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutGenerator.Data;

namespace WorkoutGenerator.Extensions
{
    public static class MuscleTypeExtensions
    {
        public static bool IsSmallExercise(this MuscleType muscleType)
        {
            return muscleType == MuscleType.Triceps || muscleType == MuscleType.Biceps || muscleType == MuscleType.Shoulders;
        }
    }
}

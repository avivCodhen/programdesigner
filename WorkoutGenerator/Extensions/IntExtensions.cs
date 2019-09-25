using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutGenerator.Extensions
{
    public static class IntExtensions
    {
        public static String ToFitnessRepsFormat(this int reps)
        {
            if (reps < 1 && reps < 3)
            {
                return "1-3";
            }

            if (reps > 3 && reps < 5)
            {
                return "3-5";
            }

            if (reps > 6 && reps < 8)
            {
                return "6-10";
            }

            if (reps > 8 && reps < 10)
            {
                return "6-10";
            }

            if (reps > 10 && reps < 12)
            {
                return "10-15";
            }
            if (reps > 12 && reps < 15)
            {
                return "10-15";
            }

            return reps.ToString();
        }
    }
}

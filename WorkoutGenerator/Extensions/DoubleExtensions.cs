using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutGenerator.Extensions
{
    public static class DoubleExtensions
    {
        public static string ToFitnessTimeFormat(this double d)
        {
            var minutes = (int) d != 0;
            return $"{ (minutes ? d.ToString(CultureInfo.InvariantCulture) :  d.ToString(CultureInfo.InvariantCulture).Remove(0,2))} { ( minutes ? "min" : "sec")}";
        }
    }
}

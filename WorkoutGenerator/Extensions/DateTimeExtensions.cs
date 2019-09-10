using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutGenerator.Extensions
{
    public static class DateTimeExtensions
    {
        public static string FormatDate(this DateTime s)
        {
            return $"{s:MMM d, yyyy}";
        }

        public static string FormatDateTime(this DateTime s)
        {
            return $"{s:MMM d, yyyy, hh:mm tt}";
        }

        public static string MonthAndDay(this DateTime s)
        {
            return $"{s:MMM d}";
        }
    }
}

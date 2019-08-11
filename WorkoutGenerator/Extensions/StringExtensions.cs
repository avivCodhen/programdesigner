using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutGenerator.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsAny(this string input, IEnumerable<string> containsKeywords)
        {
            return containsKeywords.Any(keyword => input.IndexOf(keyword, StringComparison.CurrentCultureIgnoreCase) >= 0);
        }
    }
}

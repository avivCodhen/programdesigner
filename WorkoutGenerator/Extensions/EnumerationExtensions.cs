using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Remotion.Linq.Clauses;
using WorkoutGenerator.Data;
using WorkoutGenerator.Extentions;

namespace WorkoutGenerator.Extensions
{
    public static class EnumerationExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> list, Enum selectedEnum) where T : Enum
        {
            return list.Select(x => new SelectListItem()
                {Value = x.ToString(), Text = x.Description(), Selected = Equals(selectedEnum, x)}).ToList();
        }
    }
}
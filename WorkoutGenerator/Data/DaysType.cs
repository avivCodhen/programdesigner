using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutGenerator.Data
{
    public enum DaysType
    {
        [Description("1 Day Per Week")] OneDay,
        [Description("2 Days Per Week")] TwoDays,
        [Description("3 Days Per Week")] ThreeDays,
        [Description("4 Days Per Week")] FourDays,
        [Description("5 Days Per Week")] FiveDays,
        [Description("6 Days Per Week")] SixDays,
        [Description("7 Days Per Week")] SevenDays
    }
}
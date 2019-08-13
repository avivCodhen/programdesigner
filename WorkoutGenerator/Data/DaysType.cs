using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutGenerator.Data
{
    public enum DaysType
    {
        [Description("1 Day per week")] OneDay,
        [Description("2 Days per week")] TwoDays,
        [Description("3 Days per week")] ThreeDays,
        [Description("4 Days per week")] FourDays,
        [Description("5 Days per week")] FiveDays,
        [Description("6 Days per week")] SixDays,
        [Description("7 Days per week")] SevenDays
    }
}
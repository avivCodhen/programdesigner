using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutGenerator.Data
{
    public enum TemplateType
    {
        [Description("Full Body Workout")]FBW,
        [Description("AB Split")]AB,
        [Description("ABC Split")]ABC,
        [Description("ABCD Split")]ABCD,
        /*ABCDE,
        ABCDEF*/
    }
}
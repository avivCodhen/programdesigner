using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutGenerator.Data
{
    public enum TrainerLevelType
    {
        [Description("Novice")]Novice,
        [Description("Intermediate")] Intermediate,
        [Description("Advanced")] Advanced, 
    }
}

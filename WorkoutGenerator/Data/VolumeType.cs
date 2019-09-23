using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutGenerator.Data
{
    public enum VolumeType
    {
        [Description("Metabolic Stress")] MetabolicStress,
        [Description("Mechanical Tension")] MechanicalTension
    }
}

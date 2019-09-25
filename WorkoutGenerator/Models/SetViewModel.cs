using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutGenerator.Models
{
    public class SetViewModel
    {
        public bool SetAdded { get; set; }
        public bool RepsChanged { get; set; }
        public bool RestChanged { get; set; }
        public int Reps { get; set; }
        public double Rest { get; set; }
    }
}

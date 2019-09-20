using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutGenerator.Data;

namespace WorkoutGenerator.Models
{
    public class SetViewModel
    {
        public SetViewModel()
        {
            
        }

        public SetViewModel(Set set)
        {
            NumberOfSets = set.NumberOfSets;
            Reps = set.Reps;
            Rest = set.Rest;
        }
        public int NumberOfSets { get; set; }
        public string Reps { get; set; }
        public double Rest { get; set; }
    }
}

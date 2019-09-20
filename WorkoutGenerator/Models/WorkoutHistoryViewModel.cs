using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutGenerator.Models
{
    public class WorkoutHistoryViewModel
    {
        public int Id { get; set; }
        public List<MuscleExerciseViewModel> MuscleExerciseViewModels { get; set; }
    }
}

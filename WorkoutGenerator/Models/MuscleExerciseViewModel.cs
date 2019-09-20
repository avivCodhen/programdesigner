using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutGenerator.Data;

namespace WorkoutGenerator.Models
{
    public class MuscleExerciseViewModel
    {
        public MuscleExerciseViewModel()
        {
        }

        public int Id { get; set; }

        public  LinkedList<WorkoutExerciseViewModel> Exercises { get; set; }
        public MuscleType MuscleType { get; set; }
    }
}

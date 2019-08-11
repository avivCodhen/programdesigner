using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutGenerator.Data;

namespace WorkoutGenerator.Models
{
    public class ExerciseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Sets { get; set; }
        public string Reps { get; set; }
        public string Rest { get; set; }

        public ExerciseViewModel(WorkoutExercise exercise)
        {
            Name = exercise.Exercise.Name;
            Sets = exercise.Sets;
            Reps = exercise.Reps;
            Rest = exercise.Rest;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutGenerator.Data;

namespace WorkoutGenerator.Models
{
    public class MuscleExerciseViewModel
    {
        public MuscleExerciseViewModel(MuscleExercises muscleExercises)
        {
            Id = muscleExercises.Id;
            MuscleType = muscleExercises.MuscleType;
            Exercises = muscleExercises.Exercises.Select(x => new ExerciseViewModel(x)).ToList();
        }

        public int Id { get; set; }

        public  List<ExerciseViewModel> Exercises { get; set; }
        public MuscleType MuscleType { get; set; }
    }
}

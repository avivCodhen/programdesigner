using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutGenerator.Data;

namespace WorkoutGenerator.Models
{
    public class WorkoutViewModel
    {
        public WorkoutViewModel(Workout workout)
        {
            Id = workout.Id;
            MuscleExerciseViewModels = workout.MuscleExercises.Select(x => new MuscleExerciseViewModel(x)).ToList();
            Name = workout.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        
        public  List<MuscleExerciseViewModel> MuscleExerciseViewModels { get; set; }
    }
}

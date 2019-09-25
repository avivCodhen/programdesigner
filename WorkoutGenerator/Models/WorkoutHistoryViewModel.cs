using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutGenerator.Data;

namespace WorkoutGenerator.Models
{
    public class WorkoutHistoryViewModel
    {
        public WorkoutHistoryViewModel(WorkoutHistory wh)
        {
            Id = wh.Id;
            MuscleExerciseViewModels = wh.MuscleExercises.Select(m => new MuscleExerciseViewModel(m)).ToList();
        }
        public int Id { get; set; }
        public List<MuscleExerciseViewModel> MuscleExerciseViewModels { get; set; }
    }
}

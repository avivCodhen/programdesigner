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
            Name = workout.Name;
            WorkoutHistoryViewModels = workout.WorkoutHistories.Select(x => new WorkoutHistoryViewModel()
            {
                Id = x.Id,
                MuscleExerciseViewModels = x.MuscleExercises.Select(m => new MuscleExerciseViewModel(m)).ToList(),
            }).ToList();
        }

        public WorkoutViewModel()
        {
            
        }
        public string Name { get; set; }

        public List<WorkoutHistoryViewModel> WorkoutHistoryViewModels { get; set; }
    }
}
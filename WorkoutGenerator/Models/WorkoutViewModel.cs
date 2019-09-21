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
            Id = workout.Id;
            WorkoutHistoryViewModels = workout.WorkoutHistories.Select(x => new WorkoutHistoryViewModel(x)).ToList();
        }

        public WorkoutViewModel()
        {
            
        }
        public string Name { get; set; }
        public int Id { get; set; }
        public List<WorkoutHistoryViewModel> WorkoutHistoryViewModels { get; set; }
    }
}
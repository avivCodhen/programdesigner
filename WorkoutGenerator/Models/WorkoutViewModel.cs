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
            WorkoutHistoryViewModel = new WorkoutHistoryViewModel(workout.WorkoutHistories.Last());
        }

        public WorkoutViewModel()
        {
            
        }
        public string Name { get; set; }
        public int Id { get; set; }
        public WorkoutHistoryViewModel WorkoutHistoryViewModel { get; set; }
    }
}
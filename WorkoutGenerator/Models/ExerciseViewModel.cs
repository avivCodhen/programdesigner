using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WorkoutGenerator.Data;

namespace WorkoutGenerator.Models
{
    public class ExerciseViewModel
    {
        public string YoutubeVideoId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SetViewModel> SetViewModels { get; set; } = new List<SetViewModel>();

        public ExerciseViewModel(WorkoutExercise exercise)
        {
            Name = exercise.Name;
            SetViewModels = exercise.Sets
                .Select(x => new SetViewModel()
                {
                    NumberOfSets = x.NumberOfSets,
                    Rest = x.Rest,
                    Reps = x.Reps
                }).ToList();
        }
    }
}
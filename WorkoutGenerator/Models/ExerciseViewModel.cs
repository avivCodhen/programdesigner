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
        public bool ChangedName { get; set; }
        public bool AddedExercise { get; set; }
        public string YoutubeVideoId { get; set; }
        public string SupersetYoutubeVideoId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SetViewModel> SetViewModels { get; set; } = new List<SetViewModel>();
        public List<SetViewModel> SupersetSetViewModels { get; set; } = new List<SetViewModel>();

        public string SupersetName { get; set; }

        public ExerciseViewModel(WorkoutExercise exercise)
        {
            Id = exercise.Id;
            Name = exercise.Name;
            SetViewModels = exercise.Sets
                .Select(x => new SetViewModel()
                {
                    Rest = x.Rest,
                    Reps = x.Reps
                }).ToList();
        }
    }
}
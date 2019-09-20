using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutGenerator.Models
{
    public class WorkoutExerciseDataViewModel
    {
        public string YoutubeVideoId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SetViewModel> SetViewModels { get; set; } = new List<SetViewModel>();

    }
}

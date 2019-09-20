using System.Collections.Generic;
using System.Linq;
using WorkoutGenerator.Data;

namespace WorkoutGenerator.Models
{
    public class TemplateViewModel
    {
        public TemplateViewModel()
        {
        }

      public int Id { get; set; }
        public List<WorkoutViewModel> Workouts { get; set; }

        public DaysType DaysType { get; set; }

        public TrainerLevelType TrainerLevelType { get; set; }

        public TemplateType TemplateType { get; set; }
    }
}
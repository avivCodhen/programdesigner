using System.Collections.Generic;
using System.Linq;
using WorkoutGenerator.Data;

namespace WorkoutGenerator.Models
{
    public class TemplateViewModel
    {
        public TemplateViewModel(Template t)
        {
            Id = t.Id;
            Workouts = t.Workouts.Select(x => new WorkoutViewModel(x)).ToList();
            DaysType = t.DaysType;
            TrainerLevelType = t.TrainerLevelType;
            TemplateType = t.TemplateType;
        }

      public int Id { get; set; }
        public List<WorkoutViewModel> Workouts { get; set; }

        public DaysType DaysType { get; set; }

        public TrainerLevelType TrainerLevelType { get; set; }

        public TemplateType TemplateType { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WorkoutGenerator.Data
{
    public class Template
    {
        [Key] public int Id { get; set; }
        public virtual ICollection<Workout> Workouts { get; set; }

        public DaysType DaysType { get; set; }

        public TrainerLevelType TrainerLevelType { get; set; }

        public Template(ICollection<Workout> workouts)
        {
            Workouts = workouts;
        }

        public Template()
        {
            
        }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutGenerator.Data
{
    public class WorkoutExercise
    {
        public WorkoutExercise()
        {
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Reps { get; set; }
        public string Sets { get; set; }
        public string Rest { get; set; }

    }
}

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

        [Key]
        public int Id { get; set; }
        [ForeignKey("ExerciseId")]
        public virtual Exercise Exercise { get; set; }
        public int ExerciseId { get; set; }
        public string Reps { get; set; }
        public string Sets { get; set; }
        public string Rest { get; set; }

    }
}

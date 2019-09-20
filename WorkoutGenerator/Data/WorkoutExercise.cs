using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WorkoutGenerator.Models;

namespace WorkoutGenerator.Data
{
    public class WorkoutExercise
    {
        [Key]
        public int Id { get; set; }

        public virtual IList<WorkoutExerciseData> WorkoutExerciseData { get; set; }
    }
}

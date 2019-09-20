using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutGenerator.Data
{
    public class WorkoutHistory
    {
        [Key] public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public virtual ICollection<MuscleExercises> MuscleExercises { get; set; } = new List<MuscleExercises>();

    }
}

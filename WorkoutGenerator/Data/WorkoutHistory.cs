using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WorkoutGenerator.Data
{
    public class WorkoutHistory
    {
        [Key] public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        [ForeignKey("WorkoutId")]
        public virtual Workout Workout { get; set; }
        public int WorkoutId { get; set; }
        public virtual IList<MuscleExercises> MuscleExercises { get; set; } = new List<MuscleExercises>();

    }
}

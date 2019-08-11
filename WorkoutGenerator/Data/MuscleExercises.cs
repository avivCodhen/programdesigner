using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutGenerator.Data
{
    public class MuscleExercises
    {
        [Key] public int Id { get; set; }

        public int WorkoutId { get; set; }
        [ForeignKey("WorkoutId")] public virtual Workout Workout { get; set; }
        public virtual IList<WorkoutExercise> Exercises { get; set; }
        public MuscleType MuscleType { get; set; }

    }
}
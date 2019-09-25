using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutGenerator.Data
{
    public class MuscleExercises
    {
        [Key] public int Id { get; set; }
        public virtual IList<WorkoutExercise> Exercises { get; set; } = new List<WorkoutExercise>();
        public MuscleType MuscleType { get; set; }
    }

 
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutGenerator.Data
{
    public class Exercise
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public MuscleType MuscleType { get; set; }
        public ExerciseType ExerciseType { get; set; }
        public UtilityType Utility { get; set; }

    }
}

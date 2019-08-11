using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutGenerator.Data;

namespace WorkoutGenerator.Models
{
    public class ExerciseSettings
    {
        public string[] Reps { get; set; }
        public string[] Sets { get; set; }
        public string[] Rest { get; set; }
        public ExerciseType[] ExerciseTypes { get; set; }
        public string[] ExerciseEquipments { get; set; }
        public TrainerLevelType[] AllowedTrainerLevel { get; set; }
    }
}

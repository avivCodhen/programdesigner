using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WorkoutGenerator.Data;

namespace WorkoutGenerator.Models
{
    public class WorkoutExerciseViewModel
    {
        public int Id { get; set; }
        public List<WorkoutExerciseDataViewModel> WorkoutExerciseDataViewModels { get; set; }
        public WorkoutExerciseViewModel()
        {
        }
    }
}
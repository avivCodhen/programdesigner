using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutGenerator.Data
{
    public class Set
    {
        public DateTime Created { get; } = DateTime.Now;
        [Key] public int Id { get; set; }
        public int Reps { get; set; }
        public double Rest { get; set; }
    }
}
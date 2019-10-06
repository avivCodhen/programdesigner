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
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string SupersetName { get; set; }
        public virtual IList<Set> SupersetSets { get; set; } = new List<Set>();
        public virtual IList<Set> Sets { get; set; } = new List<Set>();

    }
}

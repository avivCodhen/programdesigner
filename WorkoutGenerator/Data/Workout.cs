using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutGenerator.Data
{
    public class Workout
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("TemplateId")]
        public virtual Template Template { get; set; }
        public int TemplateId { get; set; }
        public virtual IList<WorkoutHistory> WorkoutHistories { get; set; } = new List<WorkoutHistory>();

        public Workout(string name)
        {
            Name = name;
        }
    }
}
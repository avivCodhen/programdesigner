using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutGenerator.Data
{
    public class FitnessProgram
    {
        public int ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        [Key]
        public int Id { get; set; }
        [ForeignKey("TemplateId")]
        public virtual Template Template { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public int TemplateId { get; set; }
    }
}

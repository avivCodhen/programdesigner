using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutGenerator.Data
{
    public class BodyBuildingProgram
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("TemplateId")]
        public virtual Template Template { get; set; }
        public int TemplateId { get; set; }
    }
}

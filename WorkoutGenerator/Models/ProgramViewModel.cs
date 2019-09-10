using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutGenerator.Models
{
    public class ProgramViewModel
    {
        public int Id { get; set; }
        public TemplateViewModel TemplateViewModel { get; set; }
        public bool FeedBack { get; set; }
        public DateTime Created { get; set; }
        public bool ApplicationIdNull { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutGenerator.Data
{
    public class FeedBack
    {
        public DateTime Created { get; set; } = DateTime.Now;
        [Key] public int Id { get; set; }
        public string Text { get; set; }
    }
}

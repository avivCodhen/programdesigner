using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WorkoutGenerator.Data
{
    public class ApplicationUser : IdentityUser<int>
    {
        public DateTime Created { get; set; } = DateTime.Now;
        public virtual ICollection<FitnessProgram> FitnessPrograms { get; set; }
    }
}

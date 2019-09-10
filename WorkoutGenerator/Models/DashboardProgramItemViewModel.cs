using System;

namespace WorkoutGenerator.Models
{
    public class DashboardProgramItemViewModel
    {
        public string Type { get; set; }
        public string Level { get; set; }
        public int ProgramId { get; set; }
        public string Days { get; set; }
        public DateTime Created { get; set; }
    }
}
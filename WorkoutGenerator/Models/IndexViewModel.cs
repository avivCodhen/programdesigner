﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutGenerator.Data;

namespace WorkoutGenerator.Models
{
    public class IndexViewModel
    {
        public TrainerLevelType TrainerLevelType { get; set; }
        public DaysType DaysType { get; set; }
        public string TemplateType { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutGenerator.Data
{
    public class YoutubeVideoQuery
    {

        [Key] public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string Query { get; set; }
        public string LinkId { get; set; }

    }
}

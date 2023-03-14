﻿using System;
using System.Collections.Generic;

namespace CheckRequestWorker.Models
{
    public partial class Department
    {
        public Department()
        {
            Workers = new HashSet<Worker>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Worker> Workers { get; set; }
    }
}

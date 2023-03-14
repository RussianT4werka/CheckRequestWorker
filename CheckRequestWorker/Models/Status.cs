﻿using System;
using System.Collections.Generic;

namespace CheckRequestWorker.Models
{
    public partial class Status
    {
        public Status()
        {
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Request> Requests { get; set; }
    }
}

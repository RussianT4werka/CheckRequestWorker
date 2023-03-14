﻿using System;
using System.Collections.Generic;

namespace CheckRequestWorker.Models
{
    public partial class VisitorsVisit
    {
        public int Id { get; set; }
        public int VisitorsId { get; set; }
        public int VisitId { get; set; }

        public virtual Visit Visit { get; set; } = null!;
        public virtual Visitor Visitors { get; set; } = null!;
    }
}

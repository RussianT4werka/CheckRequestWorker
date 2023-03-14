﻿using System;
using System.Collections.Generic;

namespace CheckRequestWorker.Models
{
    public partial class Request
    {
        public Request()
        {
            VisitorsRequests = new HashSet<VisitorsRequest>();
        }

        public int Id { get; set; }
        public int TypeRequestId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string TargetVisit { get; set; } = null!;
        public int WorkerId { get; set; }
        public int StatusId { get; set; }
        public string? Cause { get; set; }
        public int UsersId { get; set; }

        public virtual Status Status { get; set; } = null!;
        public virtual TypeRequest TypeRequest { get; set; } = null!;
        public virtual User Users { get; set; } = null!;
        public virtual Worker Worker { get; set; } = null!;
        public virtual ICollection<VisitorsRequest> VisitorsRequests { get; set; }
    }
}

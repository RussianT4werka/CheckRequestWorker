using System;
using System.Collections.Generic;

namespace CheckRequestWorker.Models
{
    public partial class Visit
    {
        public Visit()
        {
            VisitorsVisits = new HashSet<VisitorsVisit>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int WorkerId { get; set; }
        public string? GroupNumber { get; set; }

        public virtual Worker Worker { get; set; } = null!;
        public virtual ICollection<VisitorsVisit> VisitorsVisits { get; set; }
    }
}

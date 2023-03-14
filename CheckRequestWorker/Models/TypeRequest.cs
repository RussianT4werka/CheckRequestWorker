using System;
using System.Collections.Generic;

namespace CheckRequestWorker.Models
{
    public partial class TypeRequest
    {
        public TypeRequest()
        {
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Request> Requests { get; set; }
    }
}

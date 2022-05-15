using System;
using System.Collections.Generic;

#nullable disable

namespace LaborExchangeApi.Models
{
    public partial class UserHasJobRequest
    {
        public int UserId { get; set; }
        public int JobRequestId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual JobRequest JobRequest { get; set; }
        public virtual User User { get; set; }
    }
}

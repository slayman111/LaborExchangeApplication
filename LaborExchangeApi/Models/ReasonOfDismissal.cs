using System;
using System.Collections.Generic;

#nullable disable

namespace LaborExchangeApi.Models
{
    public partial class ReasonOfDismissal
    {
        public ReasonOfDismissal()
        {
            UserHasJobs = new HashSet<UserHasJob>();
        }

        public int Id { get; set; }
        public string Value { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<UserHasJob> UserHasJobs { get; set; }
    }
}

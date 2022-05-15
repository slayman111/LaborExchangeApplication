using System;
using System.Collections.Generic;

#nullable disable

namespace LaborExchangeApi.Models
{
    public partial class JobRequest
    {
        public JobRequest()
        {
            UserHasJobRequests = new HashSet<UserHasJobRequest>();
        }

        public int Id { get; set; }
        public int ProfessionId { get; set; }
        public decimal? SalaryRequirements { get; set; }
        public int? WorkDayRequirementsId { get; set; }
        public string Info { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Profession Profession { get; set; }
        public virtual WorkDayRequirement WorkDayRequirements { get; set; }
        public virtual ICollection<UserHasJobRequest> UserHasJobRequests { get; set; }
    }
}

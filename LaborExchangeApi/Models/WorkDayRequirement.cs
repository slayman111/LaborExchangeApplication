using System;
using System.Collections.Generic;

#nullable disable

namespace LaborExchangeApi.Models
{
    public partial class WorkDayRequirement
    {
        public WorkDayRequirement()
        {
            JobRequests = new HashSet<JobRequest>();
            Vacancies = new HashSet<Vacancy>();
        }

        public int Id { get; set; }
        public string Value { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<JobRequest> JobRequests { get; set; }
        public virtual ICollection<Vacancy> Vacancies { get; set; }
    }
}

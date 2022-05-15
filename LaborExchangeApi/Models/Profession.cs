using System;
using System.Collections.Generic;

#nullable disable

namespace LaborExchangeApi.Models
{
    public partial class Profession
    {
        public Profession()
        {
            Educations = new HashSet<Education>();
            JobRequests = new HashSet<JobRequest>();
            UserHasJobs = new HashSet<UserHasJob>();
            Vacancies = new HashSet<Vacancy>();
        }

        public int Id { get; set; }
        public string Value { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Education> Educations { get; set; }
        public virtual ICollection<JobRequest> JobRequests { get; set; }
        public virtual ICollection<UserHasJob> UserHasJobs { get; set; }
        public virtual ICollection<Vacancy> Vacancies { get; set; }
    }
}

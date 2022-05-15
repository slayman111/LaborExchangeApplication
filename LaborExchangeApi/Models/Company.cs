using System;
using System.Collections.Generic;

#nullable disable

namespace LaborExchangeApi.Models
{
    public partial class Company
    {
        public Company()
        {
            CompanyHasVacancies = new HashSet<CompanyHasVacancy>();
            UserHasCompanies = new HashSet<UserHasCompany>();
            UserHasJobs = new HashSet<UserHasJob>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ActivityId { get; set; }
        public byte[] Logotype { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Activity Activity { get; set; }
        public virtual ICollection<CompanyHasVacancy> CompanyHasVacancies { get; set; }
        public virtual ICollection<UserHasCompany> UserHasCompanies { get; set; }
        public virtual ICollection<UserHasJob> UserHasJobs { get; set; }
    }
}

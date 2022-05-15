using System;
using System.Collections.Generic;

#nullable disable

namespace LaborExchangeApi.Models
{
    public partial class Vacancy
    {
        public Vacancy()
        {
            CompanyHasVacancies = new HashSet<CompanyHasVacancy>();
        }

        public int Id { get; set; }
        public int ProfessionId { get; set; }
        public int? EducationId { get; set; }
        public int? WorkDayRequirementsId { get; set; }
        public decimal Payment { get; set; }
        public string SpecialistRequirements { get; set; }
        public string Info { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Education Education { get; set; }
        public virtual Profession Profession { get; set; }
        public virtual WorkDayRequirement WorkDayRequirements { get; set; }
        public virtual ICollection<CompanyHasVacancy> CompanyHasVacancies { get; set; }
    }
}

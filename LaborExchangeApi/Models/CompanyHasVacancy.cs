using System;
using System.Collections.Generic;

#nullable disable

namespace LaborExchangeApi.Models
{
    public partial class CompanyHasVacancy
    {
        public int CompanyId { get; set; }
        public int VacancyId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Company Company { get; set; }
        public virtual Vacancy Vacancy { get; set; }
    }
}

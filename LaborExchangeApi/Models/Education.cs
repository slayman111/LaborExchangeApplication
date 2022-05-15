using System;
using System.Collections.Generic;

#nullable disable

namespace LaborExchangeApi.Models
{
    public partial class Education
    {
        public Education()
        {
            UserHasEducations = new HashSet<UserHasEducation>();
            Vacancies = new HashSet<Vacancy>();
        }

        public int Id { get; set; }
        public int? RankId { get; set; }
        public int? QualificationId { get; set; }
        public int? ProfessionId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Profession Profession { get; set; }
        public virtual Qualification Qualification { get; set; }
        public virtual Rank Rank { get; set; }
        public virtual ICollection<UserHasEducation> UserHasEducations { get; set; }
        public virtual ICollection<Vacancy> Vacancies { get; set; }
    }
}

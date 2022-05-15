using System;
using System.Collections.Generic;

#nullable disable

namespace LaborExchangeApi.Models
{
    public partial class Qualification
    {
        public Qualification()
        {
            Educations = new HashSet<Education>();
        }

        public int Id { get; set; }
        public string Value { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Education> Educations { get; set; }
    }
}

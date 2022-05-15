using System;
using System.Collections.Generic;

#nullable disable

namespace LaborExchangeApi.Models
{
    public partial class Activity
    {
        public Activity()
        {
            Companies = new HashSet<Company>();
        }

        public int Id { get; set; }
        public string Value { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}

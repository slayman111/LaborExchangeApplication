using System;
using System.Collections.Generic;

#nullable disable

namespace LaborExchangeApi.Models
{
    public partial class FamilyStatus
    {
        public FamilyStatus()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Value { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace LaborExchangeApi.Models
{
    public partial class UserHasCompany
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Company Company { get; set; }
        public virtual User User { get; set; }
    }
}

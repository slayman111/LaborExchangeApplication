using System;
using System.Collections.Generic;

#nullable disable

namespace LaborExchangeApi.Models
{
    public partial class UserHasJob
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int ProfessionId { get; set; }
        public int? ReasonOfDismissalId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Company Company { get; set; }
        public virtual Profession Profession { get; set; }
        public virtual ReasonOfDismissal ReasonOfDismissal { get; set; }
        public virtual User User { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace LaborExchangeApi.Models
{
    public partial class UserHasEducation
    {
        public int UserId { get; set; }
        public int EducationId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Education Education { get; set; }
        public virtual User User { get; set; }
    }
}

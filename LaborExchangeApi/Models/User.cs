using System;
using System.Collections.Generic;

#nullable disable

namespace LaborExchangeApi.Models
{
    public partial class User
    {
        public User()
        {
            UserHasCompanies = new HashSet<UserHasCompany>();
            UserHasEducations = new HashSet<UserHasEducation>();
            UserHasJobRequests = new HashSet<UserHasJobRequest>();
            UserHasJobs = new HashSet<UserHasJob>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime BornDate { get; set; }
        public int GenderId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public int CityId { get; set; }
        public int FamilyStatusId { get; set; }
        public string HousingConditions { get; set; }
        public string Info { get; set; }
        public byte[] Avatar { get; set; }
        public bool IsDeleted { get; set; }

        public virtual City City { get; set; }
        public virtual FamilyStatus FamilyStatus { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<UserHasCompany> UserHasCompanies { get; set; }
        public virtual ICollection<UserHasEducation> UserHasEducations { get; set; }
        public virtual ICollection<UserHasJobRequest> UserHasJobRequests { get; set; }
        public virtual ICollection<UserHasJob> UserHasJobs { get; set; }
    }
}

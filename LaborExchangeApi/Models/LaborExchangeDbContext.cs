using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LaborExchangeApi.Models
{
    public partial class LaborExchangeDbContext : DbContext
    {
        public LaborExchangeDbContext()
        {
        }

        public LaborExchangeDbContext(DbContextOptions<LaborExchangeDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyHasVacancy> CompanyHasVacancies { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<FamilyStatus> FamilyStatuses { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<JobRequest> JobRequests { get; set; }
        public virtual DbSet<Profession> Professions { get; set; }
        public virtual DbSet<Qualification> Qualifications { get; set; }
        public virtual DbSet<Rank> Ranks { get; set; }
        public virtual DbSet<ReasonOfDismissal> ReasonOfDismissals { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserHasCompany> UserHasCompanies { get; set; }
        public virtual DbSet<UserHasEducation> UserHasEducations { get; set; }
        public virtual DbSet<UserHasJob> UserHasJobs { get; set; }
        public virtual DbSet<UserHasJobRequest> UserHasJobRequests { get; set; }
        public virtual DbSet<Vacancy> Vacancies { get; set; }
        public virtual DbSet<WorkDayRequirement> WorkDayRequirements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=LaborExchangeDb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.ToTable("Activity");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.ActivityId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Logotype).HasColumnType("image");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.ActivityId)
                    .HasConstraintName("FK__Company__Activit__412EB0B6");
            });

            modelBuilder.Entity<CompanyHasVacancy>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.VacancyId })
                    .HasName("PK__CompanyH__CBD27BCFB95B832F");

                entity.ToTable("CompanyHasVacancy");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyHasVacancies)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CompanyHa__Compa__4F7CD00D");

                entity.HasOne(d => d.Vacancy)
                    .WithMany(p => p.CompanyHasVacancies)
                    .HasForeignKey(d => d.VacancyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CompanyHa__Vacan__5070F446");
            });

            modelBuilder.Entity<Education>(entity =>
            {
                entity.ToTable("Education");

                entity.Property(e => e.ProfessionId).HasDefaultValueSql("((1))");

                entity.Property(e => e.QualificationId).HasDefaultValueSql("((1))");

                entity.Property(e => e.RankId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Profession)
                    .WithMany(p => p.Educations)
                    .HasForeignKey(d => d.ProfessionId)
                    .HasConstraintName("FK__Education__Profe__36B12243");

                entity.HasOne(d => d.Qualification)
                    .WithMany(p => p.Educations)
                    .HasForeignKey(d => d.QualificationId)
                    .HasConstraintName("FK__Education__Quali__34C8D9D1");

                entity.HasOne(d => d.Rank)
                    .WithMany(p => p.Educations)
                    .HasForeignKey(d => d.RankId)
                    .HasConstraintName("FK__Education__RankI__32E0915F");
            });

            modelBuilder.Entity<FamilyStatus>(entity =>
            {
                entity.ToTable("FamilyStatus");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<JobRequest>(entity =>
            {
                entity.ToTable("JobRequest");

                entity.Property(e => e.Info).HasMaxLength(500);

                entity.Property(e => e.SalaryRequirements).HasColumnType("money");

                entity.Property(e => e.WorkDayRequirementsId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Profession)
                    .WithMany(p => p.JobRequests)
                    .HasForeignKey(d => d.ProfessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__JobReques__Profe__6D0D32F4");

                entity.HasOne(d => d.WorkDayRequirements)
                    .WithMany(p => p.JobRequests)
                    .HasForeignKey(d => d.WorkDayRequirementsId)
                    .HasConstraintName("FK__JobReques__WorkD__6E01572D");
            });

            modelBuilder.Entity<Profession>(entity =>
            {
                entity.ToTable("Profession");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Qualification>(entity =>
            {
                entity.ToTable("Qualification");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Rank>(entity =>
            {
                entity.ToTable("Rank");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ReasonOfDismissal>(entity =>
            {
                entity.ToTable("ReasonOfDismissal");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Avatar).HasColumnType("image");

                entity.Property(e => e.BornDate).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.GenderId).HasDefaultValueSql("((1))");

                entity.Property(e => e.HousingConditions).HasMaxLength(100);

                entity.Property(e => e.Info).HasMaxLength(400);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Middlename)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.RoleId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__CityId__5AEE82B9");

                entity.HasOne(d => d.FamilyStatus)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.FamilyStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__FamilyStat__5BE2A6F2");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__GenderId__571DF1D5");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__RoleId__59063A47");
            });

            modelBuilder.Entity<UserHasCompany>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.CompanyId })
                    .HasName("PK__UserHasC__C551BD8655CFC55A");

                entity.ToTable("UserHasCompany");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.UserHasCompanies)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserHasCo__Compa__60A75C0F");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserHasCompanies)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserHasCo__UserI__5FB337D6");
            });

            modelBuilder.Entity<UserHasEducation>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.EducationId })
                    .HasName("PK__UserHasE__53332FCC1188DD48");

                entity.ToTable("UserHasEducation");

                entity.Property(e => e.EducationId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Education)
                    .WithMany(p => p.UserHasEducations)
                    .HasForeignKey(d => d.EducationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserHasEd__Educa__656C112C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserHasEducations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserHasEd__UserI__6477ECF3");
            });

            modelBuilder.Entity<UserHasJob>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.CompanyId, e.ProfessionId })
                    .HasName("PK__UserHasJ__FB6E8D18F0E05461");

                entity.ToTable("UserHasJob");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.UserHasJobs)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserHasJo__Compa__787EE5A0");

                entity.HasOne(d => d.Profession)
                    .WithMany(p => p.UserHasJobs)
                    .HasForeignKey(d => d.ProfessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserHasJo__Profe__797309D9");

                entity.HasOne(d => d.ReasonOfDismissal)
                    .WithMany(p => p.UserHasJobs)
                    .HasForeignKey(d => d.ReasonOfDismissalId)
                    .HasConstraintName("FK__UserHasJo__Reaso__7A672E12");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserHasJobs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserHasJo__UserI__778AC167");
            });

            modelBuilder.Entity<UserHasJobRequest>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.JobRequestId })
                    .HasName("PK__UserHasJ__602E57C8E20BCEB4");

                entity.ToTable("UserHasJobRequest");

                entity.HasOne(d => d.JobRequest)
                    .WithMany(p => p.UserHasJobRequests)
                    .HasForeignKey(d => d.JobRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserHasJo__JobRe__73BA3083");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserHasJobRequests)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserHasJo__UserI__72C60C4A");
            });

            modelBuilder.Entity<Vacancy>(entity =>
            {
                entity.ToTable("Vacancy");

                entity.Property(e => e.Info).HasMaxLength(500);

                entity.Property(e => e.Payment).HasColumnType("money");

                entity.Property(e => e.SpecialistRequirements).HasMaxLength(200);

                entity.Property(e => e.WorkDayRequirementsId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Education)
                    .WithMany(p => p.Vacancies)
                    .HasForeignKey(d => d.EducationId)
                    .HasConstraintName("FK__Vacancy__Educati__49C3F6B7");

                entity.HasOne(d => d.Profession)
                    .WithMany(p => p.Vacancies)
                    .HasForeignKey(d => d.ProfessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Vacancy__Profess__48CFD27E");

                entity.HasOne(d => d.WorkDayRequirements)
                    .WithMany(p => p.Vacancies)
                    .HasForeignKey(d => d.WorkDayRequirementsId)
                    .HasConstraintName("FK__Vacancy__WorkDay__4AB81AF0");
            });

            modelBuilder.Entity<WorkDayRequirement>(entity =>
            {
                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(u => u.Workday);
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Workday).HasName("pkUser");
                entity.Property(u => u.Workday).IsRequired().ValueGeneratedNever();
                entity.HasOne(u => u.Employee).WithMany(e => e.Users).HasForeignKey(u => u.Workday).HasConstraintName("fkUser_Employee");
                entity.HasData(new
                {
                    Workday = 1001,
                    Email = "esteban@hotmail.com",
                    Username = "esteban.fernandez",
                    Password = "lz2ZrEsIER3XoE79aWAi2kAi5CckccEK90P4Bxvjoc8=",
                    IsActive = true
                });
            });


            modelBuilder.Entity<Employee>().HasKey(u => u.Workday);
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Workday).HasName("pkEmployee");
                entity.Property(e => e.Workday).IsRequired().ValueGeneratedNever();
                entity.Property(e => e.Salary).HasPrecision(18, 2);
                entity.HasOne(e => e.Site).WithMany(s => s.Employees).HasForeignKey(e => e.SiteId).HasConstraintName("fkEmployee_Site").OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(e => e.JobTitle).WithMany(j => j.Employees).HasForeignKey(e => e.JobId).HasConstraintName("fkEmployee_JobTitle").OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(e => e.Bank).WithMany(b => b.Employees).HasForeignKey(e => e.BankId).HasConstraintName("fkEmployee_Bank").OnDelete(DeleteBehavior.NoAction);
                entity.HasData(new
                {
                    Workday = 1001,
                    IdCard = 102220333,
                    FirstName = "Esteban",
                    LastName = "Fernandez",
                    Birthdate = DateTime.UtcNow,
                    JobId = 1,
                    Salary = 9000.00m,
                    BankId = 1,
                    BankAccountNumber = 1011110220440,
                    SiteId = 1,
                    SupervisorId = 1001,
                    HireDate = DateTime.UtcNow,
                    IsActive = true
                });
            });


            modelBuilder.Entity<JobTitle>(entity =>
            {
                entity.HasKey(j => j.JobId).HasName("pkJob");
                entity.Property(j => j.JobId).IsRequired();
                entity.HasData( new { JobId = 1, Title = "Designer", IsActive = true },
                                new { JobId = 2, Title = "Supervisor", IsActive = true },
                                new { JobId = 3, Title = "Manager", IsActive = true });
            });


            modelBuilder.Entity<Bank>(entity =>
            {
                entity.HasKey(b => b.BankId).HasName("pkBank");
                entity.Property(b => b.BankId).IsRequired();
                entity.HasOne(b => b.Site).WithMany(s => s.Banks).HasForeignKey(s => s.SiteId).HasConstraintName("fkBank_Site");
                entity.HasData(new { BankId = 1, BankName = "Banco de Costa Rica", IsActive = true, SiteId = 1 },
                               new { BankId = 2, BankName = "Banco Nacional de Costa Rica", IsActive = true, SiteId = 1 },
                               new { BankId = 3, BankName = "Banco BAC San Jose S.A.", IsActive = true, SiteId = 1 });
            });


            modelBuilder.Entity<Site>(entity =>
            {
                entity.HasKey(s => s.SiteId).HasName("pkSite");
                entity.Property(s => s.SiteId).IsRequired();
                entity.HasData(new { SiteId = 1, SiteName = "Costa Rica - San Jose", IsActive = true },
                               new { SiteId = 2, SiteName = "Mexico - Mexicali", IsActive = true },
                               new { SiteId = 3, SiteName = "China - ", IsActive = true },
                               new { SiteId = 4, SiteName = "Spain - Madrid", IsActive = true });
            });


            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.RoleId).HasName("pkRole");
                entity.Property(r => r.RoleId).IsRequired();
                entity.HasOne(r => r.Creator).WithMany(u => u.RolesCreator).HasForeignKey(r => r.CreatedBy).HasConstraintName("fkRole_User_CreatedBy");
                entity.HasOne(r => r.Modifier).WithMany(u => u.RolesModifier).HasForeignKey(r => r.UpdatedBy).HasConstraintName("fkRole_User_UpdatedBy");
                entity.HasMany(r => r.Policies).WithOne(p => p.Role).HasForeignKey(r => r.RoleId).HasConstraintName("fkRole_Policy").OnDelete(DeleteBehavior.Cascade);
                entity.HasData( new Role { RoleId = 1, RoleName = "root", CreatedBy = 1001, Description = "root access", CreationDate = DateTime.Now, UpdatedBy = null, UpdateDate = null, IsActive = true });
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.HasKey(m => m.ModuleId).HasName("pkModule");
                entity.Property(m => m.ModuleId).IsRequired();
                entity.HasMany(r => r.Policies).WithOne(p => p.Module).HasForeignKey(r => r.ModuleId).HasConstraintName("fkModule_Policy").OnDelete(DeleteBehavior.NoAction);
                entity.HasData(new Module { ModuleId = 1, ModuleName = "payroll", ComponentSubModuleName = "employee", SubModuleName = "Employee", IsActive = true },
                               new Module { ModuleId = 2, ModuleName = "payroll", ComponentSubModuleName = "incident", SubModuleName = "Incident", IsActive = true },
                               new Module { ModuleId = 3, ModuleName = "payroll", ComponentSubModuleName = "incidentApproval", SubModuleName = "Incident Approval", IsActive = true },
                               new Module { ModuleId = 4, ModuleName = "security", ComponentSubModuleName = "user", SubModuleName = "User", IsActive = true },
                               new Module { ModuleId = 5, ModuleName = "security", ComponentSubModuleName = "role", SubModuleName = "Role" , IsActive = true });
            });

            modelBuilder.Entity<Policy>(entity => 
            {
                entity.HasKey(p => p.PolicyId).HasName("pkPolicy");
                entity.Property(p => p.PolicyId).IsRequired();
            });
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }


   
        public DbSet<Bank> Banks { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<Site> Sites { get; set; }


        public DbSet<Role> Roles { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Module> Modules { get; set; }

    }
}

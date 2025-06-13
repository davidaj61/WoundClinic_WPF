using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WoundClinic_WPF.Models;

namespace WoundClinic_WPF.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<WoundCare> WoundCares { get; set; }
        public DbSet<Dressing> Dressings { get; set; }
        public DbSet<DressingCare> DressingCares { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        //public ApplicationDbContext()
        //{

        //}
        //// کانستراکتور حتما باید اینجا باشه و به base پاس داده بشه
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        //{
        //}

        // این بخش اختیاری است و اگر بخواهید کانکشن استرینگ در اینجا باشد
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) // اگر کانفیگ از بیرون نیامده بود، اینجا کانفیگ کن
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=WoundCareDb;Trusted_Connection=True;").LogTo(message => System.Diagnostics.Debug.WriteLine(message), LogLevel.Information); 
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationRole>().HasData(
                    new ApplicationRole { Id = 1, RoleName = "admin", RoleDescription = "مدیر سیستم"},
                    new ApplicationRole { Id = 2, RoleName = "Supervisor", RoleDescription = "سوپروایزور" },
                    new ApplicationRole { Id = 3, RoleName = "user", RoleDescription = "کاربر" });
            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    FirstName="داود",
                    LastName="اقاویل جهرمی",
                    Gender=true,
                    NationalCode=1285046358,
                });
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    NationalCode = 1285046358,
                    PasswordHash= Encryption.GetSha256Hash("Aa@123456"),
                    IsActive=true,
                    
                });

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(q => q.Role)
                .WithMany(q => q.Users)
                .HasForeignKey(q=>q.RoleId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(true);

            modelBuilder.Entity<Person>()
                .HasOne(q => q.Patient)
                .WithOne(q => q.Person)
                .HasForeignKey<Patient>(q => q.NationalCode)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            modelBuilder.Entity<Patient>()
                .HasOne(q => q.ApplicationUser)
                .WithMany(q => q.Patients)
                .HasPrincipalKey(q => q.NationalCode)
                .HasForeignKey(q => q.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            modelBuilder.Entity<Person>()
                .HasOne(e => e.ApplicationUser)
                .WithOne(e => e.Person)
                .HasForeignKey<ApplicationUser>(e => e.NationalCode)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            modelBuilder.Entity<WoundCare>()
                .HasOne(q => q.ApplicationUser)
                .WithMany(q => q.WoundCares)
                .HasPrincipalKey(q => q.NationalCode)
                .HasForeignKey(q => q.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            modelBuilder.Entity<WoundCare>()
                .HasOne(q => q.Patient)
                .WithMany(q => q.WoundCares)
                .HasForeignKey(q => q.PatientId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            modelBuilder.Entity<WoundCare>()
                .HasMany(q => q.DressingCares)
                .WithOne(q => q.WoundCare)
                .HasForeignKey(q => q.WoundCareId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            modelBuilder.Entity<Dressing>()
                .HasMany(q => q.DressingCares)
                .WithOne(q => q.Dressing)
                .HasForeignKey(q => q.DressingId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}

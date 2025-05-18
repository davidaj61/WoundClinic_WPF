using Microsoft.EntityFrameworkCore;
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

        public ApplicationDbContext()
        {
               
        }
        // کانستراکتور حتما باید اینجا باشه و به base پاس داده بشه
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // این بخش اختیاری است و اگر بخواهید کانکشن استرینگ در اینجا باشد
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) // اگر کانفیگ از بیرون نیامده بود، اینجا کانفیگ کن
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=WoundCareDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(q => q.Roles)
                .WithMany(q => q.Users);

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

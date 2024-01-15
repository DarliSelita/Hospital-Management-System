// YourDbContext.cs
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace HospitalManagementSystem.Database
{
    public class YourDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Billing> Billing { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Diagnostic> PatientRecords { get; set; }
        public DbSet<MedicalTest> MedicalTests { get; set; }
        public DbSet<ChronicIllness> ChronicIllnesses { get; set; } 


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
               .HasKey(p => p.ProductID);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Billing)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BillingID);

            modelBuilder.Entity<Diagnostic>().HasKey(d => d.PatientFile);

            modelBuilder.Entity<MedicalTest>()
                .HasKey(mt => mt.TestId);

            modelBuilder.Entity<MedicalTest>()
                .HasOne(mt => mt.PatientRecord)
                .WithMany(pr => pr.MedicalTests)
                .HasForeignKey(mt => mt.PatientRecordId);

            modelBuilder.Entity<ChronicIllness>()
                .HasKey(ci => ci.ChronicIllnessId);

            modelBuilder.Entity<ChronicIllness>()
                .HasOne(ci => ci.Diagnostic)
                .WithMany(d => d.ChronicIllnesses)
                .HasForeignKey(ci => ci.PatientFile)
                .OnDelete(DeleteBehavior.Cascade);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=127.0.0.1;Database=HospitalManagementSystem;User=root;Password=erisat123;");
        }
    }
}

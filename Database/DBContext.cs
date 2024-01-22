using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;



namespace HospitalManagementSystem.Database
{
    public class YourDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } 

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Billing> Billing { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Diagnostic> PatientRecords { get; set; }
        public DbSet<MedicalTest> MedicalTest { get; set; }
        public DbSet<ChronicIllness> ChronicIllnesses { get; set; }
        public DbSet<VitalSigns> VitalSigns { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PatientPrc> PatientPrcs { get; set; }
        public DbSet<PatientDetails> PatientDetails { get; set; }
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<PrescribedMedication> PrescribedMedications { get; set; }
        public DbSet<MedicalInventory> MedicalInventories { get; set; }
        public DbSet<DispensingRecord> DispensingRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasKey(u => u.UserId);


            modelBuilder.Entity<Product>()
      .HasKey(p => p.ProductID);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Billing)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BillingID)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for the Billing-Product relationship

            modelBuilder.Entity<Diagnostic>().HasKey(d => d.Id);

            modelBuilder.Entity<MedicalTest>()
                .HasOne(mt => mt.Diagnostic)
                .WithMany(d => d.MedicalTests)
                .HasForeignKey(mt => mt.DiagnosticId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for the Diagnostic-MedicalTest relationship

            modelBuilder.Entity<ChronicIllness>()
                .HasOne(ci => ci.Diagnostic)
                .WithMany(d => d.ChronicIllnesses)
                .HasForeignKey(ci => ci.DiagnosticId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for the Diagnostic-ChronicIllness relationship

            modelBuilder.Entity<Diagnostic>()
                 .HasOne(d => d.PatientVitalSigns)
                 .WithOne(vs => vs.Diagnostic)
                 .HasForeignKey<VitalSigns>(vs => vs.DiagnosticId)
                 .OnDelete(DeleteBehavior.Cascade); // Cascade delete for the Diagnostic-VitalSigns relationship


            // Medication
            modelBuilder.Entity<Medication>()
                .HasKey(m => m.MedicationId);

            // Prescription
            modelBuilder.Entity<Prescription>()
                .HasKey(p => p.PrescriptionId);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Patient)
                .WithMany(patient => patient.Prescriptions)
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Diagnose)
                .WithMany(d => d.Prescriptions)
                .HasForeignKey(p => p.DiagnoseId)
                .OnDelete(DeleteBehavior.Restrict);

            // PrescribedMedication
            modelBuilder.Entity<PrescribedMedication>()
                .HasKey(pm => pm.PrescribedMedicationId);

            modelBuilder.Entity<PrescribedMedication>()
                .HasOne(pm => pm.Prescription)
                .WithMany(p => p.PrescribedMedications)
                .HasForeignKey(pm => pm.PrescriptionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PrescribedMedication>()
                .HasOne(pm => pm.Medication)
                .WithMany(m => m.PrescribedMedications)
                .HasForeignKey(pm => pm.MedicationId)
                .OnDelete(DeleteBehavior.Cascade);

            // MedicalInventory
            modelBuilder.Entity<MedicalInventory>()
                .HasKey(mi => mi.MedicalInventoryId);

            modelBuilder.Entity<MedicalInventory>()
                .HasOne(mi => mi.Medication)
                .WithOne(m => m.MedicalInventory)
                .HasForeignKey<MedicalInventory>(mi => mi.MedicationId)
                .OnDelete(DeleteBehavior.Cascade);

            // DispensingRecord
            modelBuilder.Entity<DispensingRecord>()
                .HasKey(dr => dr.DispensingRecordId);

            modelBuilder.Entity<DispensingRecord>()
                .HasOne(dr => dr.Prescription)
                .WithMany(p => p.DispensingRecords)
                .HasForeignKey(dr => dr.PrescriptionId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddProvider(new MyLoggerProvider());
            });

            optionsBuilder.UseMySql("Server=127.0.0.1;Database=HospitalManagementSystem;User=root;Password=erisat123;")
                          .EnableSensitiveDataLogging()  // Enable sensitive data logging
                          .UseLoggerFactory(loggerFactory);
        }


    }
}


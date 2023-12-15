// YourDbContext.cs
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace HospitalManagementSystem.Database
{
    public class YourDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure the connection string for your database
            optionsBuilder.UseMySql("Server=127.0.0.1;Database=HospitalManagementSystem;User=root;Password=erisat123;");
        }

    }
}

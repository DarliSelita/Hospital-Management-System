// YourDbContext.cs
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;

namespace HospitalManagementSystem.Database
{
    public class YourDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure the connection string for your database
            optionsBuilder.UseSqlServer("YourConnectionString");
        }
    }
}

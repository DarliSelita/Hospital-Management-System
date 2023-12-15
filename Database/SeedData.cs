using HospitalManagementSystem.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Database
{
    public static class SeedData
    {
        public static void Initialize(YourDbContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Check if there's already data in the database
            if (context.Patients.Any() || context.Appointments.Any())
            {
                return; // Database has been seeded
            }

            // Seed the database with some initial data
            var patient1 = new Patient
            {
                Name = "John Doe",
                Surname = "Doe",
                DateOfBirth = new DateTime(1980, 1, 1),
                FileNumber = "P123001",
                PhoneNumber = "555-1001",
                Email = "john.doe@example.com",
                AssignedDoctor = "Dr. Smith"
            };

            var patient2 = new Patient
            {
                Name = "Jane Doe",
                Surname = "Doe",
                DateOfBirth = new DateTime(1990, 5, 10),
                FileNumber = "P123002",
                PhoneNumber = "555-1002",
                Email = "jane.doe@example.com",
                AssignedDoctor = "Dr. Johnson"
            };


            // Add the patients to the context
            context.Patients.AddRange(patient1, patient2);

            // Save changes to the database
            context.SaveChanges();

            // Seed appointment data
            var appointments = new Appointment[]
            {
                new Appointment { Id = 1, ScheduleHour = DateTime.Now.AddHours(1), /* other properties */ },
                new Appointment { Id = 2, ScheduleHour = DateTime.Now.AddHours(2), /* other properties */ },
                // Add more appointments as needed
            };

            // Add the appointments to the context
            context.Appointments.AddRange(appointments);

            // Save changes to the database
            context.SaveChanges();
        }
    }
}


using System;


    public class Appointment
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string PatientFileNumber { get; set; }
        public DateTime ScheduleHour { get; set; }
        public string DoctorName { get; set; }
        public AppointmentType Type { get; set; }
    }

    public enum AppointmentType
    {
        Temporary,
        FixedTerm,
        Continuing
    }


using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalManagementSystem.Forms;
using Project.Forms.Appointment_Forms;
using Project.Forms.Billing_Forms;
using Project.Forms.Diagnostic_Forms;
using Project.Forms.Employee_Forms;
using Project.Forms.Patient_Forms;
using Project.Forms.Pharmacy_Forms;

namespace Project
{
     static class  Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LogIn());
        }
    }
}

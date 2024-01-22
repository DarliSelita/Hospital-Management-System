using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project.Forms.Patient_Forms;
using Project.Forms.Diagnostic_Forms;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Database;

namespace Project.Forms.Employee_Forms
{
    public partial class DoctorDashboard : Form
    {
        private YourDbContext dbContext;

        public DoctorDashboard(string username)
        {
            InitializeComponent();
            SetUserName(username);
            dbContext = new YourDbContext();
            DisplayCounts();

        }

        private void DisplayCounts()
        {
            int patientCount = dbContext.Patients.Count();
            int appointmentCount = dbContext.Appointments.Count();
            int prescriptionCount = dbContext.Prescriptions.Count();

            PatientCountLabel.Text = $"{patientCount}";
            AppointmentCountLabel.Text = $"{appointmentCount}";
            PrescriptionCountLabel.Text = $"{prescriptionCount}";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ViewPatientForm vp = new ViewPatientForm(); 
            vp.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ViewDiagnosticForm df = new ViewDiagnosticForm();
            df.Show();
        }

        public void SetUserName(string username)
        {
            // Display the username in the top right corner label
            lblWelcome.Text = $"Hello, {username}!";
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to Sign Out?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                LogIn lg = new LogIn();
                lg.Show();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You do not have permission to access this feature.");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You do not have permission to access this feature.");

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You do not have permission to access this feature.");

        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void AppointmentCountLabel_Click(object sender, EventArgs e)
        {

        }
    }
}

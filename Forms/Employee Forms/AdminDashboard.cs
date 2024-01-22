﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project.Forms.Patient_Forms;
using Project.Forms.Appointment_Forms;
using Project.Forms.Diagnostic_Forms;
using Project.Forms.Billing_Forms;
using Project.Forms.Pharmacy_Forms;
using HospitalManagementSystem.Forms;
using HospitalManagementSystem.Database;

namespace Project.Forms.Employee_Forms
{
    public partial class AdminDashboard : Form
    {
        private YourDbContext dbContext;

        public AdminDashboard(string username)
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

        public void SetUserName(string username)
        {
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
            ViewAppointmentForm ap = new ViewAppointmentForm();
            ap.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ViewPatientForm vp = new ViewPatientForm();
            vp.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ViewDiagnosticForm vd= new ViewDiagnosticForm();
            vd.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ViewPrescriptionForm vp = new ViewPrescriptionForm();
            vp.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            MedicalInventoryForm mi = new MedicalInventoryForm();
            mi.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            ViewInvoiceForm iv = new ViewInvoiceForm();
            iv.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void PatientCountLabel_Click(object sender, EventArgs e)
        {


        }

        private void AppointmentCountLabel_Click(object sender, EventArgs e)
        {

        }

        private void PrescriptionCountLabel_Click(object sender, EventArgs e)
        {

        }
    }
}

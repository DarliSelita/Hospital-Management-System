using System;
using System.Linq;
using System.Windows.Forms;
using HospitalManagementSystem.Database;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Forms
{
    public partial class DeletePrescriptionForm : Form
    {
        private readonly Prescription prescriptionToDelete;

        public DeletePrescriptionForm(Prescription prescription)
        {
            InitializeComponent();
            prescriptionToDelete = prescription;
            InitializeUIComponents();
            this.Size = new System.Drawing.Size(650, 500);

        }

        private void InitializeUIComponents()
        {
            var lblConfirmation = new Label
            {
                Text = $"Are you sure you want to delete Prescription ID {prescriptionToDelete.PrescriptionId}?",
                Location = new System.Drawing.Point(50, 50),
                AutoSize = true
            };

            var btnDelete = new Button
            {
                Text = "Delete",
                Location = new System.Drawing.Point(150, 100),
                Size = new System.Drawing.Size(100, 40),
                BackColor = System.Drawing.Color.FromArgb(255, 0, 0),
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Open Sans", 12, System.Drawing.FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 }
            };
            btnDelete.Click += btnDelete_Click;

            var btnCancel = new Button
            {
                Text = "Cancel",
                Location = new System.Drawing.Point(300, 100),
                Size = new System.Drawing.Size(100, 40),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = System.Drawing.Color.LightBlue,
                Padding = new Padding(6),
                Font = new System.Drawing.Font("French Script MT", 18)
            };
            btnCancel.Click += btnCancel_Click;

            Controls.Add(lblConfirmation);
            Controls.Add(btnDelete);
            Controls.Add(btnCancel);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (var dbContext = new YourDbContext()) // Replace with your actual DbContext
            {
                try
                {
                    // Retrieve the prescription with related entities from the database
                    var prescription = dbContext.Prescriptions
                        .Include(p => p.PrescribedMedications)
                        .Include(p => p.DispensingRecords)
                        .Where(p => p.PrescriptionId == prescriptionToDelete.PrescriptionId)
                        .FirstOrDefault();

                    if (prescription != null)
                    {
                        // Remove prescribed medications and dispensing records
                        dbContext.PrescribedMedications.RemoveRange(prescription.PrescribedMedications);
                        dbContext.DispensingRecords.RemoveRange(prescription.DispensingRecords);

                        // Remove the prescription
                        dbContext.Prescriptions.Remove(prescription);

                        dbContext.SaveChanges();

                        MessageBox.Show("Prescription deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Prescription not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting prescription: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Close the form after deleting the prescription
            this.Close();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Close the form without deleting the prescription
            this.Close();
        }

        private void InitializeComponent() { }

    }
}
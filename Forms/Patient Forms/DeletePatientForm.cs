using System;
using System.Windows.Forms;
using HospitalManagementSystem.Database; // Assuming your database namespace

namespace HospitalManagementSystem.Forms.PatientForms
{
    public partial class DeletePatientForm : Form
    {
        private Patient patientToDelete; // Assuming you have a Patient class

        public DeletePatientForm(Patient patient)
        {
            InitializeComponent();
            patientToDelete = patient;
            InitializeUIComponents();
        }

        private void InitializeUIComponents()
        {
            // Initialize labels and buttons as needed
            var lblConfirmation = new Label
            {
                Text = $"Are you sure you want to delete {patientToDelete.Name}?",
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
                BackColor = System.Drawing.Color.FromArgb(0, 123, 255),
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Open Sans", 12, System.Drawing.FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 }
            };
            btnCancel.Click += btnCancel_Click;

            Controls.Add(lblConfirmation);
            Controls.Add(btnDelete);
            Controls.Add(btnCancel);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Perform the deletion logic here
            using (var dbContext = new YourDbContext()) // Replace with your actual DbContext
            {
                try
                {
                    dbContext.Patients.Remove(patientToDelete);
                    dbContext.SaveChanges();

                    MessageBox.Show("Patient deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Optionally, you can close the form after deleting the patient
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting patient: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Close the form without deleting the patient
            this.Close();
        }

        private void DeletePatientForm_Load(object sender, EventArgs e)
        {

        }
    }
}

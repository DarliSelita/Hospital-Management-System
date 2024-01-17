using System;
using System.Drawing;
using System.Windows.Forms;
using HospitalManagementSystem.Database;

namespace Project.Forms.Diagnostic_Forms
{
    public partial class DeleteDiagnosticForm : Form
    {
        private readonly Diagnostic diagnosticRecordToDelete; // Marking it as readonly

        public DeleteDiagnosticForm(Diagnostic diagnosticRecord)
        {
            InitializeComponent();
            diagnosticRecordToDelete = diagnosticRecord;
            InitializeUIComponents();
        }

        private void InitializeUIComponents()
        {
            // Initialize labels and buttons as needed
            var lblConfirmation = new Label
            {
                Text = $"Are you sure you want to delete the diagnostic record for {diagnosticRecordToDelete.PatientName}?",
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
                BackColor = Color.LightBlue,
                Padding = new Padding(6),
                Font = new Font("French Script MT", 18)
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
                    dbContext.PatientRecords.Remove(diagnosticRecordToDelete);
                    dbContext.SaveChanges();

                    MessageBox.Show("Diagnostic record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting diagnostic record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Close the form after deleting the diagnostic record
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Close the form without deleting the diagnostic record
            this.Close();
        }

        private void DeleteDiagnosticForm_Load(object sender, EventArgs e)
        {

        }
    }
}

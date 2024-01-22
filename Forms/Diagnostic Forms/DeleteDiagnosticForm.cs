using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalManagementSystem.Database;

namespace Project.Forms.Diagnostic_Forms
{
    public partial class DeleteDiagnosticForm : Form
    {
        private readonly Diagnostic diagnosticRecordToDelete;

        public DeleteDiagnosticForm(Diagnostic diagnosticRecord)
        {
            InitializeComponent();
            diagnosticRecordToDelete = diagnosticRecord;
            InitializeUIComponents();
        }

        private void InitializeUIComponents()
        {
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
            using (var dbContext = new YourDbContext())
            {
                try
                {
                    // Attach the diagnostic record to the context
                    dbContext.Attach(diagnosticRecordToDelete);

                    // Manually delete associated MedicalTest records
                    var medicalTestsToDelete = dbContext.MedicalTest
                        .Where(mt => mt.DiagnosticId == diagnosticRecordToDelete.Id)
                        .ToList();

                    dbContext.MedicalTest.RemoveRange(medicalTestsToDelete);

                    // Manually delete associated ChronicIllness records
                    var chronicIllnessesToDelete = dbContext.ChronicIllnesses
                        .Where(ci => ci.DiagnosticId == diagnosticRecordToDelete.Id)
                        .ToList();

                    dbContext.ChronicIllnesses.RemoveRange(chronicIllnessesToDelete);

                    // Manually delete associated VitalSigns record
                    var vitalSignsToDelete = dbContext.VitalSigns
                        .Where(vs => vs.DiagnosticId == diagnosticRecordToDelete.Id)
                        .ToList();

                    dbContext.VitalSigns.RemoveRange(vitalSignsToDelete);

                    // Remove the diagnostic record
                    dbContext.PatientRecords.Remove(diagnosticRecordToDelete);

                    // Save changes to the database
                    dbContext.SaveChanges();

                    MessageBox.Show("Diagnostic record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting diagnostic record: {ex.Message}\nInner Exception: {ex.InnerException?.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

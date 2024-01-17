using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using HospitalManagementSystem.Database;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using FontAwesome.Sharp;
using System.Windows.Media.Media3D;

namespace Project.Forms.Diagnostic_Forms
{
    public partial class UpdateDiagnosticForm : Form
    {
        private YourDbContext DbContext;
        private Diagnostic patientRecord;
        private List<string> initialChronicIllnesses;
        private List<MedicalTest> initialMedicalTests;
        private TextBox txtPatientName;
        private TextBox txtPatientFile;
        private ComboBox cboGender;
        private TextBox txtPhoneNumber;
        private TextBox txtReasonForAppointment;
        private ListBox lstChronicIllnesses;
        private TextBox txtAllergies;
        private CheckBox chkTobaccoUse;
        private TextBox txtRespiratoryRate;
        private TextBox txtTemperature;
        private TextBox txtHeight;
        private TextBox txtWeight;
        private CheckBox chkDrugUse;
        private TextBox txtBloodPressure;
        private TextBox txtHeartRate;
        private TextBox txtAlcoholConsumption;
        private TextBox txtMedicationsAndVaccines;

        public UpdateDiagnosticForm(Diagnostic selectedPatientRecord)
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1050, 500);
            DbContext = new YourDbContext();
            patientRecord = selectedPatientRecord;
            initialChronicIllnesses = patientRecord.ChronicIllnesses.Select(ci => ci.ChronicIllnessName).ToList();
            initialMedicalTests = new List<MedicalTest>(patientRecord.MedicalTests);

            LoadPatientRecordData();
        }

        private void LoadPatientRecordData()
        {
            txtPatientName.Text = patientRecord.PatientName;
            txtPatientFile.Text = patientRecord.PatientFile;
            cboGender.SelectedItem = patientRecord.Gender;
            txtPhoneNumber.Text = patientRecord.PhoneNumber;
            txtReasonForAppointment.Text = patientRecord.ReasonForAppointment;

            lstChronicIllnesses.Items.AddRange(initialChronicIllnesses.ToArray());

            txtAllergies.Text = patientRecord.Allergies;
            txtMedicationsAndVaccines.Text = patientRecord.MedicationsAndVaccines;
            chkTobaccoUse.Checked = patientRecord.TobaccoUse;
            txtAlcoholConsumption.Text = patientRecord.AlcoholConsumptionPerWeek.ToString();
            chkDrugUse.Checked = patientRecord.DrugUse;
            txtBloodPressure.Text = patientRecord.PatientVitalSigns?.BloodPressure.ToString();
            txtHeartRate.Text = patientRecord.PatientVitalSigns?.HeartRate.ToString();
            txtRespiratoryRate.Text = patientRecord.PatientVitalSigns?.RespiratoryRate.ToString();
            txtTemperature.Text = patientRecord.PatientVitalSigns?.Temperature.ToString();
            txtHeight.Text = patientRecord.PatientVitalSigns?.Height.ToString();
            txtWeight.Text = patientRecord.PatientVitalSigns?.Weight.ToString();

            foreach (var test in initialMedicalTests)
            {
                UpdateButtonAppearance(test.TestName);
            }
        }

               private void UpdateButtonAppearance(string testType)
        {
            // Find the corresponding button based on the testType
            string buttonName = testType == "X-Ray" ? "btnAddXRayTest" : "btnAddCheckupTest";
            IconButton button = Controls.Find(buttonName, true).FirstOrDefault() as IconButton;

            if (button != null)
            {
                // Change button appearance and icon
                button.IconChar = IconChar.CheckCircle;
                button.IconColor = Color.Green;
                button.BackColor = Color.FromArgb(33, 33, 33); // Update with your desired color
            }
        }
       

        private void btnSaveToDatabase_Click(object sender, EventArgs e)
        {
            using (var dbContext = new YourDbContext())
            {
                try
                {
                    // Load the original patient record from the database
                    var originalPatientRecord = dbContext.PatientRecords
                        .Include(pr => pr.MedicalTests)
                        .Include(pr => pr.ChronicIllnesses)
                        .Include(pr => pr.PatientVitalSigns)
                        .FirstOrDefault(pr => pr.PatientFile == patientRecord.PatientFile);

                    if (originalPatientRecord != null)
                    {
                        // Update the patient record properties
                        originalPatientRecord.PatientName = txtPatientName.Text;
                        originalPatientRecord.Gender = cboGender.SelectedItem?.ToString();
                        originalPatientRecord.PhoneNumber = txtPhoneNumber.Text;
                        originalPatientRecord.ReasonForAppointment = txtReasonForAppointment.Text;
                        originalPatientRecord.Allergies = txtAllergies.Text;
                        originalPatientRecord.MedicationsAndVaccines = txtMedicationsAndVaccines.Text;
                        originalPatientRecord.TobaccoUse = chkTobaccoUse.Checked;
                        originalPatientRecord.AlcoholConsumptionPerWeek = double.Parse(txtAlcoholConsumption.Text);
                        originalPatientRecord.DrugUse = chkDrugUse.Checked;

                        // Update chronic illnesses
                        var currentChronicIllnesses = lstChronicIllnesses.Items.Cast<string>().ToList();
                        UpdateChronicIllnesses(originalPatientRecord, currentChronicIllnesses);

                        // Update patient vital signs
                        UpdatePatientVitalSigns(originalPatientRecord);

                        // Save changes to the database
                        dbContext.SaveChanges();
                        MessageBox.Show("Patient record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Patient record not found in the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred. Please contact support. Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Additional methods for updating chronic illnesses and vital signs
        // ...

        private void UpdateChronicIllnesses(Diagnostic patientRecord, List<string> currentChronicIllnesses)
        {
            // Remove deleted chronic illnesses
            var deletedChronicIllnesses = initialChronicIllnesses.Except(currentChronicIllnesses).ToList();
            foreach (var deletedIllness in deletedChronicIllnesses)
            {
                var illnessToRemove = patientRecord.ChronicIllnesses.FirstOrDefault(ci => ci.ChronicIllnessName == deletedIllness);
                if (illnessToRemove != null)
                {
                    DbContext.Entry(illnessToRemove).State = EntityState.Deleted;
                }
            }

            // Add new chronic illnesses
            var newChronicIllnesses = currentChronicIllnesses.Except(initialChronicIllnesses).ToList();
            foreach (var newIllness in newChronicIllnesses)
            {
                var illnessToAdd = new ChronicIllness { ChronicIllnessName = newIllness };
                patientRecord.ChronicIllnesses.Add(illnessToAdd);
            }

            // Update existing chronic illnesses
            foreach (var currentIllness in currentChronicIllnesses)
            {
                var existingIllness = patientRecord.ChronicIllnesses.FirstOrDefault(ci => ci.ChronicIllnessName == currentIllness);
                if (existingIllness != null)
                {
                    // Perform any additional updates to the chronic illness entity if needed
                }
            }
        }

        private void UpdatePatientVitalSigns(Diagnostic patientRecord)
        {
            // If patient vital signs are not already created, create a new instance
            if (patientRecord.PatientVitalSigns == null)
            {
                patientRecord.PatientVitalSigns = new VitalSigns();
            }

            // Update patient vital signs properties
            patientRecord.PatientVitalSigns.BloodPressure = int.Parse(txtBloodPressure.Text);
            patientRecord.PatientVitalSigns.HeartRate = int.Parse(txtHeartRate.Text);
            patientRecord.PatientVitalSigns.RespiratoryRate = int.Parse(txtRespiratoryRate.Text);
            patientRecord.PatientVitalSigns.Temperature = float.Parse(txtTemperature.Text);
            patientRecord.PatientVitalSigns.Height = float.Parse(txtHeight.Text);
            patientRecord.PatientVitalSigns.Weight = float.Parse(txtWeight.Text);
        }
    }
}

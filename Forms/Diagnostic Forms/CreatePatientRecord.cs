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


namespace Project.Forms.Diagnostic_Forms
{
    public partial class CreatePatientRecord : Form
    {
        private Diagnostic patientRecord;

        public CreatePatientRecord()
        {
            InitializeComponent();
            patientRecord = new Diagnostic();

        }
        private void InitializeComponent()
        {
            Label lblPatientName = new Label { Text = "Patient Name:", Location = new Point(50, 50), AutoSize = true };
            TextBox txtPatientName = new TextBox { Location = new Point(200, 50), Size = new Size(200, 20) };
            txtPatientName.TextChanged += (sender, e) => patientRecord.PatientName = txtPatientName.Text;

            Label lblPatientFile = new Label { Text = "Patient File:", Location = new Point(50, 80), AutoSize = true };
            TextBox txtPatientFile = new TextBox { Location = new Point(200, 80), Size = new Size(200, 20) };
            txtPatientFile.TextChanged += (sender, e) => patientRecord.PatientFile = txtPatientFile.Text;

            Label lblGender = new Label { Text = "Gender:", Location = new Point(50, 110), AutoSize = true };
            ComboBox cboGender = new ComboBox { Location = new Point(200, 110), Size = new Size(200, 20), DropDownStyle = ComboBoxStyle.DropDownList };
            cboGender.Items.AddRange(new object[] { "Male", "Female", "Other" });
            cboGender.SelectedIndexChanged += (sender, e) => patientRecord.Gender = cboGender.SelectedItem?.ToString();

            Label lblPhoneNumber = new Label { Text = "Phone Number:", Location = new Point(50, 140), AutoSize = true };
            TextBox txtPhoneNumber = new TextBox { Location = new Point(200, 140), Size = new Size(200, 20) };
            txtPhoneNumber.TextChanged += (sender, e) => patientRecord.PhoneNumber = txtPhoneNumber.Text;

            Label lblReasonForAppointment = new Label { Text = "Reason for Appointment:", Location = new Point(50, 170), AutoSize = true };
            TextBox txtReasonForAppointment = new TextBox { Location = new Point(200, 170), Size = new Size(200, 20) };
            txtReasonForAppointment.TextChanged += (sender, e) => patientRecord.ReasonForAppointment = txtReasonForAppointment.Text;

            Label lblChronicIllnesses = new Label { Text = "Chronic Illnesses:", Location = new Point(50, 200), AutoSize = true };
            TextBox txtChronicIllnesses = new TextBox { Location = new Point(200, 200), Size = new Size(200, 20) };

            ListBox lstChronicIllnesses = new ListBox { Location = new Point(200, 240), Size = new Size(200, 100) };
            Button btnAddChronicIllness = new Button { Text = "+", Size = new Size(50, 25), Location = new Point(420, 200), Font = new Font("Open Sans", 12, FontStyle.Bold), TextAlign = ContentAlignment.MiddleCenter };

            btnAddChronicIllness.Click += (sender, e) =>
            {
                if (!string.IsNullOrWhiteSpace(txtChronicIllnesses.Text))
                {
                    lstChronicIllnesses.Items.Add(txtChronicIllnesses.Text);
                    txtChronicIllnesses.Clear();
                }
            };

            lstChronicIllnesses.SelectedIndexChanged += (sender, e) =>
            {
                var selectedChronicIllness = lstChronicIllnesses.SelectedItem?.ToString();

                if (!string.IsNullOrEmpty(selectedChronicIllness))
                {
                    // Create a ChronicIllness object and add it to the list
                    ChronicIllness chronicIllness = new ChronicIllness { ChronicIllnessName = selectedChronicIllness };
                    patientRecord.ChronicIllnesses.Add(chronicIllness);
                }
            };

            Label lblAllergies = new Label { Text = "Allergies:", Location = new Point(50, 360), AutoSize = true };
            TextBox txtAllergies = new TextBox { Location = new Point(200, 360), Size = new Size(200, 20) };
            txtAllergies.TextChanged += (sender, e) => patientRecord.Allergies = txtAllergies.Text;

            Label lblMedicationsAndVaccines = new Label { Text = "Medications and Vaccines:", Location = new Point(50, 390), AutoSize = true };
            TextBox txtMedicationsAndVaccines = new TextBox { Location = new Point(200, 390), Size = new Size(200, 20) };
            txtMedicationsAndVaccines.TextChanged += (sender, e) => patientRecord.MedicationsAndVaccines = txtMedicationsAndVaccines.Text;

            CheckBox chkTobaccoUse = new CheckBox { Text = "Tobacco Use", Location = new Point(50, 420) };
            chkTobaccoUse.CheckedChanged += (sender, e) => patientRecord.TobaccoUse = chkTobaccoUse.Checked;

            Label lblAlcoholConsumption = new Label { Text = "Alcohol Consumption (ml per week):", Location = new Point(50, 450), AutoSize = true };
            TextBox txtAlcoholConsumption = new TextBox { Location = new Point(250, 450), Size = new Size(100, 20) };
            txtAlcoholConsumption.TextChanged += (sender, e) =>
            {
                if (double.TryParse(txtAlcoholConsumption.Text, out double result))
                    patientRecord.AlcoholConsumptionPerWeek = result;
                else
                    patientRecord.AlcoholConsumptionPerWeek = 0;
            };

            CheckBox chkDrugUse = new CheckBox { Text = "Drug Use", Location = new Point(50, 480) };
            chkDrugUse.CheckedChanged += (sender, e) => patientRecord.DrugUse = chkDrugUse.Checked;

            Label lblBloodPressure = new Label { Text = "Blood Pressure:", Location = new Point(50, 510), AutoSize = true };
            TextBox txtBloodPressure = new TextBox { Location = new Point(200, 510), Size = new Size(100, 20) };
            txtBloodPressure.TextChanged += (sender, e) =>
            {
                if (int.TryParse(txtBloodPressure.Text, out int result))
                    patientRecord.PatientVitalSigns.BloodPressure = result;
                else
                    patientRecord.PatientVitalSigns.BloodPressure = 0;
            };

            Label lblHeartRate = new Label { Text = "Heart Rate:", Location = new Point(50, 540), AutoSize = true };
            TextBox txtHeartRate = new TextBox { Location = new Point(200, 540), Size = new Size(100, 20) };
            txtHeartRate.TextChanged += (sender, e) =>
            {
                if (int.TryParse(txtHeartRate.Text, out int result))
                    patientRecord.PatientVitalSigns.HeartRate = result;
                else
                    patientRecord.PatientVitalSigns.HeartRate = 0;
            };

            Label lblRespiratoryRate = new Label { Text = "Respiratory Rate:", Location = new Point(50, 570), AutoSize = true };
            TextBox txtRespiratoryRate = new TextBox { Location = new Point(200, 570), Size = new Size(100, 20) };
            txtRespiratoryRate.TextChanged += (sender, e) =>
            {
                if (int.TryParse(txtRespiratoryRate.Text, out int result))
                    patientRecord.PatientVitalSigns.RespiratoryRate = result;
                else
                    patientRecord.PatientVitalSigns.RespiratoryRate = 0;
            };

            Label lblTemperature = new Label { Text = "Temperature:", Location = new Point(50, 600), AutoSize = true };
            TextBox txtTemperature = new TextBox { Location = new Point(200, 600), Size = new Size(100, 20) };
            txtTemperature.TextChanged += (sender, e) =>
            {
                if (float.TryParse(txtTemperature.Text, out float result))
                    patientRecord.PatientVitalSigns.Temperature = result;
                else
                    patientRecord.PatientVitalSigns.Temperature = 0;
            };

            Label lblHeight = new Label { Text = "Height:", Location = new Point(50, 630), AutoSize = true };
            TextBox txtHeight = new TextBox { Location = new Point(200, 630), Size = new Size(100, 20) };
            txtHeight.TextChanged += (sender, e) =>
            {
                if (float.TryParse(txtHeight.Text, out float result))
                    patientRecord.PatientVitalSigns.Height = result;
                else
                    patientRecord.PatientVitalSigns.Height = 0;
            };

            Label lblWeight = new Label { Text = "Weight:", Location = new Point(50, 660), AutoSize = true };
            TextBox txtWeight = new TextBox { Location = new Point(200, 660), Size = new Size(100, 20) };
            txtWeight.TextChanged += (sender, e) =>
            {
                if (float.TryParse(txtWeight.Text, out float result))
                    patientRecord.PatientVitalSigns.Weight = result;
                else
                    patientRecord.PatientVitalSigns.Weight = 0;
            };
            //                


            IconButton btnAddXRayTest = new IconButton
            {
                Name = "btnAddXRayTest",
                IconChar = IconChar.XRay,
                IconColor = Color.White,
                IconFont = IconFont.Auto,
                IconSize = 30,
                Size = new Size(150, 40),
                Location = new Point(50, 710),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnAddXRayTest.Click += (sender, e) => AddMedicalTest("X-Ray");

            IconButton btnAddCheckupTest = new IconButton
            {
                Name = "btnAddCheckupTest",
                IconChar = IconChar.VialCircleCheck,
                IconColor = Color.White,
                IconFont = IconFont.Auto,
                IconSize = 30,
                Size = new Size(150, 40),
                Location = new Point(220, 710),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnAddCheckupTest.Click += (sender, e) => AddMedicalTest("Checkup");

            Button btnSaveToDatabase = new Button
            {
                Name = "btnSaveToDatabase",
                Text = "Save to Database",
                Location = new Point(390, 710),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                Font = new Font("Open Sans", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnSaveToDatabase.Click += btnSaveToDatabase_Click;

            // Ensure that the layout is not affected by the addition of new controls
            int verticalSpacing = 10;

            // Adjust the Y position of existing controls
            btnSaveToDatabase.Location = new Point(btnSaveToDatabase.Location.X, btnSaveToDatabase.Location.Y + verticalSpacing);

            Controls.Add(lblPatientName);
            Controls.Add(txtPatientName);
            Controls.Add(lblPatientFile);
            Controls.Add(txtPatientFile);
            Controls.Add(lblGender);
            Controls.Add(cboGender);
            Controls.Add(lblPhoneNumber);
            Controls.Add(txtPhoneNumber);
            Controls.Add(lblReasonForAppointment);
            Controls.Add(txtReasonForAppointment);
            Controls.Add(lblChronicIllnesses);
            Controls.Add(txtChronicIllnesses);
            Controls.Add(lstChronicIllnesses);
            Controls.Add(btnAddChronicIllness);
            Controls.Add(lblAllergies);
            Controls.Add(txtAllergies);
            Controls.Add(lblMedicationsAndVaccines);
            Controls.Add(txtMedicationsAndVaccines);
            Controls.Add(chkTobaccoUse);
            Controls.Add(lblAlcoholConsumption);
            Controls.Add(txtAlcoholConsumption);
            Controls.Add(chkDrugUse);
            Controls.Add(lblBloodPressure);
            Controls.Add(txtBloodPressure);
            Controls.Add(lblHeartRate);
            Controls.Add(txtHeartRate);
            Controls.Add(lblRespiratoryRate);
            Controls.Add(txtRespiratoryRate);
            Controls.Add(lblTemperature);
            Controls.Add(txtTemperature);
            Controls.Add(lblHeight);
            Controls.Add(txtHeight);
            Controls.Add(lblWeight);
            Controls.Add(txtWeight);
            Controls.Add(btnAddXRayTest);
            Controls.Add(btnAddCheckupTest);
            Controls.Add(btnSaveToDatabase);
        }




        private void AddMedicalTest(string testType)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";
            openFileDialog.Title = $"Add {testType} Test";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Get the file path
                    string filePath = openFileDialog.FileName;

                    // Create a MedicalTest entity
                    MedicalTest medicalTest = new MedicalTest
                    {
                        TestName = testType,
                        TestDate = DateTime.Now,
                        FilePath = filePath,
                        PatientRecordId = patientRecord.PatientFile
                    };

                    // Save the MedicalTest entity to the database
                    using (var dbContext = new YourDbContext())
                    {
                        dbContext.MedicalTests.Add(medicalTest);
                        dbContext.SaveChanges();
                    }

                    MessageBox.Show($"Medical test '{testType}' added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while adding the medical test. Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSaveToDatabase_Click(object sender, EventArgs e)
        {
            using (var dbContext = new YourDbContext())
            {
                try
                {
                    // Check if a record with the same PatientFile already exists
                    if (!dbContext.PatientRecords.Any(p => p.PatientFile == patientRecord.PatientFile))
                    {
                        // Add the patientRecord to the context
                        dbContext.PatientRecords.Add(patientRecord);

                        // Check for duplicate PatientRecordId in MedicalTests
                        if (patientRecord.MedicalTests.GroupBy(mt => mt.PatientRecordId).Any(g => g.Count() > 1))
                        {
                            MessageBox.Show("Duplicate PatientRecordId found in MedicalTests. Each MedicalTest should have a unique PatientRecordId.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Check for existing PatientRecordId in MedicalTests
                        foreach (var medicalTest in patientRecord.MedicalTests)
                        {
                            if (!dbContext.MedicalTests.Any(mt => mt.PatientRecordId == medicalTest.PatientRecordId))
                            {
                                dbContext.MedicalTests.Add(medicalTest);
                            }
                            else
                            {
                                MessageBox.Show($"MedicalTest with PatientRecordId {medicalTest.PatientRecordId} already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                        // Save changes to the database
                        dbContext.SaveChanges();
                        MessageBox.Show("Patient record and associated medical tests saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Patient file number must be unique.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred. Please contact support. Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
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
            patientRecord = selectedPatientRecord;
            DbContext = new YourDbContext();
            initialChronicIllnesses = patientRecord.ChronicIllnesses.Select(ci => ci.ChronicIllnessName).ToList();
            initialMedicalTests = new List<MedicalTest>(patientRecord.MedicalTests);
            LoadPatientRecordData();
           ;
        }
        private void InitializeComponent()
        {
            AutoScaleMode = AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1050, 500);

            Label lblPatientName = new Label { Text = "Patient Name:", Location = new Point(50, 50), AutoSize = true, Font = new Font("Open Sans", 11, FontStyle.Regular) };
             txtPatientName = new TextBox { Location = new Point(280, 50), Size = new Size(200, 20), Font = new Font("Open Sans", 11, FontStyle.Regular) };
            txtPatientName.TextChanged += (sender, e) => patientRecord.PatientName = txtPatientName.Text;


            Label lblPatientFile = new Label { Text = "Patient File:", Location = new Point(50, 80), AutoSize = true, Font = new Font("Open Sans", 11, FontStyle.Regular) };
             txtPatientFile = new TextBox { Location = new Point(280, 80), Size = new Size(200, 35), Font = new Font("Open Sans", 11, FontStyle.Regular) };
            txtPatientFile.TextChanged += (sender, e) => patientRecord.PatientFile = txtPatientFile.Text;

            Label lblGender = new Label { Text = "Gender:", Location = new Point(50, 110), AutoSize = true, Font = new Font("Open Sans", 11, FontStyle.Regular) };
             cboGender = new ComboBox { Location = new Point(280, 110), Size = new Size(200, 20), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Open Sans", 11, FontStyle.Regular) };
            cboGender.Items.AddRange(new object[] { "Male", "Female", "Other" });
            cboGender.SelectedIndexChanged += (sender, e) => patientRecord.Gender = cboGender.SelectedItem?.ToString();

            Label lblPhoneNumber = new Label { Text = "Phone Number:", Location = new Point(50, 140), AutoSize = true, Font = new Font("Open Sans", 11, FontStyle.Regular) };
             txtPhoneNumber = new TextBox { Location = new Point(280, 140), Size = new Size(200, 20), Font = new Font("Open Sans", 11, FontStyle.Regular) };
            txtPhoneNumber.TextChanged += (sender, e) => patientRecord.PhoneNumber = txtPhoneNumber.Text;

            Label lblReasonForAppointment = new Label { Text = "Reason for Appointment:", Location = new Point(50, 170), AutoSize = true, Font = new Font("Open Sans", 11, FontStyle.Regular) };
             txtReasonForAppointment = new TextBox { Location = new Point(280, 170), Size = new Size(200, 20), Font = new Font("Open Sans", 11, FontStyle.Regular) };
            txtReasonForAppointment.TextChanged += (sender, e) => patientRecord.ReasonForAppointment = txtReasonForAppointment.Text;

            Label lblChronicIllnesses = new Label { Text = "Chronic Illnesses:", Location = new Point(50, 200), AutoSize = true, Font = new Font("Open Sans", 11, FontStyle.Regular) };
            TextBox txtChronicIllnesses = new TextBox { Location = new Point(280, 200), Size = new Size(200, 20), Font = new Font("Open Sans", 11, FontStyle.Regular) };

             lstChronicIllnesses = new ListBox { Location = new Point(280, 240), Size = new Size(200, 100) };
            Button btnAddChronicIllness = CreateIconButton("btnAddChronicIllness", IconChar.PlusCircle, Color.Black, Color.FromArgb(0, 123, 255), 40, new Size(45, 25), new Point(500, 200));


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
                    ChronicIllness chronicIllness = new ChronicIllness { ChronicIllnessName = selectedChronicIllness };
                    patientRecord.ChronicIllnesses.Add(chronicIllness);
                }
            };

            Label lblAllergies = new Label { Text = "Allergies:", Location = new Point(560, 50), AutoSize = true, Font = new Font("Open Sans", 11, FontStyle.Regular) };
             txtAllergies = new TextBox { Location = new Point(810, 50), Size = new Size(200, 20), Font = new Font("Open Sans", 11, FontStyle.Regular) };
            txtAllergies.TextChanged += (sender, e) => patientRecord.Allergies = txtAllergies.Text;

            Label lblMedicationsAndVaccines = new Label { Text = "Medications and Vaccines:", Location = new Point(560, 80), AutoSize = true, Font = new Font("Open Sans", 11, FontStyle.Regular) };
             txtMedicationsAndVaccines = new TextBox { Location = new Point(810, 80), Size = new Size(200, 20), Font = new Font("Open Sans", 11, FontStyle.Regular) };
            txtMedicationsAndVaccines.TextChanged += (sender, e) => patientRecord.MedicationsAndVaccines = txtMedicationsAndVaccines.Text;

             chkTobaccoUse = new CheckBox { Text = "Tobacco Use", Location = new Point(50, 270), Font = new Font("Open Sans", 11, FontStyle.Regular), AutoSize = true };
            chkTobaccoUse.CheckedChanged += (sender, e) => patientRecord.TobaccoUse = chkTobaccoUse.Checked;

            Label lblAlcoholConsumption = new Label { Text = "Alcohol Consumption (ml/week):", Location = new Point(560, 110), AutoSize = true, Font = new Font("Open Sans", 11, FontStyle.Regular) };
             txtAlcoholConsumption = new TextBox { Location = new Point(910, 110), Size = new Size(100, 20), Font = new Font("Open Sans", 11, FontStyle.Regular) };
            txtAlcoholConsumption.TextChanged += (sender, e) =>
            {
                if (double.TryParse(txtAlcoholConsumption.Text, out double result))
                    patientRecord.AlcoholConsumptionPerWeek = result;
                else
                    patientRecord.AlcoholConsumptionPerWeek = 0;
            };

             chkDrugUse = new CheckBox { Text = "Drug Use", Location = new Point(50, 300), Font = new Font("Open Sans", 11, FontStyle.Regular), AutoSize = true };
            chkDrugUse.CheckedChanged += (sender, e) => patientRecord.DrugUse = chkDrugUse.Checked;

            Label lblBloodPressure = new Label { Text = "Blood Pressure:", Location = new Point(560, 150), AutoSize = true, Font = new Font("Open Sans", 11, FontStyle.Regular) };
             txtBloodPressure = new TextBox { Location = new Point(810, 150), Size = new Size(100, 20), Font = new Font("Open Sans", 11, FontStyle.Regular) };
            txtBloodPressure.TextChanged += (sender, e) =>
            {
                if (int.TryParse(txtBloodPressure.Text, out int result))
                    patientRecord.PatientVitalSigns.BloodPressure = result;
                else
                    patientRecord.PatientVitalSigns.BloodPressure = 0;
            };

            Label lblHeartRate = new Label { Text = "Heart Rate:", Location = new Point(560, 180), AutoSize = true, Font = new Font("Open Sans", 11, FontStyle.Regular) };
             txtHeartRate = new TextBox { Location = new Point(810, 180), Size = new Size(100, 20), Font = new Font("Open Sans", 11, FontStyle.Regular) };
            txtHeartRate.TextChanged += (sender, e) =>
            {
                if (int.TryParse(txtHeartRate.Text, out int result))
                    patientRecord.PatientVitalSigns.HeartRate = result;
                else
                    patientRecord.PatientVitalSigns.HeartRate = 0;
            };

            Label lblRespiratoryRate = new Label { Text = "Respiratory Rate:", Location = new Point(560, 210), AutoSize = true, Font = new Font("Open Sans", 11, FontStyle.Regular) };
             txtRespiratoryRate = new TextBox { Location = new Point(810, 210), Size = new Size(100, 20), Font = new Font("Open Sans", 11, FontStyle.Regular) };
            txtRespiratoryRate.TextChanged += (sender, e) =>
            {
                if (int.TryParse(txtRespiratoryRate.Text, out int result))
                    patientRecord.PatientVitalSigns.RespiratoryRate = result;
                else
                    patientRecord.PatientVitalSigns.RespiratoryRate = 0;
            };

            Label lblTemperature = new Label { Text = "Temperature:", Location = new Point(560, 240), AutoSize = true, Font = new Font("Open Sans", 11, FontStyle.Regular) };
             txtTemperature = new TextBox { Location = new Point(810, 240), Size = new Size(100, 20), Font = new Font("Open Sans", 11, FontStyle.Regular) };
            txtTemperature.TextChanged += (sender, e) =>
            {
                if (float.TryParse(txtTemperature.Text, out float result))
                    patientRecord.PatientVitalSigns.Temperature = result;
                else
                    patientRecord.PatientVitalSigns.Temperature = 0;
            };

            Label lblHeight = new Label { Text = "Height:", Location = new Point(560, 270), AutoSize = true, Font = new Font("Open Sans", 11, FontStyle.Regular) };
             txtHeight = new TextBox { Location = new Point(810, 270), Size = new Size(100, 20), Font = new Font("Open Sans", 11, FontStyle.Regular) };
            txtHeight.TextChanged += (sender, e) =>
            {
                if (float.TryParse(txtHeight.Text, out float result))
                    patientRecord.PatientVitalSigns.Height = result;
                else
                    patientRecord.PatientVitalSigns.Height = 0;
            };

            Label lblWeight = new Label { Text = "Weight:", Location = new Point(560, 300), AutoSize = true, Font = new Font("Open Sans", 11, FontStyle.Regular) };
             txtWeight = new TextBox { Location = new Point(810, 300), Size = new Size(100, 20), Font = new Font("Open Sans", 11, FontStyle.Regular) };
            txtWeight.TextChanged += (sender, e) =>
            {
                if (float.TryParse(txtWeight.Text, out float result))
                    patientRecord.PatientVitalSigns.Weight = result;
                else
                    patientRecord.PatientVitalSigns.Weight = 0;
            };

            Button btnAddXRayTest = CreateIconButton("btnAddXRayTest", IconChar.XRay, Color.White, Color.FromArgb(33, 28, 28), 40, new Size(90, 40), new Point(280, 350));
            btnAddXRayTest.Click += (sender, e) => AddMedicalTest("X-Ray");

            Button btnAddCheckupTest = CreateIconButton("btnAddCheckupTest", IconChar.Stethoscope, Color.Black, Color.FromArgb(15, 153, 119), 40, new Size(90, 40), new Point(390, 350));
            btnAddCheckupTest.Click += (sender, e) => AddMedicalTest("Checkup");

            Button btnSaveToDatabase = new Button
            {
                Name = "btnSaveToDatabase",
                Text = "Add",
                Location = new Point(700, 430),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                Font = new Font("Open Sans", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnSaveToDatabase.Click += btnSaveToDatabase_Click;

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
                        this.Close();

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

        private void AddMedicalTest(string testType)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";
                openFileDialog.Title = $"Add {testType} Test";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Get the file path
                        string filePath = openFileDialog.FileName;

                        // Ensure that patientRecord is not null
                        if (patientRecord == null)
                        {
                            MessageBox.Show("Error: The patient record is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Check the state of the entity entry
                        var diagnosticEntry = DbContext.Entry(patientRecord);

                        if (diagnosticEntry.State == EntityState.Detached)
                        {
                            // If the entity is detached, attach it to the context
                            DbContext.Attach(patientRecord);
                        }

                        // Create a MedicalTest entity
                        MedicalTest medicalTest = new MedicalTest
                        {
                            TestName = testType,
                            TestDate = DateTime.Now,
                            FilePath = filePath,
                            DiagnosticId = patientRecord.Id, // Assuming Id is the primary key in Diagnostic
                            Diagnostic = patientRecord // Setting the navigation property
                        };

                        // Attach the MedicalTest entity to the context
                        DbContext.MedicalTest.Add(medicalTest);

                        // Update the button appearance after adding the PDF file
                        UpdateButtonAppearance(testType);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while adding the medical test. Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }






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

        private IconButton CreateIconButton(string name, IconChar icon, Color foreColor, Color backColor, int iconSize, Size size, Point location)
        {
            IconButton button = new IconButton();
            button.Name = name;
            button.IconChar = icon;
            button.IconColor = foreColor;
            button.IconSize = iconSize;
            button.Size = size;
            button.Location = location;
            button.BackColor = backColor;
            button.ForeColor = foreColor;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.TextAlign = ContentAlignment.MiddleCenter;
            return button;
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

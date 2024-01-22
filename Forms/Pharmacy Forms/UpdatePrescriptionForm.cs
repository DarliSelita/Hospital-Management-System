using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalManagementSystem.Database;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Forms
{
    public partial class UpdatePrescriptionForm : Form
    {
        private PatientPrc patientPrc;
        private PatientDetails patientDetails;
        private List<Diagnose> diagnoses;
        private List<Medication> medications;
        private List<PrescribedMedication> prescribedMedications;

        private TextBox txtFirstName;
        private TextBox txtLastName;
        private DateTimePicker dateOfBirthPicker;
        private TextBox txtPhoneNumber;
        private TextBox txtEmail;
        private TextBox txtAddress;
        private TextBox txtEmergencyContactName;
        private TextBox txtEmergencyContactNumber;

        private ComboBox cboDiagnoses;
        private ComboBox cboMedications;
        private NumericUpDown numericQuantity;
        private Button btnUpdatePrescription;

        private int prescriptionId; // This will store the prescriptionId that you want to update

        public UpdatePrescriptionForm(int prescriptionId)
        {
            InitializeComponent();
            this.prescriptionId = prescriptionId;
            prescribedMedications = new List<PrescribedMedication>();
            LoadDiagnoses();
            LoadMedications();
            InitializeUIComponents();
            LoadPrescriptionData();
            this.Size = new System.Drawing.Size(650, 500);

        }

        private void LoadDiagnoses()
        {
            using (var dbContext = new YourDbContext())
            {
                diagnoses = dbContext.Diagnoses.ToList();
            }
        }

        private void LoadMedications()
        {
            using (var dbContext = new YourDbContext())
            {
                medications = dbContext.Medications.ToList();
            }
        }

        private void LoadPrescriptionData()
        {
            using (var dbContext = new YourDbContext())
            {
                var existingPrescription = dbContext.Prescriptions
                    .Include(p => p.Patient)  // Include related data
                    .FirstOrDefault(p => p.PrescriptionId == prescriptionId);

                if (existingPrescription != null && existingPrescription.Patient != null)
                {
                    txtFirstName.Text = existingPrescription.Patient.FirstName;
                    txtLastName.Text = existingPrescription.Patient.LastName;
                    dateOfBirthPicker.Value = existingPrescription.Patient.DateOfBirth;
                    txtPhoneNumber.Text = existingPrescription.Patient.PhoneNumber;
                    txtEmail.Text = existingPrescription.Patient.Email;

                    if (existingPrescription.Patient.Details != null)
                    {
                        txtAddress.Text = existingPrescription.Patient.Details.Address;
                        txtEmergencyContactName.Text = existingPrescription.Patient.Details.EmergencyContactName;
                        txtEmergencyContactNumber.Text = existingPrescription.Patient.Details.EmergencyContactNumber;
                    }
                }
                else
                {
                    // Handle the case where the prescription or patient is not found
                    MessageBox.Show("Prescription not found or patient information is missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InitializeUIComponents()
        {
            txtFirstName = new TextBox();
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Location = new Point(150, 20);
            txtFirstName.Size = new Size(200, 20);

            txtLastName = new TextBox();
            txtLastName.Name = "txtLastName";
            txtLastName.Location = new Point(150, 50);
            txtLastName.Size = new Size(200, 20);

            dateOfBirthPicker = new DateTimePicker();
            dateOfBirthPicker.Name = "dateOfBirthPicker";
            dateOfBirthPicker.Location = new Point(150, 80);
            dateOfBirthPicker.Size = new Size(200, 20);

            txtPhoneNumber = new TextBox();
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Location = new Point(150, 110);
            txtPhoneNumber.Size = new Size(200, 20);

            txtEmail = new TextBox();
            txtEmail.Name = "txtEmail";
            txtEmail.Location = new Point(150, 140);
            txtEmail.Size = new Size(200, 20);

            txtAddress = new TextBox();
            txtAddress.Name = "txtAddress";
            txtAddress.Location = new Point(150, 170);
            txtAddress.Size = new Size(200, 20);

            txtEmergencyContactName = new TextBox();
            txtEmergencyContactName.Name = "txtEmergencyContactName";
            txtEmergencyContactName.Location = new Point(150, 200);
            txtEmergencyContactName.Size = new Size(200, 20);

            txtEmergencyContactNumber = new TextBox();
            txtEmergencyContactNumber.Name = "txtEmergencyContactNumber";
            txtEmergencyContactNumber.Location = new Point(150, 230);
            txtEmergencyContactNumber.Size = new Size(200, 20);

            cboDiagnoses = new ComboBox();
            cboDiagnoses.Name = "cboDiagnoses";
            cboDiagnoses.Location = new Point(150, 260);
            cboDiagnoses.Size = new Size(200, 20);
            cboDiagnoses.DataSource = diagnoses;
            cboDiagnoses.DisplayMember = "Name";
            cboDiagnoses.ValueMember = "DiagnoseId";

            cboMedications = new ComboBox();
            cboMedications.Name = "cboMedications";
            cboMedications.Location = new Point(150, 290);
            cboMedications.Size = new Size(200, 20);
            cboMedications.DataSource = medications;
            cboMedications.DisplayMember = "Name";
            cboMedications.ValueMember = "MedicationId";

            numericQuantity = new NumericUpDown();
            numericQuantity.Name = "numericQuantity";
            numericQuantity.Location = new Point(150, 320);
            numericQuantity.Size = new Size(50, 20);
            numericQuantity.Minimum = 1;

            btnUpdatePrescription = new Button();
            btnUpdatePrescription.Name = "btnUpdatePrescription";
            btnUpdatePrescription.Text = "Update Prescription";
            btnUpdatePrescription.Location = new Point(150, 360);
            btnUpdatePrescription.Click += btnUpdatePrescription_Click;
            btnUpdatePrescription.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            btnUpdatePrescription.ForeColor = System.Drawing.Color.White;
            btnUpdatePrescription.Font = new System.Drawing.Font("Open Sans", 12, System.Drawing.FontStyle.Bold);
            btnUpdatePrescription.FlatStyle = FlatStyle.Flat;
            btnUpdatePrescription.FlatAppearance.BorderSize = 0;

            Controls.Add(txtFirstName);
            Controls.Add(txtLastName);
            Controls.Add(dateOfBirthPicker);
            Controls.Add(txtPhoneNumber);
            Controls.Add(txtEmail);
            Controls.Add(txtAddress);
            Controls.Add(txtEmergencyContactName);
            Controls.Add(txtEmergencyContactNumber);
            Controls.Add(cboDiagnoses);
            Controls.Add(cboMedications);
            Controls.Add(numericQuantity);
            Controls.Add(btnUpdatePrescription);

            Controls.Add(new Label { Text = "First Name:", Location = new Point(50, 20) });
            Controls.Add(new Label { Text = "Last Name:", Location = new Point(50, 50) });
            Controls.Add(new Label { Text = "Date of Birth:", Location = new Point(50, 80) });
            Controls.Add(new Label { Text = "Phone Number:", Location = new Point(50, 110) });
            Controls.Add(new Label { Text = "Email:", Location = new Point(50, 140) });
            Controls.Add(new Label { Text = "Address:", Location = new Point(50, 170) });
            Controls.Add(new Label { Text = "Emergency Contact Name:", Location = new Point(50, 200) });
            Controls.Add(new Label { Text = "Emergency Contact Number:", Location = new Point(50, 230) });
            Controls.Add(new Label { Text = "Diagnoses:", Location = new Point(50, 260) });
            Controls.Add(new Label { Text = "Medications:", Location = new Point(50, 290) });
            Controls.Add(new Label { Text = "Quantity:", Location = new Point(50, 320) });
        }

        private void btnUpdatePrescription_Click(object sender, EventArgs e)
        {
            using (var dbContext = new YourDbContext())
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        patientPrc.FirstName = txtFirstName.Text;
                        patientPrc.LastName = txtLastName.Text;
                        patientPrc.DateOfBirth = dateOfBirthPicker.Value;
                        patientPrc.PhoneNumber = txtPhoneNumber.Text;
                        patientPrc.Email = txtEmail.Text;
                        patientPrc.Details.Address = txtAddress.Text;
                        patientPrc.Details.EmergencyContactName = txtEmergencyContactName.Text;
                        patientPrc.Details.EmergencyContactNumber = txtEmergencyContactNumber.Text;

                        var existingPrescription = dbContext.Prescriptions
                            .FirstOrDefault(p => p.PrescriptionId == prescriptionId);

                        if (existingPrescription != null)
                        {
                            existingPrescription.DiagnoseId = ((Diagnose)cboDiagnoses.SelectedItem).DiagnoseId;
                            existingPrescription.StartDate = DateTime.Now;
                            existingPrescription.EndDate = DateTime.Now.AddDays(7);
                            existingPrescription.PatientId = patientPrc.PatientId;

                            var selectedMedication = (Medication)cboMedications.SelectedItem;
                            var quantity = (int)numericQuantity.Value;

                            var medication = dbContext.Medications.Find(selectedMedication.MedicationId);

                            if (medication != null)
                            {
                                var existingPrescribedMedication = dbContext.PrescribedMedications
                                    .FirstOrDefault(pm => pm.PrescriptionId == prescriptionId);

                                if (existingPrescribedMedication != null)
                                {
                                    existingPrescribedMedication.MedicationId = medication.MedicationId;
                                    existingPrescribedMedication.Quantity = quantity;

                                    var inventoryItem = dbContext.MedicalInventories
                                        .FirstOrDefault(item => item.MedicationId == medication.MedicationId);

                                    if (inventoryItem != null)
                                    {
                                        inventoryItem.QuantityOnHand -= quantity;
                                    }
                                }
                            }
                        }

                        dbContext.SaveChanges();
                        transaction.Commit();
                        MessageBox.Show("Prescription updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Error updating prescription: {ex.Message}\n\nStackTrace: {ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void UpdatePrescriptionForm_Load(object sender, EventArgs e)
        {
            // Additional initialization logic, if needed
        }

        private void InitializeComponent()
        {
            // Specific initialization logic, if needed
        }
    }
}

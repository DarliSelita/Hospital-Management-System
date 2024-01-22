using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using HospitalManagementSystem.Database;

namespace HospitalManagementSystem.Forms
{
    public partial class AddPrescriptionForm : Form
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
        private Button btnSavePrescription;

        public AddPrescriptionForm()
        {
            InitializeComponent();
            prescribedMedications = new List<PrescribedMedication>();
            LoadDiagnoses();
            LoadMedications();
            InitializeUIComponents();
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

        private void InitializeUIComponents()
        {


            // Initialize textboxes for Patient information
            txtFirstName = new TextBox();
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Location = new Point(150, 20);
            txtFirstName.Size = new Size(200, 20);
            txtFirstName.Font = new Font("Open Sans", 11);

            txtLastName = new TextBox();
            txtLastName.Name = "txtLastName";
            txtLastName.Location = new Point(150, 50);
            txtLastName.Size = new Size(200, 20);
            txtLastName.Font = new Font("Open Sans", 11);

            dateOfBirthPicker = new DateTimePicker();
            dateOfBirthPicker.Name = "dateOfBirthPicker";
            dateOfBirthPicker.Location = new Point(150, 80);
            dateOfBirthPicker.Size = new Size(200, 20);
            dateOfBirthPicker.Font = new Font("Open Sans", 11);

            txtPhoneNumber = new TextBox();
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Location = new Point(150, 110);
            txtPhoneNumber.Size = new Size(200, 20);
            txtPhoneNumber.Font = new Font("Open Sans", 11);

            txtEmail = new TextBox();
            txtEmail.Name = "txtEmail";
            txtEmail.Location = new Point(150, 140);
            txtEmail.Size = new Size(200, 20);
            txtEmail.Font = new Font("Open Sans", 11);

            // Initialize textboxes for PatientDetails information
            txtAddress = new TextBox();
            txtAddress.Name = "txtAddress";
            txtAddress.Location = new Point(150, 170);
            txtAddress.Size = new Size(200, 20);
            txtAddress.Font = new Font("Open Sans", 14);

            txtEmergencyContactName = new TextBox();
            txtEmergencyContactName.Name = "txtEmergencyContactName";
            txtEmergencyContactName.Location = new Point(150, 200);
            txtEmergencyContactName.Size = new Size(200, 20);
            txtEmergencyContactName.Font = new Font("Open Sans", 11);

            txtEmergencyContactNumber = new TextBox();
            txtEmergencyContactNumber.Name = "txtEmergencyContactNumber";
            txtEmergencyContactNumber.Location = new Point(150, 230);
            txtEmergencyContactNumber.Size = new Size(200, 20);
            txtEmergencyContactNumber.Font = new Font("Open Sans", 11);
            // Initialize ComboBox for Diagnoses
            cboDiagnoses = new ComboBox();
            cboDiagnoses.Name = "cboDiagnoses";
            cboDiagnoses.Location = new Point(150, 260);
            cboDiagnoses.Size = new Size(200, 20);
            cboDiagnoses.DataSource = diagnoses;
            cboDiagnoses.DisplayMember = "Name";
            cboDiagnoses.ValueMember = "DiagnoseId";

          

            // Initialize ComboBox for Medications
            cboMedications = new ComboBox();
            cboMedications.Name = "cboMedications";
            cboMedications.Location = new Point(150, 290);
            cboMedications.Size = new Size(200, 20);
            cboMedications.DataSource = medications;
            cboMedications.DisplayMember = "Name";
            cboMedications.ValueMember = "MedicationId";

            // Initialize numeric up-down for medication quantity
            numericQuantity = new NumericUpDown();
            numericQuantity.Name = "numericQuantity";
            numericQuantity.Location = new Point(150, 320);
            numericQuantity.Size = new Size(50, 20);
            numericQuantity.Minimum = 1;


            // Initialize button to save prescription
            btnSavePrescription = new Button();
            btnSavePrescription.Name = "btnSavePrescription";
            btnSavePrescription.Text = "Save Prescription";
            btnSavePrescription.Location = new Point(150, 360);
            btnSavePrescription.Click += btnSavePrescription_Click;
            btnSavePrescription.Size = new System.Drawing.Size(150, 40); // Set the appropriate size
            btnSavePrescription.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            btnSavePrescription.ForeColor = System.Drawing.Color.White;
            btnSavePrescription.Font = new System.Drawing.Font("Open Sans", 12, System.Drawing.FontStyle.Bold);
            btnSavePrescription.FlatStyle = FlatStyle.Flat;
            btnSavePrescription.FlatAppearance.BorderSize = 0;
            
            // Add controls to the form
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
            Controls.Add(btnSavePrescription);

            // Add labels
            Controls.Add(new Label { Text = "First Name:", Location = new Point(50, 20) });
            Controls.Add(new Label { Text = "Last Name:", Location = new Point(50, 50) });
            Controls.Add(new Label { Text = "Date of Birth:", Location = new Point(50, 80) });
            Controls.Add(new Label { Text = "Phone Number:", Location = new Point(50, 110) });
            Controls.Add(new Label { Text = "Email:", Location = new Point(50, 140) });
            Controls.Add(new Label { Text = "Address:", Location = new Point(50, 170) });
            Controls.Add(new Label { Text = "EC Name:", Location = new Point(50, 200) });
            Controls.Add(new Label { Text = "EC Number:", Location = new Point(50, 230) });
            Controls.Add(new Label { Text = "Diagnoses:", Location = new Point(50, 260) });
            Controls.Add(new Label { Text = "Medications:", Location = new Point(50, 290) });
            Controls.Add(new Label { Text = "Quantity:", Location = new Point(50, 320) });
             
        
        
        
        }
        private void btnSavePrescription_Click(object sender, EventArgs e)
        {
            using (var dbContext = new YourDbContext())
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        // Add patient and patient details
                        patientPrc = new PatientPrc
                        {
                            FirstName = txtFirstName.Text,
                            LastName = txtLastName.Text,
                            DateOfBirth = dateOfBirthPicker.Value,
                            PhoneNumber = txtPhoneNumber.Text,
                            Email = txtEmail.Text,
                            Details = new PatientDetails
                            {
                                Address = txtAddress.Text,
                                EmergencyContactName = txtEmergencyContactName.Text,
                                EmergencyContactNumber = txtEmergencyContactNumber.Text
                            }
                        };

                        dbContext.PatientPrcs.Add(patientPrc);

                        // Save changes to get PatientId
                        dbContext.SaveChanges();

                        // Get the selected diagnosis and medication
                        var selectedDiagnose = (Diagnose)cboDiagnoses.SelectedItem;
                        var selectedMedication = (Medication)cboMedications.SelectedItem;

                        if (selectedDiagnose != null && selectedMedication != null)
                        {
                            var quantity = (int)numericQuantity.Value;

                            // Create a prescription for the selected diagnosis
                            var prescription = new Prescription
                            {
                                DiagnoseId = selectedDiagnose.DiagnoseId,
                                StartDate = DateTime.Now,
                                EndDate = DateTime.Now.AddDays(7),
                                PatientId = patientPrc.PatientId
                            };

                            dbContext.Prescriptions.Add(prescription);

                            // Save changes to get PrescriptionId
                            dbContext.SaveChanges();

                            // Add prescribed medication to the prescription
                            var medication = dbContext.Medications.Find(selectedMedication.MedicationId);

                            if (medication != null)
                            {
                                var newPrescribedMedication = new PrescribedMedication
                                {
                                    PrescriptionId = prescription.PrescriptionId,
                                    MedicationId = medication.MedicationId,
                                    Quantity = quantity
                                };

                                dbContext.PrescribedMedications.Add(newPrescribedMedication);

                                // Subtract prescribed quantity from inventory
                                var inventoryItem = dbContext.MedicalInventories
                                    .FirstOrDefault(item => item.MedicationId == medication.MedicationId);

                                if (inventoryItem != null)
                                {
                                    inventoryItem.QuantityOnHand -= quantity;
                                }

                            }
                        }

                        // Save all changes in a single transaction
                        dbContext.SaveChanges();

                        transaction.Commit();
                        MessageBox.Show("Prescription saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        // Log the complete exception details, including inner exception
                        transaction.Rollback();
                        MessageBox.Show($"Error saving prescription: {ex.Message}\n\nStackTrace: {ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        



        private void AddPrescriptionForm_Load(object sender, EventArgs e)
        {

        }

        private void InitializeComponent()
        {

        }
    }
}


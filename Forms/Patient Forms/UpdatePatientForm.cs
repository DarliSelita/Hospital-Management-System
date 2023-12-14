using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalManagementSystem.Database;

namespace HospitalManagementSystem.Forms.PatientForms
{
    public partial class UpdatePatientForm : Form
    {
        private Patient patientToUpdate; // Assuming you have a Patient class
        private TextBox txtPatientName;
        private TextBox txtSurname;
        private DateTimePicker dateOfBirthPicker;
        private TextBox txtFileNumber;
        private TextBox txtPhoneNumber;
        private TextBox txtEmail;
        private TextBox txtAssignedDoctor;
        private Button btnUpdatePatient;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;

        public UpdatePatientForm(Patient patient)
        {
            InitializeComponent();
            patientToUpdate = patient;
            InitializeUIComponents();
            LoadPatientData(); // Load existing patient data into the form
        }

        public UpdatePatientForm()
        {
        }

        private void InitializeUIComponents()
        {
            // Initialize textboxes
            txtPatientName = new TextBox();
            txtPatientName.Name = "txtPatientName";
            txtPatientName.Location = new System.Drawing.Point(150, 50);
            txtPatientName.Size = new System.Drawing.Size(200, 20);

            txtSurname = new TextBox();
            txtSurname.Name = "txtSurname";
            txtSurname.Location = new System.Drawing.Point(150, 80);
            txtSurname.Size = new System.Drawing.Size(200, 20);

            dateOfBirthPicker = new DateTimePicker();
            dateOfBirthPicker.Name = "dateOfBirthPicker";
            dateOfBirthPicker.Location = new System.Drawing.Point(150, 110);
            dateOfBirthPicker.Size = new System.Drawing.Size(200, 20);

            txtFileNumber = new TextBox();
            txtFileNumber.Name = "txtFileNumber";
            txtFileNumber.Location = new System.Drawing.Point(150, 140);
            txtFileNumber.Size = new System.Drawing.Size(200, 20);

            txtPhoneNumber = new TextBox();
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Location = new System.Drawing.Point(150, 170);
            txtPhoneNumber.Size = new System.Drawing.Size(200, 20);

            txtEmail = new TextBox();
            txtEmail.Name = "txtEmail";
            txtEmail.Location = new System.Drawing.Point(150, 200);
            txtEmail.Size = new System.Drawing.Size(200, 20);

            txtAssignedDoctor = new TextBox();
            txtAssignedDoctor.Name = "txtAssignedDoctor";
            txtAssignedDoctor.Location = new System.Drawing.Point(150, 230);
            txtAssignedDoctor.Size = new System.Drawing.Size(200, 20);

            pictureBox1 = new PictureBox();
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Location = new System.Drawing.Point(500, 320); // Adjust the appropriate location
            pictureBox1.Size = new System.Drawing.Size(150, 75); // Adjust the appropriate size
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Image.FromFile(Path.Combine("C:\\Users\\user\\Desktop\\Project\\Resources", "hospital logo.png"));
            // ... (additional properties for pictureBox1)

            // Initialize pictureBox2 (patient logo) to the right of input fields
            pictureBox2 = new PictureBox();
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Location = new System.Drawing.Point(440, 80); // Adjust the appropriate location
            pictureBox2.Size = new System.Drawing.Size(150, 130); // Adjust the appropriate size
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Image = Image.FromFile(Path.Combine("C:\\Users\\user\\Desktop\\Project\\Resources", "patient logo.jpg"));


            // Initialize button for updating patient information
            btnUpdatePatient = new Button();
            btnUpdatePatient.Name = "btnUpdatePatient";
            btnUpdatePatient.Text = "Update Patient";
            btnUpdatePatient.Location = new System.Drawing.Point(150, 300);
            btnUpdatePatient.Click += btnUpdatePatient_Click;
            btnUpdatePatient.Size = new System.Drawing.Size(150, 40);
            btnUpdatePatient.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            btnUpdatePatient.ForeColor = System.Drawing.Color.White;
            btnUpdatePatient.Font = new System.Drawing.Font("Open Sans", 12, System.Drawing.FontStyle.Bold);
            btnUpdatePatient.FlatStyle = FlatStyle.Flat;
            btnUpdatePatient.FlatAppearance.BorderSize = 0;

            // Add these controls to the form's Controls collection
            Controls.Add(txtPatientName);
            Controls.Add(txtSurname);
            Controls.Add(dateOfBirthPicker);
            Controls.Add(txtFileNumber);
            Controls.Add(txtPhoneNumber);
            Controls.Add(txtEmail);
            Controls.Add(txtAssignedDoctor);
            Controls.Add(btnUpdatePatient);

            // Add labels
            Controls.Add(new Label { Text = "Name:", Location = new System.Drawing.Point(50, 50) });
            Controls.Add(new Label { Text = "Surname:", Location = new System.Drawing.Point(50, 80) });
            Controls.Add(new Label { Text = "Date of Birth:", Location = new System.Drawing.Point(50, 110) });
            Controls.Add(new Label { Text = "Patient File Number:", Location = new System.Drawing.Point(50, 140) });
            Controls.Add(new Label { Text = "Phone Number:", Location = new System.Drawing.Point(50, 170) });
            Controls.Add(new Label { Text = "Email:", Location = new System.Drawing.Point(50, 200) });
            Controls.Add(new Label { Text = "Assigned Doctor:", Location = new System.Drawing.Point(50, 230) });
        }

        private void LoadPatientData()
        {
            // Load existing patient data into the form for updating
            txtPatientName.Text = patientToUpdate.Name;
            txtSurname.Text = patientToUpdate.Surname;
            dateOfBirthPicker.Value = patientToUpdate.DateOfBirth;
            txtFileNumber.Text = patientToUpdate.FileNumber;
            txtPhoneNumber.Text = patientToUpdate.PhoneNumber;
            txtEmail.Text = patientToUpdate.Email;
            txtAssignedDoctor.Text = patientToUpdate.AssignedDoctor;
        }

        private void btnUpdatePatient_Click(object sender, EventArgs e)
        {
            // Perform input validation
            if (string.IsNullOrWhiteSpace(txtPatientName.Text) || string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Additional validation for email format
            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Additional validation for date of birth
            if (dateOfBirthPicker.Value > DateTime.Now)
            {
                MessageBox.Show("Please enter a valid date of birth.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Update the patient data
            patientToUpdate.Name = txtPatientName.Text;
            patientToUpdate.Surname = txtSurname.Text;
            patientToUpdate.DateOfBirth = dateOfBirthPicker.Value;
            patientToUpdate.FileNumber = txtFileNumber.Text;
            patientToUpdate.PhoneNumber = txtPhoneNumber.Text;
            patientToUpdate.Email = txtEmail.Text;
            patientToUpdate.AssignedDoctor = txtAssignedDoctor.Text;

            // Save the updated patient to the database
            using (var dbContext = new YourDbContext()) // Replace with the actual name of your database context
            {
                try
                {
                    dbContext.Update(patientToUpdate);
                    dbContext.SaveChanges();

                    MessageBox.Show("Patient information updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Optionally, you can close the form after updating the patient
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating patient: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Additional method for email validation
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}

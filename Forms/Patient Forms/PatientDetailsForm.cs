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

namespace Project.Forms.Patient_Forms
{
    public abstract partial class PatientDetailsForm : Form
    {
        protected TextBox txtPatientName;
        protected TextBox txtSurname;
        protected DateTimePicker dateOfBirthPicker;
        protected TextBox txtFileNumber;
        protected TextBox txtPhoneNumber;
        protected TextBox txtEmail;
        protected TextBox txtAssignedDoctor;
        protected PictureBox pictureBox1;
        protected PictureBox pictureBox2;
        public PatientDetailsForm()
        {
            InitializeComponent();
            InitializeCommonUIComponents();
        }
        protected void InitializeCommonUIComponents()
        {
            // Common UI initialization logic for patient-related forms
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
            pictureBox1.Location = new System.Drawing.Point(500, 320);
            pictureBox1.Size = new System.Drawing.Size(150, 75);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Image.FromFile(Path.Combine("C:\\Users\\user\\Desktop\\Project\\Resources", "hospital logo.png"));

            pictureBox2 = new PictureBox();
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Location = new System.Drawing.Point(440, 80);
            pictureBox2.Size = new System.Drawing.Size(150, 130);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Image = Image.FromFile(Path.Combine("C:\\Users\\user\\Desktop\\Project\\Resources", "patient logo.jpg"));

            // Add these controls to the form's Controls collection
            Controls.Add(txtPatientName);
            Controls.Add(txtSurname);
            Controls.Add(dateOfBirthPicker);
            Controls.Add(txtFileNumber);
            Controls.Add(txtPhoneNumber);
            Controls.Add(txtEmail);
            Controls.Add(txtAssignedDoctor);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox2);

            // Add labels
            Controls.Add(new Label { Text = "Name:", Location = new System.Drawing.Point(50, 50) });
            Controls.Add(new Label { Text = "Surname:", Location = new System.Drawing.Point(50, 80) });
            Controls.Add(new Label { Text = "Date of Birth:", Location = new System.Drawing.Point(50, 110) });
            Controls.Add(new Label { Text = "Patient File Number:", Location = new System.Drawing.Point(50, 140) });
            Controls.Add(new Label { Text = "Phone Number:", Location = new System.Drawing.Point(50, 170) });
            Controls.Add(new Label { Text = "Email:", Location = new System.Drawing.Point(50, 200) });
            Controls.Add(new Label { Text = "Assigned Doctor:", Location = new System.Drawing.Point(50, 230) });
        }

        protected abstract void DisplayPatientDetails();
        protected abstract void UpdatePatient();

        private void PatientDetailsForm_Load(object sender, EventArgs e)
        {

        }
    }
}

using HospitalManagementSystem.Database;
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

namespace Project.Forms.Appointment_Forms
{
    public partial class ViewAppointmentForm : Form
    {
        protected TextBox txtPatientName;
        protected TextBox txtPatientFileNumber;
        protected DateTimePicker dateTimePicker;
        protected TextBox txtDoctor;
        protected ComboBox cmbAppointmentType;
        protected PictureBox pictureBox1;
        protected PictureBox pictureBox2;

        public ViewAppointmentForm(Appointment appointment)
        {
            InitializeComponent();
            InitializeCommonUIComponents();
            DisplayAppointmentDetails(appointment);
        }

        protected void InitializeCommonUIComponents()
        {
            // Common UI initialization logic for appointment-related forms
            txtPatientName = new TextBox();
            txtPatientName.Name = "txtPatientName";
            txtPatientName.Location = new System.Drawing.Point(150, 50);
            txtPatientName.Size = new System.Drawing.Size(200, 20);
            txtPatientName.ReadOnly = true;

            txtPatientFileNumber = new TextBox();
            txtPatientFileNumber.Name = "txtPatientFileNumber";
            txtPatientFileNumber.Location = new System.Drawing.Point(150, 80);
            txtPatientFileNumber.Size = new System.Drawing.Size(200, 20);
            txtPatientFileNumber.ReadOnly = true;

            dateTimePicker = new DateTimePicker();
            dateTimePicker.Name = "dateTimePicker";
            dateTimePicker.Location = new System.Drawing.Point(150, 110);
            dateTimePicker.Size = new System.Drawing.Size(200, 20);
            dateTimePicker.Enabled = false;

            txtDoctor = new TextBox();
            txtDoctor.Name = "txtDoctor";
            txtDoctor.Location = new System.Drawing.Point(150, 140);
            txtDoctor.Size = new System.Drawing.Size(200, 20);
            txtDoctor.ReadOnly = true;

            cmbAppointmentType = new ComboBox();
            cmbAppointmentType.Name = "cmbAppointmentType";
            cmbAppointmentType.Location = new System.Drawing.Point(150, 170);
            cmbAppointmentType.Size = new System.Drawing.Size(200, 20);
            cmbAppointmentType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAppointmentType.Enabled = false;
            cmbAppointmentType.Items.AddRange(Enum.GetNames(typeof(AppointmentType)));

            // Initialize pictureBox1 (hospital logo) at the bottom right
            pictureBox1 = new PictureBox();
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Location = new System.Drawing.Point(500, 320);
            pictureBox1.Size = new System.Drawing.Size(150, 75);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Image.FromFile(Path.Combine("C:\\Users\\user\\Desktop\\Project\\Resources", "hospital logo.png"));

            // Initialize pictureBox2 (patient logo) to the right of input fields
            pictureBox2 = new PictureBox();
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Location = new System.Drawing.Point(440, 80);
            pictureBox2.Size = new System.Drawing.Size(150, 130);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Image = Image.FromFile(Path.Combine("C:\\Users\\user\\Desktop\\Project\\Resources", "patient logo.jpg"));

            // Add these controls to the form's Controls collection
            Controls.Add(txtPatientName);
            Controls.Add(txtPatientFileNumber);
            Controls.Add(dateTimePicker);
            Controls.Add(txtDoctor);
            Controls.Add(cmbAppointmentType);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox2);

            // Add labels
            Controls.Add(new Label { Text = "Patient Name:", Location = new System.Drawing.Point(50, 50) });
            Controls.Add(new Label { Text = "Patient File Number:", Location = new System.Drawing.Point(50, 80) });
            Controls.Add(new Label { Text = "Date and Time:", Location = new System.Drawing.Point(50, 110) });
            Controls.Add(new Label { Text = "Doctor:", Location = new System.Drawing.Point(50, 140) });
            Controls.Add(new Label { Text = "Appointment Type:", Location = new System.Drawing.Point(50, 170) });
        }

        protected void DisplayAppointmentDetails(Appointment appointment)
        {
            // Display appointment details in the form controls
            txtPatientName.Text = appointment.PatientName;
            txtPatientFileNumber.Text = appointment.PatientFileNumber;
            dateTimePicker.Value = appointment.ScheduleHour;
            txtDoctor.Text = appointment.DoctorName;
            cmbAppointmentType.SelectedItem = appointment.Type.ToString();
        }
    }
}
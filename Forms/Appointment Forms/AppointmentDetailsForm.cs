using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Project.Forms.Appointment_Forms
{
    public abstract partial class AppointmentDetailsForm : Form
    {
        protected TextBox txtPatientName;
        protected TextBox txtPatientFileNumber;
        protected DateTimePicker dateTimePicker;
        protected DateTimePicker timePicker; // Added timePicker
        protected TextBox txtDoctor;
        protected ComboBox cmbAppointmentType;
        protected PictureBox pictureBox1;
        protected PictureBox pictureBox2;

        public AppointmentDetailsForm()
        {
            InitializeComponent();
            InitializeCommonUIComponents();
        }
        protected void InitializeCommonUIComponents()
        {
            // Common UI initialization logic for appointment-related forms
            txtPatientName = new TextBox();
            txtPatientName.Name = "txtPatientName";
            txtPatientName.Location = new System.Drawing.Point(150, 50);
            txtPatientName.Size = new System.Drawing.Size(200, 20);

            txtPatientFileNumber = new TextBox();
            txtPatientFileNumber.Name = "txtPatientFileNumber";
            txtPatientFileNumber.Location = new System.Drawing.Point(150, 80);
            txtPatientFileNumber.Size = new System.Drawing.Size(200, 20);

            dateTimePicker = new DateTimePicker();
            dateTimePicker.Name = "dateTimePicker";
            dateTimePicker.Location = new System.Drawing.Point(150, 110);
            dateTimePicker.Size = new System.Drawing.Size(200, 20);

            timePicker = new DateTimePicker(); // Initialize timePicker
            timePicker.Name = "timePicker";
            timePicker.Format = DateTimePickerFormat.Time;
            timePicker.ShowUpDown = true;
            timePicker.Location = new System.Drawing.Point(150, 140);
            timePicker.Size = new System.Drawing.Size(200, 20);

            txtDoctor = new TextBox();
            txtDoctor.Name = "txtDoctor";
            txtDoctor.Location = new System.Drawing.Point(150, 170);
            txtDoctor.Size = new System.Drawing.Size(200, 20);

            cmbAppointmentType = new ComboBox();
            cmbAppointmentType.Name = "cmbAppointmentType";
            cmbAppointmentType.Location = new System.Drawing.Point(150, 200);
            cmbAppointmentType.Size = new System.Drawing.Size(200, 20);
            cmbAppointmentType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAppointmentType.Items.AddRange(Enum.GetNames(typeof(AppointmentType)));

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
            Controls.Add(txtPatientFileNumber);
            Controls.Add(dateTimePicker);
            Controls.Add(timePicker); // Add timePicker
            Controls.Add(txtDoctor);
            Controls.Add(cmbAppointmentType);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox2);

            Controls.Add(new Label { Text = "Patient Name:", Location = new System.Drawing.Point(50, 50) });
            Controls.Add(new Label { Text = "Patient File Number:", Location = new System.Drawing.Point(50, 80) });
            Controls.Add(new Label { Text = "Date:", Location = new System.Drawing.Point(50, 110) });
            Controls.Add(new Label { Text = "Time:", Location = new System.Drawing.Point(50, 140) });
            Controls.Add(new Label { Text = "Doctor:", Location = new System.Drawing.Point(50, 170) });
            Controls.Add(new Label { Text = "Appointment Type:", Location = new System.Drawing.Point(50, 200) });
        }

        protected abstract void DisplayAppointmentDetails();
        protected abstract void UpdateAppointment();

        private void AppointmentDetailsForm_Load(object sender, EventArgs e)
        {

        }
    }
}

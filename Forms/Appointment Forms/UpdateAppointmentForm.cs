using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HospitalManagementSystem.Database;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Project.Forms.Appointment_Forms
{
    public partial class UpdateAppointmentForm : Form
    {
        private Appointment appointmentToUpdate;
        private YourDbContext DbContext;
        private TextBox txtPatientName;
        private TextBox txtPatientFileNumber;
        private DateTimePicker dateTimePicker;
        private DateTimePicker timePicker;
        private TextBox txtDoctor;
        private ComboBox cmbAppointmentType;
        private Button btnUpdateAppointment;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;

        public UpdateAppointmentForm(Appointment appointment)
        {
            InitializeComponent();
            appointmentToUpdate = appointment;
            DbContext = new YourDbContext();
            InitializeUIComponents();
            LoadAppointmentData();
        }

        private void InitializeUIComponents()
        {
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

            timePicker = new DateTimePicker();
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

            btnUpdateAppointment = new Button();
            btnUpdateAppointment.Name = "btnUpdateAppointment";
            btnUpdateAppointment.Text = "Update Appointment";
            btnUpdateAppointment.Location = new System.Drawing.Point(150, 230);
            btnUpdateAppointment.Click += btnUpdateAppointment_Click;
            btnUpdateAppointment.Size = new System.Drawing.Size(150, 40);
            btnUpdateAppointment.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            btnUpdateAppointment.ForeColor = System.Drawing.Color.White;
            btnUpdateAppointment.Font = new System.Drawing.Font("Open Sans", 12, System.Drawing.FontStyle.Bold);
            btnUpdateAppointment.FlatStyle = FlatStyle.Flat;
            btnUpdateAppointment.FlatAppearance.BorderSize = 0;

            Controls.Add(txtPatientName);
            Controls.Add(txtPatientFileNumber);
            Controls.Add(dateTimePicker);
            Controls.Add(timePicker);
            Controls.Add(new Label { Text = "Time:", Location = new System.Drawing.Point(50, 140) });
            Controls.Add(txtDoctor);
            Controls.Add(cmbAppointmentType);
            Controls.Add(btnUpdateAppointment);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox2);

            Controls.Add(new Label { Text = "Patient Name:", Location = new System.Drawing.Point(50, 50) });
            Controls.Add(new Label { Text = "Patient File Number:", Location = new System.Drawing.Point(50, 80) });
            Controls.Add(new Label { Text = "Date:", Location = new System.Drawing.Point(50, 110) });
            Controls.Add(new Label { Text = "Doctor:", Location = new System.Drawing.Point(50, 170) });
            Controls.Add(new Label { Text = "Appointment Type:", Location = new System.Drawing.Point(50, 200) });
        }

        private void LoadAppointmentData()
        {
            txtPatientName.Text = appointmentToUpdate.PatientName;
            txtPatientFileNumber.Text = appointmentToUpdate.PatientFileNumber;
            dateTimePicker.Value = appointmentToUpdate.ScheduleHour.Date;
            timePicker.Value = appointmentToUpdate.ScheduleHour;
            txtDoctor.Text = appointmentToUpdate.DoctorName;
            cmbAppointmentType.SelectedItem = appointmentToUpdate.Type.ToString();
        }

        private void btnUpdateAppointment_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPatientName.Text) || string.IsNullOrWhiteSpace(txtPatientFileNumber.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            appointmentToUpdate.PatientName = txtPatientName.Text;
            appointmentToUpdate.PatientFileNumber = txtPatientFileNumber.Text;
            appointmentToUpdate.ScheduleHour = new DateTime(dateTimePicker.Value.Year, dateTimePicker.Value.Month, dateTimePicker.Value.Day,
                timePicker.Value.Hour, timePicker.Value.Minute, 0);
            appointmentToUpdate.DoctorName = txtDoctor.Text;

            if (cmbAppointmentType.SelectedItem != null)
            {
                if (Enum.TryParse(cmbAppointmentType.SelectedItem.ToString(), out AppointmentType appointmentType))
                {
                    appointmentToUpdate.Type = appointmentType;
                }
                else
                {
                    MessageBox.Show("Please select a valid appointment type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select an appointment type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                DbContext.SaveChanges();
                MessageBox.Show("Appointment information updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

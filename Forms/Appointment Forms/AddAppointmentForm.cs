using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using HospitalManagementSystem.Database;
using Microsoft.EntityFrameworkCore;

namespace Project.Forms.Appointment_Forms
{
    public partial class AddAppointmentForm : Form
    {
        private YourDbContext DbContext;
        private TextBox txtPatientName;
        private TextBox txtPatientFileNumber;
        private DateTimePicker dateTimePicker;
        private DateTimePicker timePicker;
        private Label lblTimePicker;
        private TextBox txtDoctor;
        private ComboBox cmbAppointmentType;
        private Label lblDoctor;
        private Label lblAppointmentType;
        private Button btnAddAppointment;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;

        public AddAppointmentForm()
        {
            InitializeComponent();
            DbContext = new YourDbContext();
            InitializeUIComponents();
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

            lblTimePicker = new Label();
            lblTimePicker.Text = "Time:";
            lblTimePicker.Location = new System.Drawing.Point(50, 140);

            txtDoctor = new TextBox();
            txtDoctor.Name = "txtDoctor";
            txtDoctor.Location = new System.Drawing.Point(150, 170);
            txtDoctor.Size = new System.Drawing.Size(200, 20);

            lblDoctor = new Label();
            lblDoctor.Text = "Doctor:";
            lblDoctor.Location = new System.Drawing.Point(50, 170);

            cmbAppointmentType = new ComboBox();
            cmbAppointmentType.Name = "cmbAppointmentType";
            cmbAppointmentType.Location = new System.Drawing.Point(150, 200);
            cmbAppointmentType.Size = new System.Drawing.Size(200, 20);
            cmbAppointmentType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAppointmentType.Items.AddRange(Enum.GetNames(typeof(AppointmentType)));

            lblAppointmentType = new Label();
            lblAppointmentType.Text = "Appointment Type:";
            lblAppointmentType.Location = new System.Drawing.Point(50, 200);

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

            btnAddAppointment = new Button();
            btnAddAppointment.Name = "btnAddAppointment";
            btnAddAppointment.Text = "Add Appointment";
            btnAddAppointment.Location = new System.Drawing.Point(150, 230);
            btnAddAppointment.Click += btnAddAppointment_Click;
            btnAddAppointment.Size = new System.Drawing.Size(150, 40);
            btnAddAppointment.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            btnAddAppointment.ForeColor = System.Drawing.Color.White;
            btnAddAppointment.Font = new System.Drawing.Font("Open Sans", 12, System.Drawing.FontStyle.Bold);
            btnAddAppointment.FlatStyle = FlatStyle.Flat;
            btnAddAppointment.FlatAppearance.BorderSize = 0;
            btnAddAppointment.TextAlign = ContentAlignment.MiddleCenter;

            Controls.Add(txtPatientName);
            Controls.Add(txtPatientFileNumber);
            Controls.Add(dateTimePicker);
            Controls.Add(timePicker);
            Controls.Add(lblTimePicker);
            Controls.Add(txtDoctor);
            Controls.Add(lblDoctor);
            Controls.Add(cmbAppointmentType);
            Controls.Add(lblAppointmentType);
            Controls.Add(btnAddAppointment);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox2);

            Controls.Add(new Label { Text = "Patient Name:", Location = new System.Drawing.Point(50, 50) });
            Controls.Add(new Label { Text = "Patient File Number:", Location = new System.Drawing.Point(50, 80) });
            Controls.Add(new Label { Text = "Date:", Location = new System.Drawing.Point(50, 110) });
            // Time label added in the loop
            Controls.Add(new Label { Text = "Doctor:", Location = new System.Drawing.Point(50, 170) });
            Controls.Add(new Label { Text = "Appointment Type:", Location = new System.Drawing.Point(50, 200) });
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPatientName.Text) || string.IsNullOrWhiteSpace(txtPatientFileNumber.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var appointment = new Appointment
            {
                PatientName = txtPatientName.Text,
                PatientFileNumber = txtPatientFileNumber.Text,
                ScheduleHour = dateTimePicker.Value.Date + timePicker.Value.TimeOfDay,
                DoctorName = txtDoctor.Text,
            };

            if (cmbAppointmentType.SelectedItem != null)
            {
                if (Enum.TryParse(cmbAppointmentType.SelectedItem.ToString(), out AppointmentType appointmentType))
                {
                    appointment.Type = appointmentType;
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
                DbContext.Appointments.Add(appointment);
                DbContext.SaveChanges();
                MessageBox.Show("Appointment added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (DbUpdateException dbEx)
            {
                MessageBox.Show("Error adding appointment. Please ensure the patient file number is unique.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred. Please contact support. Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddAppointmentForm_Load(object sender, EventArgs e)
        {
            // ... (existing code)
        }
    }
}

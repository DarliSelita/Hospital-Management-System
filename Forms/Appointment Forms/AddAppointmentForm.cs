using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using HospitalManagementSystem.Database;

namespace Project.Forms.Appointment_Forms
{
    public partial class AddAppointmentForm : Form
    {
        private YourDbContext DbContext; // Replace with your actual database context
        private TextBox txtPatientName;
        private TextBox txtPatientFileNumber;
        private DateTimePicker dateTimePicker;
        private TextBox txtDoctor;
        private ComboBox cmbAppointmentType;
        private Button btnAddAppointment;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;

        public AddAppointmentForm(YourDbContext dbContext)
        {
            InitializeComponent();
            this.DbContext = dbContext; // Save the database context
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

            txtDoctor = new TextBox();
            txtDoctor.Name = "txtDoctor";
            txtDoctor.Location = new System.Drawing.Point(150, 140);
            txtDoctor.Size = new System.Drawing.Size(200, 20);

            cmbAppointmentType = new ComboBox();
            cmbAppointmentType.Name = "cmbAppointmentType";
            cmbAppointmentType.Location = new System.Drawing.Point(150, 170);
            cmbAppointmentType.Size = new System.Drawing.Size(200, 20);
            cmbAppointmentType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAppointmentType.Items.AddRange(Enum.GetNames(typeof(AppointmentType)));

            // Initialize pictureBox1 (hospital logo) at the bottom right
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
            // ... (additional properties for pictureBox2)

            // Initialize button in the center and a bit below
            btnAddAppointment = new Button();
            btnAddAppointment.Name = "btnAddAppointment";
            btnAddAppointment.Text = "Add Appointment";
            btnAddAppointment.Location = new System.Drawing.Point(150, 210);
            btnAddAppointment.Click += btnAddAppointment_Click;
            btnAddAppointment.Size = new System.Drawing.Size(150, 40);
            btnAddAppointment.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            btnAddAppointment.ForeColor = System.Drawing.Color.White;
            btnAddAppointment.Font = new System.Drawing.Font("Open Sans", 12, System.Drawing.FontStyle.Bold);
            btnAddAppointment.FlatStyle = FlatStyle.Flat;
            btnAddAppointment.FlatAppearance.BorderSize = 0;
            btnAddAppointment.TextAlign = ContentAlignment.MiddleCenter; // Center the text

            // Add these controls to the form's Controls collection
            Controls.Add(txtPatientName);
            Controls.Add(txtPatientFileNumber);
            Controls.Add(dateTimePicker);
            Controls.Add(txtDoctor);
            Controls.Add(cmbAppointmentType);
            Controls.Add(btnAddAppointment);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox2);

            // Add labels
            Controls.Add(new Label { Text = "Patient Name:", Location = new System.Drawing.Point(50, 50) });
            Controls.Add(new Label { Text = "Patient File Number:", Location = new System.Drawing.Point(50, 80) });
            Controls.Add(new Label { Text = "Date and Time:", Location = new System.Drawing.Point(50, 110) });
            Controls.Add(new Label { Text = "Doctor:", Location = new System.Drawing.Point(50, 140) });
            Controls.Add(new Label { Text = "Appointment Type:", Location = new System.Drawing.Point(50, 170) });
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            // Create an Appointment object with the form data
            var appointment = new Appointment
            {
                PatientName = txtPatientName.Text,
                PatientFileNumber = txtPatientFileNumber.Text,
                ScheduleHour = dateTimePicker.Value,
                DoctorName = txtDoctor.Text,
                Type = (AppointmentType)cmbAppointmentType.SelectedItem
            };

            // Add the appointment to the database
            DbContext.Appointments.Add(appointment);

            try
            {
                // Save changes to the database
                DbContext.SaveChanges();

                // Display success message or close the form, etc.
                MessageBox.Show("Appointment added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                // Handle the exception (display an error message, log the error, etc.)
                MessageBox.Show($"Error adding appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

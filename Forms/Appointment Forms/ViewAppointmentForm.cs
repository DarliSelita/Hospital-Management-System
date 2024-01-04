using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using HospitalManagementSystem.Database;

namespace Project.Forms.Appointment_Forms
{
    public partial class ViewAppointmentForm : Form
    {
        private DataGridView dataGridView;
        private Button btnAddAppointment;
        private Button btnUpdateAppointment;
        private Button btnDeleteAppointment;

        public ViewAppointmentForm()
        {
            InitializeComponent();
            InitializeUIComponents();
            LoadAppointmentData();
        }

        private void InitializeUIComponents()
        {
            dataGridView = new DataGridView();
            dataGridView.Name = "dataGridView";
            dataGridView.Location = new Point(50, 50);
            dataGridView.Size = new Size(500, 200);
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;

            btnAddAppointment = CreateIconButton("btnAddAppointment", IconChar.Plus, Color.Black, Color.Green);
            btnAddAppointment.Location = new Point(50, 280);
            btnAddAppointment.Click += btnAddAppointment_Click;

            btnUpdateAppointment = CreateIconButton("btnUpdateAppointment", IconChar.Edit, Color.Black, Color.Yellow);
            btnUpdateAppointment.Location = new Point(180, 280);
            btnUpdateAppointment.Click += btnUpdateAppointment_Click;

            btnDeleteAppointment = CreateIconButton("btnDeleteAppointment", IconChar.TrashAlt, Color.Black, Color.Red);
            btnDeleteAppointment.Location = new Point(310, 280);
            btnDeleteAppointment.Click += btnDeleteAppointment_Click;

            SetAnchorStyles(dataGridView, AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            SetAnchorStyles(btnAddAppointment, AnchorStyles.Top | AnchorStyles.Left);
            SetAnchorStyles(btnUpdateAppointment, AnchorStyles.Top | AnchorStyles.Left);
            SetAnchorStyles(btnDeleteAppointment, AnchorStyles.Top | AnchorStyles.Right);

            Controls.Add(dataGridView);
            Controls.Add(btnAddAppointment);
            Controls.Add(btnUpdateAppointment);
            Controls.Add(btnDeleteAppointment);
        }

        private Button CreateIconButton(string name, IconChar icon, Color foreColor, Color backColor)
        {
            Button button = new Button();
            button.Name = name;
            button.Size = new Size(120, 40);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = backColor;
            button.ForeColor = foreColor;
            button.Image = icon.ToBitmap(foreColor, 20, 20);
            return button;
        }

        private void LoadAppointmentData()
        {
            using (var dbContext = new YourDbContext())
            {
                List<Appointment> appointments = dbContext.Appointments.ToList();
                DataTable dataTable = ConvertToDataTable(appointments);
                dataGridView.DataSource = dataTable;
            }
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            AddAppointmentForm addAppointmentForm = new AddAppointmentForm();
            addAppointmentForm.ShowDialog();
            LoadAppointmentData();
        }

        private void btnUpdateAppointment_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                int selectedAppointmentId = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["Id"].Value);
                using (var dbContext = new YourDbContext())
                {
                    Appointment selectedAppointment = dbContext.Appointments.Find(selectedAppointmentId);
                    UpdateAppointmentForm updateAppointmentForm = new UpdateAppointmentForm(selectedAppointment);
                    updateAppointmentForm.ShowDialog();
                    LoadAppointmentData();
                }
            }
            else
            {
                MessageBox.Show("Please select an appointment to update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDeleteAppointment_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                int selectedAppointmentId = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["Id"].Value);
                using (var dbContext = new YourDbContext())
                {
                    Appointment selectedAppointment = dbContext.Appointments.Find(selectedAppointmentId);
                    DeleteAppointmentForm deleteAppointmentForm = new DeleteAppointmentForm(selectedAppointment);
                    deleteAppointmentForm.ShowDialog();
                    LoadAppointmentData();
                }
            }
            else
            {
                MessageBox.Show("Please select an appointment to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private DataTable ConvertToDataTable(List<Appointment> appointments)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("PatientName", typeof(string));
            dataTable.Columns.Add("PatientFileNumber", typeof(string));
            dataTable.Columns.Add("ScheduleHour", typeof(DateTime));
            dataTable.Columns.Add("DoctorName", typeof(string));
            dataTable.Columns.Add("Type", typeof(string));

            foreach (var appointment in appointments)
            {
                dataTable.Rows.Add(
                    appointment.Id,
                    appointment.PatientName,
                    appointment.PatientFileNumber,
                    appointment.ScheduleHour,
                    appointment.DoctorName,
                    appointment.Type.ToString());
            }

            return dataTable;
        }

        private void SetAnchorStyles(Control control, AnchorStyles anchorStyles)
        {
            control.Anchor = anchorStyles;
        }

        private void ViewAppointmentForm_Load(object sender, EventArgs e)
        {

        }
    }
}

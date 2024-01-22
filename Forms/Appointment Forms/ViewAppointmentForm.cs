using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using HospitalManagementSystem.Database;
using Microsoft.EntityFrameworkCore;

namespace Project.Forms.Appointment_Forms
{
    public partial class ViewAppointmentForm : Form
    {
        private DataGridView dataGridView;
        private Button btnAddAppointment;
        private Button btnUpdateAppointment;
        private Button btnDeleteAppointment;
        private Label lblSearch;
        private TextBox txtSearch;


        public ViewAppointmentForm()
        {
            InitializeComponent();
            InitializeUIComponents();
            LoadAppointmentData();
            WindowState = FormWindowState.Maximized;

        }

        private void InitializeUIComponents()
        {

            BackColor = Color.FromArgb(28, 41, 34); // Adjusted form background color


            dataGridView = new DataGridView();
            dataGridView.Name = "dataGridView";
            dataGridView.Location = new Point(50, 50);
            dataGridView.Size = new Size(500, 200);
            dataGridView.Size = new Size(ClientSize.Width - 100, ClientSize.Height + 85);
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.RowTemplate.Height = 35;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            dataGridView.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.ColumnHeadersHeight = 50;
            dataGridView.DefaultCellStyle.Font = new Font("Segoe UI", 11);
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeColumns = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.GridColor = Color.FromArgb(28, 41, 34);
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(36, 156, 116);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(28, 41, 34);
            dataGridView.DefaultCellStyle.ForeColor = Color.White;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 92, 164);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView.RowsDefaultCellStyle.BackColor = Color.FromArgb(36, 156, 154);
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(36, 156, 116);
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            lblSearch = new Label();
            lblSearch.Text = "Search: ";
            lblSearch.Font = new Font("Segoe UI", 14);
            lblSearch.Location = new Point(790, 915);
            lblSearch.ForeColor = Color.FromArgb(5, 250, 99);
            lblSearch.AutoSize = true; // Set AutoSize to true

            txtSearch = new TextBox();
            txtSearch.Location = new Point(900, 915);
            txtSearch.Font = new Font("Segoe UI", 18);
            txtSearch.TextChanged += TxtSearch_TextChanged;
            txtSearch.Size = new Size(200, txtSearch.Height);
            txtSearch.TextChanged += TxtSearch_TextChanged;


            btnAddAppointment = CreateIconButton("btnAddAppointment", IconChar.Plus, Color.Black, Color.Green);
            btnAddAppointment.Location = new Point(100, 915);
            btnAddAppointment.Click += btnAddAppointment_Click;

            btnUpdateAppointment = CreateIconButton("btnUpdateAppointment", IconChar.Edit, Color.Black, Color.Yellow);
            btnUpdateAppointment.Location = new Point(230, 915);
            btnUpdateAppointment.Click += btnUpdateAppointment_Click;

            btnDeleteAppointment = CreateIconButton("btnDeleteAppointment", IconChar.TrashAlt, Color.Black, Color.Red);
            btnDeleteAppointment.Location = new Point(360, 915);
            btnDeleteAppointment.Click += btnDeleteAppointment_Click;

            SetAnchorStyles(dataGridView, AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            

            Controls.Add(dataGridView);
            Controls.Add(btnAddAppointment);
            Controls.Add(btnUpdateAppointment);
            Controls.Add(btnDeleteAppointment);
            Controls.Add(lblSearch);
            Controls.Add(txtSearch);
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            LiveSearch();
        }

        private void LiveSearch()
        {
            using (var dbContext = new YourDbContext())
            {
                string searchText = txtSearch.Text.Trim();

                var searchResult = dbContext.Appointments
                    .Where(a => a.PatientFileNumber.Contains(searchText))
                    .ToList();

                DataTable dataTable = ConvertToDataTable(searchResult);
                dataGridView.DataSource = dataTable;
            }
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

                int totalHeight = dataGridView.ColumnHeadersHeight +
                                  (dataGridView.Rows.Count * dataGridView.RowTemplate.Height);

                dataGridView.Height = totalHeight;

                this.Height = totalHeight + 200;  
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

      

        private void SetAnchorStyles(Control control, AnchorStyles anchorStyles)
        {
            control.Anchor = anchorStyles;
        }

    }
}

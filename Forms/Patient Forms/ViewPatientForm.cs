using HospitalManagementSystem.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using HospitalManagementSystem.Forms.PatientForms;

namespace Project.Forms.Patient_Forms
{
    public partial class ViewPatientForm : Form
    {
        private DataGridView dataGridView;
        private Button btnAddPatient;
        private Button btnUpdatePatient;
        private Button btnDeletePatient;
        private Label lblSearch;
        private TextBox txtSearch;

        public ViewPatientForm()
        {
            InitializeComponent();
            InitializeUIComponents();
            LoadPatientData();
            WindowState = FormWindowState.Maximized; // Set to maximize the form
        }

        private void InitializeUIComponents()
        {
            BackColor = Color.FromArgb(28, 41, 34); // Adjusted form background color


            dataGridView = new DataGridView();
            dataGridView.Name = "dataGridView";
            dataGridView.Location = new Point(50, 50);
            dataGridView.Size = new Size(ClientSize.Width - 100, ClientSize.Height - 200);
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
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView.RowsDefaultCellStyle.BackColor = Color.FromArgb(36, 156, 154);
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(36, 156, 116);
            dataGridView.BackgroundColor = Color.FromArgb(28, 41, 34);
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            btnAddPatient = CreateIconButton("btnAddPatient", FontAwesome.Sharp.IconChar.Plus, Color.Black, Color.Green);
            btnAddPatient.Location = new Point(50, dataGridView.Bottom + 20);
            btnAddPatient.Click += btnAddPatient_Click;

            btnUpdatePatient = CreateIconButton("btnUpdatePatient", FontAwesome.Sharp.IconChar.Edit, Color.Black, Color.Yellow);
            btnUpdatePatient.Location = new Point(btnAddPatient.Right + 10, dataGridView.Bottom + 20);
            btnUpdatePatient.Click += btnUpdatePatient_Click;

            btnDeletePatient = CreateIconButton("btnDeletePatient", FontAwesome.Sharp.IconChar.TrashAlt, Color.Black, Color.Red);
            btnDeletePatient.Location = new Point(btnUpdatePatient.Right + 10, dataGridView.Bottom + 20);
            btnDeletePatient.Click += btnDeletePatient_Click;

            lblSearch = new Label();
            lblSearch.Text = "Search: ";
            lblSearch.Font = new Font("Segoe UI", 14);
            lblSearch.Location = new Point(btnDeletePatient.Right + 170, 910);
            lblSearch.ForeColor = Color.FromArgb(5, 250, 99);
            lblSearch.AutoSize = true; // Set AutoSize to true

            txtSearch = CreateTextBox();
            txtSearch.Location = new Point(btnDeletePatient.Right + 280, 915);
            txtSearch.Font = new Font("Segoe UI", 18);
            txtSearch.TextChanged += TxtSearch_TextChanged;

            SetAnchorStyles(dataGridView, AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom);
            SetAnchorStyles(btnAddPatient, AnchorStyles.Bottom | AnchorStyles.Left);
            SetAnchorStyles(btnUpdatePatient, AnchorStyles.Bottom | AnchorStyles.Left);
            SetAnchorStyles(btnDeletePatient, AnchorStyles.Bottom | AnchorStyles.Left);
            SetAnchorStyles(lblSearch, AnchorStyles.Top | AnchorStyles.Left);
            SetAnchorStyles(txtSearch, AnchorStyles.Top | AnchorStyles.Left);

            Controls.Add(dataGridView);
            Controls.Add(btnAddPatient);
            Controls.Add(btnUpdatePatient);
            Controls.Add(btnDeletePatient);
            Controls.Add(lblSearch);
            Controls.Add(txtSearch);
        }

        private TextBox CreateTextBox()
        {
            TextBox textBox = new TextBox();
            textBox.Size = new Size(200, 30);
            return textBox;
        }

        private Button CreateIconButton(string name, FontAwesome.Sharp.IconChar icon, Color foreColor, Color backColor)
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

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            LiveSearch();
        }

        private void LiveSearch()
        {
            using (var dbContext = new YourDbContext())
            {
                var searchResult = dbContext.Patients
                    .Where(p => p.Name.Contains(txtSearch.Text) || p.Surname.Contains(txtSearch.Text))
                    .ToList();

                DataTable dataTable = ConvertToDataTable(searchResult);
                dataGridView.DataSource = dataTable;
            }
        }

        private void btnAddPatient_Click(object sender, EventArgs e)
        {
            AddPatientForm addPatientForm = new AddPatientForm();
            addPatientForm.ShowDialog();
            LoadPatientData();
        }

        private void btnUpdatePatient_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                int selectedPatientId = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["Id"].Value);
                using (var dbContext = new YourDbContext())
                {
                    Patient selectedPatient = dbContext.Patients.Find(selectedPatientId);
                    UpdatePatientForm updatePatientForm = new UpdatePatientForm(selectedPatient);
                    updatePatientForm.ShowDialog();
                    LoadPatientData();
                }
            }
            else
            {
                MessageBox.Show("Please select a patient to update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDeletePatient_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                int selectedPatientId = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["Id"].Value);
                using (var dbContext = new YourDbContext())
                {
                    Patient selectedPatient = dbContext.Patients.Find(selectedPatientId);
                    DeletePatientForm deletePatientForm = new DeletePatientForm(selectedPatient);
                    deletePatientForm.ShowDialog();
                    LoadPatientData();
                }
            }
            else
            {
                MessageBox.Show("Please select a patient to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private DataTable ConvertToDataTable(List<Patient> patients)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Surname", typeof(string));
            dataTable.Columns.Add("DateOfBirth", typeof(DateTime));
            dataTable.Columns.Add("FileNumber", typeof(string));
            dataTable.Columns.Add("PhoneNumber", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            dataTable.Columns.Add("AssignedDoctor", typeof(string));

            foreach (var patient in patients)
            {
                dataTable.Rows.Add(
                    patient.Id,
                    patient.Name,
                    patient.Surname,
                    patient.DateOfBirth,
                    patient.FileNumber,
                    patient.PhoneNumber,
                    patient.Email,
                    patient.AssignedDoctor
                );
            }

            return dataTable;
        }

        private void LoadPatientData()
        {
            using (var dbContext = new YourDbContext())
            {
                List<Patient> patients = dbContext.Patients.ToList();
                DataTable dataTable = ConvertToDataTable(patients);
                dataGridView.DataSource = dataTable;
            }
        }

        private void SetAnchorStyles(Control control, AnchorStyles anchorStyles)
        {
            control.Anchor = anchorStyles;
        }

        private void ViewPatientForm_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

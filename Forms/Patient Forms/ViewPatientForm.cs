using HospitalManagementSystem.Database;
using HospitalManagementSystem.Forms.PatientForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace Project.Forms.Patient_Forms
{
    public partial class ViewPatientForm : Form
    {
        private DataGridView dataGridView;
        private Button btnAddPatient;
        private Button btnUpdatePatient;
        private Button btnDeletePatient;

        public ViewPatientForm()
        {
            InitializeComponent();
            InitializeUIComponents();
            LoadPatientData();
        }

        private void InitializeUIComponents()
        {
            dataGridView = new DataGridView();
            dataGridView.Name = "dataGridView";
            dataGridView.Location = new Point(50, 50);
            dataGridView.Size = new Size(500, 200);
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;

            btnAddPatient = CreateIconButton("btnAddPatient", IconChar.Plus, Color.Black, Color.Green);
            btnAddPatient.Location = new Point(50, 280);
            btnAddPatient.Click += btnAddPatient_Click;

            btnUpdatePatient = CreateIconButton("btnUpdatePatient", IconChar.Edit, Color.Black, Color.Yellow);
            btnUpdatePatient.Location = new Point(180, 280);
            btnUpdatePatient.Click += btnUpdatePatient_Click;

            btnDeletePatient = CreateIconButton("btnDeletePatient", IconChar.TrashAlt, Color.Black, Color.Red);
            btnDeletePatient.Location = new Point(310, 280);
            btnDeletePatient.Click += btnDeletePatient_Click;

            SetAnchorStyles(dataGridView, AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            SetAnchorStyles(btnAddPatient, AnchorStyles.Top | AnchorStyles.Left);
            SetAnchorStyles(btnUpdatePatient, AnchorStyles.Top | AnchorStyles.Left);
            SetAnchorStyles(btnDeletePatient, AnchorStyles.Top | AnchorStyles.Right);

            Controls.Add(dataGridView);
            Controls.Add(btnAddPatient);
            Controls.Add(btnUpdatePatient);
            Controls.Add(btnDeletePatient);
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

        private void LoadPatientData()
        {
            using (var dbContext = new YourDbContext())
            {
                List<Patient> patients = dbContext.Patients.ToList();
                DataTable dataTable = ConvertToDataTable(patients);
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

        private void SetAnchorStyles(Control control, AnchorStyles anchorStyles)
        {
            control.Anchor = anchorStyles;
        }

        private void ViewPatientForm_Load(object sender, EventArgs e)
        {

        }
    }
}

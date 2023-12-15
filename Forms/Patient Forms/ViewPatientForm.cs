using HospitalManagementSystem.Database;
using HospitalManagementSystem.Forms.PatientForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project.Forms.Patient_Forms
{
    public partial class ViewPatientForm : Form
    {
        private DataGridView dataGridView;
        private Button btnAddPatient;
        private Button btnUpdatePatient;

        public ViewPatientForm()
        {
            InitializeComponent();
            InitializeUIComponents();
            LoadPatientData();

        }

        private void InitializeUIComponents()
        {
            // Initialize DataGridView
            dataGridView = new DataGridView();
            dataGridView.Name = "dataGridView";
            dataGridView.Location = new Point(50, 50);
            dataGridView.Size = new Size(500, 200);
            // Configure DataGridView properties, columns, etc.

            // Initialize buttons
            btnAddPatient = new Button();
            btnAddPatient.Name = "btnAddPatient";
            btnAddPatient.Text = "Add Patient";
            btnAddPatient.Location = new Point(50, 280);
            btnAddPatient.Click += btnAddPatient_Click;

            btnUpdatePatient = new Button();
            btnUpdatePatient.Name = "btnUpdatePatient";
            btnUpdatePatient.Text = "Update Patient";
            btnUpdatePatient.Location = new Point(180, 280);
            btnUpdatePatient.Click += btnUpdatePatient_Click;

            // Add controls to the form's Controls collection
            Controls.Add(dataGridView);
            Controls.Add(btnAddPatient);
            Controls.Add(btnUpdatePatient);
        }

        private void LoadPatientData()
        {
            // Load patient data into the DataGridView
            using (var dbContext = new YourDbContext()) // Replace with the actual name of your database context
            {
                List<Patient> patients = dbContext.Patients.ToList();
                DataTable dataTable = ConvertToDataTable(patients);
                dataGridView.DataSource = dataTable;
            }
        }

        private void btnAddPatient_Click(object sender, EventArgs e)
        {
            // Open the AddPatientForm when the Add Patient button is clicked
            AddPatientForm addPatientForm = new AddPatientForm();
            addPatientForm.ShowDialog();
            // Refresh the DataGridView after adding a new patient
            LoadPatientData();
        }

        private void btnUpdatePatient_Click(object sender, EventArgs e)
        {
            // Check if a row is selected in the DataGridView
            if (dataGridView.SelectedRows.Count > 0)
            {
                // Get the selected patient's ID from the DataGridView
                int selectedPatientId = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["PatientId"].Value);

                // Retrieve the patient from the database based on the selected ID
                using (var dbContext = new YourDbContext())
                {
                    Patient selectedPatient = dbContext.Patients.Find(selectedPatientId);

                    // Open the UpdatePatientForm with the selected patient
                    UpdatePatientForm updatePatientForm = new UpdatePatientForm(selectedPatient);
                    updatePatientForm.ShowDialog();

                    // Refresh the DataGridView after updating a patient
                    LoadPatientData();
                }
            }
            else
            {
                MessageBox.Show("Please select a patient to update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private DataTable ConvertToDataTable(List<Patient> patients)
        {
            DataTable dataTable = new DataTable();

            // Adding columns to the DataTable based on Patient model properties
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
                // Adding rows to the DataTable based on patient data
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

        private void ViewPatientForm_Load(object sender, EventArgs e)
        {

        }
    }
}



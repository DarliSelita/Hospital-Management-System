using FontAwesome.Sharp;
using HospitalManagementSystem.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project.Forms.Diagnostic_Forms
{
    public partial class ViewDiagnosticForm : Form
    {
        private DataGridView dataGridView;
        private Button btnAddDiagnostic;
        private Button btnUpdateDiagnostic;
        private Button btnDeleteDiagnostic;
        private Button btnDownloadXRay;
        private Button btnDownloadCheckup;
        private Label lblSearch;
        private TextBox txtSearch;
        private DataTable dataTable;

        public ViewDiagnosticForm()
        {
            InitializeComponent();
            InitializeUIComponents();
            LoadDiagnosticData();
            WindowState = FormWindowState.Maximized;
            dataTable = new DataTable();


        }

        private void InitializeUIComponents()
        {

            BackColor = Color.FromArgb(28, 41, 34); // Adjusted form background color


            dataGridView = new DataGridView();
            dataGridView.Name = "dataGridView";
            dataGridView.Location = new Point(50, 50);
            dataGridView.Size = new Size(ClientSize.Width - 100, ClientSize.Height+85);
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
            lblSearch.Location = new Point( 790, 915);
            lblSearch.ForeColor = Color.FromArgb(5, 250, 99);
            lblSearch.AutoSize = true; // Set AutoSize to true

            txtSearch = new TextBox();
            txtSearch.Location = new Point(900, 915);
            txtSearch.Font = new Font("Segoe UI", 18);
            txtSearch.TextChanged += TxtSearch_TextChanged;
            txtSearch.Size = new Size(200, txtSearch.Height); // Adjust the width to 200 pixels (you can use your desired width)
            txtSearch.TextChanged += TxtSearch_TextChanged;


            btnAddDiagnostic = CreateIconButton("btnAddDiagnostic", IconChar.Plus, Color.Black, Color.Green);
            btnAddDiagnostic.Location = new Point(100, 915);
            btnAddDiagnostic.Click += btnAddDiagnostic_Click;

            btnUpdateDiagnostic = CreateIconButton("btnUpdateDiagnostic", IconChar.Edit, Color.Black, Color.Yellow);
            btnUpdateDiagnostic.Location = new Point(230, 915);
            btnUpdateDiagnostic.Click += btnUpdateDiagnostic_Click;

            btnDeleteDiagnostic = CreateIconButton("btnDeleteDiagnostic", IconChar.TrashAlt, Color.Black, Color.Red);
            btnDeleteDiagnostic.Location = new Point(360, 915);
            btnDeleteDiagnostic.Click += btnDeleteDiagnostic_Click;

            btnDownloadXRay = CreateDownloadButton("btnDownloadXRay", "", IconChar.FileMedicalAlt, Color.Black, Color.Blue);
            btnDownloadXRay.Location = new Point(490, 915);
            btnDownloadXRay.Click += btnDownloadXRay_Click;

            btnDownloadCheckup = CreateDownloadButton("btnDownloadCheckup", "", IconChar.FileMedical, Color.Black, Color.Orange);
            btnDownloadCheckup.Location = new Point(620, 915);
            btnDownloadCheckup.Click += btnDownloadCheckup_Click;

            SetAnchorStyles(dataGridView, AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            

            Controls.Add(dataGridView);
            Controls.Add(btnAddDiagnostic);
            Controls.Add(btnUpdateDiagnostic);
            Controls.Add(btnDeleteDiagnostic);
            Controls.Add(btnDownloadXRay);
            Controls.Add(btnDownloadCheckup);
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
                // Assuming your DbContext includes PatientRecords and related entities
                var searchResult = dbContext.PatientRecords
                    .Include(d => d.PatientVitalSigns)  // Include any related entities as needed
                    .Where(d => d.PatientName.Contains(txtSearch.Text))
                    .ToList();

                // Convert the search result to a DataTable
                dataTable = ConvertDiagnosticsToDataTable(searchResult);

                // Set DataSource to null before binding to refresh the DataGridView
                dataGridView.DataSource = null;

                // Bind DataGridView to the new data
                dataGridView.DataSource = dataTable;
            }
        }


        private void SetAnchorStyles(Control control, AnchorStyles anchorStyles)
        {
            control.Anchor = anchorStyles;
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

        private Button CreateDownloadButton(string name, string text, IconChar icon, Color foreColor, Color backColor)
        {
            Button button = CreateIconButton(name, icon, foreColor, backColor);
            button.Text = text;
            return button;
        }

        private void LoadDiagnosticData()
        {
            using (var dbContext = new YourDbContext())
            {
                List<Diagnostic> diagnostics = dbContext.PatientRecords.Include(d => d.MedicalTests).ToList();
                DataTable dataTable = ConvertDiagnosticsToDataTable(diagnostics);

                // Set DataSource to null before binding to refresh the DataGridView
                dataGridView.DataSource = null;

                // Bind DataGridView to the new data
                dataGridView.DataSource = dataTable;
            }
        }

        private void btnAddDiagnostic_Click(object sender, EventArgs e)
        {
            AddDiagnosticForm addDiagnosticForm = new AddDiagnosticForm();
            addDiagnosticForm.ShowDialog();
            LoadDiagnosticData();
        }

        private void btnUpdateDiagnostic_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                int selectedDiagnosticId = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["Id"].Value);
                using (var dbContext = new YourDbContext())
                {
                    Diagnostic selectedDiagnostic = dbContext.PatientRecords.Find(selectedDiagnosticId);
                    UpdateDiagnosticForm updateDiagnosticForm = new UpdateDiagnosticForm(selectedDiagnostic);
                    updateDiagnosticForm.ShowDialog();
                    LoadDiagnosticData();
                }
            }
            else
            {
                MessageBox.Show("Please select a Diagnostic Record to update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDeleteDiagnostic_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                int selectedDiagnosticId = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["Id"].Value);
                using (var dbContext = new YourDbContext())
                {
                    Diagnostic selectedDiagnostic = dbContext.PatientRecords.Find(selectedDiagnosticId);
                    DeleteDiagnosticForm deleteDiagnosticForm = new DeleteDiagnosticForm(selectedDiagnostic);
                    deleteDiagnosticForm.ShowDialog();
                    LoadDiagnosticData();
                }
            }
            else
            {
                MessageBox.Show("Please select a Diagnostic Record to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDownloadXRay_Click(object sender, EventArgs e)
        {
            DownloadMedicalTest("X-Ray");
        }

        private void btnDownloadCheckup_Click(object sender, EventArgs e)
        {
            DownloadMedicalTest("Checkup");
        }

        private void DownloadMedicalTest(string testType)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                int selectedDiagnosticId = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["Id"].Value);
                using (var dbContext = new YourDbContext())
                {
                    // Load the selected Diagnostic including MedicalTests
                    Diagnostic selectedDiagnostic = dbContext.PatientRecords
                        .Include(d => d.MedicalTests)
                        .FirstOrDefault(d => d.Id == selectedDiagnosticId);

                    if (selectedDiagnostic != null)
                    {
                        // Find the medical test of the specified type
                        MedicalTest medicalTest = selectedDiagnostic.MedicalTests.FirstOrDefault(mt => mt.TestName.Equals(testType, StringComparison.OrdinalIgnoreCase));

                        if (medicalTest != null)
                        {
                            SaveFileDialog saveFileDialog = new SaveFileDialog();
                            saveFileDialog.Filter = "All files (*.*)|*.*";
                            saveFileDialog.FileName = $"{selectedDiagnostic.PatientName}_{testType}.pdf"; // Set the default file name

                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                try
                                {
                                    // Save the medical test file data to the selected file path
                                    File.WriteAllBytes(saveFileDialog.FileName, medicalTest.TestFile);
                                    MessageBox.Show("File downloaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"An error occurred while downloading the file. Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show($"No {testType} found for the selected diagnostic record.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selected diagnostic record not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show($"Please select a diagnostic record to download {testType}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private DataTable ConvertDiagnosticsToDataTable(List<Diagnostic> diagnostics)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("PatientName", typeof(string)); // Add PatientName column
            dataTable.Columns.Add("PatientFile", typeof(string));
            dataTable.Columns.Add("Gender", typeof(string));
            dataTable.Columns.Add("PhoneNumber", typeof(string));
            dataTable.Columns.Add("ReasonForAppointment", typeof(string));
            dataTable.Columns.Add("Allergies", typeof(string));
            dataTable.Columns.Add("MedicationsAndVaccines", typeof(string));
            dataTable.Columns.Add("TobaccoUse", typeof(bool));
            dataTable.Columns.Add("AlcoholConsumptionPerWeek", typeof(double));
            dataTable.Columns.Add("DrugUse", typeof(bool));
            dataTable.Columns.Add("BloodPressure", typeof(int));
            dataTable.Columns.Add("HeartRate", typeof(int));
            dataTable.Columns.Add("RespiratoryRate", typeof(int));
            dataTable.Columns.Add("Temperature", typeof(float));
            dataTable.Columns.Add("Height", typeof(float));
            dataTable.Columns.Add("Weight", typeof(float));

            foreach (var diagnostic in diagnostics)
            {
                dataTable.Rows.Add(
                    diagnostic.Id,
                    diagnostic.PatientName,
                    diagnostic.PatientFile,
                    diagnostic.Gender,
                    diagnostic.PhoneNumber,
                    diagnostic.ReasonForAppointment,
                    diagnostic.Allergies,
                    diagnostic.MedicationsAndVaccines,
                    diagnostic.TobaccoUse,
                    diagnostic.AlcoholConsumptionPerWeek,
                    diagnostic.DrugUse,
                    diagnostic.PatientVitalSigns?.BloodPressure,
                    diagnostic.PatientVitalSigns?.HeartRate,
                    diagnostic.PatientVitalSigns?.RespiratoryRate,
                    diagnostic.PatientVitalSigns?.Temperature,
                    diagnostic.PatientVitalSigns?.Height,
                    diagnostic.PatientVitalSigns?.Weight);
            }

            return dataTable;
        }

    }
}

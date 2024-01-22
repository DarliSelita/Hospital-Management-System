using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using HospitalManagementSystem.Database;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using QRCoder;

namespace HospitalManagementSystem.Forms
{
    public partial class ViewPrescriptionForm : Form
    {
        private DataGridView dataGridView;
        private Button btnAddPrescription;
        private Button btnUpdatePrescription;
        private Button btnDeletePrescription;
        private Button btnDownloadPdf;
        private Label lblSearch;
        private TextBox txtSearch;

        public ViewPrescriptionForm()
        {
            InitializeUIComponents();
            LoadPrescriptionData();
            WindowState = FormWindowState.Maximized; // Set to maximize the form
        }

        private void InitializeUIComponents()
        {
            BackColor = Color.FromArgb(28, 41, 34); // Adjusted form background color

            dataGridView = new DataGridView();
            dataGridView.Name = "dgvPrescription";
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
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 92, 164);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView.RowsDefaultCellStyle.BackColor = Color.FromArgb(36, 156, 154);
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(36, 156, 116);
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            btnAddPrescription = CreateIconButton("btnAddPrescription", FontAwesome.Sharp.IconChar.Plus, Color.Black, Color.Green);
            btnAddPrescription.Location = new Point(50, dataGridView.Bottom + 20);
            btnAddPrescription.Click += btnAddPrescription_Click;

            btnUpdatePrescription = CreateIconButton("btnUpdatePrescription", FontAwesome.Sharp.IconChar.Edit, Color.Black, Color.Yellow);
            btnUpdatePrescription.Location = new Point(btnAddPrescription.Right + 10, dataGridView.Bottom + 20);
            btnUpdatePrescription.Click += btnUpdatePrescription_Click;

            btnDeletePrescription = CreateIconButton("btnDeletePrescription", FontAwesome.Sharp.IconChar.TrashAlt, Color.Black, Color.Red);
            btnDeletePrescription.Location = new Point(btnUpdatePrescription.Right + 10, dataGridView.Bottom + 20);
            btnDeletePrescription.Click += btnDeletePrescription_Click;

            btnDownloadPdf = CreatePdfButton("btnDownloadPdf", FontAwesome.Sharp.IconChar.FilePdf);
            btnDownloadPdf.Location = new Point(btnDeletePrescription.Right + 10, dataGridView.Bottom + 20);
            btnDownloadPdf.Click += btnDownloadPdf_Click;

            lblSearch = new Label();
            lblSearch.Text = "Search: ";
            lblSearch.Font = new Font("Segoe UI", 14);
            lblSearch.Location = new Point(btnDownloadPdf.Right + 170, 915);
            lblSearch.ForeColor = Color.FromArgb(5, 250, 99);
            lblSearch.AutoSize = true; // Set AutoSize to true

            txtSearch = CreateTextBox();
            txtSearch.Location = new Point(btnDownloadPdf.Right + 280, 915);
            txtSearch.Font = new Font("Segoe UI", 18);
            txtSearch.TextChanged += TxtSearch_TextChanged;

            SetAnchorStyles(dataGridView, AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom);
            SetAnchorStyles(btnAddPrescription, AnchorStyles.Bottom | AnchorStyles.Left);
            SetAnchorStyles(btnUpdatePrescription, AnchorStyles.Bottom | AnchorStyles.Left);
            SetAnchorStyles(btnDeletePrescription, AnchorStyles.Bottom | AnchorStyles.Left);
            SetAnchorStyles(btnDownloadPdf, AnchorStyles.Bottom | AnchorStyles.Left);
            SetAnchorStyles(lblSearch, AnchorStyles.Top | AnchorStyles.Left);
            SetAnchorStyles(txtSearch, AnchorStyles.Top | AnchorStyles.Left);

            Controls.Add(dataGridView);
            Controls.Add(btnAddPrescription);
            Controls.Add(btnUpdatePrescription);
            Controls.Add(btnDeletePrescription);
            Controls.Add(btnDownloadPdf);
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
                var searchResult = dbContext.Prescriptions
                    .Include(p => p.Patient)
                    .Include(p => p.Diagnose)
                    .Where(p => p.Patient.FirstName.Contains(txtSearch.Text) || p.Patient.LastName.Contains(txtSearch.Text) || p.Diagnose.Name.Contains(txtSearch.Text))
                    .ToList();

                DataTable dataTable = ConvertToDataTable(searchResult);
                dataGridView.DataSource = dataTable;
            }
        }

        private void btnDownloadPdf_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                int selectedPrescriptionId = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["PrescriptionId"].Value);
                using (var dbContext = new YourDbContext())
                {
                    var prescription = dbContext.Prescriptions
                        .Include("Patient")
                        .Include("Diagnose")
                        .Include("PrescribedMedications.Medication")
                        .FirstOrDefault(p => p.PrescriptionId == selectedPrescriptionId);

                    if (prescription != null)
                    {
                        GeneratePdf(prescription);
                        MessageBox.Show("PDF generated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a prescription to download the PDF.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

       
        private void GeneratePdf(Prescription prescription)
        {
            using (PdfDocument pdf = new PdfDocument())
            {
                PdfPage page = pdf.AddPage();
                XGraphics graphics = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Arial", 12, XFontStyle.Regular);

                int yPosition = 30;

                // Patient Information
                graphics.DrawString($"Patient Information: {prescription.Patient.FirstName} {prescription.Patient.LastName}", font, XBrushes.Black, 30, yPosition);
                yPosition += 20;

                // Diagnoses and Medications Table
                graphics.DrawString("Diagnoses and Medications:", font, XBrushes.Black, 30, yPosition);
                yPosition += 20;

                foreach (var prescribedMedication in prescription.PrescribedMedications)
                {
                    string medicationInfo = $"{prescribedMedication.Medication.Name} - Quantity: {prescribedMedication.Quantity}";
                    graphics.DrawString(medicationInfo, font, XBrushes.Black, 50, yPosition);
                    yPosition += 15;
                }

                // Calculate and display the final price based on your criteria
                decimal totalPrice = prescription.PrescribedMedications.Sum(pm => pm.Medication.Price * pm.Quantity);
                graphics.DrawString($"Total Price: {totalPrice}", font, XBrushes.Black, 30, yPosition + 20);

                // Save the PDF
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Prescription.pdf");
                pdf.Save(filePath);

                // Generate QR Code
                GenerateQrCode(filePath);
            }
        }

        private void GenerateQrCode(string filePath)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(filePath, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            // Save the QR Code image
            string qrCodeImagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "PrescriptionQRCode.png");
            qrCodeImage.Save(qrCodeImagePath);

            MessageBox.Show("QR Code generated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadPrescriptionData()
        {
            using (var dbContext = new YourDbContext())
            {
                var prescriptions = dbContext.Prescriptions
                    .Include(p => p.Patient)
                    .Include(p => p.Diagnose)
                    .Include(p => p.PrescribedMedications)
                    .ToList();

                DataTable dataTable = ConvertToDataTable(prescriptions);
                dataGridView.DataSource = dataTable;
            }
        }

        private DataTable ConvertToDataTable(List<Prescription> prescriptions)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("PrescriptionId", typeof(int));
            dataTable.Columns.Add("PatientId", typeof(int));
            dataTable.Columns.Add("PatientName", typeof(string)); // Add patient name column
            dataTable.Columns.Add("DiagnoseId", typeof(int));
            dataTable.Columns.Add("DiagnoseName", typeof(string)); // Add diagnose name column
            dataTable.Columns.Add("StartDate", typeof(DateTime));
            dataTable.Columns.Add("EndDate", typeof(DateTime));

            foreach (var prescription in prescriptions)
            {
                dataTable.Rows.Add(
                    prescription.PrescriptionId,
                    prescription.PatientId,
                    $"{prescription.Patient.FirstName} {prescription.Patient.LastName}", // Concatenate patient name
                    prescription.DiagnoseId,
                    prescription.Diagnose.Name, // Assuming there's a Name property in Diagnose
                    prescription.StartDate,
                    prescription.EndDate);
            }

            return dataTable;
        }


        private void btnAddPrescription_Click(object sender, EventArgs e)
        {
            AddPrescriptionForm createPrescriptionForm = new AddPrescriptionForm();
            createPrescriptionForm.ShowDialog();
            LoadPrescriptionData();
        }

        private void btnUpdatePrescription_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                int selectedPrescriptionId = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["PrescriptionId"].Value);
                using (var dbContext = new YourDbContext())
                {
                    Prescription selectedPrescription = dbContext.Prescriptions.Find(selectedPrescriptionId);
                    UpdatePrescriptionForm updatePrescriptionForm = new UpdatePrescriptionForm(selectedPrescriptionId);
                    updatePrescriptionForm.ShowDialog();
                    LoadPrescriptionData();
                }
            }
            else
            {
                MessageBox.Show("Please select a prescription to update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDeletePrescription_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                int selectedPrescriptionId = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["PrescriptionId"].Value);
                using (var dbContext = new YourDbContext())
                {
                    Prescription selectedPrescription = dbContext.Prescriptions.Find(selectedPrescriptionId);
                    DeletePrescriptionForm deletePrescriptionForm = new DeletePrescriptionForm(selectedPrescription);
                    deletePrescriptionForm.ShowDialog();
                    LoadPrescriptionData();
                }
            }
            else
            {
                MessageBox.Show("Please select a prescription to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private Button CreatePdfButton(string name, FontAwesome.Sharp.IconChar icon)
        {
            Button button = new Button();
            button.Name = name;
            button.Size = new Size(120, 40);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = Color.FromArgb(52, 152, 219); // Set your desired background color
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Text = ""; // Change the text if needed
            button.Image = icon.ToBitmap(Color.White, 20, 20);
            button.Click += btnDownloadPdf_Click; // Add your click event handler
            return button;
        }

        private void SetAnchorStyles(Control control, AnchorStyles anchorStyles)
        {
            control.Anchor = anchorStyles;
        }

        private void ViewPrescriptionForm_Load(object sender, EventArgs e)
        {

        }
    }
}

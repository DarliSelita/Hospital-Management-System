using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using HospitalManagementSystem.Database;
using Microsoft.EntityFrameworkCore;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using ZXing;
using iText.IO.Image;
using iText.Barcodes;
using System.Linq;
using System.Drawing.Imaging;

namespace Project.Forms.Billing_Forms
{
    public partial class AddInvoiceForm : Form
    {
        private YourDbContext DbContext;
        private TextBox txtPatientName;
        private TextBox txtPatientFileNumber;
        private DateTimePicker dateTimePicker;
        private Label lblDateTimePicker;
        private Button btnAddInvoice;
        private Button btnDownloadPDF;
        private DataGridView dgvProducts;
        private PictureBox pictureBox2;
        private List<Product> productsList;
        
        public AddInvoiceForm()
        {
            InitializeComponent();
            DbContext = new YourDbContext();
            InitializeUIComponents();
            InitializeData();
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

            lblDateTimePicker = new Label();
            lblDateTimePicker.Text = "Date:";
            lblDateTimePicker.Location = new System.Drawing.Point(50, 110);

            dgvProducts = new DataGridView();
            dgvProducts.Name = "dgvProducts";
            dgvProducts.Location = new System.Drawing.Point(50, 140);
            dgvProducts.Size = new System.Drawing.Size(300, 150);
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.Columns.Add("ProductNumber", "Product Number");
            dgvProducts.Columns.Add("ServiceName", "Service Name");
            dgvProducts.Columns.Add("Price", "Price");

            btnAddInvoice = new Button();
            btnAddInvoice.Name = "btnAddInvoice";
            btnAddInvoice.Text = "Add Invoice";
            btnAddInvoice.Location = new System.Drawing.Point(150, 300);
            btnAddInvoice.Click += btnAddInvoice_Click;
            btnAddInvoice.Size = new System.Drawing.Size(150, 40);
            btnAddInvoice.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            btnAddInvoice.ForeColor = System.Drawing.Color.White;
            btnAddInvoice.Font = new System.Drawing.Font("Open Sans", 12, System.Drawing.FontStyle.Bold);
            btnAddInvoice.FlatStyle = FlatStyle.Flat;
            btnAddInvoice.FlatAppearance.BorderSize = 0;
            btnAddInvoice.TextAlign = ContentAlignment.MiddleCenter;

            btnDownloadPDF = new Button();
            btnDownloadPDF.Name = "btnDownloadPDF";
            btnDownloadPDF.Text = "Download PDF";
            btnDownloadPDF.Location = new System.Drawing.Point(310, 300);
            btnDownloadPDF.Click += btnDownloadPDF_Click;
            btnDownloadPDF.Size = new System.Drawing.Size(150, 40);
            btnDownloadPDF.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            btnDownloadPDF.ForeColor = System.Drawing.Color.White;
            btnDownloadPDF.Font = new System.Drawing.Font("Open Sans", 12, System.Drawing.FontStyle.Bold);
            btnDownloadPDF.FlatStyle = FlatStyle.Flat;
            btnDownloadPDF.FlatAppearance.BorderSize = 0;
            btnDownloadPDF.TextAlign = ContentAlignment.MiddleCenter;

            pictureBox2 = new PictureBox();
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Location = new System.Drawing.Point(440, 80);
            pictureBox2.Size = new System.Drawing.Size(150, 130);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Image = System.Drawing.Image.FromFile(Path.Combine("C:\\Users\\user\\Desktop\\Project\\Resources", "patient logo.jpg"));

            Controls.Add(txtPatientName);
            Controls.Add(txtPatientFileNumber);
            Controls.Add(dateTimePicker);
            Controls.Add(lblDateTimePicker);
            Controls.Add(dgvProducts);
            Controls.Add(btnAddInvoice);
            Controls.Add(btnDownloadPDF);
            Controls.Add(pictureBox2);

            Controls.Add(new Label { Text = "Patient Name:", Location = new System.Drawing.Point(50, 50) });
            Controls.Add(new Label { Text = "Patient File Number:", Location = new System.Drawing.Point(50, 80) });
            Controls.Add(new Label { Text = "Date:", Location = new System.Drawing.Point(50, 110) });

            dgvProducts.CellContentClick += dgvProducts_CellContentClick;
        }

        private void InitializeData()
        {
            productsList = new List<Product>();
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvProducts.Columns["ProductNumber"].Index && e.RowIndex != -1)
            {
                dgvProducts.Rows.Add(dgvProducts.Rows.Count + 1, "", 0.0);
            }
        }

        private void btnAddInvoice_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPatientName.Text) || string.IsNullOrWhiteSpace(txtPatientFileNumber.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            productsList.Clear();

            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                if (row.Cells["ServiceName"].Value != null && row.Cells["Price"].Value != null)
                {
                    string serviceName = row.Cells["ServiceName"].Value.ToString();
                    double price;

                    if (double.TryParse(row.Cells["Price"].Value.ToString(), out price))
                    {
                        productsList.Add(new Product { ServiceName = serviceName, Price = price });
                    }
                    else
                    {
                        MessageBox.Show("Invalid price format. Please enter a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            var billing = new Billing
            {
                PatientName = txtPatientName.Text,
                PatientFileNumber = txtPatientFileNumber.Text,
                BillingDate = dateTimePicker.Value,
                Products = productsList
            };

            try
            {
                DbContext.Billing.Add(billing);
                DbContext.SaveChanges();

                MessageBox.Show("Invoice added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (DbUpdateException dbEx)
            {
                MessageBox.Show($"Database error adding invoice. Details: {dbEx.InnerException?.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred. Please contact support. Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDownloadPDF_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPatientName.Text) || string.IsNullOrWhiteSpace(txtPatientFileNumber.Text))
            {
                MessageBox.Show("Please fill in all required fields before generating the PDF.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            productsList.Clear();

            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                if (row.Cells["ServiceName"].Value != null && row.Cells["Price"].Value != null)
                {
                    string serviceName = row.Cells["ServiceName"].Value.ToString();
                    double price;

                    if (double.TryParse(row.Cells["Price"].Value.ToString(), out price))
                    {
                        productsList.Add(new Product { ServiceName = serviceName, Price = price });
                    }
                    else
                    {
                        MessageBox.Show("Invalid price format. Please enter a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            var billing = new Billing
            {
                PatientName = txtPatientName.Text,
                PatientFileNumber = txtPatientFileNumber.Text,
                BillingDate = dateTimePicker.Value,
                Products = productsList
            };

            GeneratePDF(billing);
        }

        private void GeneratePDF(Billing billing)
        {
            string invoiceNumber = Guid.NewGuid().ToString();
            string barcodeContent = invoiceNumber;

            string filePath = $"Invoice_{invoiceNumber}.pdf";

            using (PdfWriter writer = new PdfWriter(filePath))
            {
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    Document document = new Document(pdf);

                    document.Add(new Paragraph($"Invoice Number: {invoiceNumber}"));
                    document.Add(new Paragraph($"Patient Name: {billing.PatientName}"));
                    document.Add(new Paragraph($"Patient File Number: {billing.PatientFileNumber}"));
                    document.Add(new Paragraph($"Billing Date: {billing.BillingDate.ToShortDateString()}"));

                    // Add a table for product details
                    Table table = new Table(3);
                    table.AddHeaderCell("Product Number");
                    table.AddHeaderCell("Service Name");
                    table.AddHeaderCell("Price");

                    foreach (var product in billing.Products)
                    {
                        table.AddCell(product.ProductID.ToString());
                        table.AddCell(product.ServiceName);
                        table.AddCell(product.Price.ToString("C"));
                    }

                    document.Add(table);

                    // Use the Sum extension method from System.Linq
                    double totalAmount = billing.Products.Sum(product => product.Price);
                    document.Add(new Paragraph($"Total Amount: {totalAmount.ToString("C")}")); // Add total amount

                    // Generate barcode using iText7.pdf2
                    Barcode128 code128 = new Barcode128(pdf);
                    code128.SetCodeType(Barcode128.CODE128);
                    code128.SetCode(barcodeContent);
                    iText.Kernel.Pdf.Xobject.PdfFormXObject barcodeFormXObject = code128.CreateFormXObject(null, null, pdf);
                    iText.Layout.Element.Image barcodeImage = new iText.Layout.Element.Image(barcodeFormXObject);
                    document.Add(barcodeImage);

                    // Add QR code with payment link
                    BarcodeWriter barcodeWriter = new BarcodeWriter();
                    barcodeWriter.Format = BarcodeFormat.QR_CODE;
                    barcodeWriter.Options = new ZXing.Common.EncodingOptions
                    {
                        Width = 150,
                        Height = 150
                    };

                    // Example URL for payment or service
                    string paymentUrl = "https://www.paypal.com/signin?returnUri=https%3A%2F%2Fwww.paypal.com%2Fmep%2F" + invoiceNumber + "&amount=" + totalAmount;
                    string qrCodeContent = paymentUrl;

                    var qrCodeBitmap = barcodeWriter.Write(qrCodeContent);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        qrCodeBitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                        iText.Layout.Element.Image qrCodeImage = new iText.Layout.Element.Image(ImageDataFactory.Create(memoryStream.ToArray()));
                        document.Add(qrCodeImage);
                    }

                }
            }

            MessageBox.Show($"PDF generated successfully! File path: {Path.GetFullPath(filePath)}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }




        private void ClearForm()
        {
            txtPatientName.Clear();
            txtPatientFileNumber.Clear();
            dateTimePicker.Value = DateTime.Now;
            dgvProducts.Rows.Clear();
        }
    }
}

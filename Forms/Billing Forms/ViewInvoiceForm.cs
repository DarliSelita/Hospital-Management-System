using HospitalManagementSystem.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using ZXing;
using iText.IO.Image;
using iText.Barcodes;
using System.Drawing.Imaging;
using FontAwesome.Sharp;

namespace Project.Forms.Billing_Forms
{
    public partial class ViewInvoiceForm : Form
    {
        private YourDbContext DbContext;
        private DataGridView dgvInvoices;

        public ViewInvoiceForm()
        {
            InitializeComponent();
            DbContext = new YourDbContext();
            InitializeUIComponents();
            LoadInvoices();
        }

        private void InitializeUIComponents()
        {
            dgvInvoices = new DataGridView();
            dgvInvoices.Name = "dgvInvoices";
            dgvInvoices.Location = new System.Drawing.Point(50, 50);
            dgvInvoices.Size = new System.Drawing.Size(400, 200);
            dgvInvoices.AutoGenerateColumns = false;
            dgvInvoices.Columns.Add("InvoiceID", "Invoice ID");
            dgvInvoices.Columns.Add("PatientName", "Patient Name");
            dgvInvoices.Columns.Add("BillingDate", "Billing Date");

            Button btnAddInvoice = CreateIconButton("btnAddInvoice", IconChar.PlusSquare, Color.Black, Color.Green);
            Button btnUpdateInvoice = CreateIconButton("btnUpdateInvoice", IconChar.PenToSquare, Color.Black, Color.Yellow);
            Button btnDeleteInvoice = CreateIconButton("btnDeleteInvoice", IconChar.Trash, Color.Black, Color.Red);
            Button btnDownloadPDF = CreateIconButton("btnDownloadPDF", IconChar.FilePdf, Color.Black, Color.White);

            SetButtonLocation(btnAddInvoice, 50, 270);
            SetButtonLocation(btnUpdateInvoice, 210, 270);
            SetButtonLocation(btnDeleteInvoice, 370, 270);
            SetButtonLocation(btnDownloadPDF, 530, 270);

            btnAddInvoice.Click += btnAddInvoice_Click;
            btnUpdateInvoice.Click += btnUpdateInvoice_Click;
            btnDeleteInvoice.Click += btnDeleteInvoice_Click;
            btnDownloadPDF.Click += btnDownloadPDF_Click;

            Controls.Add(dgvInvoices);
            Controls.Add(btnAddInvoice);
            Controls.Add(btnUpdateInvoice);
            Controls.Add(btnDeleteInvoice);
            Controls.Add(btnDownloadPDF);

            dgvInvoices.CellClick += dgvInvoices_CellClick;
        }

        private void LoadInvoices()
        {
            dgvInvoices.Rows.Clear();
            var invoices = DbContext.Billing.Include(b => b.Products).ToList();

            foreach (var invoice in invoices)
            {
                foreach (var product in invoice.Products)
                {
                    dgvInvoices.Rows.Add(
                        invoice.BillingID,
                        invoice.PatientName,
                        invoice.BillingDate,
                        product.ProductID,
                        product.ServiceName,
                        product.Price
                    );
                }
            }

            // Set AutoSizeMode to Fill for each column
            foreach (DataGridViewColumn column in dgvInvoices.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }



        private void dgvInvoices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnAddInvoice_Click(object sender, EventArgs e)
        {
            AddInvoiceForm addInvoiceForm = new AddInvoiceForm();
            addInvoiceForm.ShowDialog();
            LoadInvoices();
        }

        private void btnUpdateInvoice_Click(object sender, EventArgs e)
        {
            if (dgvInvoices.SelectedRows.Count > 0)
            {
                int selectedInvoiceID = (int)dgvInvoices.SelectedRows[0].Cells["InvoiceID"].Value;
                Billing selectedBilling = DbContext.Billing.Find(selectedInvoiceID);

                if (selectedBilling != null)
                {
                    UpdateInvoiceFom updateInvoiceForm = new UpdateInvoiceFom(selectedBilling);
                    updateInvoiceForm.ShowDialog();
                    LoadInvoices();
                }
                else
                {
                    MessageBox.Show("Selected invoice not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an invoice to update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteInvoice_Click(object sender, EventArgs e)
        {
            if (dgvInvoices.SelectedRows.Count > 0)
            {
                int selectedInvoiceID = (int)dgvInvoices.SelectedRows[0].Cells["InvoiceID"].Value;

                try
                {
                    var productsToDelete = DbContext.Product.Where(p => p.BillingID == selectedInvoiceID);
                    DbContext.Product.RemoveRange(productsToDelete);

                    var invoiceToDelete = DbContext.Billing.Find(selectedInvoiceID);

                    if (invoiceToDelete != null)
                    {
                        DbContext.Billing.Remove(invoiceToDelete);
                        DbContext.SaveChanges();
                        MessageBox.Show("Invoice deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadInvoices();
                    }
                    else
                    {
                        MessageBox.Show("Selected invoice not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (DbUpdateException dbEx)
                {
                    MessageBox.Show($"Database error deleting invoice. Details: {dbEx.InnerException?.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred. Please contact support. Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an invoice to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDownloadPDF_Click(object sender, EventArgs e)
        {
            if (dgvInvoices.SelectedRows.Count > 0)
            {
                int selectedInvoiceID = (int)dgvInvoices.SelectedRows[0].Cells["InvoiceID"].Value;
                Billing selectedBilling = DbContext.Billing.Find(selectedInvoiceID);

                if (selectedBilling != null)
                {
                    GeneratePDF(selectedBilling);
                }
                else
                {
                    MessageBox.Show("Selected invoice not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an invoice to download its PDF.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                    double totalAmount = billing.Products.Sum(product => product.Price);
                    document.Add(new Paragraph($"Total Amount: {totalAmount.ToString("C")}"));

                    Barcode128 code128 = new Barcode128(pdf);
                    code128.SetCodeType(Barcode128.CODE128);
                    code128.SetCode(barcodeContent);
                    iText.Kernel.Pdf.Xobject.PdfFormXObject barcodeFormXObject = code128.CreateFormXObject(null, null, pdf);
                    iText.Layout.Element.Image barcodeImage = new iText.Layout.Element.Image(barcodeFormXObject);
                    document.Add(barcodeImage);

                    BarcodeWriter barcodeWriter = new BarcodeWriter();
                    barcodeWriter.Format = BarcodeFormat.QR_CODE;
                    barcodeWriter.Options = new ZXing.Common.EncodingOptions
                    {
                        Width = 150,
                        Height = 150
                    };

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

        private Button CreateIconButton(string name, IconChar icon, Color foreColor, Color backColor)
        {
            Button button = new Button();
            button.Name = name;
            button.Size = new Size(150, 40);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = backColor;
            button.ForeColor = foreColor;
            button.Font = new Font("Open Sans", 12, FontStyle.Bold);
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Image = icon.ToBitmap(foreColor, 20, 20);
            return button;
        }

        private void SetButtonLocation(Button button, int x, int y)
        {
            button.Location = new Point(x, y);
        }



    }
}

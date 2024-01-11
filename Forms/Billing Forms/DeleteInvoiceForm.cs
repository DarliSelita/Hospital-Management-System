using HospitalManagementSystem.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project.Forms.Billing_Forms
{
    public partial class DeleteInvoiceForm : Form
    {
        private YourDbContext DbContext;
        private DataGridView dgvInvoices;

        public DeleteInvoiceForm(Billing billing)
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

            Button btnDeleteInvoice = new Button();
            btnDeleteInvoice.Name = "btnDeleteInvoice";
            btnDeleteInvoice.Text = "Delete Invoice";
            btnDeleteInvoice.Location = new System.Drawing.Point(50, 270);
            btnDeleteInvoice.Click += btnDeleteInvoice_Click;
            btnDeleteInvoice.Size = new System.Drawing.Size(150, 40);
            btnDeleteInvoice.BackColor = System.Drawing.Color.FromArgb(255, 0, 0);
            btnDeleteInvoice.ForeColor = System.Drawing.Color.White;
            btnDeleteInvoice.Font = new System.Drawing.Font("Open Sans", 12, System.Drawing.FontStyle.Bold);
            btnDeleteInvoice.FlatStyle = FlatStyle.Flat;
            btnDeleteInvoice.FlatAppearance.BorderSize = 0;
            btnDeleteInvoice.TextAlign = ContentAlignment.MiddleCenter;

            Controls.Add(dgvInvoices);
            Controls.Add(btnDeleteInvoice);

            dgvInvoices.CellClick += dgvInvoices_CellClick;
        }

        private void LoadInvoices()
        {
            dgvInvoices.Rows.Clear();
            var invoices = DbContext.Billing.ToList();

            foreach (var invoice in invoices)
            {
                dgvInvoices.Rows.Add(invoice.BillingID, invoice.PatientName, invoice.BillingDate);
            }
        }

        private void dgvInvoices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle cell click event, if needed
        }

        private void btnDeleteInvoice_Click(object sender, EventArgs e)
        {
            if (dgvInvoices.SelectedRows.Count > 0)
            {
                int selectedInvoiceID = (int)dgvInvoices.SelectedRows[0].Cells["InvoiceID"].Value;

                // Double-check confirmation
                DialogResult result = MessageBox.Show($"Are you sure you want to delete Invoice ID: {selectedInvoiceID}?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var invoiceToDelete = DbContext.Billing.Find(selectedInvoiceID);

                        if (invoiceToDelete != null)
                        {
                            var productsToDelete = DbContext.Product.Where(p => p.BillingID == selectedInvoiceID);
                            DbContext.Product.RemoveRange(productsToDelete);

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
            }
            else
            {
                MessageBox.Show("Please select an invoice to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

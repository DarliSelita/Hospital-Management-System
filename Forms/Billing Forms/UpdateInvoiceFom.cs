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
    public partial class UpdateInvoiceFom : Form
    {
        private YourDbContext DbContext;
        private TextBox txtPatientName;
        private TextBox txtPatientFileNumber;
        private DateTimePicker dateTimePicker;
        private Label lblDateTimePicker;
        private Button btnUpdateInvoice;
        private DataGridView dgvProducts;
        private List<Product> productsList;
        private Billing selectedBilling;
        
        public UpdateInvoiceFom(Billing billing)
        {
            InitializeComponent();
            DbContext = new YourDbContext();
            InitializeUIComponents();
            InitializeData();
            selectedBilling = billing;
            LoadBillingData();
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

            btnUpdateInvoice = new Button();
            btnUpdateInvoice.Name = "btnUpdateInvoice";
            btnUpdateInvoice.Text = "Update Invoice";
            btnUpdateInvoice.Location = new System.Drawing.Point(150, 300);
            btnUpdateInvoice.Click += btnUpdateInvoice_Click;
            btnUpdateInvoice.Size = new System.Drawing.Size(150, 40);
            btnUpdateInvoice.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            btnUpdateInvoice.ForeColor = System.Drawing.Color.White;
            btnUpdateInvoice.Font = new System.Drawing.Font("Open Sans", 12, System.Drawing.FontStyle.Bold);
            btnUpdateInvoice.FlatStyle = FlatStyle.Flat;
            btnUpdateInvoice.FlatAppearance.BorderSize = 0;
            btnUpdateInvoice.TextAlign = ContentAlignment.MiddleCenter;

            Controls.Add(txtPatientName);
            Controls.Add(txtPatientFileNumber);
            Controls.Add(dateTimePicker);
            Controls.Add(lblDateTimePicker);
            Controls.Add(dgvProducts);
            Controls.Add(btnUpdateInvoice);

            Controls.Add(new Label { Text = "Patient Name:", Location = new System.Drawing.Point(50, 50) });
            Controls.Add(new Label { Text = "Patient File Number:", Location = new System.Drawing.Point(50, 80) });
            Controls.Add(new Label { Text = "Date:", Location = new System.Drawing.Point(50, 110) });

            dgvProducts.CellContentClick += dgvProducts_CellContentClick;
        }

        private void InitializeData()
        {
            productsList = new List<Product>();
        }

        private void LoadBillingData()
        {
            txtPatientName.Text = selectedBilling.PatientName;
            txtPatientFileNumber.Text = selectedBilling.PatientFileNumber;
            dateTimePicker.Value = selectedBilling.BillingDate;

            dgvProducts.Rows.Clear();
            foreach (var product in selectedBilling.Products)
            {
                dgvProducts.Rows.Add(dgvProducts.Rows.Count + 1, product.ServiceName, product.Price);
            }
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvProducts.Columns["ProductNumber"].Index && e.RowIndex != -1)
            {
                dgvProducts.Rows.Add(dgvProducts.Rows.Count + 1, "", 0.0);
            }
        }

        private void btnUpdateInvoice_Click(object sender, EventArgs e)
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

            selectedBilling.PatientName = txtPatientName.Text;
            selectedBilling.PatientFileNumber = txtPatientFileNumber.Text;
            selectedBilling.BillingDate = dateTimePicker.Value;
            selectedBilling.Products = productsList;

            try
            {
                DbContext.Entry(selectedBilling).State = EntityState.Modified;
                DbContext.SaveChanges();

                MessageBox.Show("Invoice updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (DbUpdateException dbEx)
            {
                MessageBox.Show($"Database error updating invoice. Details: {dbEx.InnerException?.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred. Please contact support. Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}

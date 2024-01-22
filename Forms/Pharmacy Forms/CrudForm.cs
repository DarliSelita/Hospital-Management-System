using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalManagementSystem.Database;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Forms
{
    public partial class CrudForm : Form
    {
        private Label lblMedicationName;
        private Label lblQuantityOnHand;
        private Label lblReorderLevel;
        private Label lblSupplierInfo;
        private Label lblManufacturingDate;
        private Label lblExpiryDate;
        private Label lblStorageLocation;

        private TextBox txtMedicationName;
        private TextBox txtQuantityOnHand;
        private TextBox txtReorderLevel;
        private TextBox txtSupplierInfo;
        private DateTimePicker dtpManufacturingDate;
        private DateTimePicker dtpExpiryDate;
        private TextBox txtStorageLocation;

        private Button btnSave;

        private int? medicalInventoryId;

        public CrudForm(int? medicalInventoryId = null)
        {
            InitializeComponent();
            this.medicalInventoryId = medicalInventoryId;
            if (medicalInventoryId.HasValue)
            {
                LoadMedicalInventoryData();
            }
            this.Size = new System.Drawing.Size(650, 500);

        }

        private void InitializeComponent()
        {
            lblMedicationName = CreateLabel("Medication Name:");
            lblMedicationName.Location = new Point(50, 50);

            txtMedicationName = CreateTextBox();
            txtMedicationName.Location = new Point(200, 50);

            lblQuantityOnHand = CreateLabel("Quantity On Hand:");
            lblQuantityOnHand.Location = new Point(50, 80);

            txtQuantityOnHand = CreateTextBox();
            txtQuantityOnHand.Location = new Point(200, 80);

            lblReorderLevel = CreateLabel("Reorder Level:");
            lblReorderLevel.Location = new Point(50, 110);

            txtReorderLevel = CreateTextBox();
            txtReorderLevel.Location = new Point(200, 110);

            lblSupplierInfo = CreateLabel("Supplier Information:");
            lblSupplierInfo.Location = new Point(50, 140);

            txtSupplierInfo = CreateTextBox();
            txtSupplierInfo.Location = new Point(200, 140);

            lblManufacturingDate = CreateLabel("Manufacturing Date:");
            lblManufacturingDate.Location = new Point(50, 170);

            dtpManufacturingDate = new DateTimePicker();
            dtpManufacturingDate.Format = DateTimePickerFormat.Short;
            dtpManufacturingDate.Location = new Point(200, 170);

            lblExpiryDate = CreateLabel("Expiry Date:");
            lblExpiryDate.Location = new Point(50, 200);

            dtpExpiryDate = new DateTimePicker();
            dtpExpiryDate.Format = DateTimePickerFormat.Short;
            dtpExpiryDate.Location = new Point(200, 200);

            lblStorageLocation = CreateLabel("Storage Location:");
            lblStorageLocation.Location = new Point(50, 230);

            txtStorageLocation = CreateTextBox();
            txtStorageLocation.Location = new Point(200, 230);

            btnSave = CreateButton("Save", Color.Blue, btnSave_Click);
            btnSave.Location = new Point(200, 300);

            Controls.Add(lblMedicationName);
            Controls.Add(txtMedicationName);
            Controls.Add(lblQuantityOnHand);
            Controls.Add(txtQuantityOnHand);
            Controls.Add(lblReorderLevel);
            Controls.Add(txtReorderLevel);
            Controls.Add(lblSupplierInfo);
            Controls.Add(txtSupplierInfo);
            Controls.Add(lblManufacturingDate);
            Controls.Add(dtpManufacturingDate);
            Controls.Add(lblExpiryDate);
            Controls.Add(dtpExpiryDate);
            Controls.Add(lblStorageLocation);
            Controls.Add(txtStorageLocation);
            Controls.Add(btnSave);
        }

        private Label CreateLabel(string text)
        {
            Label label = new Label();
            label.Text = text;
            label.AutoSize = true;
            return label;
        }

        private TextBox CreateTextBox()
        {
            TextBox textBox = new TextBox();
            textBox.Size = new Size(150, 20);
            return textBox;
        }

        private Button CreateButton(string text, Color backColor, EventHandler clickHandler)
        {
            Button button = new Button();
            button.Text = text;
            button.Size = new Size(120, 40);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = backColor;
            button.ForeColor = Color.Black;
            button.Click += clickHandler;
            return button;
        }
        private void LoadMedicalInventoryData()
        {
            using (var dbContext = new YourDbContext())
            {
                var medicalInventory = dbContext.MedicalInventories
                    .Include(mi => mi.Medication)  // Include the Medication navigation property
                    .FirstOrDefault(mi => mi.MedicalInventoryId == medicalInventoryId.Value);

                if (medicalInventory != null)
                {
                    txtMedicationName.Text = medicalInventory.Medication?.Name ?? string.Empty;
                    txtQuantityOnHand.Text = medicalInventory.QuantityOnHand.ToString();
                    txtReorderLevel.Text = medicalInventory.ReorderLevel.ToString();
                    txtSupplierInfo.Text = medicalInventory.SupplierInformation ?? string.Empty;
                    dtpManufacturingDate.Value = medicalInventory.ManufacturingDate;
                    dtpExpiryDate.Value = medicalInventory.ExpiryDate;
                    txtStorageLocation.Text = medicalInventory.StorageLocation ?? string.Empty;
                }
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            using (var dbContext = new YourDbContext())
            {
                if (medicalInventoryId.HasValue)
                {
                    var existingMedicalInventory = dbContext.MedicalInventories.Find(medicalInventoryId.Value);

                    if (existingMedicalInventory != null)
                    {
                        UpdateMedicalInventory(existingMedicalInventory);
                        dbContext.SaveChanges();
                    }
                }
                else
                {
                    MedicalInventory newMedicalInventory = new MedicalInventory();
                    UpdateMedicalInventory(newMedicalInventory);
                    dbContext.MedicalInventories.Add(newMedicalInventory);
                    dbContext.SaveChanges();
                }
            }

            MessageBox.Show("Medical Inventory saved successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void UpdateMedicalInventory(MedicalInventory medicalInventory)
        {
            medicalInventory.QuantityOnHand = Convert.ToInt32(txtQuantityOnHand.Text);
            medicalInventory.ReorderLevel = Convert.ToInt32(txtReorderLevel.Text);
            medicalInventory.SupplierInformation = txtSupplierInfo.Text;
            medicalInventory.ManufacturingDate = dtpManufacturingDate.Value;
            medicalInventory.ExpiryDate = dtpExpiryDate.Value;
            medicalInventory.StorageLocation = txtStorageLocation.Text;

            // Assume you have a MedicationId available (for simplicity, you may need to adjust this part based on your actual implementation)
            int medicationId = GetMedicationIdFromDatabase(txtMedicationName.Text);

            if (medicationId > 0)
            {
                medicalInventory.MedicationId = medicationId;
            }
            else
            {
                // Handle the case where the medication is not found
                MessageBox.Show("Medication not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetMedicationIdFromDatabase(string medicationName)
        {
            using (var dbContext = new YourDbContext())
            {
                var medication = dbContext.Medications.FirstOrDefault(m => m.Name == medicationName);

                if (medication != null)
                {
                    return medication.MedicationId;
                }

                return -1; // Return a value that indicates medication not found
            }
        }

    }
}

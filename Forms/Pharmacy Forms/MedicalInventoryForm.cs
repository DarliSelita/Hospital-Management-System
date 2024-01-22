using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalManagementSystem.Database;
using Project.Forms.Pharmacy_Forms;

namespace HospitalManagementSystem.Forms
{
    public partial class MedicalInventoryForm : Form
    {
        private DataGridView dgvMedicalInventory;
        private Button btnAddMedication;
        private Button btnUpdateMedication;
        private Button btnDeleteMedication;
        private TextBox txtSearch;
        private Label lblSearch;

        public MedicalInventoryForm()
        {
            InitializeUIComponents();
            LoadMedicalInventoryData();
            WindowState = FormWindowState.Maximized;
        }

        private void InitializeUIComponents()
        {
            BackColor = Color.FromArgb(28, 41, 34);

            dgvMedicalInventory = new DataGridView();
            dgvMedicalInventory.Name = "dgvMedicalInventory";
            dgvMedicalInventory.Location = new System.Drawing.Point(50, 50);
            dgvMedicalInventory.Size = new Size(ClientSize.Width - 100, ClientSize.Height - 200);
            dgvMedicalInventory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMedicalInventory.RowTemplate.Height = 35;
            dgvMedicalInventory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            dgvMedicalInventory.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvMedicalInventory.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMedicalInventory.ColumnHeadersHeight = 50;
            dgvMedicalInventory.DefaultCellStyle.Font = new Font("Segoe UI", 11);
            dgvMedicalInventory.ReadOnly = true;
            dgvMedicalInventory.AllowUserToAddRows = false;
            dgvMedicalInventory.AllowUserToDeleteRows = false;
            dgvMedicalInventory.AllowUserToResizeColumns = false;
            dgvMedicalInventory.AllowUserToResizeRows = false;
            dgvMedicalInventory.GridColor = Color.FromArgb(28, 41, 34);
            dgvMedicalInventory.BorderStyle = BorderStyle.None;
            dgvMedicalInventory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(36, 156, 116);
            dgvMedicalInventory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMedicalInventory.DefaultCellStyle.BackColor = Color.FromArgb(28, 41, 34);
            dgvMedicalInventory.DefaultCellStyle.ForeColor = Color.White;
            dgvMedicalInventory.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 92, 164);
            dgvMedicalInventory.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvMedicalInventory.RowsDefaultCellStyle.BackColor = Color.FromArgb(36, 156, 154);
            dgvMedicalInventory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(36, 156, 116);
            dgvMedicalInventory.BackgroundColor = Color.FromArgb(28, 41, 34);
            dgvMedicalInventory.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            btnAddMedication = CreateButton("Add Medication", Color.FromArgb(76, 175, 80), btnAddMedication_Click);
            btnAddMedication.Location = new System.Drawing.Point(50, dgvMedicalInventory.Bottom + 20);
            btnAddMedication.MouseEnter += Button_MouseEnter;
            btnAddMedication.MouseLeave += Button_MouseLeave;

            btnUpdateMedication = CreateButton("Update Medication", Color.FromArgb(255, 193, 7), btnUpdateMedication_Click);
            btnUpdateMedication.Location = new System.Drawing.Point(btnAddMedication.Right + 10, dgvMedicalInventory.Bottom + 20);
            btnUpdateMedication.MouseEnter += Button_MouseEnter;
            btnUpdateMedication.MouseLeave += Button_MouseLeave;

            btnDeleteMedication = CreateButton("Delete Medication", Color.FromArgb(239, 83, 80), btnDeleteMedication_Click);
            btnDeleteMedication.Location = new System.Drawing.Point(btnUpdateMedication.Right + 10, dgvMedicalInventory.Bottom + 20);
            btnDeleteMedication.MouseEnter += Button_MouseEnter;
            btnDeleteMedication.MouseLeave += Button_MouseLeave;

            lblSearch = new Label();
            lblSearch.Text = "Search: ";
            lblSearch.Font = new Font("Segoe UI", 14);
            lblSearch.Location = new Point(760, 710);
            lblSearch.ForeColor = Color.FromArgb(5, 250, 99);

            txtSearch = CreateTextBox();
            txtSearch.Location = new Point(850, 710);
            txtSearch.Font = new Font("Segoe UI", 14);
            txtSearch.TextChanged += TxtSearch_TextChanged;

            SetAnchorStyles(dgvMedicalInventory, AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom);
            SetAnchorStyles(btnAddMedication, AnchorStyles.Bottom | AnchorStyles.Left);
            SetAnchorStyles(btnUpdateMedication, AnchorStyles.Bottom | AnchorStyles.Left);
            SetAnchorStyles(btnDeleteMedication, AnchorStyles.Bottom | AnchorStyles.Left);
            SetAnchorStyles(txtSearch, AnchorStyles.Top | AnchorStyles.Left);

            Controls.Add(dgvMedicalInventory);
            Controls.Add(btnAddMedication);
            Controls.Add(btnUpdateMedication);
            Controls.Add(btnDeleteMedication);
            Controls.Add(txtSearch);
            Controls.Add(lblSearch);
        }

        private Button CreateButton(string text, Color backColor, EventHandler clickHandler)
        {
            Button button = new Button();
            button.Text = text;
            button.Size = new System.Drawing.Size(150, 40);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = backColor;
            button.ForeColor = Color.White;
            button.Click += clickHandler;
            return button;
        }

        private TextBox CreateTextBox()
        {
            TextBox textBox = new TextBox();
            textBox.Size = new Size(200, 30);
            return textBox;
        }

        private void SetAnchorStyles(Control control, AnchorStyles anchorStyles)
        {
            control.Anchor = anchorStyles;
        }

        private void LoadMedicalInventoryData()
        {
            using (var dbContext = new YourDbContext())
            {
                var medicalInventory = dbContext.MedicalInventories
                    .Select(mi => new
                    {
                        mi.MedicalInventoryId,
                        mi.Medication.Name,
                        mi.QuantityOnHand,
                        mi.ReorderLevel,
                        mi.SupplierInformation
                    })
                    .ToList();

                dgvMedicalInventory.DataSource = ConvertToDataTable(medicalInventory);
            }
        }

        private DataTable ConvertToDataTable<T>(System.Collections.Generic.IEnumerable<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;

                table.Rows.Add(row);
            }
            return table;
        }

        private void btnAddMedication_Click(object sender, EventArgs e)
        {
            CrudForm addOrUpdateMedicationForm = new CrudForm();
            addOrUpdateMedicationForm.ShowDialog();
            LoadMedicalInventoryData();
        }

        private void btnUpdateMedication_Click(object sender, EventArgs e)
        {
            if (dgvMedicalInventory.SelectedRows.Count > 0)
            {
                int selectedInventoryId = Convert.ToInt32(dgvMedicalInventory.SelectedRows[0].Cells["MedicalInventoryId"].Value);

                CrudForm addOrUpdateMedicationForm = new CrudForm(selectedInventoryId);
                addOrUpdateMedicationForm.ShowDialog();
                LoadMedicalInventoryData();
            }
            else
            {
                MessageBox.Show("Please select a medication to update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDeleteMedication_Click(object sender, EventArgs e)
        {
            if (dgvMedicalInventory.SelectedRows.Count > 0)
            {
                int selectedInventoryId = Convert.ToInt32(dgvMedicalInventory.SelectedRows[0].Cells["MedicalInventoryId"].Value);

                DialogResult result = MessageBox.Show("Are you sure you want to delete this medication?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    using (var dbContext = new YourDbContext())
                    {
                        var medicalInventoryItem = dbContext.MedicalInventories.Find(selectedInventoryId);

                        if (medicalInventoryItem != null)
                        {
                            dbContext.MedicalInventories.Remove(medicalInventoryItem);
                            dbContext.SaveChanges();
                            LoadMedicalInventoryData();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a medication to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            LiveSearch();
        }

        private void LiveSearch()
        {
            using (var dbContext = new YourDbContext())
            {
                var searchResult = dbContext.MedicalInventories
                    .Where(mi => mi.Medication.Name.Contains(txtSearch.Text))
                    .Select(mi => new
                    {
                        mi.MedicalInventoryId,
                        mi.Medication.Name,
                        mi.QuantityOnHand,
                        mi.ReorderLevel,
                        mi.SupplierInformation
                    })
                    .ToList();

                dgvMedicalInventory.DataSource = ConvertToDataTable(searchResult);
            }
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                button.BackColor = DarkenColor(button.BackColor, 20);
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                button.BackColor = DarkenColor(button.BackColor, -20);
            }
        }

        private void DgvMedicalInventory_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewCell cell = dgvMedicalInventory.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.Style.BackColor = DarkenColor(cell.Style.BackColor, 20);
            }
        }

        private void DgvMedicalInventory_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewCell cell = dgvMedicalInventory.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.Style.BackColor = DarkenColor(cell.Style.BackColor, -20);
            }
        }

        private Color DarkenColor(Color color, int amount)
        {
            int r = Math.Max(0, Math.Min(255, color.R + amount));
            int g = Math.Max(0, Math.Min(255, color.G + amount));
            int b = Math.Max(0, Math.Min(255, color.B + amount));

            return Color.FromArgb(color.A, r, g, b);
        }
    }
}

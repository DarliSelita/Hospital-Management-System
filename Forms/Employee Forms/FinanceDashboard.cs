using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalManagementSystem.Forms;
using Project.Forms.Billing_Forms;
using Project.Forms.Pharmacy_Forms;

namespace Project.Forms.Employee_Forms
{
    public partial class FinanceDashboard : Form
    {
        public FinanceDashboard(string username)
        {
            InitializeComponent();
            SetUserName(username);

        }

        public void SetUserName(string username)
        {
            // Display the username in the top right corner label
            lblWelcome.Text = $"Hello, {username}!";
        }

        private void FinanceDashboard_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            MedicalInventoryForm mv = new MedicalInventoryForm();
            mv.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            ViewInvoiceForm iv = new ViewInvoiceForm();
            iv.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You do not have permission to access this feature.");

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You do not have permission to access this feature.");

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You do not have permission to access this feature.");

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You do not have permission to access this feature.");

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to Sign Out?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                LogIn lg = new LogIn();
                lg.Show();
            }
        }
    }
}

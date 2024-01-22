using HospitalManagementSystem.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Project.Forms.Employee_Forms
{
    public partial class LogIn : Form
    {
        private AuthService _authService;

        public LogIn()
        {
            InitializeComponent();
            _authService = new AuthService(new YourDbContext());

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            User authenticatedUser = _authService.AuthenticateUser(username, password);

            if (authenticatedUser != null)
            {
                System.Windows.Forms.MessageBox.Show($"Welcome, {authenticatedUser.Username}!");

                Loading_Screen loadingScreen = new Loading_Screen();
                loadingScreen.Show();

                // Simulate some loading process using Task.Delay (replace with actual loading logic)
                await Task.Delay(10000);

                // Close the loading screen
                loadingScreen.Hide();

                // Open the dashboard after loading
                OpenDashboard(authenticatedUser.RoleId, authenticatedUser.Username);
            
        }
            else
            {
                System.Windows.Forms.MessageBox.Show("Invalid username or password. Please try again.");
            
}

        }


            private void label2_Click(object sender, EventArgs e)
            {
                System.Windows.Forms.Application.Exit();
            }

            private void textBox2_TextChanged(object sender, EventArgs e)
            {

            }

            private void OpenDashboard(int roleId, string username)
            {
                Form dashboardForm = null;

                switch (roleId)
                {
                    case 1:
                        // Admin Dashboard
                        dashboardForm = new AdminDashboard(username);
                        break;
                    case 2:
                        // Doctor Dashboard
                        dashboardForm = new DoctorDashboard(username);
                        break;
                    case 3:
                        // Receptionist Dashboard
                        dashboardForm = new ReceptionistDashboard(username);
                        break;
                    case 4:
                        // Finance Dashboard
                        dashboardForm = new FinanceDashboard(username);
                        break;
                    default:
                        // Handle unknown role (optional)
                        System.Windows.Forms.MessageBox.Show("Unknown role. Cannot open dashboard.");
                        return; // Return without hiding the login form
                }

                // Open the selected dashboard form
                dashboardForm.Show();

                // Close the current login form
                this.Hide();
            }



        }
    }

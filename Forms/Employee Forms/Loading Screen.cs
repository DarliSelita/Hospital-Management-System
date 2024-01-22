using System;
using System.Windows.Forms;

namespace Project.Forms.Employee_Forms
{
    public partial class Loading_Screen : Form
    {
        private int startpoint = 0;

        public Loading_Screen()
        {
            InitializeComponent();
        }

        public void StartLoading()
        {
            startpoint = 0;
            progressBar1.Value = 0;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            startpoint += 1;
            progressBar1.Value = startpoint;

            // Set the desired duration in milliseconds (e.g., 3000 milliseconds = 3 seconds)
            int desiredDuration = 10000;

            if (startpoint >= progressBar1.Maximum || startpoint * timer1.Interval >= desiredDuration)
            {
                timer1.Stop();
                this.Hide();
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            StartLoading();  // Start loading when the progress bar is clicked
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

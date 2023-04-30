using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_challanApp
{
    public partial class AdminPannel : Form
    {
        public AdminPannel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                this.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ManageOfficer MO1 = new ManageOfficer();
            MO1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChallanHistory CH1 = new ChallanHistory();
            CH1.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OfficerDetails OD1 = new OfficerDetails();
            OD1.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AdminLogin AdLog = new AdminLogin();
            AdLog.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            MainScreen MS2 = new MainScreen();
            MS2.Show();
            this.Hide();
        }
    }
}

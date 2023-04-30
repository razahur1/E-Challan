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
    public partial class OfficerPannel : Form
    {
        public OfficerPannel()
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            MainScreen MS2 = new MainScreen();
            MS2.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ScanFace scan = new ScanFace();
            scan.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OfficerChallanHistory OCH = new OfficerChallanHistory();
            OCH.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OfficerLogin login = new OfficerLogin();
            login.Show();
            this.Hide();
        }
    }
}

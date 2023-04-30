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
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            panel2.Width += 3;

            if(panel2.Width >= 374)
            {
                timer1.Enabled = false;
                MainScreen MS1 = new MainScreen();
                MS1.Show();
                this.Hide();
            }
        }
    }
}

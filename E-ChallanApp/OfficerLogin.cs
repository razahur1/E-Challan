using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace E_challanApp
{
    public partial class OfficerLogin : Form
    {
        SqlCommand cmd;
        SqlDataReader dr;

        public static string ofname;
        

        public OfficerLogin()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=E-Challan;Integrated Security=True");

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
            MainScreen MS1 = new MainScreen();
            MS1.Show();
            this.Hide();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox2.PasswordChar = '*';
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            try
            {

                string query = "Select * from Officer_Info where Of_Id = '" + textBox1.Text + "' AND Of_Pass = '" + textBox2.Text + "'";
                con.Open();
                cmd = new SqlCommand(query, con);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    ofname = dr.GetValue(1).ToString();

                    MessageBox.Show("Login Successfully..");

                    OfficerPannel Pannel = new OfficerPannel();
                    Pannel.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect id or password...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Clear();
                    textBox2.Clear();
                }

            }
            catch
            {
                MessageBox.Show("Invalid Entered...", "Error");
            }
            finally
            {
                con.Close();
            }
        }

        private void OfficerLogin_Load(object sender, EventArgs e)
        {

            textBox1.Select(0, 0);
            textBox2.Select(0, 0);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

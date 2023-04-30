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
    public partial class OfficerDetails : Form
    {
         public OfficerDetails()
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
            AdminPannel Ad = new AdminPannel();
            Ad.Show();
            this.Hide();
        }

        private void OfficerDetails_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "Select * from Officer_Info order by Of_Id ASC";
                ExecuteGridCmd(query);
            }
            catch
            {
                MessageBox.Show("Error Occured...", "Error");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    string query = "Select * from Officer_Info order by Of_Id ASC";
                    ExecuteGridCmd(query);
                    ColName();
                }
                else
                {
                    string query = "Select * from Officer_Info Where '" + textBox1.Text + "' in (Of_Id,Of_Name,Of_Pass,Of_Cnic,Of_Email,Of_Phone) order by Of_Id ASC";

                    ExecuteGridCmd(query);
                    ColName();
                }
            }
            catch
            {
                MessageBox.Show("Error Occured...", "Error");
            }
        }

        public void ExecuteGridCmd(string Query)
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void ColName()
        {
            dataGridView1.Columns[0].HeaderCell.Value = "ID";
            dataGridView1.Columns[1].HeaderCell.Value = "NAME";
            dataGridView1.Columns[2].HeaderCell.Value = "PASSWORD";
            dataGridView1.Columns[3].HeaderCell.Value = "CNIC NO.";
            dataGridView1.Columns[4].HeaderCell.Value = "EMAIL";
            dataGridView1.Columns[5].HeaderCell.Value = "PHONE NO.";
        }
    }
}

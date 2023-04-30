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
    public partial class OfficerChallanHistory : Form
    {
        public OfficerChallanHistory()
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
            OfficerPannel Pannel = new OfficerPannel();
            Pannel.Show();
            this.Hide();
        }

        private void OfficerChallanHistory_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "Select * from Challan_Info order by Challan_No ASC";
                
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
                    string query = "Select * from Challan_Info order by Challan_No ASC";
                    ExecuteGridCmd(query);
                    ColName();
                }
                else
                {
                    string query = "Select * from Challan_Info Where '" + textBox1.Text + "' in (C_ID,Challan_No,C_Name,IssuanceDate,ChallanBy,TrafficSection,VehicleType,VehicleNo,ViolationType,ChallanAmmount,ServiceCharges) order by Challan_No";
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
            dataGridView1.Columns[1].HeaderCell.Value = "C Id";
            dataGridView1.Columns[0].HeaderCell.Value = "Challan No";
            dataGridView1.Columns[2].HeaderCell.Value = "Challan Name";
            dataGridView1.Columns[3].HeaderCell.Value = "Issue Date";
            dataGridView1.Columns[4].HeaderCell.Value = "Challan By";
            dataGridView1.Columns[5].HeaderCell.Value = "Traffic Section";
            dataGridView1.Columns[6].HeaderCell.Value = "Vehicle Type";
            dataGridView1.Columns[7].HeaderCell.Value = "Vehicle No";
            dataGridView1.Columns[8].HeaderCell.Value = "Violation Type";
            dataGridView1.Columns[9].HeaderCell.Value = "Challan Amount";
            dataGridView1.Columns[10].HeaderCell.Value = "Service Charges";
        }
    }
}

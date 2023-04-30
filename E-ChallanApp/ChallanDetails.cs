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
    public partial class ChallanDetails : Form
    {
        SqlCommand cmd;
        SqlDataReader dr;
        int amount = 0;
        int charges = 0;
        string cid;

        public ChallanDetails()
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
            OfficerPannel ofp = new OfficerPannel();
            ofp.Show();
            this.Hide();
        }

        private void ChallanDetails_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;

            comboBox5.Items.Insert(0,"MotorCycle");
            comboBox5.Items.Insert(1, "MotorCar");

            textBox1.ReadOnly = true;

            textBox3.Text = ScanFace.cname;
            textBox3.ReadOnly = true;

            textBox4.Text = OfficerLogin.ofname;
            textBox4.ReadOnly = true;

            cid = ScanFace.cid;
            textBox7.Text = cid;

             string query = "Select * from Challan_Info order by Challan_No ASC";
             con.Open();
             SqlDataAdapter da = new SqlDataAdapter(query, con);
              DataTable dt = new DataTable();
              da.Fill(dt);
              int cno = dt.Rows.Count + 101;
              textBox1.Text = cno.ToString();
              con.Close();

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox6.Items.Clear();

            if (comboBox5.SelectedItem.ToString() == "MotorCycle")
            {
                comboBox6.Items.Insert(0, "Voilation of Traffic Signal");
                comboBox6.Items.Insert(1, "Driving Wrong Way");
                comboBox6.Items.Insert(2, "Unregistered Vehicle");
                comboBox6.Items.Insert(3, "Fancy Number plate");
                comboBox6.Items.Insert(4, "Driving without driving license.");
                comboBox6.Items.Insert(5, "Riding Without Helmet");
                comboBox6.Items.Insert(6, "One Wheeling of Motorcycle");
            }
            else if (comboBox5.SelectedItem.ToString() == "MotorCar")
            {
                comboBox6.Items.Insert(0, "Voilation of Traffic Signal");
                comboBox6.Items.Insert(1, "Driving Wrong Way");
                comboBox6.Items.Insert(2, "Unregistered Vehicle");
                comboBox6.Items.Insert(3, "Fancy Number plate");
                comboBox6.Items.Insert(4, "Driving without driving license.");
                comboBox6.Items.Insert(5, "Driving Without Seat Belt");
                comboBox6.Items.Insert(6, "Driving Vehicle with Tinted glasses");
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedIndex == 0)
            {
                if (comboBox6.SelectedIndex == 0)
                {
                    amount = 400;
                }
                else if (comboBox6.SelectedIndex == 1)
                {
                    amount = 150;
                }
                else if (comboBox6.SelectedIndex == 2)
                {
                    amount = 500;
                }
                else if (comboBox6.SelectedIndex == 3)
                {
                    amount = 500;
                }
                else if (comboBox6.SelectedIndex == 4)
                {
                    amount = 500;
                }
                else if (comboBox6.SelectedIndex == 5)
                {
                    amount = 150;
                }
                else if (comboBox6.SelectedIndex == 6)
                {
                    amount = 500;
                }

                charges = 20;
            }
            else if (comboBox5.SelectedIndex == 1)
            {
                if (comboBox6.SelectedIndex == 0)
                {
                    amount = 500;
                }
                else if (comboBox6.SelectedIndex == 1)
                {
                    amount = 300;
                }
                else if (comboBox6.SelectedIndex == 2)
                {
                    amount = 1000;
                }
                else if (comboBox6.SelectedIndex == 3)
                {
                    amount = 1000;
                }
                else if (comboBox6.SelectedIndex == 4)
                {
                    amount = 700;
                }
                else if (comboBox6.SelectedIndex == 5)
                {
                    amount = 500;
                }
                else if (comboBox6.SelectedIndex == 6)
                {
                    amount = 800;
                }
                charges = 50;
            }
            else
            {
                textBox5.Clear();
                textBox6.Clear();
            }

            textBox5.Text = amount.ToString();
            textBox6.Text = charges.ToString();

            textBox5.ReadOnly = true;
            textBox6.ReadOnly = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == string.Empty || comboBox5.SelectedItem.ToString() == string.Empty || comboBox6.SelectedItem.ToString() == string.Empty || comboBox2.SelectedItem.ToString() == string.Empty)
            {
                MessageBox.Show("Fill all fields....");
            }
            else
            {
                try
                {
                    string query = "Insert into Challan_Info values (@cid,@name,@date,@challanby,@section,@type,@Vno,@violType,@amount,@tax)";
                   
                    con.Open();

                    cmd = new SqlCommand(query, con);
                     
                    cmd.Parameters.AddWithValue("@cid", textBox7.Text);
                    cmd.Parameters.AddWithValue("@name", textBox3.Text);
                    cmd.Parameters.AddWithValue("@date", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@challanby", textBox4.Text);
                    cmd.Parameters.AddWithValue("@section", comboBox2.SelectedItem);
                    cmd.Parameters.AddWithValue("@type", comboBox5.SelectedItem);
                    cmd.Parameters.AddWithValue("@Vno", textBox2.Text);
                    cmd.Parameters.AddWithValue("@violType", comboBox6.SelectedItem);
                    cmd.Parameters.AddWithValue("@amount", textBox5.Text);
                    cmd.Parameters.AddWithValue("@tax", textBox6.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Challan Created Successfully..");

                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error Occured...  " + ex.ToString() );
                }
                finally
                {
                    con.Close();
                }
            }
        }

    }
}

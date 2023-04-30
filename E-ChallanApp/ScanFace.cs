
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using FaceRecognition;

namespace E_challanApp
{
    public partial class ScanFace : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=E-Challan;Integrated Security=True");
        FaceRec faceRec = new FaceRec();
        public static string cname;
        public static string cid;

        public ScanFace()
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
            OfficerPannel Pannel = new OfficerPannel();
            Pannel.Show();
            this.Hide();
        }
        private void txtname_Click(object sender, EventArgs e)
        {
            txtname.Clear();
        }

        private void txtcnic_Click(object sender, EventArgs e)
        {
            txtcnic.Clear();
        }

        private void txtlicno_Click(object sender, EventArgs e)
        {
            txtlicno.Clear();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            faceRec.openCamera(pictureBox3, pictureBox2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                txtcnic.ReadOnly = true;
                txtlicno.ReadOnly = true;

                if(txtname.Text == string.Empty ||txtname.Text == "Name")
                {
                    MessageBox.Show("Fill all the fields...");
                }
                else
                {
                    faceRec.Save_IMAGE(txtname.Text);
                    AddFaceInUnder(pictureBox3.Image, txtname.Text);
                    
                }
            }
            else if(radioButton2.Checked == true)
            {
                int status = ValidateCnic(txtcnic.Text);

                if (txtname.Text == string.Empty || txtname.Text == "Name" || txtcnic.Text == string.Empty || txtcnic.Text == "Cnic No" || txtlicno.Text == string.Empty || txtlicno.Text == "License No")
                {
                    MessageBox.Show("Fill all the fields...");
                }
                else if (status == 1)
                {
                    MessageBox.Show("Entered CNIC Number is Not Valid.");
                }
                else
                {
                    faceRec.Save_IMAGE(txtname.Text);
                    AddFaceInAbove(pictureBox3.Image, txtname.Text, txtcnic.Text, txtlicno.Text);
                   
                }
            }
            else
            {
                MessageBox.Show("Select the category first...");
            }
        
        }

        private void button4_Click(object sender, EventArgs e)
        {

            
            faceRec.isTrained = true;
            faceRec.getPersonName(label6);
            GetData(label6.Text);
        }

        private byte[] ConvertToDBFormat(Image InputImage)
        {
            Bitmap BmpImage = new Bitmap(InputImage);
            MemoryStream stream = new MemoryStream();
            BmpImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] ImageAsBytes = stream.ToArray();
            return ImageAsBytes;
        }

        private void AddFaceInUnder(Image InputFace, string Name)
        {

            byte[] FaceAsBytes = ConvertToDBFormat(InputFace);
            con.Open();
            string query = "insert into Under18 values (@FaceImg,'" + Name + "')";
            
            SqlCommand cmd = new SqlCommand(query, con);
            SqlParameter imageParameter = cmd.Parameters.AddWithValue("@FaceImg", SqlDbType.Binary);
            imageParameter.Value = FaceAsBytes;
            imageParameter.Size = FaceAsBytes.Length;
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Record Inserted in Under 18");
                FillAll();
            }
            else
            {
                MessageBox.Show("Fill all the feilds");
            }
            con.Close();

         }

        private void AddFaceInAbove(Image InputFace, string Name, string nic, string licno)
        {
            con.Open();
            byte[] FaceAsBytes = ConvertToDBFormat(InputFace);
            string query1 = "insert into Above18 values (@FaceImg , '" + Name + "','" + nic + "','" + licno + "')";
            SqlCommand cmd = new SqlCommand(query1, con);
            SqlParameter imageParameter = cmd.Parameters.AddWithValue("@FaceImg", SqlDbType.Binary);
            imageParameter.Value = FaceAsBytes;
            imageParameter.Size = FaceAsBytes.Length;
            int a = cmd.ExecuteNonQuery();
            
            if (a > 0)
            {
                MessageBox.Show("Record Inserted in Above 18");
                FillAll();
            }
            else
            {
                    MessageBox.Show("Fill all the feilds");
            }
            con.Close();
        }


        private void GetData(string name)
        {
            con.Open();
            string query1 = "select C_ID,C_Name, C_Cnic, C_LicenceNo  from Above18 where C_Name = '" + name + "' ";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            SqlDataReader DR1 = cmd1.ExecuteReader();
            
            if (DR1.Read())
            {
                cid = DR1.GetValue(0).ToString();
                txtname.Text = DR1.GetValue(1).ToString();
                txtcnic.Text = DR1.GetValue(2).ToString();
                txtlicno.Text = DR1.GetValue(3).ToString();
                label5.Text = "Record Found in Above 18";
                con.Close();
            }
            else
            {
                con.Close();
                SearchInUnder(name);
            }
            
        }

        private void SearchInUnder(string name)
        {
            con.Open();
            string query = "select C_id , C_Name from Under18 where C_Name = '" + name + "' ";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader DR1 = cmd.ExecuteReader();

            if (DR1.Read())
            {
                cid = DR1.GetValue(0).ToString();
                txtname.Text = DR1.GetValue(1).ToString();
                label5.Text = "Record Found in Under 18";
            }
            else 
            {
                label5.Text = "Record Not Found";
            }
            con.Close();
        }

        public int ValidateCnic(string cnic)
        {
            System.Text.RegularExpressions.Regex rcnic = new
            System.Text.RegularExpressions.Regex(@"^[0-9]{5}-[0-9]{7}-[0-9]$");

            if (!rcnic.IsMatch(cnic))
            {
                return 1;
            }
            else return 0;

        }

        private void FillAll()
        {
            txtname.Text = "Name";
            txtcnic.Text = "Cnic No";
            txtlicno.Text = "License No";
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            cname = txtname.Text;

            ChallanDetails chd = new ChallanDetails();
            chd.Show();
            this.Hide();
        }

        private void ScanFace_Load(object sender, EventArgs e)
        {
            txtname.Select(0, 0);
            txtcnic.Select(0, 0);
            txtlicno.Select(0, 0);
        }
    }
}

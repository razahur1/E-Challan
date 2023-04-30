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
using System.Text.RegularExpressions;

namespace E_challanApp
{
    public partial class ManageOfficer : Form
    {
        SqlCommand cmd;
        SqlDataReader dr;
        public ManageOfficer()
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

        private void txt_id_Click(object sender, EventArgs e)
        {
            txt_id.Clear();
        }

        private void txt_name_Click(object sender, EventArgs e)
        {
            txt_name.Clear();
        }

        private void txt_pass_Click(object sender, EventArgs e)
        {
            txt_pass.Clear();
        }

        private void txt_cnic_Click(object sender, EventArgs e)
        {
            txt_cnic.Clear();
        }

        private void txt_email_Click(object sender, EventArgs e)
        {
            txt_email.Clear();
        }

        private void txt_phone_Click(object sender, EventArgs e)
        {
            txt_phone.Clear();
        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            try
            {

                string query = "Select * from Officer_info where Of_Id=@id";
                con.Open();
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", txt_id.Text);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txt_name.Text = dr.GetValue(1).ToString();
                    txt_pass.Text = dr.GetValue(2).ToString();
                    txt_cnic.Text = dr.GetValue(3).ToString();
                    txt_email.Text = dr.GetValue(4).ToString();
                    txt_phone.Text = dr.GetValue(5).ToString();

                }
                else
                {
                    MessageBox.Show("Record Not Found...");
                }
            }
            catch

            {
                MessageBox.Show("Error Occured...");
            }
            finally
            {
                con.Close();
            }
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            int C_status = ValidateCnic(txt_cnic.Text);
            int E_status = ValidateEmail(txt_email.Text);
            int P_status = ValidatePhone(txt_phone.Text);


            if ((txt_id.Text == string.Empty) || (txt_name.Text == string.Empty) || (txt_pass.Text == string.Empty) || (txt_cnic.Text == string.Empty) || (txt_email.Text == string.Empty) || (txt_phone.Text == string.Empty) || (txt_name.Text == "Name") || (txt_pass.Text == "Password") || (txt_cnic.Text == "Cnic") || (txt_email.Text == "Email") || (txt_phone.Text == "Phone"))
            {
                MessageBox.Show("Fill all the fields...");
            }
            else if (C_status == 1)
            {
                MessageBox.Show("Entered CNIC Number is Not Valid.");
            }
            else if (E_status == 1)
            {
                MessageBox.Show("Entered Email is Not Valid.");
            }
            else if (P_status == 1)
            {
                MessageBox.Show("Entered Phone Number is Not Valid.");
            }
            else
            {
                try
                {
                    string query = "Insert into Officer_info values (@id,@name,@pass,@cnic,@email,@phone);";
                    query += "Insert into Officer_Login values (@id,@pass)";

                    con.Open();

                    cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@id", txt_id.Text);
                    cmd.Parameters.AddWithValue("@name", txt_name.Text);
                    cmd.Parameters.AddWithValue("@pass", txt_pass.Text);
                    cmd.Parameters.AddWithValue("@cnic", txt_cnic.Text);
                    cmd.Parameters.AddWithValue("@email", txt_email.Text);
                    cmd.Parameters.AddWithValue("@phone", txt_phone.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record inserted Successfully..");
                    FillAll();
                }
                catch
                {
                    MessageBox.Show("Error Occured...");
                }
                finally
                {
                    con.Close();
                }

            }            
        }

        private void update_btn_Click(object sender, EventArgs e)
        {
            int C_status = ValidateCnic(txt_cnic.Text);
            int E_status = ValidateEmail(txt_email.Text);
            int P_status = ValidatePhone(txt_phone.Text);


            if ((txt_id.Text == string.Empty) || (txt_name.Text == string.Empty) || (txt_pass.Text == string.Empty) || (txt_cnic.Text == string.Empty) || (txt_email.Text == string.Empty) || (txt_phone.Text == string.Empty) || (txt_name.Text == "Name") || (txt_pass.Text == "Password") || (txt_cnic.Text == "Cnic") || (txt_email.Text == "Email") || (txt_phone.Text == "Phone"))
            {
                MessageBox.Show("Fill all the fields...");
            }
            else if (C_status == 1)
            {
                MessageBox.Show("Entered CNIC Number is Not Valid.");
            }
            else if (E_status == 1)
            {
                MessageBox.Show("Entered Email is Not Valid.");
            }
            else if (P_status == 1)
            {
                MessageBox.Show("Entered Phone Number is Not Valid.");
            }
            else
            {
                try
                {

                    string query = "Update Officer_Info set Of_Name=@name, Of_Pass=@pass, Of_Cnic=@cnic, Of_Email=@email, Of_Phone=@phone  where Of_Id = @id;";
                    query += "Update Officer_Login set Of_Pass=@pass where Of_Id = @id";

                    con.Open();
                    cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@id", txt_id.Text);
                    cmd.Parameters.AddWithValue("@name", txt_name.Text);
                    cmd.Parameters.AddWithValue("@pass", txt_pass.Text);
                    cmd.Parameters.AddWithValue("@cnic", txt_cnic.Text);
                    cmd.Parameters.AddWithValue("@email", txt_email.Text);
                    cmd.Parameters.AddWithValue("@phone", txt_phone.Text);

                    int a = cmd.ExecuteNonQuery();

                    if (a > 1)
                    {
                        MessageBox.Show("Record Updated Successfully..");
                    }
                    else
                    {
                        MessageBox.Show("Record Not Updated...");
                    }
                    FillAll();
                }
                catch
                {
                    MessageBox.Show("Error Occured...");
                }
                finally
                {
                    con.Close();
                }
            }

        }

        private void delt_btn_Click(object sender, EventArgs e)
        {

            try
            {

                string query = "Delete from Officer_Info where Of_Id = @id;";
                query += "Delete from Officer_Login where Of_Id = @id";

                con.Open();
                cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@id", txt_id.Text);

                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    MessageBox.Show("Record Deleted Successfully..");
                }
                else
                {
                    MessageBox.Show("Record Not Deleted...");
                }
                FillAll();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error Occured... /n" + ex);
            }
            finally
            {
                con.Close();
            }
        }

        private void ClearAll()
        {
            txt_id.Clear();
            txt_name.Clear();
            txt_pass.Clear();
            txt_cnic.Clear();
            txt_email.Clear();
            txt_phone.Clear();
        }

        private void FillAll()
        {
            txt_id.Text = "Id";
            txt_name.Text = "Name";
            txt_pass.Text = "Password";
            txt_cnic.Text = "Cnic";
            txt_email.Text = "Email";
            txt_phone.Text = "Phone";
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
        public int ValidateEmail(string email)
        {
            System.Text.RegularExpressions.Regex remail = new
            System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            
            if (!remail.IsMatch(email))
            {
                return 1;
            }
            else return 0;
        }
        public int ValidatePhone(string phone)
        {
            System.Text.RegularExpressions.Regex rphone = new
            System.Text.RegularExpressions.Regex(@"^[\d]{4}-[\d]{7}$");
            
            if (!rphone.IsMatch(phone))
            {
                return 1;
            }
            else return 0;

        }
        
    }
}
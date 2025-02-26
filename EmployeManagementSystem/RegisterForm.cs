using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace EmployeManagementSystem
{
    public partial class RegisterForm : Form
    {
        SqlConnection connect = new SqlConnection(@"Server=LAPTOP-JF5NT7O3\SQLEXPRESS02;Database=EmployeeManagementDB;User Id=sa;Password=12345;TrustServerCertificate=true;");
        public RegisterForm()
        {
            InitializeComponent();  
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void signup_loginBtn_Click(object sender, EventArgs e)
        {
            RegisterForm RegForm = new RegisterForm();
            RegForm.Show();
            this.Hide();

          }

        private void signup_showPass_CheckedChanged(object sender, EventArgs e)
        {
            signup_password.PasswordChar = signup_showPass.Checked ? '*' : '\0';  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Butona tıklanma işlemleri burada olacak
        }

        private void signup_btn_Click(object sender, EventArgs e)
        {
            //validation
            var usname = signup_username.Text;
            var passw = signup_password.Text;

            if (String.IsNullOrEmpty(usname) || String.IsNullOrEmpty(passw))
            {
                MessageBox.Show("please fill all blank fields", "Error Mesage", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {             
                if (connect.State != ConnectionState.Open)
                {
                    try
                    {
                        
                        connect.Open();
                        string selectUsername = "SELECT COUNT(id) FROM users WHERE username = @user";

                        using (SqlCommand checkUsers = new SqlCommand(selectUsername, connect))
                        {  
                            
                            checkUsers.Parameters.AddWithValue("@user",signup_username.Text.Trim());  
                            
                            int count = (int)checkUsers.ExecuteScalar();
                            if (count >= 1) 
                            {
                                MessageBox.Show(signup_username.Text.Trim()+"is allready", "Error Mesage", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            else
                            {
                                DateTime today = DateTime.Today;
                                
                                string insertData = "INSERT INTO useres" +
                                    "(username,password,date_register) " +
                                    "VALUES(@username,@password,@dateReg)";
                                using (SqlCommand cmd = new SqlCommand(insertData, connect))
                                {
                                    cmd.Parameters.AddWithValue("@username", signup_username.Text.Trim()); 
                                    cmd.Parameters.AddWithValue("@passaport", signup_password.Text.Trim());
                                    cmd.Parameters.AddWithValue("@dateReg", today);                 
                                     
                                    cmd.ExecuteNonQuery();

                                    MessageBox.Show("register successfull"
                                        , "Information Messasge", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    Form1 loginForm = new Form1();
                                    loginForm.ShowDialog();
                                    this.Hide();    
                                }
                            }
                           
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error : " + ex, "Error Mesage", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
        }
    }
}

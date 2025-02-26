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
    public partial class Form1 : Form
    {
        //SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ömer faruk\Documents\employee.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True");
        
        SqlConnection connect = new SqlConnection(@"Server=LAPTOP-JF5NT7O3\SQLEXPRESS02;Database=EmployeeManagementDB;User Id=sa;Password=12345;TrustServerCertificate=true;");
        public Form1()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void login_showPass_CheckedChanged(object sender, EventArgs e)
        {
            login_password.Text = login_showPass.Checked ? "***********" : "login_password.Text";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // Butona tıklanma işlemleri burada olacak
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            var usname = login_username.Text;
            var passw = login_password.Text;

            if (String.IsNullOrEmpty(usname) || String.IsNullOrEmpty(passw)) 
            {
                MessageBox.Show("please fill all blank fields", "Error Mesage", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
            else
            {   
                if (connect.State == ConnectionState.Closed)
                {
                    try
                    {
                        connect.Open();
                        string selectData = "SELECT * FROM users WHERE username = @username AND password=@password";
                        using (SqlCommand cmd = new SqlCommand(selectData, connect))
                        {
                            cmd.Parameters.AddWithValue("@username",login_username.Text.Trim());  
                            cmd.Parameters.AddWithValue("@Password",login_password.Text.Trim());
                           

                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            if (table.Rows.Count > 0)
                            {

                                MessageBox.Show("login succesfully","information message",MessageBoxButtons.OK, MessageBoxIcon.Information); ;

                                MainForm mForm = new MainForm();
                                mForm.Show();
                                this.Hide();    


                            }
                            else
                            {
                                MessageBox.Show("Incorrect Username/Password"
                                    , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); 
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

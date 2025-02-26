using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Contexts;



namespace EmployeManagementSystem
{
    public partial class Dasboard : UserControl
    {

        SqlConnection connect = new SqlConnection(@"Server=LAPTOP-JF5NT7O3\SQLEXPRESS02;Database=EmployeeManagementDB;User Id=sa;Password=12345;TrustServerCertificate=true;");
        public Dasboard()
        {
            InitializeComponent();
            displayIE();
            displayTA();
            displayTE();
          
        }
       
        public void RefreshData
            ()

        public void displayTE()
        {
            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT COUNT(id) FROM employees WHERE delete_date IS NULL ";

                    using (SqlCommand cmd = new SqlCommand(selectData))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int count = Convert.ToInt32(reader[0]);
                            dashboard_TE.Text= count.ToString();
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex, "Error Message",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally 
                {  
                    connect.Close();
                }
            }
        }
        public void displayTA()
        { 
         if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT COUNT(id) FROM employees WHERE Status =@status"+
                        " delete_date IS NULL ";

                    using (SqlCommand cmd = new SqlCommand(selectData))
                    {
                        cmd.Parameters.AddWithValue("@status", "Active");


                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int count = Convert.ToInt32(reader[0]);
                             label2.Text= count.ToString();
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex, "Error Message",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally 
                {
                 connect.Close();
                }
         }   
      
        }

        public void displayIE()
        {
            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT COUNT(id) FROM employees WHERE Status =@status" +
                        " delete_date IS NULL ";

                    using (SqlCommand cmd = new SqlCommand(selectData))
                    {
                        cmd.Parameters.AddWithValue("@status", "Inactive");


                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int count = Convert.ToInt32(reader[0]);
                            label3.Text = count.ToString();
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex, "Error Message",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }

        }

        private void Dasboard_Load(object sender, EventArgs e)
        {

        }
    }

}



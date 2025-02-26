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
using System.Diagnostics.Eventing.Reader;




namespace EmployeManagementSystem
{
    public partial class Salary : UserControl
    {

        SqlConnection connect = new SqlConnection(@"Server=LAPTOP-JF5NT7O3\SQLEXPRESS02;Database=EmployeeManagementDB;User Id=sa;Password=12345;TrustServerCertificate=true;");
        public Salary()
        {
            InitializeComponent();
            displayEmployees();
            disableFields();
        }

        public void disableFields()
        {
            salary_employeeID.Enabled = false;
            salary_name.Enabled = false;
            salary_position.Enabled = false;

        }



        public void displayEmployees()
        {
            SalaryData ed = new SalaryData();
            List<SalaryData> listData = ed.salaryEmployeeListData();
            {


                dataGridView1.DataSource = listData;

            }
            //  private void salary_updateBtn_Click(object sender, EventArgs e)
            {

            }
        }

        private void Salary_Load(object sender, EventArgs e)
        {

        }
        public void clearFields() 
        {
            salary_employeeID.Text = "";
            salary_name.Text = "";
            salary_position.Text = "";
            salary_salary.Text = "";


        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                salary_employeeID.Text = row.Cells[0].Value.ToString();
                salary_name.Text = row.Cells[1].Value.ToString();
                salary_position.Text = row.Cells[4].Value.ToString();
                salary_salary.Text = row.Cells[5].Value.ToString();


            }
        }

        private void salary_updateBtn_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(salary_employeeID.Text) || String.IsNullOrEmpty(salary_name.Text) || String.IsNullOrEmpty(salary_position.Text) || String.IsNullOrEmpty(salary_salary.Text))
            {
                MessageBox.Show("please fill all blankfields ", "error message ", MessageBoxButtons
                    .OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult check = MessageBox.Show("are you sure update employe ID:"
                + salary_employeeID.Text.Trim() + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (check == DialogResult.Yes)
                {
                    if (connect.State == ConnectionState.Closed)
                    {
                        try
                        {
                            connect.Open();
                            DateTime today = DateTime.Today;

                            String updateData = "UPDATE employees SET salary =@salary,update_date= WHERE employee_id = @employeeID";

                            using (SqlCommand cmd = new SqlCommand(updateData, connect))
                            {
                                cmd.Parameters.AddWithValue("@Salary", salary_salary.Text.Trim());
                                cmd.Parameters.AddWithValue("@updateData", today);
                                cmd.Parameters.AddWithValue("employeeID", salary_employeeID.Text.Trim());

                                cmd.ExecuteNonQuery();

                                displayEmployees();

                                MessageBox.Show("Update succesfully", "Information message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                clearFields();
                               // Diğer kodlar burada yer alabilir.
                            }
                        
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("cancelled ıd ", "Error  message ", MessageBoxButtons
                     .OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            connect.Close();
                        }

                    }
                    else
                    {
                        MessageBox.Show("cancelled ıd ", "ınformation message ", MessageBoxButtons
                        .OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void salary_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields(); 
        }
    }
}
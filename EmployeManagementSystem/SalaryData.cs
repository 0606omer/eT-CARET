using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace EmployeManagementSystem
{
    internal class SalaryData
    {
       
        public string Name { get; set; } //1 
        public string contact { get; set; }

        public string gender { get; set; }
        public int EmployeeID { get; set; } //0
        public string Position { get; set; } //2
        public int Salary { get; set; } //3
       


        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ömer faruk\Documents\[EmployeeManagementDB].mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True");

        public List<SalaryData> salaryEmployeeListData()
        {
            List<SalaryData> ListData = new List<SalaryData>();

            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT * FROM employee WHERE delete_date IS NULL ";
                    
                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            SalaryData sd = new SalaryData();

                            sd.EmployeeID = reader["employee_id"].ToString().Length;
                            sd.Name = reader["full_name"].ToString();     
                            sd.gender= reader[gender].ToString();
                            sd.Position = reader["position"].ToString();
                            sd.contact =reader["contact_number"].ToString();
                            sd.Position = reader["position"].ToString();
                            sd.Salary = (int)reader["salary"];


                            ListData.Add(sd);
                        }


                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error" + ex);
                }
                finally
                {
                    connect.Close();
                }
            }
            return ListData;


        }


    }
}

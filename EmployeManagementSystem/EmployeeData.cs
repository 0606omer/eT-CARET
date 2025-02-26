using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EmployeManagementSystem
{
    //DAL : Data Access Layer
    public class EmployeeData 
    {
       
        public int ID {  get; set; } //0
        public string Name { get; set; } //2
        public string  Gender { get; set; } //3
        public int EmployeeID { get; set; } //1
        public string Contact {  get; set; } //4

        public string Position { get; set; } //5
        
        public string Image { get; set; } //6
        public string Address { get; set; }

        public int Salary { get; set; } //7
        public string status { get; set; } //8
        
        
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ömer faruk\Documents\[EmployeeManagementDB].mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True");

        public List<EmployeeData> employeListData()
        {
            List<EmployeeData> ListData = new List<EmployeeData>();

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
                            EmployeeData ed = new EmployeeData(); 
                         
                            ed.EmployeeID = reader["employee_id"].ToString().Length;
                            ed.Name = reader["full_name"].ToString();                                                    
                            ed.Contact = reader["contact_number"].ToString() ;
                            ed.Position = reader ["position"].ToString();                
                            ed.Salary = (int)reader["salary"];
                        
                           
                            ListData.Add(ed); 
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

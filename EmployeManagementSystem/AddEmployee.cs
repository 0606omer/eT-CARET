using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Diagnostics.Eventing.Reader;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;


namespace EmployeManagementSystem
{
    public partial class AddEmployee : UserControl
    {    


        private SqlConnection connect = new SqlConnection();
        private object today;

        public AddEmployee()
        {
            InitializeComponent();
            displayEmployeeData();
        } 

        public void displayEmployeeData()
        {
            //enkapsülasyon
            EmployeeData ed = new EmployeeData();
           
            List<EmployeeData> listData = ed.employeListData();

            dataGridView1.DataSource = listData;

        }
        private void addEmployee_addBtn_Click(object sender, EventArgs e)
        {
            if (addEmployee_id.Text == ""
                || addEmployeeFullName.Text == ""
                || addEmployee_genders.Text == ""
                || addEmployee_phoneNumbers.Text == ""
                || addEmployee_position.Text == ""
                || addEmployee_polition.Text == ""
                || addEmployee_id.Text == null)
            {
                MessageBox.Show("please fill all blank fields",
                    "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                if (connect.State == ConnectionState.Closed)

                {
                    try
                    {
                        connect.Open();
                        string checkedEmID = "SELECT * FROM employees WHERE employee_id= @emID AND delete_date IS  NULL";

                        using (SqlCommand checkEm = new SqlCommand(checkedEmID, connect))
                        {

                            checkEm.Parameters.AddWithValue("@emID", addEmployee_id.Text.Trim());


                            int count = (int)checkEm.ExecuteScalar();
                            if (count > 1)
                            {
                                MessageBox.Show(addEmployee_id.Text.Trim() + "allready taken",
                                    "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                DateTime today = DateTime.Now;
                                string insertData = "INSERT INTO employees " +
                                    "(employees_id,full_name,gender,contact_number+" +
                                    "position, image ,salary, insert_date,status,)" +
                                    "VALUES(@employeesID,@fullName,@gender,@contactNumber" +
                                    ",@position,@image,@salary,@insertDate,@satatus)";


                                string path = Path.Combine(@"C:\Users\ömer faruk\source\repos\EmployeManagementSystem\Directory\"
                                + addEmployee_id.Text.Trim() + ".jpg");

                                string directoryPath = Path.GetDirectoryName(path);

                                if (Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }

                                File.Copy(addEmployee_picture.ImageLocation, path, true);





                                using (SqlCommand cmd = new SqlCommand(insertData, connect))
                                {
                                    cmd.Parameters.AddWithValue("employeeID", addEmployee_id.Text.Trim());
                                    cmd.Parameters.AddWithValue("@fullName", addEmployeeFullName.Text.Trim());
                                    cmd.Parameters.AddWithValue("@gender", addEmployee_genders.Text.Trim());
                                    cmd.Parameters.AddWithValue("contactNumber", addEmployee_phoneNumbers.Text.Trim());
                                    cmd.Parameters.AddWithValue("position", addEmployee_position.Text.Trim());
                                    cmd.Parameters.AddWithValue("image", path);
                                    cmd.Parameters.AddWithValue("salary", 0);
                                    cmd.Parameters.AddWithValue("insertDate", today);
                                    cmd.Parameters.AddWithValue("status", addEmployee_polition.Text.Trim());

                                    cmd.ExecuteNonQuery();

                                    displayEmployeeData();

                                    MessageBox.Show("Added Succesfully", "Information Mesage",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    clearFields();

                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {


                    }
                    finally
                    {
                        connect.Close();
                    }

                }

            }
        }



        private void addEmployee_importBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // OpenFileDialog nesnesini oluşturun ve bir değişken adı verin
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files (*.jpg;*.png)|*.jpg;*.png";

                // Dosya yolunu saklamak için bir değişken oluşturun
                string imagePath = "";

                // Eğer kullanıcı bir dosya seçerse
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dialog.FileName;
                    addEmployee_picture.ImageLocation = imagePath; // Seçilen resmi PictureBox'a yükleyin
                }
            }
            catch (Exception ex)
            {
                // Hata mesajını gösterin
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddEmployee_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                addEmployee_id.Text = row.Cells[1].Value.ToString();
                addEmployeeFullName.Text = row.Cells[2].Value.ToString();
                addEmployee_genders.Text = row.Cells[3].Value.ToString();
                addEmployee_phoneNumbers.Text = row.Cells[4].Value.ToString();
                addEmployee_position.Text = row.Cells[5].Value.ToString();

                string imagePath = row.Cells[6].Value.ToString();

                if (!string.IsNullOrEmpty(imagePath))
                {
                    // If a picture already exists, dispose of the previous one
                    if (addEmployee_picture.Image != null)
                    {
                        addEmployee_picture.Image.Dispose();
                    }

                    // Load the image from the file and assign it to PictureBox
                    addEmployee_picture.Image = Image.FromFile(imagePath);
                }
                else
                {
                    addEmployee_picture.Image = null;
                }
            }
        }

        public void clearFields()
        {
            addEmployee_id.Text = "";
            addEmployeeFullName.Text = "";
            addEmployee_genders.Text = "";
            addEmployee_phoneNumbers.Text = "";
            addEmployee_position.SelectedIndex = -1;
            addEmployee_picture.Image = null;
        }

        private void addEmployee_updateBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(addEmployee_id.Text)
                || string.IsNullOrEmpty(addEmployeeFullName.Text)
                || string.IsNullOrEmpty(addEmployee_genders.Text)
                || string.IsNullOrEmpty(addEmployee_phoneNumbers.Text)
                || string.IsNullOrEmpty(addEmployee_position.Text))
            {
                MessageBox.Show("Please fill all blank fields",
                    "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult check = MessageBox.Show(
                    "Are you sure you want to update Employee ID: " + addEmployee_id.Text.Trim() + "?",
                    "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (check == DialogResult.Yes)
                {
                    try
                    {
                        connect.Open();

                        string updateQuery = "UPDATE employees SET full_name = @fullName, gender = @gender, contact_number = @contactNumber, " +
                            "position = @position, update_date = @updateDate, status = @status " +
                            "WHERE employee_id = @employeeID";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, connect))
                        {
                            // Add parameters correctly
                            cmd.Parameters.AddWithValue("@fullName", addEmployeeFullName.Text.Trim());
                            cmd.Parameters.AddWithValue("@gender", addEmployee_genders.Text.Trim());
                            cmd.Parameters.AddWithValue("@contactNumber", addEmployee_phoneNumbers.Text.Trim());
                            cmd.Parameters.AddWithValue("@position", addEmployee_position.Text.Trim());
                            cmd.Parameters.AddWithValue("@updateDate", DateTime.Now);
                            //  cmd.Parameters.AddWithValue("@status", addEmployee_status.Text.Trim());
                            cmd.Parameters.AddWithValue("@employeeID", addEmployee_id.Text.Trim());

                            cmd.ExecuteNonQuery();

                            displayEmployeeData();  // Refresh employee data

                            MessageBox.Show("Updated Successfully", "Information Message",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            clearFields();  // Clear fields after update
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Update cancelled.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void addEmployee_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void addEmployee_deleteBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(addEmployee_id.Text)
               || string.IsNullOrEmpty(addEmployeeFullName.Text)
               || string.IsNullOrEmpty(addEmployee_genders.Text)
               || string.IsNullOrEmpty(addEmployee_phoneNumbers.Text)
               || string.IsNullOrEmpty(addEmployee_position.Text))
            {
                MessageBox.Show("Please fill all blank fields",
                    "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult check = MessageBox.Show(
                    "Are you sure you want to delete Employee ID: " + addEmployee_id.Text.Trim() + "?",
                    "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (check == DialogResult.Yes)
                {
                    try
                    {
                        connect.Open();

                        string updateQuery = "UPDATE wmployees SET delete_date = @delete "+
                            "WHERE employees_id = @employeeID";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, connect))
                        {
                            // Add parameters correctly
                            cmd.Parameters.AddWithValue("@fullName", addEmployeeFullName.Text.Trim());
                            cmd.Parameters.AddWithValue("@gender", addEmployee_genders.Text.Trim());
                            cmd.Parameters.AddWithValue("@contactNumber", addEmployee_phoneNumbers.Text.Trim());
                            cmd.Parameters.AddWithValue("@position", addEmployee_position.Text.Trim());
                            cmd.Parameters.AddWithValue("@updateDate", DateTime.Now);
                            //  cmd.Parameters.AddWithValue("@status", addEmployee_status.Text.Trim());
                            cmd.Parameters.AddWithValue("@employeeID", addEmployee_id.Text.Trim());

                            cmd.ExecuteNonQuery();

                            displayEmployeeData();  // Refresh employee data

                            MessageBox.Show("Updated Successfully", "Information Message",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            clearFields();  // Clear fields after update
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Update cancelled.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }
    }
}


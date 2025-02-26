using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeManagementSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void exıt_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Çıkmak istediğinizden emin misiniz?"
              , "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
           
            
            if (check == DialogResult.Yes)
            {
                Form1 loginForm = new Form1();
                loginForm.ShowDialog();
                this.Hide();
            }
        }

        private void dashboard_btn_Click(object sender, EventArgs e)
        {
            dasboard1.Visible = true;
            addEmployee1.Visible = false;   
            salary1.Visible = false; 

           Dasboard dashForm = new dashboard1 as Dasboard; 

           if(dashForm =!null) 
            {
                dashForm.Refresh();

            }

        }

        private void addEmployee_btn_Click(object sender, EventArgs e)
        {

            dasboard1.Visible = true;
            addEmployee1.Visible = true;
            salary1.Visible = true;

        }

        private void salary_btn_Click(object sender, EventArgs e)
        {
            dasboard1.Visible = false;
            addEmployee1.Visible = false;
            salary1.Visible = true;
        }
    }
}

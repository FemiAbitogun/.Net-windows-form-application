using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel.Application;
using MySql.Data.MySqlClient;

namespace windowAPP
{
    public partial class Students : Form
    {
        public Students()
        {
            InitializeComponent();
        }

        MySqlDataAdapter adapter;

        private void Booking_Load(object sender, EventArgs e)
        {
            
            showStudents();
            dataGridView1.Columns[0].Visible = false;
        }



        private void showStudents()
        {
           
            try
            {
                using (MySqlConnection connection = new MySqlConnection(@"Server=localhost; Database=students_db;Uid=root;Pwd=root"))
                {
                    connection.Open();
                    adapter = new MySqlDataAdapter("getAllStudent", connection);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                   System.Data.DataTable table = new System.Data.DataTable();
                   
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void btnHome_Click(object sender, EventArgs e)
        {
            Home_Admin home = new Home_Admin();
            home.Show();
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            string firstName = txtFirstName.Text;
            string middleName = txtMiddleName.Text;
            string lastName = txtLastName.Text;
            string studentClass = txtClass.Text;
            string schoolFees = txtSchoolFees.Text;
            string payment = labelPayment.Text;
            string balance = labelBalance.Text;

   
            string paymentMode = "full payment";
            if (   radioFullPayment .Checked)
            {
                paymentMode = "full payment";
            }
            else if (   radioHalfPayment.Checked)
            {
                paymentMode = "half payment";
            }


            string sex = "male";
            if (radioFemale.Checked)
            {
                sex = "female";
            }
            else if(  radioMale.Checked ){
                sex = "male";
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(@"Server=localhost; Database=students_db;Uid=root;Pwd=root"))
                {
                    connection.Open();
                    MySqlCommand mysqlcmd = new MySqlCommand("studentInfo", connection);
                    mysqlcmd.CommandType = CommandType.StoredProcedure;
                    mysqlcmd.Parameters.AddWithValue("_firstName", firstName);
                    mysqlcmd.Parameters.AddWithValue("_middleName", middleName);
                    mysqlcmd.Parameters.AddWithValue("_lastName", lastName);
                    mysqlcmd.Parameters.AddWithValue("_sex", sex);
                    mysqlcmd.Parameters.AddWithValue("_studentClass", studentClass);
                    mysqlcmd.Parameters.AddWithValue("_schoolFees", schoolFees);
                    mysqlcmd.Parameters.AddWithValue("_paymentMode", paymentMode);
                    mysqlcmd.Parameters.AddWithValue("_payment", payment);
                    mysqlcmd.Parameters.AddWithValue("_balance", balance);
       
                    mysqlcmd.ExecuteNonQuery();
                }

                showStudents();

            }
            catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

        } //  private void btnSave_Click(object sender, EventArgs e)

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string connectionString = @"Server=localhost; Database=students_db;Uid=root;Pwd=root";
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);

            using (sqlConnection = new MySqlConnection(connectionString))
            {
                try
                { 
                    sqlConnection.Open();
                    MySqlDataAdapter  sqlAdapter = new MySqlDataAdapter("select * from students where firstName   like  '%" + txtSearch.Text + "%' ", sqlConnection);
                    System.Data.DataTable    usersTable = new System.Data.DataTable();
                    sqlAdapter.Fill(usersTable);
                    dataGridView1.DataSource = usersTable;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }





    
    }//      public partial class Students : Form





}

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
using GemBox.Spreadsheet;
namespace windowAPP
{
    public partial class Register : Form
    {
        MySqlConnection sqlConnection;
        MySqlDataAdapter sqlAdapter;
        System.Data.DataTable  usersTable;
        string connectionString = @"Server=localhost; Database=user;Uid=root;Pwd=root";
        int ID;



        public Register()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
        {
        

            showGridContent();
            dataGridView1.Columns[0].Visible = false;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
        }

        private void showGridContent()
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    sqlAdapter = new MySqlDataAdapter("select * from user.authenticatedUsers", sqlConnection);
                    usersTable = new System.Data.DataTable();
                    sqlAdapter.Fill(usersTable);
                    dataGridView1.DataSource = usersTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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

            string status="user";
            if (chkAdmin.Checked)
                status = "admin";
            else
                status = "user";

            using (sqlConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    MySqlCommand sqlCommand =
                    new MySqlCommand("  insert into user.authenticatedUsers (name,password,status ) values ('" + txtUserName.Text.Trim()+"','"+txtPassword.Text.Trim()+"','"+status+"')",
                    sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("successfully registered user");
                    showGridContent();
                    txtUserName.Text = txtPassword.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                txtUserName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtPassword.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;
                btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (ID != 0 )
                {
                    using (sqlConnection = new MySqlConnection(connectionString))
                    {
                        sqlConnection.Open();
                        MySqlCommand sqlCommand = new MySqlCommand("delete from  user.authenticatedUsers  where ID= ' " + ID + " '  ", sqlConnection);
                        sqlCommand.ExecuteNonQuery();
                        MessageBox.Show("successfully deleted user");
                        showGridContent();
                        txtUserName.Text=txtPassword.Text="";
                        btnDelete.Enabled = false;
                        btnUpdate.Enabled = false;
                        ID = 0;
                    }
                }
                else
                {
                    MessageBox.Show(" you have not selected any row");
                }   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {

            string status = "user";
            if (chkAdmin.Checked)
            {
                status = "admin";
            }
           
            try
            {
                if (ID != 0)
                {
                    using (sqlConnection = new MySqlConnection(connectionString))
                    {
                        sqlConnection.Open();
                        MySqlCommand sqlCommand = new MySqlCommand("update  user.authenticatedUsers  set name='" + txtUserName.Text.TrimStart()+"',password='"+txtPassword.Text.TrimStart()+"', status='"+status+"' where id='"+ID+"' ",sqlConnection);
                        sqlCommand.ExecuteNonQuery();
                        MessageBox.Show("successfully updated user");
                        txtUserName.Text = txtPassword.Text = "";
                        ID = 0;
                        showGridContent();
                    }
                }
                else
                {
                    MessageBox.Show(" you have not selected any row to update");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    sqlAdapter = new MySqlDataAdapter("select * from authenticatedUsers where name  like  '%" + txtSearchUser.Text+"%' ",sqlConnection);
                    usersTable = new System.Data.DataTable();
                    sqlAdapter.Fill(usersTable);
                    dataGridView1.DataSource = usersTable;
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtUserName.Text = txtPassword.Text = "";
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
        }
      





private void btnExcel_Click(object sender, EventArgs e)
        {
           

            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            ExcelFile workbook = new ExcelFile();
            ExcelWorksheet worksheet = workbook.Worksheets.Add("New worksheet");


            /*
             * using Excel = Microsoft.Office.Interop.Excel.Application;............................................

             * 

                 Workbook workbook;
                 Worksheet worksheet;
                 Excel excel_the_Application = new  Excel();
                 workbook = excel_the_Application.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                  // workbook = excel_the_Application.Workbooks.Add(Type.Missing);
                 worksheet = workbook.Worksheets[1];
             */


            try
            {
                  
                for (int gridHeaderColumn=0; gridHeaderColumn < dataGridView1.Columns.Count; gridHeaderColumn++)
                {
                    worksheet.Cells[1, gridHeaderColumn + 1].Value = dataGridView1.Columns[gridHeaderColumn].HeaderText;
                }
                for (int gridRow=0; gridRow<dataGridView1.Rows.Count; gridRow++)
                {
                    for(int gridColumnElement=0; gridColumnElement<dataGridView1.Columns.Count; gridColumnElement++)
                    {
                        worksheet.Cells[2 + gridRow, 1 + gridColumnElement].Value = dataGridView1.Rows[gridRow].Cells[gridColumnElement].Value.ToString();
                    }

                }

                // workbook.SaveAs(@"C:\Users\Femi Abitogun\Desktop\formApplication\ExcelReports");
                workbook.Save(@"C:\Users\Femi Abitogun\Desktop\formApplication\ExcelReports.xls");

                MessageBox.Show("successfully saved to excel report");


              //workbook.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }            // funtion end private void btnExcel_Click(object sender, EventArgs e)

        private void btnViewExcel_Click(object sender, EventArgs e)
        {
            EXCEL_FILES files = new EXCEL_FILES();
            files.Show();
        }
    }
}

/*
  for (int gridColumn = 0; gridColumn < dataGridView1.Columns.Count; gridColumn++)
                        {
                            worksheet.Cells[1, gridColumn + 1] = dataGridView1.Columns[gridColumn].HeaderText;
                        }

                for (int gridRow = 0; gridRow < dataGridView1.Rows.Count; gridRow++)
                {
                    for (int datacoulmn = 0; datacoulmn < dataGridView1.Columns.Count; datacoulmn++)
                            {
                                worksheet.Cells[2 + gridRow, 1 + datacoulmn] = dataGridView1.Rows[gridRow].Cells[datacoulmn].Value.ToString();
                            }

     */

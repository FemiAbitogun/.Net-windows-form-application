using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace windowAPP
{
    public partial class Login : Form
    {

        MySqlConnection sqlConnection;
        MySqlDataAdapter sqlAdapter;
        static public string _theStatus="user";
        
        string connectionString = @"Server=localhost; Database=user;Uid=root;Pwd=root";


        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home_Admin home = new Home_Admin();
            home.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string status = "user";
            if (chkAdmin.Checked)
            {
                status = "admin";
            }
            else
            {
                status = "user";
            }
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                try
                {
                   

                    sqlConnection.Open();
              
                    sqlAdapter = new MySqlDataAdapter("LoginAuthentication", sqlConnection);
                    sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlAdapter.SelectCommand.Parameters.AddWithValue("_name", txtUserName.Text.Trim());
                    sqlAdapter.SelectCommand.Parameters.AddWithValue("_password", txtPassword.Text.Trim());
                    sqlAdapter.SelectCommand.Parameters.AddWithValue("_status", status);

                    DataTable sqlTable = new DataTable();
                    sqlAdapter.Fill(sqlTable);
                   if(sqlTable.Rows.Count >0)
                    {

                        this.Hide();
                        if ( status=="admin" )
                        {
                            Home_Admin home = new Home_Admin();
                            _theStatus = "admin";
                            home.Show();
                        }
                        else if(status=="user")
                        {
                            Home_Admin home = new Home_Admin();
                            _theStatus = "user";
                            home.Show();

                        }

                     
                        
                    }
                   else
                    {
                        MessageBox.Show("failed authorization");
                        txtPassword.Text = txtUserName.Text = "";
                    }
                    sqlConnection.Close();
                  
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

            }




            }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
            Application.Exit();
            this.Hide();

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using GemBox.Spreadsheet;

namespace windowAPP
{
    public partial class EXCEL_FILES : Form
    {
        DataTable table = new DataTable();
        public EXCEL_FILES()
        {
            InitializeComponent();
        }


     


        private void btnFilePath_Click(object sender, EventArgs e)
        {

            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog()   == System.Windows.Forms.DialogResult.OK)
            {
                this.txtFilePath.Text = file.FileName;
            }
        }
        /*
        private string ExcelConnection()
        {
            return @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                        @"Data Source=" + txtFilePath.Text + ";" +
                        @"Extended Properties=" + Convert.ToChar(34).ToString() +
                        @"Excel 8.0" + Convert.ToChar(34).ToString() + ";";
        }
        */

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source='" + txtFilePath.Text + "';" +"Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1;\"");
                OleDbDataAdapter adapter = new OleDbDataAdapter("select * from ["+txtSheet.Text+ "$]", connection);
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                connection.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
         
        }
    }
}

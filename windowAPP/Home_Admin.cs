using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace windowAPP
{
    public partial class Home_Admin : Form
    {
        public Home_Admin()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            this.Hide();
            register.Show();
            
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void Home_Admin_Load(object sender, EventArgs e)
        {
           string status= Login._theStatus;
            if (status == "admin")
            {
                btnRegister.Visible = true;
            }
            else
            {
                btnRegister.Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Students booking = new Students();
            booking.Show();
            this.Hide();
        }
    }
}

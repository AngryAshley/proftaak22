using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RailView_database_GUI
{
    public partial class Form1 : Form
    {
        connection conn = new connection();
        public Form1()
        {
            InitializeComponent();
            conn.OpenConection();
            conn.CloseConnection();

            txbUserName.Text = "Gebruikersnaam";
            txbPassword.PasswordChar = '*';
            btnLogin.TabStop = false;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
        }

        private void txbUserName_Enter(object sender, EventArgs e)
        {
            if (txbUserName.Text == "Gebruikersnaam")
            {
                txbUserName.Text = "";
            }
        }

        private void txbUserName_Leave(object sender, EventArgs e)
        {
            if (txbUserName.Text == "")
            {
                txbUserName.Text = "Gebruikersnaam";
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            conn.OpenConection();
            
            // query 
            // sql comman execute daarin de query en de connection 
            // 
        }
    }
}

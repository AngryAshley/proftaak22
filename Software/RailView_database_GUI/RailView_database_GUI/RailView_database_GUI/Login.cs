﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RailView_database_GUI
{
    public partial class Login : Form
    {
        Connection conn = new Connection();
        public Login()
        {
            InitializeComponent();

            txbUsername.Text = "Username";
            txbPassword.PasswordChar = '*';
            btnLogin.TabStop = false;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=192.168.161.205;Port=3306;Database=RailView;Uid=" + txbUsername.Text + ";Pwd=" + txbPassword.Text + ";";

            try
            {
                conn.OpenConection(connectionString);

                Dashboard dahsboard = new Dashboard();
                this.Hide();
                dahsboard.ShowDialog();
                conn.CloseConnection();

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server");
                        break;
                    case 1045:
                        MessageBox.Show("invalid username/password");
                        break;
                }
            }
        }
    }
}
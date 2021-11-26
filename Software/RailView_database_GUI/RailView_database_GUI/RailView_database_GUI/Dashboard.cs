using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace RailView_database_GUI
{
    public partial class Dashboard : Form
    {
        connection conn = new connection();

        public Dashboard()
        {
            InitializeComponent();
        }

        private void lblDatabase1_Click(object sender, EventArgs e)
        {

            DatabaseSelected databaseSelected = new DatabaseSelected();
            this.Hide();
            databaseSelected.ShowDialog();
        }   
    }
}

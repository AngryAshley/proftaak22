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
            //als er hier op gelikt wordt laat de tables zien uit deze DB
            string connectionString = "Server=192.168.161.205;Port=3306;Database=RailView;Uid=admin;Pwd=TopMaster99;";
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            List<string> tableList = new List<string>();

            try
            {
                string sql = "SHOW TABLES";

                conn = new MySqlConnection(connectionString);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    tableList.Add(da.ToString());
                }

                lblTest.Text = tableList[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("An error occurred {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

        }
    }
}

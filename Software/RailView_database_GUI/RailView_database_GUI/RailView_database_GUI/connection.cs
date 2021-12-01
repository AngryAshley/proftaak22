using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web;

namespace RailView_database_GUI
{
    public class Connection
    {
        public MySqlConnection conn;

        public void OpenConection(string connectionString)
        {
            conn = new MySqlConnection(connectionString);
            conn.Open();
        }

        public void CloseConnection()
        {
            conn.Close();
        }
    }
}

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
    public class connection
    {
        MySqlConnection con;

        public void OpenConection(String connectionString)
        {
            con = new MySqlConnection(connectionString);
            con.Open();
        }

        public void CloseConnection()
        {
            con.Close();
        }
    }
}

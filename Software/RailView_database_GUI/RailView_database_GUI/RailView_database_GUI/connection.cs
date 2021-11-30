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
        MySqlConnection conn = null;

        public void OpenConection(string connectionString)
        {
            conn = new MySqlConnection(connectionString);
            conn.Open();
        }

        public void CloseConnection()
        {
            conn.Close();
        }

        public List<string> ShowDatabase(string sql, bool countRows)
        {
            List<string> tableList = new List<string>();
            List<string> rowList = new List<string>();
            int amountOfRows = 0;

            MySqlCommand command = new MySqlCommand(sql, conn);
            command.ExecuteNonQuery();
            MySqlDataReader data = command.ExecuteReader();

            while (data.Read())
            {
                if (countRows == true) { amountOfRows++; }

                for (int i = 0; i < data.FieldCount; i++)
                {
                    tableList.Add(data.GetString(i));
                }

                rowList.AddRange(tableList);
                tableList.Clear();
            }

            if (countRows == true) { rowList.Add(amountOfRows.ToString()); }

            return rowList;
        }

        public void CreateTable(string sql)
        {
            MySqlCommand command = new MySqlCommand(sql, conn);
            command.ExecuteNonQuery();
        }
    }
}

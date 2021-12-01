using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RailView_database_GUI
{
    class ExecuteQuery : Connection
    {

        public List<string> ShowDatabase(string sql, bool countRows, string connectionString)
        {
            OpenConection(connectionString);

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

            CloseConnection();
            return rowList;
        }

        public void CreateTable(string sql, string connectionString)
        {
            OpenConection(connectionString);
            MySqlCommand command = new MySqlCommand(sql, conn);
            command.ExecuteNonQuery();
            CloseConnection();
        }
    }
}

using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace RailView_database_GUI
{
    class ExecuteQuery : Connection
    {
        public List<string> GetData(string sql, bool countRows, string connectionString)
        {
            OpenConection(connectionString);

            List<string> list = new List<string>();
            int amountOfRows = 0;

            MySqlCommand command = new MySqlCommand(sql, conn);
            command.ExecuteNonQuery();
            MySqlDataReader data = command.ExecuteReader();

            while (data.Read())
            {
                if (countRows == true) 
                { 
                    amountOfRows++; 
                }
                else
                {
                    for (int i = 0; i < data.FieldCount; i++)
                    {
                        if (data.IsDBNull(i) == false)
                        {
                            list.Add(data.GetString(i));
                        }
                        else
                        {
                            list.Add("NULL");
                        }
                    }
                }
            }

            if (countRows == true) { list.Add(amountOfRows.ToString()); }

            CloseConnection();
            return list;
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

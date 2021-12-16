using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace RailView_database_GUI
{
    class ExecuteQuery
    {
        public MySqlConnection Conn { get; set; }

        public ExecuteQuery(string connectionString)
        {
            Conn = new MySqlConnection(connectionString);
        }

        public List<string> GetData(string sql, bool countRows)
        {
            Conn.Open();

            List<string> list = new List<string>();
            int amountOfRows = 0;

            MySqlCommand command = new MySqlCommand(sql, Conn);
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

            Conn.Close();
            return list;
        }

        public void SimpleExecute(string sql)
        {
            Conn.Open();
            MySqlCommand command = new MySqlCommand(sql, Conn);
            command.ExecuteNonQuery();
            Conn.Close();
        }
    }
}

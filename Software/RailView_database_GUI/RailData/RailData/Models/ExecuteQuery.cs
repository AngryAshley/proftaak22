using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace RailData.Models
{
    public class ExecuteQuery
    {
        public MySqlConnection Conn { get; set; }

        public ExecuteQuery(string connectionString)
        {
            Conn = new MySqlConnection(connectionString);
        }

        public List<string> SimpleExecute(string sql)
        {
            List<string> list = new List<string>();
            try
            {
                Conn.Open();
                MySqlCommand command = new MySqlCommand(sql, Conn);
                command.ExecuteNonQuery();

                MySqlDataReader databaseReader = command.ExecuteReader();

                while (databaseReader.Read())
                {
                    for (int i = 0; i < databaseReader.FieldCount; i++)
                    {
                        list.Add(databaseReader.GetString(i));
                    }
                }

                Conn.Close();
            }
            catch (MySqlException ex)
            {
                System.Console.WriteLine("ERROR:" + ex.Number.ToString() + ": " + ex.Message);
            }

            return list;
        }
    }
}

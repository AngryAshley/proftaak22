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
            // Set the connectionstring to a new connection
            Conn = new MySqlConnection(connectionString);
        }

        public List<string> SimpleExecute(string sql)
        {
            // Initialize the return list.
            List<string> list = new List<string>();

            try
            {
                Conn.Open(); // Open connection
                MySqlCommand command = new MySqlCommand(sql, Conn); // Send the command with the open connection.
                command.ExecuteNonQuery(); // Execute SQL command.

                MySqlDataReader databaseReader = command.ExecuteReader(); // Read the response from the command.

                while (databaseReader.Read())
                {
                    for (int i = 0; i < databaseReader.FieldCount; i++)
                    {
                        // Loop through the response and put them in the list.
                        list.Add(databaseReader.GetString(i));
                    }
                }

                Conn.Close(); // Close the connection.
            }
            catch (MySqlException ex)
            {
                System.Console.WriteLine("ERROR:" + ex.Number.ToString() + ": " + ex.Message);
            }

            return list;
        }
    }
}

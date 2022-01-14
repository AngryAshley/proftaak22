using System.Collections.Generic;
using System.Windows.Forms;
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
            List<string> list = new List<string>();
            int amountOfRows = 0;

            try
            {
                Conn.Open();
                MySqlCommand command = new MySqlCommand(sql, Conn);
                command.ExecuteNonQuery();
                MySqlDataReader data = command.ExecuteReader();

                while (data.Read())
                {
                    if (countRows)
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

                if (countRows) { list.Add(amountOfRows.ToString()); }

                Conn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("#" + ex.Number.ToString() + ": " + ex.Message, "Error", MessageBoxButtons.OK);
            }

            return list;
        }

        public bool SimpleExecute(string sql)
        {
            try
            {
                Conn.Open();
                MySqlCommand command = new MySqlCommand(sql, Conn);
                command.ExecuteNonQuery();
                Conn.Close();
                return false;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("#" + ex.Number.ToString() + ": " + ex.Message, "Error", MessageBoxButtons.OK);
                return true;
            }

        }
    }
}

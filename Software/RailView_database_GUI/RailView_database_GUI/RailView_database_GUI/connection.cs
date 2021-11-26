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
        MySqlCommand cmd = null;

        public void OpenConection(string connectionString)
        {
            conn = new MySqlConnection(connectionString);
            conn.Open();
        }

        public void CloseConnection()
        {
            conn.Close();
        }

        public List<string> ShowDatabase(string sql)
        {
            List<string> tableList = new List<string>();
            List<string> rowList = new List<string>();

            MySqlCommand MSQLcrcommand1 = new MySqlCommand(sql, conn);
            MSQLcrcommand1.ExecuteNonQuery();
            MySqlDataReader dr = MSQLcrcommand1.ExecuteReader();

            int amountOfRows = 0;

            while (dr.Read())
            {
                amountOfRows++;

                for (int i = 0; i < dr.FieldCount; i++)
                {
                    //Console.WriteLine(dr.GetName(i));

                    //Console.WriteLine($"Dit is een getString: {dr.GetValue(i)}");
                    tableList.Add(dr.GetString(i));
                }

                rowList.AddRange(tableList);
                tableList.Clear();
            }

            rowList.Add(amountOfRows.ToString());

            return rowList;
        }
    }
}

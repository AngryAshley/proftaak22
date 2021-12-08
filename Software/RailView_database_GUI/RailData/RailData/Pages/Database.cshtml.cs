using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using RailData.Models;
using System.Collections.Generic;

namespace RailData.Pages
{
    public class DatabaseModel : PageModel
    {
        // Class handlers
        SelectDatabase selectDatabase = new SelectDatabase(); // get database set by index
        MySqlConnection _connection;
        MySqlCommand cmd = null;

        // Objects and variables
        public List<string> DescribedDatabase = new List<string>();
        string newConnectionString = "";

        public DatabaseModel()
        {
            
        }

        public void OnGet()
        {
            if (HttpContext.Session.GetString("Loggedin") != null && HttpContext.Session.GetString("connection") != null)
            {
                newConnectionString = $"Server=192.168.161.205;Port=3306;Database={selectDatabase.DatabaseName};Uid={selectDatabase.Username};Pwd=TopMaster99;";

                _connection = new MySqlConnection(newConnectionString);
                _connection.Open();

                string sql = $"USE {selectDatabase.DatabaseName}; show tables";
                TempData["databaseName"] = selectDatabase.DatabaseName;

                cmd = new MySqlCommand(sql, _connection);
                cmd.ExecuteNonQuery();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        DescribedDatabase.Add(reader.GetString(i));
                        Console.WriteLine("- " +reader.GetString(i));
                    }
                }
                _connection.Close();
            }
        }

        public void OnGetSelectTable(string tableName)
        {
            Console.WriteLine(tableName);
        }
    }
}

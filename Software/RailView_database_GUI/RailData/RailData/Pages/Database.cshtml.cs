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
        ErrorHandling errorHandling = new ErrorHandling();
        MySqlConnection _connection;
        MySqlCommand cmd = null;

        // Objects and variables
        public List<string> Databases = new List<string>();
        public List<string> DescribedDatabase = new List<string>();
        string newConnectionString = "";
        string getDatabaseName = "";

        public void OnGet()
        {
            if (HttpContext.Session.GetString("Loggedin") != null && HttpContext.Session.GetString("connection") != null)
            {
                getDatabaseName = Request.Query["databaseName"];
                newConnectionString = $"Server=192.168.161.205;Port=3306;Database={getDatabaseName};Uid=admin;Pwd=TopMaster99;";

                SelectDatabases();

                ShowTables();
            } 
        }

        public void SelectDatabases()
        {
            _connection = new MySqlConnection(newConnectionString);
            _connection.Open();

            string sqlDatabases = "SHOW DATABASES";
            cmd = new MySqlCommand(sqlDatabases, _connection);
            cmd.ExecuteNonQuery();

            MySqlDataReader databaseReader = cmd.ExecuteReader();

            while (databaseReader.Read())
            {
                for (int i = 0; i < databaseReader.FieldCount; i++)
                {
                    Databases.Add(databaseReader.GetString(i));
                }
            }

            _connection.Close();
        }

        public void ShowTables()
        {
            _connection.Open();

            string sql = $"USE {getDatabaseName}; show tables";
            TempData["databaseName"] = getDatabaseName;

            cmd = new MySqlCommand(sql, _connection);
            cmd.ExecuteNonQuery();

            MySqlDataReader tableReader = cmd.ExecuteReader();

            while (tableReader.Read())
            {
                for (int i = 0; i < tableReader.FieldCount; i++)
                {
                    DescribedDatabase.Add(tableReader.GetString(i));
                }
            }

            _connection.Close();
        }

        public void OnGetSelectDatabase(string databaseName)
        {
            Console.WriteLine(databaseName);
        }

        public void OnGetSelectTable(string tableName)
        {
            Console.WriteLine(tableName);
        }
    }
}

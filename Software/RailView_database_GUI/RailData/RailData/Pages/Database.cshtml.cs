using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using RailData.Models;
using System.Collections.Generic;

namespace RailData.Pages
{
    public class DatabaseModel : PageModel
    {
        // Class handlers
        private readonly IConfiguration _configuration;
        public ErrorHandling errorHandling = new ErrorHandling();
        MySqlConnection _connection;
        MySqlCommand cmd = null;

        // Objects and variables
        public List<string> Databases = new List<string>();
        public List<string> DescribedDatabase = new List<string>();
        string newConnectionString = "";

        public DatabaseModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            if (HttpContext.Session.GetString("Loggedin") != null && HttpContext.Session.GetString("connection") != null)
            {
                try
                {
                    string getDatabaseName = Request.Query["databaseName"];
                    var connect = _configuration.GetSection("Database");
                    newConnectionString = $"Server={connect.GetSection("Server").Value};Port={connect.GetSection("Port").Value};Database={getDatabaseName};Uid={HttpContext.Session.GetString("Loggedin")};Pwd={HttpContext.Session.GetString("Username")};";

                    SelectDatabases();

                    ShowTables(getDatabaseName);
                }
                catch (MySqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 0:
                            errorHandling.ErrorMessage = "Invalid session, please try to log in";
                            break;
                        case 1045:
                            errorHandling.ErrorMessage = "Cannot connect to server";
                            break;
                    }
                }
                catch (Exception ex)
                {
                    errorHandling.ErrorMessage = "Could not connect to server: " + ex;
                }
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

        public void ShowTables(string databaseName)
        {
            _connection.Open();

            string sql = $"USE {databaseName}; show tables";
            TempData["databaseName"] = databaseName;

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

        //public void OnGetSelectDatabase(string databaseName)
        //{
        //    Console.WriteLine(databaseName);
        //}

        public void OnGetSelectTable(string databaseName, string tableName)
        {
            Response.Redirect($"/Database/Table?databaseName={databaseName}&tableName={tableName}");
        }
    }
}

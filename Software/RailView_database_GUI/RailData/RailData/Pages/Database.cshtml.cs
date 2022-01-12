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
                // Check if the login sessions exist
                // Then execute a new query to show all tables inside the selected database and put them in a list.
                try
                {
                    string getDatabaseName = Request.Query["databaseName"];
                    var connect = _configuration.GetSection("Database");
                    // Set a new connection string with the correct database selected.
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
            // Use the ExecuteQuery Class to show all databases in the side navigation.
            ExecuteQuery executeQuery = new ExecuteQuery(newConnectionString);

            string sqlDatabases = "SHOW DATABASES";
            Databases = executeQuery.SimpleExecute(sqlDatabases);
        }

        public void ShowTables(string databaseName)
        {
            TempData["databaseName"] = databaseName;

            // Use the ExecuteQuery Class to show all tables inside the selected database.
            ExecuteQuery executeQuery = new ExecuteQuery(newConnectionString);

            string sql = $"USE {databaseName}; show tables";
            DescribedDatabase = executeQuery.SimpleExecute(sql);
        }

        public void OnGetSelectTable(string databaseName, string tableName)
        {
            // Redirect and show the records inside the selected table.
            Response.Redirect($"/Database/Table?databaseName={databaseName}&tableName={tableName}");
        }
    }
}

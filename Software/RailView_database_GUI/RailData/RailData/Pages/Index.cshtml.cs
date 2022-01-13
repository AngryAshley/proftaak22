using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using RailData.Models;
using System;
using System.Collections.Generic;

namespace RailData.Pages
{
    public class IndexModel : PageModel
    {
        // Fields & global variables
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;
        public ErrorHandling errorHandling = new ErrorHandling();
        MySqlConnection _connection;
        List<string> databases = new List<string>();
        List<string> status = new List<string>();
        string connectionString = "";

        // Properties
        public List<string> Databases { get { return databases; } }
        public List<string> Status { get { return status; } }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void OnGet()
        {
            if (HttpContext.Session.GetString("Loggedin") != null && HttpContext.Session.GetString("connection") != null)
            {
                // Check if the login sessions exist, then execute a new query to show all databases and put them in a list.
                connectionString = HttpContext.Session.GetString("connection");

                string sql = "SHOW DATABASES;";

                ExecuteQuery executeQuery = new ExecuteQuery(connectionString);
                databases = executeQuery.SimpleExecute(sql);

                GetMysqlStatus();
            }
        }

        public void OnPost()
        {
            // Request the username and password input from the form in the Index.cshtml
            string Username = Request.Form["Username"].ToString();
            string Password = Request.Form["Password"].ToString();

            if (Username != "" && Password != "")
            {
                // Check if the inputs aren't emtpy.
                // Then get the database connection information from the appsettings.json and connect to the database.
                var connect = _configuration.GetSection("Database");
                connectionString = $"Server={connect.GetSection("Server").Value};Port={connect.GetSection("Port").Value};Database={connect.GetSection("Default").Value};Uid={Username};Pwd={Password};";

                try
                {
                    _connection = new MySqlConnection(connectionString);

                    // Put the later needed information inside session variables.
                    HttpContext.Session.SetString("Loggedin", Username);
                    HttpContext.Session.SetString("Username", Password);
                    HttpContext.Session.SetString("connection", connectionString);
                    _connection.Open();
                    Response.Redirect("/"); // Refresh the page.
                }
                catch (MySqlException ex)
                {
                    ExitConnections();
                    switch (ex.Number)
                    {
                        case 0: // MySQL.Data errorcode 0
                            errorHandling.ErrorMessage = "Invalid username/password, please try again";
                            break;
                        case 1045:
                            errorHandling.ErrorMessage = "Cannot connect to server";
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ExitConnections();
                    errorHandling.ErrorMessage = "An error occurred " + ex;
                }
            }
            else
            {
                errorHandling.ErrorMessage = "Enter your username and password.";
            }
        }

        private void ExitConnections()
        {
            // Method to remove all connections when a login fails or a session ends.
            HttpContext.Session.Remove("Loggedin");
            HttpContext.Session.Remove("connection");
            _connection.Close();
        }

        private void GetMysqlStatus()
        {
            // SQL statement to get the MySQL server information.
            connectionString = HttpContext.Session.GetString("connection");

            string sql = "SHOW VARIABLES WHERE Variable_Name LIKE 'time_zone' OR Variable_Name LIKE 'version';";

            ExecuteQuery executeQuery = new ExecuteQuery(connectionString);
            status = executeQuery.SimpleExecute(sql);
        }

        public void OnGetSelectDatabase(string databaseName)
        {
            // Redirect and show the tables inside the database clicked.
            Response.Redirect($"/Database?databaseName={databaseName}");
        }
    }
}

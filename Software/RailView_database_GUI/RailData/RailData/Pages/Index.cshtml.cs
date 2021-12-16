using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using RailData.Models;
using System;
using System.Collections.Generic;

namespace RailData.Pages
{
    public class IndexModel : PageModel
    {
        // Class handlers
        private readonly ILogger<IndexModel> _logger;
        public ErrorHandling errorHandling = new ErrorHandling();
        MySqlConnection _connection;
        MySqlCommand cmd = null;

        // Objects and variables
        public List<string> Databases = new List<string>();
        public List<string> Status = new List<string>();
        string connectionString = "";
        

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            if (HttpContext.Session.GetString("Loggedin") != null && HttpContext.Session.GetString("connection") != null)
            {
                _connection = new MySqlConnection(HttpContext.Session.GetString("connection"));
                _connection.Open();
                // log database
                string sql = "SHOW DATABASES;";
                cmd = new MySqlCommand(sql, _connection);
                cmd.ExecuteNonQuery();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Databases.Add(reader.GetString(i));
                    }
                }
                _connection.Close();

                GetMysqlStatus();
            }
        }

        public void OnPost()
        {
            string Username = Request.Form["Username"].ToString();
            string Password = Request.Form["Password"].ToString();


            if (Username != "" && Password != "")
            {
                connectionString = "Server=192.168.161.205;Port=3306;Database=RailView;Uid=" + Username + ";Pwd=" + Password + ";";

                try
                {
                    _connection = new MySqlConnection(connectionString);

                    HttpContext.Session.SetString("Loggedin", Username);
                    HttpContext.Session.SetString("connection", connectionString);
                    _connection.Open();
                    Response.Redirect("/");
                }
                catch (MySqlException ex)
                {
                    ExitConnections();
                    switch (ex.Number)
                    {
                        case 0:
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
            HttpContext.Session.Remove("Loggedin");
            HttpContext.Session.Remove("connection");
            _connection.Close();
        }

        private void GetMysqlStatus()
        {
            _connection = new MySqlConnection(HttpContext.Session.GetString("connection"));
            _connection.Open();
            // log database
            string sql = "SHOW VARIABLES WHERE Variable_Name LIKE 'time_zone' OR Variable_Name LIKE 'version';";
            cmd = new MySqlCommand(sql, _connection);
            cmd.ExecuteNonQuery();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Status.Add(reader.GetString(i));
                }
            }
            _connection.Close();
        }

        public void OnGetSelectDatabase(string databaseName)
        {
            Response.Redirect($"/Database?databaseName={databaseName}");
        }
    }
}

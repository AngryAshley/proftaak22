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
        private readonly ILogger<IndexModel> _logger;
        ErrorHandling errorHandling = new ErrorHandling();
        public List<string> Tables = new List<string>();

        string connectionString = "";
        MySqlConnection _connection;
        MySqlCommand cmd = null;

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
                string sql = "SHOW TABLES";
                cmd = new MySqlCommand(sql, _connection);
                cmd.ExecuteNonQuery();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Tables.Add(reader.GetString(i));
                    }
                }
                _connection.Close();
            }
        }

        public void OnPost()
        {
            string Username = Request.Form["Username"].ToString();
            string Password = Request.Form["Password"].ToString();


            if (Username != "" && Password != "")
            {
                connectionString = "Server=192.168.161.205;Port=3306;Database=RailView;Uid=" + Username + ";Pwd=" + Password + ";";
                Console.WriteLine("Connecting...");

                try
                {
                    _connection = new MySqlConnection(connectionString);
                    Console.WriteLine("SUCCESS");

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
                            Console.WriteLine("Invalid username/password, please try again");
                            errorHandling.ErrorType = "danger";
                            errorHandling.ErrorMessage = "Invalid username/password, please try again";
                            break;
                        case 1045:
                            Console.WriteLine("Cannot connect to server");
                            errorHandling.ErrorType = "danger";
                            errorHandling.ErrorMessage = "Cannot connect to server";
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ExitConnections();
                    Console.WriteLine("An error occurred " + ex);
                    errorHandling.ErrorType = "danger";
                    errorHandling.ErrorMessage = "An error occurred " + ex;
                }
            }
            else
            {
                Console.WriteLine("Enter your username and password.");
                errorHandling.ErrorType = "danger";
                errorHandling.ErrorMessage = "Enter your username and password.";
            }
        }

        private void ExitConnections()
        {
            HttpContext.Session.Remove("Loggedin");
            HttpContext.Session.Remove("connection");
            _connection.Close();
            Console.WriteLine("Disconnecting...");
        }
    }
}

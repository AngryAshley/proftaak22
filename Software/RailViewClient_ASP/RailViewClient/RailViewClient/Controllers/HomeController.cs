using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using RailViewClient.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RailViewClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            ////var myValue = config["Database:ConnectionString"];
            ////Console.WriteLine(myValue);

            //List<Notification> notifications = new List<Notification>();

            ////Connect to Mysql
            //using (MySqlConnection con = new MySqlConnection(config["Database:ConnectionString"]))
            //{
            //    con.Open();
            //    MySqlCommand cmd = new MySqlCommand(config["Database:Queries:GetAlert"], con);
            //    MySqlDataReader reader = cmd.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        Notification notification = new Notification();
            //        notification.Id = Convert.ToInt32(reader["id"]);
            //        notification.Alert = reader["alert"].ToString();
            //        notification.Route = reader["route"].ToString();
            //        notification.Times = reader["times"].ToString();

            //        notifications.Add(notification);
            //    }
            //    reader.Close();
            //    con.Close();
            //}

            return View();
        }

        public IActionResult Popup()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            // Label1.Text = DateTime.Now.Second.ToString();
        }
    }
}

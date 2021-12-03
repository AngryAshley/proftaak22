using Microsoft.AspNetCore.Mvc;
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

        public Root trainRoute;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Alert> alerts = new List<Alert>();

            //Connect to Mysql
            using (MySqlConnection con = new MySqlConnection("Server=192.168.161.205;Port=3306;Database=RailView;Uid=admin;Pwd=TopMaster99;"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from alerts", con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Alert alert = new Alert();
                    alert.id = Convert.ToInt32(reader["id"]);
                    alert.alert = reader["alert"].ToString();
                    alert.route = reader["route"].ToString();
                    alert.times = reader["times"].ToString();

                    alerts.Add(alert);
                }
                reader.Close();
                con.Close();
            }

            LoadJson();

            return View(alerts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void LoadJson()
        {
            Console.WriteLine("72");
            using (StreamReader r = new StreamReader("Data/Railway_Trajects.json"))
            {
                string json = r.ReadToEnd();
                trainRoute = JsonConvert.DeserializeObject<Root>(json);
                Console.WriteLine("Testing: " + trainRoute.Payload.Features[0].Geometry.Coordinates[0][1].ToString() + ", " + trainRoute.Payload.Features[0].Geometry.Coordinates[0][0].ToString());
            }
        }
    }
}

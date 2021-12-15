using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using RailViewClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailViewClient.Controllers
{
    public class AlertController : Controller
    {
        public IActionResult Index()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            //var myValue = config["Database:ConnectionString"];
            //Console.WriteLine(myValue);

            List<Notification> notifications = new List<Notification>();

            //Connect to Mysql
            using (MySqlConnection con = new MySqlConnection(config["Database:ConnectionString"]))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(config["Database:Queries:GetAlert"], con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Notification notification = new Notification();
                    notification.Id = Convert.ToInt32(reader["id"]);
                    notification.Cam_Id = Convert.ToInt32(reader["cam_id"]);
                    notification.Location_X = Convert.ToDouble(reader["location_x"]);
                    notification.Location_Y = Convert.ToDouble(reader["location_y"]);
                    notification.Alert = reader["alert"].ToString();
                    notification.Route = reader["route"].ToString();
                    notification.Times = reader["times"].ToString();
                    notification.Alert_Checked = Convert.ToBoolean(reader["alert_checked"]);

                    notifications.Add(notification);
                }
                reader.Close();
                con.Close();
            }

            return Json(notifications, new System.Text.Json.JsonSerializerOptions());
        }
    }
}

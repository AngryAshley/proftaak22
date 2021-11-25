using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RailData.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        string connectionString = "";
        MySqlConnection _connection;
        MySqlCommand cmd = null;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            
            if (HttpContext.Session.GetString("Loggedin") != null)
            {
                // log database
                string sql = "SHOW TABLES";
                cmd = new MySqlCommand(sql, _connection);

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    //TODO: log mysql reader
                }
            }
        }

        public void OnPost()
        {
            string Username = Request.Form["Username"].ToString();
            string Password = Request.Form["Password"].ToString();

            connectionString = "Server=192.168.161.205;Port=3306;Database=RailView;Uid=" + Username + ";Pwd=" + Password + ";";

            HttpContext.Session.SetString("Loggedin", Username);

            try
            {
                Console.WriteLine("Connecting...");
                _connection = new MySqlConnection(connectionString);
                Console.WriteLine("SUCCESS");
                _connection.Open();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

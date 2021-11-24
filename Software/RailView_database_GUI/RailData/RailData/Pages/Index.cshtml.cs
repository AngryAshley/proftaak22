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

        [Required]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        string connectionString = "";
        MySqlConnection _connection;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Console.WriteLine("test");
        }

        public void OnPost()
        {  
            connectionString = "Server=192.168.161.205;Port=3306;Database=RailView;Uid=" + Username + ";Pwd=" + Password + ";";

            try
            {
                Console.WriteLine("Hier komt het: ", Username, Password);
                _connection = new MySqlConnection(connectionString);
                _connection.Open();
                Response.Redirect("~/Privacy");
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

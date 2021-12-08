using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RailViewClient.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailViewClient.Controllers
{
    public class LocationController : Controller
    { 
        public IActionResult Index()
        {
            Train trainLocation = new Train();

            var client = new RestClient("https://gateway.apiportal.ns.nl/virtual-train-api/api/vehicle");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Ocp-Apim-Subscription-Key", "c4f11ff5e4ea4e13981610420db4a3b1");
            IRestResponse response = client.Execute(request);
            trainLocation = JsonConvert.DeserializeObject<Train>(response.Content);
            Console.WriteLine(response.Content);

            List<double> coords = new List<double>();

            for (int i = 0; i < trainLocation.Payload.Treinen.Count; i++)
            {
                coords.Add(trainLocation.Payload.Treinen[i].Lat);
                coords.Add(trainLocation.Payload.Treinen[i].Lng);
            }

            return Json(coords, new System.Text.Json.JsonSerializerOptions());
        }
    }
}

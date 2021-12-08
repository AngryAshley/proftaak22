using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace RailViewClient.Controllers
{
    public class RouteController : Controller
    {

        public IActionResult Index()
        {
            Route trainRoute = new Route();
            var client = new RestClient("https://gateway.apiportal.ns.nl/Spoorkaart-API/api/v1/spoorkaart");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Ocp-Apim-Subscription-Key", "c4f11ff5e4ea4e13981610420db4a3b1");
            IRestResponse response = client.Execute(request);
            trainRoute = JsonConvert.DeserializeObject<Route>(response.Content);
            Console.WriteLine(response.Content);

            List<double> coords = new List<double>();
            int count = 0;
            for (int i = 0; i < trainRoute.Payload.Features.Count; i++)
            {
                if (trainRoute.Payload.Features[i].Properties.From == "ehv" && trainRoute.Payload.Features[i].Properties.To == "hmbv")
                {
                    while (trainRoute.Payload.Features[i].Geometry.Coordinates.Count != count)
                    {
                        coords.Add(trainRoute.Payload.Features[i].Geometry.Coordinates[count][1]);
                        coords.Add(trainRoute.Payload.Features[i].Geometry.Coordinates[count][0]);
                        count++;
                    }
                }
            }

            return Json(coords, new System.Text.Json.JsonSerializerOptions());
        }
    }
}

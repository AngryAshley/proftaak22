using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public Root trainRoute;
        string tester;

        public IActionResult Index()
        {

            using (StreamReader r = new StreamReader("Data/Railway_Trajects.json"))
            {
                GetJsonData();
                string json = r.ReadToEnd();
                List<double> test = new List<double>();
                int count = 0;
                trainRoute = JsonConvert.DeserializeObject<Root>(json);

                for (int i = 0; i < trainRoute.Payload.Features.Count; i++)
                {
                    if (trainRoute.Payload.Features[i].Properties.From == "ehv" && trainRoute.Payload.Features[i].Properties.To == "hmbv")
                    {
                        while (trainRoute.Payload.Features[i].Geometry.Coordinates.Count != count)
                        {
                            test.Add(trainRoute.Payload.Features[i].Geometry.Coordinates[count][1]);
                            test.Add(trainRoute.Payload.Features[i].Geometry.Coordinates[count][0]);
                            count++;
                        }
                    }
                }
                Console.WriteLine(test);
                return Json(tester, new System.Text.Json.JsonSerializerOptions());
            }
        }

        public async void GetJsonData()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "{subscription key}");

            var uri = "https://gateway.apiportal.ns.nl/Spoorkaart-API/api/v1/spoorkaart?" + queryString;

            var tester = await client.GetAsync(uri);

            Console.WriteLine(tester);
        }
    }
}

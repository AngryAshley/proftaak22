using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailViewClient.Controllers
{
    public class NsApiController : Controller
    {
        public IRestResponse Index(string requestUrl)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var client = new RestClient(requestUrl);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader(config["Api:Type"], config["Api:Key"]);
            IRestResponse response = client.Execute(request);

            return response;
        }
    }
}

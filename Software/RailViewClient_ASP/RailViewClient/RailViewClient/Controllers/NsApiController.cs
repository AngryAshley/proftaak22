using Microsoft.AspNetCore.Mvc;
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
            var client = new RestClient(requestUrl);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Ocp-Apim-Subscription-Key", "c4f11ff5e4ea4e13981610420db4a3b1");
            IRestResponse response = client.Execute(request);

            return response;
        }
    }
}

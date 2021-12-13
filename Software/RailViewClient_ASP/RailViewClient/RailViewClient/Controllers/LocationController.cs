﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
            NsApiController nsApiController = new NsApiController();
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            IRestResponse response = nsApiController.Index("https://gateway.apiportal.ns.nl/virtual-train-api/api/vehicle");
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

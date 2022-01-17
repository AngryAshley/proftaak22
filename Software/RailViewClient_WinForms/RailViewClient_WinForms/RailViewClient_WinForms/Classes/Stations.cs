using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailViewClient_WinForms.Classes
{
    public class Namen
    {
        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("middel")]
        public string Middel { get; set; }

        [JsonProperty("kort")]
        public string Kort { get; set; }
    }

    public class Contents
    {
        [JsonProperty("UICCode")]
        public string UICCode { get; set; }

        [JsonProperty("stationType")]
        public string StationType { get; set; }

        [JsonProperty("EVACode")]
        public string EVACode { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("sporen")]
        public List<object> Sporen { get; set; }

        [JsonProperty("synoniemen")]
        public List<string> Synoniemen { get; set; }

        [JsonProperty("heeftFaciliteiten")]
        public bool HeeftFaciliteiten { get; set; }

        [JsonProperty("heeftVertrektijden")]
        public bool HeeftVertrektijden { get; set; }

        [JsonProperty("heeftReisassistentie")]
        public bool HeeftReisassistentie { get; set; }

        [JsonProperty("namen")]
        public Namen Namen { get; set; }

        [JsonProperty("land")]
        public string Land { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }

        [JsonProperty("radius")]
        public int Radius { get; set; }

        [JsonProperty("naderenRadius")]
        public int NaderenRadius { get; set; }

        [JsonProperty("ingangsDatum")]
        public string IngangsDatum { get; set; }
    }

    public class Stations
    {
        [JsonProperty("contents")]
        public List<Contents> Contents { get; set; }
    }
}

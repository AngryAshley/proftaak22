using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailViewClient_WinForms
{
    public class Links
    {
    }

    public class Treinen
    {
        [JsonProperty("treinNummer")]
        public int TreinNummer { get; set; }

        [JsonProperty("ritId")]
        public string RitId { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }

        [JsonProperty("snelheid")]
        public double Snelheid { get; set; }

        [JsonProperty("richting")]
        public double Richting { get; set; }

        [JsonProperty("horizontaleNauwkeurigheid")]
        public double HorizontaleNauwkeurigheid { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("bron")]
        public string Bron { get; set; }
    }

    public class Payload
    {
        [JsonProperty("treinen")]
        public List<Treinen> Treinen { get; set; }
    }

    public class Meta
    {
    }

    public class Positions
    {
        [JsonProperty("links")]
        public Links Links { get; set; }

        [JsonProperty("payload")]
        public Payload Payload { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}

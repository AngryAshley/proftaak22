using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Is dit een nette oplossing?
namespace RailViewClient.Models
{
    public class Links
    {
    }

    public class Treinen
    {
        public int TreinNummer { get; set; }
        public string RitId { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public double Snelheid { get; set; }
        public double Richting { get; set; }
        public double HorizontaleNauwkeurigheid { get; set; }
        public string Type { get; set; }
        public string Bron { get; set; }
    }

    public class Payload
    {
        public List<Treinen> Treinen { get; set; }
    }

    public class Meta
    {
    }

    public class Train
    {
        public Links Links { get; set; }
        public Payload Payload { get; set; }
        public Meta Meta { get; set; }
    }
}

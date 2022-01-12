using System;
using System.Collections.Generic;

namespace RailViewApi.Models
{
    public partial class Accident
    {
        public int AccidentId { get; set; }
        public string AccidentType { get; set; } = null!;
        public DateTime AccidentDate { get; set; }
    }
}

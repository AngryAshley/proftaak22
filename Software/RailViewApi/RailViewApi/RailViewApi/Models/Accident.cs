using System;
using System.Collections.Generic;

namespace testapi.Models
{
    public partial class Accident
    {
        public int AccidentId { get; set; }
        public string AccidentType { get; set; } = null!;
        public DateTime AccidentDate { get; set; }
    }
}

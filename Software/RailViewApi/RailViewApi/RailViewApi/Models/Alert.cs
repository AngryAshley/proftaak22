using System;
using System.Collections.Generic;

namespace RailViewApi.Models
{
    public partial class Alert
    {
        public int Id { get; set; }
        public int? CamId { get; set; }
        public string? Alert1 { get; set; }
        public double? LocationX { get; set; }
        public double? LocationY { get; set; }
        public string? Route { get; set; }
        public DateTime? Times { get; set; }
        public bool? AlertChecked { get; set; }
    }
}

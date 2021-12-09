using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailViewClient.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public int Cam_Id { get; set; }

        public string Alert { get; set; }

        public double Location_X { get; set; }

        public double Location_Y { get; set; }

        public string Route { get; set; }

        public string Times { get; set; }

        public bool Alert_Checked { get; set; }
    }
}

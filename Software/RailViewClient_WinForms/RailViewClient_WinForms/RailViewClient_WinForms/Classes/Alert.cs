using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailViewClient_WinForms
{
    public class Alert
    {
        public int id { get; set; }

        public int cam_Id { get; set; }

        public string alert { get; set; }

        public double location_X { get; set; }

        public double location_Y { get; set; }

        public string route { get; set; }

        public string times { get; set; }

        public bool alert_Checked { get; set; }
    }
}

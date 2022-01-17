using System;
using System.Collections.Generic;

namespace RailViewClient_WinForms.Classes
{
    public partial class Notification
    {
        public int NotificationId { get; set; }
        public int CameraId { get; set; }
        public int EmployeeId { get; set; }
        public int AccidentId { get; set; }
        public string StatusType { get; set; }
        public bool RequiredAction { get; set; }
    }
}

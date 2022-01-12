using System;
using System.Collections.Generic;

namespace testapi.Models
{
    public partial class Notification
    {
        public int NotificationId { get; set; }
        public int CameraId { get; set; }
        public int EmployeeId { get; set; }
        public int AccidentId { get; set; }
        public string StatusType { get; set; } = null!;
        public bool RequiredAction { get; set; }
    }
}

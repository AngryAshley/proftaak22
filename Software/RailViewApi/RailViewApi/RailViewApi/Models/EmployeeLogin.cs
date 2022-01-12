using System;
using System.Collections.Generic;

namespace RailViewApi.Models
{
    public partial class EmployeeLogin
    {
        public int LoginId { get; set; }
        public int EmployeeId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}

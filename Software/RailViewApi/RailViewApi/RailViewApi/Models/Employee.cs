using System;
using System.Collections.Generic;

namespace RailViewApi.Models
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int DepartmentId { get; set; }
        public int LoginId { get; set; }
    }
}

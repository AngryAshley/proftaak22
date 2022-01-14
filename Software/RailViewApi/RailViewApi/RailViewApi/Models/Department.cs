using System;
using System.Collections.Generic;

namespace RailViewApi.Models
{
    public partial class Department
    {
        public int DepartmentId { get; set; }
        public string Streetname { get; set; } = null!;
        public int HouseNumber { get; set; }
        public string PostalCode { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}

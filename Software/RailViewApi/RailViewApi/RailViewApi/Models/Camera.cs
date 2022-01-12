using System;
using System.Collections.Generic;

namespace RailViewApi.Models
{
    public partial class Camera
    {
        public int CameraId { get; set; }
        public int CoordinatesId { get; set; }
        public string CameraName { get; set; } = null!;
        public string StreamLink { get; set; } = null!;
    }
}

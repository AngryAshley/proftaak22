using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailViewClient_WinForms.Classes
{
    public class Camera : Object
    {
        GMapOverlay cameras = new GMapOverlay("cameras");

        public int CameraID { get; set; }
        public string CameraName { get; set; }
        public string CameraStream { get; set; }
        public double CameraLat { get; set; }
        public double CameraLng { get; set; }
        public bool CameraAlert { get; set; }

        public Camera() : this(0, "Camera 0", "stream link", 51.4531, 5.568, false)
        {
        }

        public Camera(int camera_id, string cameraName, string cameraStream, double cameraLat, double cameraLng, bool cameraAlert)
        {
            CameraID = camera_id;
            CameraName = cameraName;
            CameraStream = cameraStream;
            CameraLat = cameraLat;
            CameraLng = cameraLng;
            CameraAlert = cameraAlert;
        }

        public void CreateCamera()
        {
            GMapMarker cameraMarker = new GMarkerGoogle(
            new PointLatLng(CameraLat, CameraLng),
            new Bitmap("cctv.png"));
            cameras.Markers.Add(cameraMarker);
            cameraMarker.ToolTipText = CameraName;
            cameraMarker.Tag = CameraID;
        }

        public void AlertCamera()
        {
            if (CameraAlert == true)
            {
                GMapMarker cameraMarker = new GMarkerGoogle(
                new PointLatLng(CameraLat, CameraLng),
                new Bitmap("cctv.png"));
                cameras.Markers.Add(cameraMarker);
                cameraMarker.ToolTipText = CameraName;
                cameraMarker.Tag = CameraID;
            }
            else
            {
                GMapMarker cameraMarker = new GMarkerGoogle(
                new PointLatLng(CameraLat, CameraLng),
                new Bitmap("alert.png"));
                cameras.Markers.Add(cameraMarker);
                cameraMarker.ToolTipText = CameraName;
                cameraMarker.Tag = CameraID;
            }
        }
    }
}

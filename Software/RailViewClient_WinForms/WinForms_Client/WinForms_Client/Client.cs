using System;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using Newtonsoft.Json;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Geometry = WinForms_Client.Geometry;
using Features = WinForms_Client.Feature;

namespace WinForms_Client
{
    public partial class Client : Form
    {
        private Trajects traject;

        public Client()
        {
            InitializeComponent();

            traject = new Trajects();

            LoadJson();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //LoadCoordinates();
        }

        public void LoadCoordinates(GMapOverlay points)
        {
            //
            foreach (Features feat in traject.Payload.Features)
            {
                Console.WriteLine("new Feature:");

                Geometry geo = feat.Geometry;

                //for each list of double in the geometry's coordinates
                foreach (List<double> dinates in geo.Coordinates)
                {
                    //for each coordinate in the list of coordinates
                    bool start = true;
                    double din1 = 0;
                    double din2 = 0;


                    foreach (double coordinate in dinates)
                    {
                        if (start == true)
                        {
                            start = false;
                            din1 = coordinate;
                        }
                        else
                        {
                            din2 = coordinate;
                        }
                    }

                    //work with the coordinates
                    string output = din2.ToString() + ", " + din1.ToString();
                    Console.WriteLine(output);

                    GMapMarker point = new GMarkerGoogle(
                    new PointLatLng(din2, din1),
                    new Bitmap("dot.png"));
                    points.Markers.Add(point);
                }
            }
            gmap.Overlays.Add(points);
        }

        public void LoadJson()
        {
            using (StreamReader r = new StreamReader("Railway_Trajects.json"))
            {
                string json = r.ReadToEnd();
                Trajects trajects = JsonConvert.DeserializeObject<Trajects>(json);
                traject = trajects;
            }
        }

        private void ResetMap()
        {
            gmap.Position = new GMap.NET.PointLatLng(52.21299, 5.27937);
            gmap.MinZoom = 6;
            gmap.MaxZoom = 18;
            gmap.Zoom = 7;
            gmap.CanDragMap = true;
            gmap.MarkersEnabled = true;
            gmap.PolygonsEnabled = true;
        }

        private void gmap_Load(object sender, EventArgs e)
        {
            gmap.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gmap.CanDragMap = true;
            gmap.ShowCenter = false;

            GMapOverlay markers = new GMapOverlay("markers");
            GMapMarker marker = new GMarkerGoogle(
                new PointLatLng(51.4687928, 5.6342143),
            new Bitmap("cctv.png"));
            markers.Markers.Add(marker);
            gmap.Overlays.Add(markers);

            marker.ToolTipText = "\nRailcam Mierlo-Hout\n ";
            marker.ToolTip.Fill = Brushes.White;
            marker.ToolTip.Foreground = Brushes.Black;
            marker.ToolTip.Stroke = Pens.Black;
            marker.ToolTip.TextPadding = new Size(20, 0);

            GMapOverlay points = new GMapOverlay("points");
            LoadCoordinates(points);


            //GMapOverlay polyOverlay = new GMapOverlay("polygons");
            //IList points = new List();
            //points.Add(new GMap.NET.LatLng());

            ResetMap();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetMap();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PopOut PopOut = new PopOut();
            PopOut.Show(this);
        }

        private void gmap_OnMarkerClick_1(GMapMarker item, MouseEventArgs e)
        {
            Console.WriteLine(String.Format("Marker {0} was clicked.", item.Tag));
            PopOut PopOut = new PopOut();
            PopOut.Show(this);
        }
    }
}
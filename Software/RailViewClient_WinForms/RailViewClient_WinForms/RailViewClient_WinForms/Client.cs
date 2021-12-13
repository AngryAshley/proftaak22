using System;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using RailViewClient;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Collections.Generic;
using System.Drawing;
using RestSharp;
using Geometry = RailViewClient.Geometry;
using MySql.Data.MySqlClient;
using Positions = RailViewClientTrain.Positions;

namespace RailViewClient_WinForms
{
    public partial class Client : Form
    {
        public Trajects traject;
        public Positions position;

        bool showTrains = true;

        GMapRoute trajectsRoute;
        GMapOverlay trajectsOverlay;

        GMapOverlay trains = new GMapOverlay("trains");
        GMapOverlay points = new GMapOverlay("points");

        List<Alert> alertList = new List<Alert>();

        public Client()
        {
            InitializeComponent();

            traject = new Trajects();
            position = new Positions();

            LoadJson();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //SQLrequest();
        }

        public void LoadTrainsTimer()
        {
            System.Windows.Forms.Timer timerAlert = new System.Windows.Forms.Timer();
            timerAlert.Tick += OnTimerTick;
            timerAlert.Interval = 2500;
            timerAlert.Enabled = true;
        }

        public void OnTimerTick(object sender, EventArgs e)
        {
            Console.WriteLine("trein coord reset");
            LoadAPI();
            LoadTrains(trains);
        }

        public void SQLrequest()
        {
            using (MySqlConnection con = new MySqlConnection("Server=192.168.161.205;Port=3306;Database=RailView;Uid=admin;Pwd=TopMaster99;"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from alerts", con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Alert alert = new Alert();
                    alert.id = Convert.ToInt32(reader["id"]);
                    alert.alert = reader["alert"].ToString();
                    alert.route = reader["route"].ToString();
                    alert.times = reader["times"].ToString();

                    alertList.Add(alert);
                    string alerts = alert.times + " " + alert.route + "\n" + alert.id + " " + alert.alert + "\n\n";

                    richTextBox1.BeginInvoke(new Action(() =>
                    {
                        richTextBox1.AppendText(alerts);
                    }));
                }
                reader.Close();
                con.Close();
            }
        }

        public void LoadTrajects(GMapOverlay points)
        {
            foreach (var trajects in traject.Payload.Features)
            {
                trajectsRoute = new GMapRoute("traject_line");
                trajectsRoute.Stroke = new Pen(Brushes.DarkBlue, 2);

                trajectsOverlay = new GMapOverlay("trajectOverlay");
                trajectsOverlay.Routes.Add(trajectsRoute);
                gmap.Overlays.Add(trajectsOverlay);
                gmap.UpdateRouteLocalPosition(trajectsRoute);

                Console.WriteLine("Traject: ");

                Geometry geometry = trajects.Geometry;

                double[,] trajectCoords = new double[geometry.Coordinates.Count, 2];
                int index = 0;



                foreach (List<double> coords in geometry.Coordinates)
                {
                    bool begin = true;
                    double coord1 = 0;
                    double coord2 = 0;

                    foreach (double coord in coords)
                    {
                        if (begin == true)
                        {
                            begin = false;
                            coord1 = coord;
                        }
                        else
                        {
                            coord2 = coord;
                        }
                    }
                    trajectCoords[index, 0] = coord2;
                    trajectCoords[index, 1] = coord1;
                    Console.WriteLine(trajectCoords[index, 0] + ", " + trajectCoords[index, 1]);
                    trajectsRoute.Points.Add(new PointLatLng(trajectCoords[index, 0], trajectCoords[index, 1]));
                    index++;
                }
            }
        }

        public void LoadTrains(GMapOverlay trains)
        {
            string train = string.Empty;

            if (showTrains == true)
            {
                trains.Markers.Clear();

                foreach (var positions in position.Payload.Treinen)
                {
                    double lat = positions.Lat;
                    double lng = positions.Lng;
                    string type = positions.Type;
                    double velocity = positions.Snelheid;
                    string train_id = positions.RitId;

                    if (type == "SPR")
                    {
                        type = "Sprinter";
                        train = "SPR.png";
                    }
                    if (type == "IC")
                    {
                        type = "InterCity";
                        train = "IC.png";
                    }
                    if (type == "ARR")
                    {
                        type = "Arriva";
                        train = "ARR.png";
                    }

                    GMapMarker traincoord = new GMarkerGoogle(
                    new PointLatLng(lat, lng),
                    new Bitmap(train));
                    trains.Markers.Add(traincoord);
                    traincoord.ToolTipText = "\nTrain Info\nID: " + train_id + "\nType: " + type + "\nVelocity: " + Math.Round(velocity, 1) + " km/h";
                }
                gmap.Overlays.Add(trains);
            }
            else
            {
                trains.Markers.Clear();
            }
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

        public void LoadAPI()
        {
            var client = new RestClient("https://gateway.apiportal.ns.nl/virtual-train-api/api/vehicle");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Ocp-Apim-Subscription-Key", "aec6b0eaaa984650838bc801f38a46ab");
            IRestResponse response = client.Execute(request);

            var positions = JsonConvert.DeserializeObject<Positions>(response.Content);
            position = positions;
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

            LoadTrajects(points);

            ResetMap();
            LoadTrainsTimer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetMap();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 PopOut = new Form2();
            PopOut.Show(this);
        }

        private void gmap_OnMarkerClick_1(GMapMarker item, MouseEventArgs e)
        {
            Console.WriteLine(String.Format("Marker {0} was clicked.", item.Tag));
            Form2 PopOut = new Form2();
            PopOut.Show(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double latitude = gmap.Position.Lat;
            double longitude = gmap.Position.Lng;
            Console.WriteLine("Lat: " + latitude + "Lng: " + longitude);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            showTrains = !showTrains;
        }
    }
}

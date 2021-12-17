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
using MySql.Data.MySqlClient;
using Geometry = RailViewClient.Geometry;
using Positions = RailViewClientTrain.Positions;
using Stations = RailViewClient_Stations.Stations;

namespace RailViewClient_WinForms
{
    public partial class Client : Form
    {
        TextWriter _writer = null;

        public Trajects traject = new Trajects();
        public Positions position = new Positions();
        public Stations station = new Stations();

        Timer timerTrains = new Timer();
        int trainTimerInterval = 2500;
        bool refreshTrains = true;
        bool showTrains = true;
        bool showStations = false;

        //trajectsRoute can be added to an array
        GMapRoute trajectsRoute;
        GMapOverlay trajectsOverlay;

        GMapOverlay trainstations = new GMapOverlay("trainstations");
        GMapOverlay trains = new GMapOverlay("trains");
        GMapOverlay points = new GMapOverlay("points");
        GMapOverlay markers = new GMapOverlay("markers");

        Timer timerAlert = new Timer();
        List<string> alertList = new List<string>();
        List<string> oldAlertList = new List<string>();
        private int ItemMargin = 5;
        string alerts = string.Empty;
        bool refreshAlerts = true;

        bool alertPerson = false;
        bool alertOther = false;
        bool do_Once = false;

        public Client()
        {
            InitializeComponent();
            LoadJson();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtConsole.ReadOnly = true;
            txtConsole.ScrollBars = ScrollBars.Vertical;
            _writer = new TextBoxStreamWriter(txtConsole);
            Console.SetOut(_writer);

            numericUpDown1.Increment = 100;
            numericUpDown1.Maximum = 10000;
            numericUpDown1.Minimum = 500;
            numericUpDown1.Value = trainTimerInterval;


        }

        public void LoadTrainsTimer()
        {
            timerTrains.Tick += OnTimerTickTrain;
            timerTrains.Interval = trainTimerInterval;
            timerTrains.Enabled = refreshTrains;
        }

        public void LoadAlertTimer()
        {
            timerAlert.Tick += OnTimerTickAlert;
            timerAlert.Interval = 1000;
            timerAlert.Enabled = refreshAlerts;
        }

        public void OnTimerTickTrain(object sender, EventArgs e)
        {
            if (refreshTrains == true)
            {
                //Console.WriteLine("trein coord reset");
                LoadAPI();
                LoadTrains(trains);
            }
        }

        public void OnTimerTickAlert(object sender, EventArgs e)
        {
            if (refreshAlerts == true)
            {
                //Console.WriteLine(DateTime.Now + " Alert timer refresh");
                SQLrequest();
            }
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

                    alerts = alert.times + " \n" + alert.route + "\n" + alert.id + " " + alert.alert + "\n";
                    alertList.Add(alerts);
                }

                if (oldAlertList.Count == 0)
                {
                    listBox1.DataSource = alertList;
                    oldAlertList = new List<string>(alertList);
                }

                if (alertList.Count != oldAlertList.Count)
                {
                    listBox1.DataSource = null;
                    listBox1.DataSource = alertList;
                    oldAlertList = new List<string>(alertList);

                    if (alertList[alertList.Count - 1].Contains("person") == true)
                    {
                        Console.WriteLine(DateTime.Now + " Person detected");
                        alertPerson = true;
                        //alertPopUp();
                    }
                    if (alertList[alertList.Count - 1].Contains("train") == true)
                    {
                        Console.WriteLine(DateTime.Now + " Train detected");
                    }
                    if (alertList[alertList.Count - 1].Contains("other") == true)
                    {
                        Console.WriteLine(DateTime.Now + " Something other detected");
                        alertOther = true;
                        //alertPopUp();
                    }

                }

                reader.Close();
                con.Close();
            }
            alertList.Clear();
            alertListBox();
        }

        public void alertPopUp()
        {
            if (do_Once == false)
            {
                do_Once = true;
                if (alertPerson == true)
                {
                    AlertCamera();
                    MessageBox.Show("A person has been detected at:\n" + alertList[alertList.Count - 1]);
                    alertPerson = false;
                    //DefaultCamera();
                }
                if (alertOther == true)
                {
                    AlertCamera();
                    MessageBox.Show("Some movement has been detected at:\n" + alertList[alertList.Count - 1]);
                    alertOther = false;
                    //DefaultCamera();
                }
            }
        }

        public void DefaultCamera()
        {
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
        }

        public void AlertCamera()
        {
            GMapMarker marker = new GMarkerGoogle(
                new PointLatLng(51.4687928, 5.6342143),
            new Bitmap("alert.png"));
            markers.Markers.Add(marker);
            gmap.Overlays.Add(markers);

            marker.ToolTipText = "\nRailcam Mierlo-Hout\n ";
            marker.ToolTip.Fill = Brushes.White;
            marker.ToolTip.Foreground = Brushes.Black;
            marker.ToolTip.Stroke = Pens.Black;
            marker.ToolTip.TextPadding = new Size(20, 0);

            gmap.Position = new PointLatLng(51.4687928, 5.6342143);
            gmap.Zoom = 12;

            Form2 PopOut = new Form2();
            PopOut.Show(this);
        }

        public void alertListBox()
        {
            listBox1.DrawMode = DrawMode.OwnerDrawVariable;
        }

        private void lstChoices_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            // Get the ListBox and the item.
            ListBox listBox1 = sender as ListBox;
            string txt = (string)listBox1.Items[e.Index];

            // Measure the string.
            SizeF txt_size = e.Graphics.MeasureString(txt, this.Font);

            // Set the required size.
            e.ItemHeight = (int)txt_size.Height + 2 * ItemMargin;
            e.ItemWidth = (int)txt_size.Width;
        }

        private void lstChoices_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Get the ListBox and the item.
            ListBox listBox1 = sender as ListBox;
            string txt = (string)listBox1.Items[e.Index];

            // Draw the background.
            e.DrawBackground();

            // See if the item is selected.
            if ((e.State & DrawItemState.Selected) ==
                DrawItemState.Selected)
            {
                // Selected. Draw with the system highlight color.
                e.Graphics.DrawString(txt, this.Font,
                    SystemBrushes.HighlightText, e.Bounds.Left,
                        e.Bounds.Top + ItemMargin);
            }
            else
            {
                // Not selected. Draw with ListBox's foreground color.
                using (SolidBrush br = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(txt, this.Font, br,
                        e.Bounds.Left, e.Bounds.Top + ItemMargin);
                }
            }

            // Draw the focus rectangle if appropriate.
            e.DrawFocusRectangle();
        }

        public void LoadTrajects(GMapOverlay points)
        {
            foreach (var trajects in traject.Payload.Features)
            {
                //trajectsRoute can be added to an array
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

        public void LoadStations(GMapOverlay trainstations)
        {
            if (showStations == true)
            {
                trainstations.Markers.Clear();

                foreach (var stations in station.Contents)
                {
                    double stationLat = stations.Lat;
                    double stationLng = stations.Lng;
                    string stationName = stations.Namen.Lang;

                    GMapMarker stationMarker = new GMarkerGoogle(
                        new PointLatLng(stationLat, stationLng),
                        new Bitmap("station.png"));
                    trainstations.Markers.Add(stationMarker);
                    //stationMarker.ToolTipMode = MarkerTooltipMode.Always;
                    stationMarker.ToolTipText = stationName;
                }
                gmap.Overlays.Add(trainstations);
            }
            else
            {
                trainstations.Markers.Clear();
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

            using (StreamReader r = new StreamReader("Stations.json"))
            {
                string json = r.ReadToEnd();
                Stations stations = JsonConvert.DeserializeObject<Stations>(json);
                station = stations;
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
            gmap.Position = new PointLatLng(52.21299, 5.27937);
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

            DefaultCamera();
            LoadTrajects(points);

            ResetMap();
            LoadTrainsTimer();
            LoadAlertTimer();
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

        private void button5_Click(object sender, EventArgs e)
        {
            showStations = !showStations;
            LoadStations(trainstations);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SQLrequest();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            refreshTrains = !refreshTrains;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            refreshAlerts = !refreshAlerts;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            listBox1.DataSource = null;
            alertList.Add("Test:\n" + DateTime.Now + "\n" + "Eindhoven naar Tilburg\n" + "0 person");
            alertPerson = true;
            refreshAlerts = false;
            SQLrequest();
            //alertPopUp();
            refreshAlerts = true;

        }

        private void button10_Click(object sender, EventArgs e)
        {
            trainTimerInterval = Convert.ToInt32(numericUpDown1.Value);
            timerTrains.Interval = trainTimerInterval;
        }
    }


}

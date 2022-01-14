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
using RailViewClient_WinForms.Classes;

namespace RailViewClient_WinForms
{
    public partial class ClientForm : Form
    {
        TextWriter _writer = null;

        MySqlConnection con = new MySqlConnection("Server=192.168.161.205;Port=3306;Database=RailView;Uid=admin;Pwd=TopMaster99;");

        public Trajects traject = new Trajects();
        public Positions position = new Positions();
        public Stations station = new Stations();

        GMapRoute trajectsRoute;
        GMapOverlay trajectsOverlay;

        GMapOverlay trainstations = new GMapOverlay("trainstations");
        GMapOverlay trains = new GMapOverlay("trains");
        GMapOverlay points = new GMapOverlay("points");
        GMapOverlay markers = new GMapOverlay("markers");

        Camera camera = new Camera();

        public ClientForm()
        {
            InitializeComponent();
            LoadJson();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            LoadTrainsTimer();
            LoadAlertTimer();

            txtConsole.ReadOnly = true;
            txtConsole.ScrollBars = ScrollBars.Vertical;
            _writer = new TextBoxStreamWriter(txtConsole);
            Console.SetOut(_writer);

            nrUpDwn_TimerInterval.Increment = 100;
            nrUpDwn_TimerInterval.Maximum = 10000;
            nrUpDwn_TimerInterval.Minimum = 500;
            nrUpDwn_TimerInterval.Value = trainTimerInterval;
        }
        #region Timers
        Timer timerTrains = new Timer();
        int trainTimerInterval = 5000;
        bool pauseAlerts = true;
        bool pauseTrains = true;
        bool showTrains = true;
        bool showStations = false;
        public void LoadTrainsTimer()
        {
            timerTrains.Tick += OnTimerTickTrain;
            timerTrains.Interval = trainTimerInterval;
            timerTrains.Enabled = pauseTrains;
        }

        public void LoadAlertTimer()
        {
            timerAlert.Tick += OnTimerTickAlert;
            timerAlert.Interval = 1000;
            timerAlert.Enabled = pauseAlerts;
        }

        public void OnTimerTickTrain(object sender, EventArgs e)
        {
            if (pauseTrains == true)
            {
                LoadAPI();
                LoadTrains(trains);
            }
        }

        public void OnTimerTickAlert(object sender, EventArgs e)
        {
            if (pauseAlerts == true)
            {
                SQLrequest();
            }
        }
        private void btn_PauseTrains_Click(object sender, EventArgs e)
        {
            pauseTrains = !pauseTrains;
        }

        private void btn_PauseAlerts_click(object sender, EventArgs e)
        {
            pauseAlerts = !pauseAlerts;
        }

        private void btn_ChangeTimerInterval_Click(object sender, EventArgs e)
        {
            trainTimerInterval = Convert.ToInt32(nrUpDwn_TimerInterval.Value);
            timerTrains.Interval = trainTimerInterval;
        }
        #endregion
        #region Alerts

        Timer timerAlert = new Timer();
        List<string> alertList = new List<string>();
        List<string> oldAlertList = new List<string>();
        private int ItemMargin = 5;
        string alerts = string.Empty;

        bool alertPerson = false;
        bool alertOther = false;
        bool do_Once = false;

        public void SQLrequest()
        {
            con.Close();

            using (con)
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

                if (oldAlertList.Count == 0 || alertList.Count < oldAlertList.Count)
                {
                    listBoxAlerts.DataSource = null;
                    listBoxAlerts.DataSource = alertList;
                    oldAlertList = new List<string>(alertList);
                }

                if (alertList.Count > oldAlertList.Count)
                {
                    listBoxAlerts.DataSource = null;
                    listBoxAlerts.DataSource = alertList;
                    oldAlertList = new List<string>(alertList);

                    if (alertList[alertList.Count - 1].Contains("person") == true)
                    {
                        Console.WriteLine(DateTime.Now + " Person detected.");
                        alertPerson = true;
                    }
                    if (alertList[alertList.Count - 1].Contains("train") == true)
                    {
                        Console.WriteLine(DateTime.Now + " Train detected.");
                    }
                    if (alertList[alertList.Count - 1].Contains("other") == true)
                    {
                        Console.WriteLine(DateTime.Now + " Something other detected.");
                        alertOther = true;
                    }
                    alertPopUp();
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
                    //if (camera.CameraAlert == true)
                    //{                        
                    //}
                    //else
                    //{
                    alertPerson = false;
                    //DefaultCamera();
                    //}                    
                }
                if (alertOther == true)
                {
                    //if (camera.CameraAlert == false)
                    //{

                    //}                   
                    alertOther = false;
                    //DefaultCamera();
                }
            }
            do_Once = false;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            int alertCount = 0;

            con.Close();

            using (con)
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from alerts", con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Alert alert = new Alert();
                    alert.id = Convert.ToInt32(reader["id"]);
                    alertCount = alert.id;
                }
                reader.Close();

                alertCount++;
                MySqlCommand cmd2 = new MySqlCommand("INSERT INTO alerts(id, cam_id, alert, location_x, location_y, route, alert_checked) VALUES(" + alertCount + ", 4, 'person', 51.4531, 5.568, 'test', 0);", con);
                cmd2.ExecuteNonQuery();

            }
            con.Close();
            SQLrequest();
        }
        #endregion
        public void DefaultCamera()
        {
            click_Once = false;

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
            marker.Tag = marker.ToolTipText;
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

            PopoutForm frmPopout = new PopoutForm(this);
            frmPopout.Show(this);
        }
        #region Alert Listbox
        public void alertListBox()
        {
            listBoxAlerts.DrawMode = DrawMode.OwnerDrawVariable;
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
        #endregion
        #region Load API's and json files
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
        #endregion
        #region GMap
        bool click_Once = false;

        private void gmap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (click_Once == false)
            {
                click_Once = true;

                if (item.Tag != null)
                {
                    Console.WriteLine(String.Format("Camera {0}was clicked.", item.Tag));
                    PopoutForm PopOut = new PopoutForm(this);
                    PopOut.Show(this);
                }
                else
                {
                    click_Once = false;
                }
            }
        }

        private void ResetMap()
        {
            gmap.Position = new PointLatLng(52.21299, 5.27937);
            gmap.MinZoom = 5;
            gmap.MaxZoom = 18;
            gmap.Zoom = 7;
            gmap.CanDragMap = true;
            gmap.MarkersEnabled = true;
            gmap.PolygonsEnabled = true;
            lblZoom.Text = "Zoom: " + Convert.ToString(gmap.Zoom);
        }

        private void gmap_Load(object sender, EventArgs e)
        {
            gmap.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gmap.CanDragMap = true;
            gmap.ShowCenter = false;

            DefaultCamera();
            LoadTrajects(points);
            ResetMap();
        }
        private void gmap_OnMapZoomChanged()
        {
            lblZoom.Text = "Zoom: " + Convert.ToString(gmap.Zoom);

            if (gmap.Zoom <= 7)
            {
                trainTimerInterval = 5000;
            }
            if (gmap.Zoom > 7 && gmap.Zoom < 11)
            {
                trainTimerInterval = 3000;
            }
            if (gmap.Zoom >= 11)
            {
                trainTimerInterval = 1000;
            }

            if (nrUpDwn_TimerInterval.Value != trainTimerInterval)
            {
                nrUpDwn_TimerInterval.Value = trainTimerInterval;
                timerTrains.Interval = trainTimerInterval;
                Console.WriteLine("Trains update interval has changed to: " + trainTimerInterval);
            }
        }
        #endregion
        #region Buttons
        private void btn_ResetMapClick(object sender, EventArgs e)
        {
            ResetMap();
        }

        private void btn_PopOutClick(object sender, EventArgs e)
        {
            PopoutForm PopOut = new PopoutForm(this);
            PopOut.Show(this);
        }



        private void btn_GetPosClick(object sender, EventArgs e)
        {
            double latitude = gmap.Position.Lat;
            double longitude = gmap.Position.Lng;
            Console.WriteLine("Lat: " + latitude + "Lng: " + longitude);
        }

        private void btn_ShowTrainsClick(object sender, EventArgs e)
        {
            showTrains = !showTrains;
        }

        private void btn_ShowStationsClick(object sender, EventArgs e)
        {
            showStations = !showStations;
            LoadStations(trainstations);
        }

        private void btn_RequestSQLClick(object sender, EventArgs e)
        {
            SQLrequest();
        }

        #endregion
    }
}

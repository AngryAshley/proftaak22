using System;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Collections.Generic;
using System.Drawing;
using RestSharp;
using MySql.Data.MySqlClient;
using RailViewClient_WinForms.Classes;
using Positions = RailViewClient_WinForms.Positions;
using System.Linq;

namespace RailViewClient_WinForms
{
    public partial class ClientForm : Form
    {
        TextWriter _writer = null;

        MySqlConnection con = new MySqlConnection("Server=192.168.161.205;Port=3306;Database=RailViewv2;Uid=admin;Pwd=TopMaster99;");

        public Trajects traject = new Trajects();
        public Positions position = new Positions();
        public Stations station = new Stations();

        GMapRoute trajectsRoute;
        GMapOverlay trajectsOverlay;

        GMapOverlay trainstations = new GMapOverlay("trainstations");
        GMapOverlay trains = new GMapOverlay("trains");
        GMapOverlay points = new GMapOverlay("points");
        GMapOverlay markers = new GMapOverlay("markers");

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

        public void SQLrequest()
        {
            try
            {
                con.Close();

                using (con)
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT `n`.`Notification_ID` AS `NotificationId`, `n`.`Camera_ID` AS `CameraId`, `n`.`Employee_ID` AS `EmployeeId`, `n`.`Accident_ID` AS `AccidentId`, `n`.`Status_Type` AS `StatusType`, `n`.`Required_Action` AS `RequiredAction`, `c`.`Coordinates_ID` AS `CoordinatesId`, `c`.`Camera_Name` AS `CameraName`, `c`.`Stream_Link` AS `StreamLink`, `c0`.`longtitude` AS `Longtitude`, `c0`.`latitude` AS `Latitude`, `a`.`Accident_Date` AS `AccidentDate`, `a`.`Accident_Type` AS `AccidentType`" +
                                                        "FROM `Notification` AS `n` " +
                                                        "INNER JOIN `Camera` AS `c` ON `n`.`Camera_ID` = `c`.`Camera_ID` " +
                                                        "INNER JOIN `Coordinates` AS `c0` ON `c`.`Coordinates_ID` = `c0`.`Coordinates_ID` " +
                                                        "INNER JOIN `Accident` AS `a` ON `n`.`Accident_ID` = `a`.`Accident_ID` ", con);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    GMapMarker marker = null;

                    while (reader.Read())
                    {
                        Notification notification = new Notification();
                        notification.NotificationId = (int)reader["notificationId"];
                        notification.CameraId = (int)reader["cameraId"];
                        notification.AccidentId = (int)reader["accidentId"];
                        notification.StatusType = (string)reader["statusType"];
                        notification.RequiredAction = (bool)reader["requiredAction"];

                        Accident accident = new Accident();
                        accident.AccidentId = (int)reader["accidentId"];
                        accident.AccidentType = (string)reader["accidentType"];
                        accident.AccidentDate = (DateTime)reader["accidentDate"];

                        Camera camera = new Camera();
                        camera.CameraId = (int)reader["cameraId"];
                        camera.CameraName = (string)reader["cameraName"];
                        camera.CoordinatesId = (int)reader["coordinatesId"];

                        Coordinate coordinate = new Coordinate();
                        coordinate.CoordinatesId = (int)reader["coordinatesId"];
                        coordinate.Latitude = (double)reader["latitude"];
                        coordinate.Longtitude = (double)reader["longtitude"];

                        alerts = accident.AccidentDate + " \n" + camera.CameraName + "\n" + accident.AccidentType + "\n" + "Status: "+ notification.StatusType + "\n" + "Action required: " +notification.RequiredAction;
                        alertList.Add(alerts);                     

                        if (notification.StatusType == "closed")
                        {
                            marker = new GMarkerGoogle(
                            new PointLatLng(coordinate.Latitude, coordinate.Longtitude),
                            new Bitmap("cctv.png"));
                        }
                        else if (notification.StatusType == "open")
                        {
                            marker = new GMarkerGoogle(
                            new PointLatLng(coordinate.Latitude, coordinate.Longtitude),
                            new Bitmap("alert.png"));
                        }
                        marker.ToolTipText = "\n" + camera.CameraName + "\n ";
                        marker.ToolTip.Fill = Brushes.White;
                        marker.ToolTip.Foreground = Brushes.Black;
                        marker.ToolTip.Stroke = Pens.Black;
                        marker.ToolTip.TextPadding = new Size(20, 0);
                        marker.Tag = camera.CameraId;

                        markers.Markers.Add(marker);
                    }


                    if (oldAlertList.Count == 0 /*|| alertList.Count < oldAlertList.Count*/ || oldAlertList.SequenceEqual(alertList)== false)
                    {
                        markers.Clear();
                        listBoxAlerts.DataSource = null;
                        listBoxAlerts.DataSource = alertList;

                        if (oldAlertList.SequenceEqual(alertList)== false)
                        {
                            oldAlertList = new List<string>(alertList);

                            if (alertList[alertList.Count - 1].Contains("person") == true)
                            {
                                Console.WriteLine(DateTime.Now + " Person detected.");
                            }
                            if (alertList[alertList.Count - 1].Contains("train") == true)
                            {
                                Console.WriteLine(DateTime.Now + " Train detected.");
                            }
                            if (alertList[alertList.Count - 1].Contains("other") == true)
                            {
                                Console.WriteLine(DateTime.Now + " Something other detected.");
                            }
                        }

                        gmap.Overlays.Add(markers);
                    }

                    //if (alertList.Count > oldAlertList.Count/* || alertList.SequenceEqual(oldAlertList)== false*/)
                    //{
                    //    markers.Clear();
                    //    listBoxAlerts.DataSource = null;
                    //    listBoxAlerts.DataSource = alertList;
                    //    oldAlertList = new List<string>(alertList);

                    //    if (alertList[alertList.Count - 1].Contains("person") == true)
                    //    {
                    //        Console.WriteLine(DateTime.Now + " Person detected.");
                    //    }
                    //    if (alertList[alertList.Count - 1].Contains("train") == true)
                    //    {
                    //        Console.WriteLine(DateTime.Now + " Train detected.");
                    //    }
                    //    if (alertList[alertList.Count - 1].Contains("other") == true)
                    //    {
                    //        Console.WriteLine(DateTime.Now + " Something other detected.");
                    //    }
                    //    gmap.Overlays.Add(markers);
                    //}
                    reader.Close();
                    con.Close();
                }
                alertList.Clear();
                alertListBox();
            }
            catch (Exception e)
            {
                pauseAlerts = false;
                MessageBox.Show(e.ToString());
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            int alertCount = 0;

            con.Close();

            using (con)
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Accident", con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Accident accident = new Accident();
                    accident.AccidentId = (int)reader["AccidentId"];
                    alertCount = accident.AccidentId;


                }
                reader.Close();

                alertCount++;
                MySqlCommand cmd2 = new MySqlCommand("INSERT INTO Accident(id, cam_id, alert, location_x, location_y, route, alert_checked) VALUES(" + alertCount + ", 4, 'person', 51.4531, 5.568, 'test', 0);", con);
                cmd2.ExecuteNonQuery();

            }
            con.Close();
            SQLrequest();
        }
        #endregion
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
                    Console.WriteLine(String.Format("Camera {0} was clicked.", item.Tag));
                    PopoutForm PopOut = new PopoutForm(this, (int)item.Tag);
                    PopOut.Show(this);
                }
                else
                {
                    click_Once = false;
                }
            }
        }

        public void FalseAlertClick(int cameraId)
        {
            click_Once = false;
            using (con)
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE Notification " +
                                                    "SET Status_Type = 'closed', Required_Action = false " +
                                                    "WHERE camera_id = " + cameraId + " AND Status_Type = 'open'; ", con);
                MySqlDataReader reader = cmd.ExecuteReader();
                con.Close();
            }
        }

        public void AlertClick(int cameraId)
        {
            click_Once = false;
            using (con)
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE Notification " +
                                                    "SET Status_Type = 'closed', Required_Action = true " +
                                                    "WHERE camera_id = " + cameraId + " AND Status_Type = 'open'; ", con);
                MySqlDataReader reader = cmd.ExecuteReader();
                con.Close();
            }
        }

        public void ClickOnce()
        {
            click_Once = false;
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
            var lat1 = 51.44524;
            var lat2 = 51.4687928;
            var lon1 = 5.49769;
            var lon2 = 5.6342143;

            var R = 6371e3; // metres
            var φ1 = lat1 * Math.PI / 180; // φ, λ in radians
            var φ2 = lat2 * Math.PI / 180;
            var Δφ = (lat2 - lat1) * Math.PI / 180;
            var Δλ = (lon2 - lon1) * Math.PI / 180;

            var a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                      Math.Cos(φ1) * Math.Cos(φ2) *
                      Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var d = R * c; // in metres

            // / 1000 for km
            MessageBox.Show("answer = " + d / 1000);

            PopoutForm PopOut = new PopoutForm(this, 1);
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

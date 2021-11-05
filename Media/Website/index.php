<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">

    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.2.0/dist/leaflet.css" />
    <link rel="stylesheet" href="https://unpkg.com/leaflet-routing-machine@latest/dist/leaflet-routing-machine.css" />
    <script src="https://unpkg.com/leaflet@1.2.0/dist/leaflet.js"></script>
    <script src="https://unpkg.com/leaflet-routing-machine@latest/dist/leaflet-routing-machine.js"></script>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous">
    <link rel="stylesheet" href="css/main.css">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>RailView</title>
</head>
<body>

    <nav class="navbar navbar-dark navbar-expand-lg bg-dark d-flex justify-content-center mb-3">
        <a class="navbar-brand" href="#">RailView</a>
    </nav>

    <div class="container">
        <div class="row">
            <div class="col col-lg-3">
                <h4>Meldingen</h4>
                <div id="log" class="border border-dark border-2">
                    <?php 
                        for ($i = 0; $i < 6; $i++)
                        {
                            ?>
                            <div class="text-center border-top border-dark border-2">
                                <p class="px-2 pt-2">
                                    {02-11-2021 15:16:20} EINDHOVEN - HELMOND ALERT!!
                                </p>
                            </div>
                            <?php
                        }
                    ?>
                </div>

                <button type="button" class="btn btn-primary my-2">Bel 112</button>

            </div>
            <div class="col">
                <div id="map"></div>
            </div>
        </div>
    </div>
</body>
</html>
<script>
    //Javascript variables
    var win;
    var eindhovenMarker ;
    var helmondMarker;
    var polyline;

    //camera icon
    var cctvIcon = L.icon({
    iconUrl: 'images/cctv.png',
    iconSize:[40, 40],});

    //alert icon
    var alertIcon = L.icon({
    iconUrl: 'images/alert.png',
    iconSize:[40, 40],});

    //load in map and map settings
    var map = L.map('map', {
        center: [51.4993, 5.6570],
        zoom: 8,
        minZoom: 8,
        maxZoom: 18,
        maxBounds: [
            [50.138758, 2.194824],
            [53.921042, 7.863770]
        ],
        zoomControl: false

    });

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(map);

    //polyline coordinates
    var latlngs = [
        [51.4432, 5.4797],
        [51.4446, 5.4897],
        [51.4448, 5.5021],
        [51.4531, 5.5680],
        [51.4534, 5.5710],
        [51.4756, 5.6620]
    ];

    var camera = L.marker([51.4531, 5.5680], {icon: alertIcon}).addTo(map);
    var test = L.marker([51.4432, 5.4797], {icon: alertIcon}).addTo(map);

    //onlick event for camera
    camera.on('click', function(){
        createWindow("popup.php", 1000, 660);
        eindhovenMarker = L.marker([51.4432, 5.4797]).addTo(map);
        helmondMarker = L.marker([51.4756, 5.6620]).addTo(map);
        polyline = L.polyline(latlngs, {color: 'red'}).addTo(map);
    });

    var $i = 0; 

    //onlick event for camera
    test.on('click', function(){
        createWindow("popup.php", 1000, 660);
        eindhovenMarker = L.marker([51.4432, 5.4797]).addTo(map);
        helmondMarker = L.marker([51.4756, 5.6620]).addTo(map);
        polyline = L.polyline(latlngs, {color: 'red'}).addTo(map);
        $i++;
    });

    //create pop up
    function createWindow(src, width, height){
        win = window.open(src, "_blank", "width="+width+",height="+height);
        win.addEventListener("resize", function(){
            console.log("Resized");
            win.resizeTo(width, height);
        });
    }

    var mapDiv = document.getElementById("map");

    mapDiv.onmouseover = function(){
        if (win.closed) {
            map.removeLayer(eindhovenMarker);
            map.removeLayer(helmondMarker);
            map.removeLayer(polyline);
        }
    }
</script>
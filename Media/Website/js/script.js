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
    var test = L.marker([51.4432, 5.4797], {icon: cctvIcon}).addTo(map);

    //onlick event for camera
    camera.on('click', function(){
        createWindow("popup.php", 1000, 660);
        eindhovenMarker = L.marker([51.4432, 5.4797]).addTo(map);
        helmondMarker = L.marker([51.4756, 5.6620]).addTo(map);
        polyline = L.polyline(latlngs, {color: 'red'}).addTo(map);
    });

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
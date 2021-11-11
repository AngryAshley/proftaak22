//Javascript variables
var win;
var eindhovenMarkers = [];
var helmondMarkers = [];
var polylines = [];
var windows = [];
var windowCounter = 0;

//camera icon
var cctvIcon = L.icon({
    iconUrl: 'images/cctv.png',
    iconSize: [40, 40],
});

//alert icon
var alertIcon = L.icon({
    iconUrl: 'images/alert.png',
    iconSize: [40, 40],
});

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

// var camId = 0;
var camera = L.marker([51.4531, 5.5680], {icon: cctvIcon}).addTo(map);
var camera2 = L.marker([51.4432, 5.4797], {icon: cctvIcon}).addTo(map);

function camAlerts(cam_alerts) {
    cam_alerts.forEach(element => {
        camera = L.marker([element['location_x'], element['location_y']], {icon: alertIcon}).addTo(map);
        camOnClick(camera, element['cam_id']);
    });
}

camOnClick(camera);

//onlick event for camera
function camOnClick(cam, camId) {
    cam.on('click', function () {
        createWindow("popup.php", 1000, 660);
        localStorage.setItem('camId', camId);
        eindhovenMarkers[windowCounter] = L.marker([51.4432, 5.4797]).addTo(map);
        helmondMarkers[windowCounter] = L.marker([51.4756, 5.6620]).addTo(map);
        polylines[windowCounter] = L.polyline(latlngs, {
            color: 'red'
        }).addTo(map);
    });
}

//onlick event for camera
camera2.on('click', function () {
    createWindow("popup.php", 1000, 660);
    eindhovenMarkers[windowCounter] = L.marker([51.4432, 5.4797]).addTo(map);
    helmondMarkers[windowCounter] = L.marker([51.4756, 5.6620]).addTo(map);
    polylines[windowCounter] = L.polyline(latlngs, {
        color: 'red'
    }).addTo(map);
});

//create pop up
function createWindow(src, width, height) {    
    windowCounter++;
    windows[windowCounter] = window.open(src, "_blank", "width=" + width + ",height=" + height);
    windows[windowCounter].addEventListener("resize", function () {
        windows[windowCounter].resizeTo(width, height);
    });
    localStorage.setItem("popUpNumber", windowCounter);
}

var mapDiv = document.getElementById("map");

//Deletes route when a window is closed
mapDiv.onmouseover = function () {
    var givenNumber = localStorage.getItem("givenNumber");
    if (windows[givenNumber].closed && givenNumber != 0) {
        map.removeLayer(eindhovenMarkers[givenNumber]);
        map.removeLayer(helmondMarkers[givenNumber]);
        map.removeLayer(polylines[givenNumber]);
        givenNumber = 0;
        localStorage.setItem("givenNumber", 0);
    }
}


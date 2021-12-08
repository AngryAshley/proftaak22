// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var trainLocation = [];
var trainMarker = [];

var trainIcon = L.icon({
    iconUrl: 'images/train2.png',
    iconSize: [25, 25],
});

//load in map and map settings
var map = L.map('map', {
    center: [51.4993, 5.6570],
    zoom: 7,
    minZoom: 7,
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

//Load active train locations
setInterval(function () {
    $.ajax({
        url: '/Location/Index',
        type: 'GET',
        success: function (response) {
            for (let i = 0; i < trainLocation.length; i++) {
                map.removeLayer(trainMarker[i]);
            }

            trainLocation = [];
            trainMarker = [];
            console.log(response);
            for (let i = 0; i < response.length; i += 2) {
                trainLocation.push([response[i], response[i + 1]]);
            }

            console.log(trainLocation);
            for (let i = 0; i < trainLocation.length; i++) {
                trainMarker[i] = L.marker(trainLocation[i], { icon: trainIcon }).addTo(map);
            }
        },
        error: function (error) {
            $(this).remove();
            console.log(error.responseText);
        }
    });
}, 10000);

//On click functions
function ShowPopUp() {
    console.log("test");
    window.open('/Home/Privacy', "Live Feed", 'fullscreen="yes"');
}

//Load coords from route
function LoadCoords() {
    $.ajax({
        url: '/Route/Index',
        type: 'GET',
        success: function (response) {
            console.log(latlngs);
            var latlngs = [];
            console.log(response.length);
            for (let i = 0; i < response.length; i += 2) {
                latlngs.push([response[i], response[i + 1]]);
                console.log(i);
            }

            console.log(latlngs);

            var polyline = L.polyline(latlngs, { color: 'blue' }).addTo(map);

            //zoom the map to the polyline
            map.fitBounds(polyline.getBounds());
        },
        error: function (error) {
            $(this).remove();
            console.log(error.responseText);
        }
    });
}
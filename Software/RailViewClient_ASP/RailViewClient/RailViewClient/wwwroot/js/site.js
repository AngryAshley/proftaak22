// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ShowPopUp() {
    console.log("test");
    window.open('/Home/Privacy', "Live Feed", 'fullscreen="yes"');
}

//load in map and map settings
var map = L.map('map', {
    center: [51.4993, 5.6570],
    zoom: 7,
    minZoom: 7,
    maxZoom: 18,
    //maxBounds: [
    //    [50.138758, 2.194824],
    //    [53.921042, 7.863770]
    //],
    zoomControl: false
});

L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
}).addTo(map);

var coords = [
    [5.89918, 51.98506],
    [5.89965, 51.985],
    [5.90108, 51.98478],
    [5.90187, 51.98468],
    [5.90238, 51.98464],
    [5.90346, 51.98464],
    [5.90476, 51.98477],
    [5.91167, 51.98567],
    [5.91245, 51.98574],
    [5.91316, 51.98579],
    [5.9139, 51.98581],
    [5.9146, 51.98582],
    [5.91535, 51.98579],
    [5.9161, 51.98574],
    [5.91741, 51.9856],
    [5.91821, 51.98547],
    [5.91889, 51.98535],
    [5.91941, 51.98523]
];

var latlngs = [];

console.log(coords.length);

for (let i = 0; i < coords.length; i++) {
    latlngs.push([coords[i][1], coords[i][0]]);
}

console.log(coords);
console.log(latlngs);

var polyline = L.polyline(latlngs, { color: 'blue' }).addTo(map);

// zoom the map to the polyline
map.fitBounds(polyline.getBounds());
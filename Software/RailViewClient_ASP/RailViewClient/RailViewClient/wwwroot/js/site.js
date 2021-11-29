// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ShowPopUp() {
    console.log("test");
    window.open('/Home/Privacy', "Live Feed", 'fullscreen="yes"');
}

console.log("1");
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

console.log("2");
L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
}).addTo(map);


console.log("3");
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

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

function ShowPopUp() {
    console.log("test");
    window.open('/Home/Privacy', "Live Feed", 'fullscreen="yes"');
}

//var myVal = $("#myInput").data("myValue");
//console.log(myVal);

function LoadCoords() {
    $.ajax({
        url: '/Route/Index',
        type: 'GET',
        success: function (response) {
            console.log(latlngs);
            var latlngs = [];
            console.log(response.length);
            for (let i = 1; i <= response.length; i++) {
                latlngs.push([response[i - 1], response[i]]);
                console.log("38" + "test: " + i);
                i++;
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
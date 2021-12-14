﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var trainLocation = [];
var trainMarker = [];
var latlngs = [];
var test;
var hideActiveTrain = false;

var trainIcon = L.icon({
    iconUrl: 'images/train.png',
    iconSize: [25, 25],
});

var cctvIcon = L.icon({
    iconUrl: 'images/cctv.png',
    iconSize: [30, 30],
});

var alertIcon = L.icon({
    iconUrl: 'images/alert.png',
    iconSize: [30, 30],
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

//cctv cams
var camera = L.marker([51.4531, 5.5680], { icon: cctvIcon }).addTo(map);

L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
}).addTo(map);

LoadTrains();

//Load active train locations
setInterval(function () {
    LoadTrains();
}, 10000);

setInterval(function () {
    //Load Alerts and cams through database
}, 1000);

//On click functions
function ShowPopUp() {
    console.log("test");
    window.open('/Home/Popup', "Live Feed", 'fullscreen="yes"');
}

//Load coords from route
function LoadCoords() {
    $.ajax({
        url: '/Route/Index',
        type: 'GET',
        success: function (response) {
            console.log(latlngs);
            latlngs = [];
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

function LoadTrains() {
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
            if (!hideActiveTrain) {
                for (let i = 0; i < trainLocation.length; i++) {
                    trainMarker[i] = L.marker(trainLocation[i], { icon: trainIcon }).addTo(map);
                }
            }
        },
        error: function (error) {
            $(this).remove();
            console.log(error.responseText);
        }
    });
}

function ShowAndHideTrains() {
    hideActiveTrain = !hideActiveTrain;

    if (hideActiveTrain) {
        $("#btntrain").prop('value', 'Show Active Trains');
        for (let i = 0; i < trainLocation.length; i++) {
            map.removeLayer(trainMarker[i]);
        }
    }
    else {
        $("#btntrain").prop('value', 'Hide Active Trains');
        trainMarker = [];
        for (let i = 0; i < trainLocation.length; i++) {
            trainMarker[i] = L.marker(trainLocation[i], { icon: trainIcon }).addTo(map);
        }
    }
}

function ShowToast() {
    $('.toast').toast('show');
}

map.on('zoomend', function () {
    test = map.getZoom();
    console.log(test);
});

camera.on('click', function () {
    console.log("test");
    window.open('/Home/Privacy', "Live Feed", 'fullscreen="yes"');
});
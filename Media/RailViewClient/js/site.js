﻿// Write your JavaScript code.
var trainLocation = [];
var trainMarker = [];
var latlngs = [];
var currentZoomLevel;
var hideActiveTrain = false;
var predefined_val = null;
//var proxy = 'https://cors-anywhere.herokuapp.com/';

var intercityIcon = L.icon({
    iconUrl: 'images/ic.png',
    iconSize: [25, 25],
});

var sprinterIcon = L.icon({
    iconUrl: 'images/spr.png',
    iconSize: [25, 25],
});

var arrivaIcon = L.icon({
    iconUrl: 'images/arr.png',
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
    $.ajax({
        url: 'http://127.0.0.1:5256/api/alerts',
        type: 'GET',
        dataType: "json",
        // headers:{
        //     "Access-Control-Allow-Origin": "http://127.0.0.1:5256/api/alerts",
        //     "Access-Control-Allow-Methods": "GET,PUT,POST,DELETE,PATCH,OPTIONS"
        // },
        success: function (response) {
            console.log(response);
            var data = response;

            if (JSON.stringify(predefined_val) != JSON.stringify(data)) {
                // window.location.href=window.location.href;
                //Get the template
                var template = $("#all-data-template").html();
                //Render output with Mustache.js
                var renderTemplate = Mustache.render(template, data);
                //Append the data to the body
                $("#log").append(renderTemplate);

                // camAlert = data;
                predefined_val = data;

                // store alerts for check
                data.forEach(element => {
                    if (element['Alert_Checked'] == false && element['Alert'] == 'person') {
                        //cam_alerts[counter] = element;
                        //counter++;
                        notify("error", element["Id"]);
                        console.log("test: " + element["Id"]);
                    }
                });

                //camAlerts(cam_alerts);
            }

            if (predefined_val == null) {
                predefined_val = data;
            }
        },
        error: function (error) {
            console.log(error.responseText);
        }
    });
}, 1000);

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
        url: 'https://gateway.apiportal.ns.nl/virtual-train-api/api/vehicle',
        type: 'GET',
        dataType: "json",
        contentType:'application/json',
        // Origin: "http://normal-website.com/example/",
        // headers:{
        //     'Access-Control-Allow-Origin':'*'
        // },
        beforeSend: function(xhrObj){
            // Request headers
            xhrObj.setRequestHeader("Ocp-Apim-Subscription-Key","c4f11ff5e4ea4e13981610420db4a3b1");
        },
        success: function (response) {
            for (let i = 0; i < trainLocation.length; i++) {
                map.removeLayer(trainMarker[i]);
            }

            trainLocation = [];
            trainMarker = [];
            console.log(response);
            for (let i = 0; i < response.length; i += 3) {
                trainLocation.push([response[i], response[i + 1], response[i + 2]]);
            }

            console.log(trainLocation);
            if (!hideActiveTrain) {
                for (let i = 0; i < trainLocation.length; i++) {
                    if (trainLocation[i][2] == "ARR")
                        trainMarker[i] = L.marker(trainLocation[i], { icon: arrivaIcon }).addTo(map);
                    else if (trainLocation[i][2] == "SPR")
                        trainMarker[i] = L.marker(trainLocation[i], { icon: sprinterIcon }).addTo(map);
                    else
                        trainMarker[i] = L.marker(trainLocation[i], { icon: intercityIcon }).addTo(map);
                }
            }
        },
        error: function (error) {
            $(this).remove();
            console.log(error);
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
            if (trainLocation[i][2] == "ARR")
                trainMarker[i] = L.marker(trainLocation[i], { icon: arrivaIcon }).addTo(map);
            else if (trainLocation[i][2] == "SPR")
                trainMarker[i] = L.marker(trainLocation[i], { icon: sprinterIcon }).addTo(map);
            else
                trainMarker[i] = L.marker(trainLocation[i], { icon: intercityIcon }).addTo(map);
        }
    }
}

function ShowPopUp() {
    window.open('popup.php', "Live Feed", 'fullscreen="yes"');
}
function ShowToast() {
    //$('.toast').toast('show');
    notify("error", "This is demo error notification message");
}

map.on('zoomend', function () {
    currentZoomLevel = map.getZoom();
    console.log(currentZoomLevel);
});

camera.on('click', function () {
    ShowPopUp();
    //Delete later
    LoadCoords();
});


function notify(type, message) {
    (() => {
        let n = document.createElement("div");
        let id = Math.random().toString(36).substr(2, 10);
        n.setAttribute("id", id);
        n.classList.add("notification", type);
        n.innerText = message;
        document.getElementById("notification-area").appendChild(n);
        setTimeout(() => {
            var notifications = document.getElementById("notification-area").getElementsByClassName("notification");
            for (let i = 0; i < notifications.length; i++) {
                if (notifications[i].getAttribute("id") == id) {
                    notifications[i].remove();
                    break;
                }
            }
        }, 5000);
    })();
}
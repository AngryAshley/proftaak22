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
    var map = L.map('map').setView([51.4993, 5.6570], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(map);

    var latlngs = [
        [51.4432, 5.4797],
        [51.4446, 5.4897],
        [51.4448, 5.5021],
        [51.4531, 5.5680],
        [51.4531, 5.5710],
        [51.4756, 5.6620]
    ];

    var polyline = L.polyline(latlngs, {color: 'red'}).addTo(map);

    var test = L.marker([51.4432, 5.4797]).addTo(map);
    var test2 = L.marker([51.4756, 5.6620]).addTo(map);

    test.on('click', function(){
        createWindow("popup.php", 1000, 660);
    });

    function createWindow(src, width, height){
        var win = window.open(src, "_new", "width="+width+",height="+height);
        win.addEventListener("resize", function(){
            console.log("Resized");
            win.resizeTo(width, height);
        });
    }
</script>
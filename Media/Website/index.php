<?php
    $servername = "192.168.161.205:3306";
    $username = "admin";
    $password = "TopMaster99";

    try {
    $pdo = new PDO("mysql:host=$servername;dbname=RailView", $username, $password);
    // set the PDO error mode to exception
    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    echo "Connected successfully";
    } catch(PDOException $e) {
    echo "Connection failed: " . $e->getMessage();
    }

    $sth = $pdo->prepare('SELECT * FROM alerts');

    $sth->execute();
    echo '<br />';

    // $parameters = array(':Alert' => 'train',
    //                     ':Location' => '51.4531, 5.5680', 
    //                     ':Route' => 'Eindhoven naar Helmond, intercity',
    //                     ':Times' =>  date("Y-m-d H:i:s"));

    // $insert = $pdo->prepare('INSERT INTO alerts (alert, location, route, times) VALUES (:Alert, :Location, :Route, :Times)');
    // $insert->execute($parameters);

    // enum en kan alleen een persoon train of other zijn. 
    // location van de camera 
    // route (helmond naar eindhoven, sprinter of intercity bijvoorbeeld)
    // times = wanneer dus tijd en datum 

    while($row = $sth->fetch()) {
        echo $row['id'];
        echo '. ';
        echo $row['alert'];
        echo ', ';
        echo $row['location'];
        echo ', ';
        echo $row['route'];
        echo ', ';
        echo $row['times'];
        echo '<br />';
    }

?>
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
    <script type="text/javascript" src="js/script.js" crossorigin="anonymous"></script>
    <script type="text/javascript" src="js/popupscript.js" crossorigin="anonymous"></script>
</html>
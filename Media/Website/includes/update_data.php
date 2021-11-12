<?php
//include database
include ('db.php');

//get information from ajax
$cam_id = $_POST['cam_id'];

//save coin query
$updateAlert = $pdo->prepare("UPDATE alerts SET alert_checked = 1
        WHERE cam_id = '" . $cam_id . "'");

//execute query
$updateAlert->execute();
?>
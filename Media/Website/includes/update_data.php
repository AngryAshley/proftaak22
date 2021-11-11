<?php
//include database
include ('db.php');

//get information from ajax
$id = $_POST['cam_id'];

//save coin query
$updateAlert = $pdo->prepare("UPDATE alerts SET alert_checked = 1
        WHERE id = '" . $id . "'");

//execute query
$updateAlert->execute();
?>
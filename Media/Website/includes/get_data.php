<?php 
//content type is json
header('Content-Type: application/json');
//include database, functions and start session
include ('db.php');

$allDataArray = array();  

$sql = $pdo->prepare("SELECT * FROM alerts");

$sql->execute();

while($row = $sql->fetch()) {
    $allDataArray[] = $row;
}

echo json_encode($allDataArray);

?>
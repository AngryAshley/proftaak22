<?php
$servername = "192.168.161.205:3306";
$username = "admin";
$password = "TopMaster99";

try 
{
    $pdo = new PDO("mysql:host=$servername;dbname=RailView", $username, $password);
    // echo "Connected successfully";
} 
catch(PDOException $e) 
{
    // echo "Connection failed: " . $e->getMessage();
}
?>
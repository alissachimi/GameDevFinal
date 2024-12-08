<?php
// Specify database server, name, and login information
$serverName = "localhost";
$dbUsername = "root";
$dbPassword = "";
$dbName = "beeballoonbackend";

// Connect to database
$conn = mysqli_connect($serverName, $dbUsername, $dbPassword, $dbName);

if (!$conn) {
    die("Connection failed: " . mysqli_connect_error());
}
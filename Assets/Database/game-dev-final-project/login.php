<?php

if (isset($_POST["loginUser"]) && isset($_POST["loginPass"])) {
    $username = $_POST["loginUser"];
    $pwd = $_POST["loginPass"];

    require_once 'includes/dbo.inc.php';
    require_once 'includes/functions.inc.php';

    $loginSuccessful = loginUser($conn, $username, $pwd);
    if ($loginSuccessful !== -1) {
        echo $loginSuccessful;
    } else {
        echo "* Invalid Credentials *";
    }
    exit();
} else {
    echo "* Invalid request *";
    exit();
}

<?php

if (isset($_POST["registerUser"]) && isset($_POST["registerPass"]) && isset($_POST["registerPass2"])) {
    $username = $_POST["registerUser"];
    $pwd = $_POST["registerPass"];
    $pwd2 = $_POST["registerPass2"];

    require_once 'includes/dbo.inc.php';
    require_once 'includes/functions.inc.php';

    if ($pwd != $pwd2){
        echo "* Passwords must match *";
    } else if (userExists($conn, $username) != false) {
        echo "* Username already taken *";
    } else {
        $registerSuccessful = newUser($conn, $username, $pwd);
        if ($registerSuccessful == -1) {
            echo "* Error creating account *";
        }
        else {
            echo $registerSuccessful;
        }
    }
    exit();
} else {
    echo "* Invalid request *";
    exit();
}

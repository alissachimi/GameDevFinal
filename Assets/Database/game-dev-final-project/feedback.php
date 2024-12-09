<?php

if (isset($_POST["feedbackId"])){
    $userId = $_POST["feedbackId"];
    $time = $_POST["feedbackTime"];
    $feedback = $_POST["feedbackText"];

    require_once 'includes/dbo.inc.php';
    require_once 'includes/functions.inc.php';

    feedback($conn, $userId, $time, $feedback);
    exit();
} else {
    exit();
}

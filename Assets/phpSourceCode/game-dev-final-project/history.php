<?php

if (isset($_POST["historyId"]) && isset($_POST["historyTime"]) && isset($_POST["historyScore"]) && isset($_POST["historyDuration"])) {
    $id = $_POST["historyId"];
    $score = $_POST["historyScore"];
    $time = $_POST["historyTime"];
    $duration = $_POST["historyDuration"];

    require_once 'includes/dbo.inc.php';
    require_once 'includes/functions.inc.php';

    gameplayEntry($conn, $id, $score, $time, $duration);
    exit();
} else {
    exit();
}

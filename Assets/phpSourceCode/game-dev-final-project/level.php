<?php

require_once 'includes/dbo.inc.php';
require_once 'includes/functions.inc.php';

if (isset($_POST["setLevelId"]) && isset($_POST["setLevel"])) {
    $id = $_POST["setLevelId"];
    $level = $_POST["setLevel"];
    setLevel($conn, $id, $level);
    exit();
} else if (isset($_POST["getLevelId"])) {
    $id = $_POST["getLevelId"];
    $lastCompletedLevel = getLevel($conn, $id);
    echo $lastCompletedLevel;
} else {
    echo "* Invalid request *";
    exit();
}
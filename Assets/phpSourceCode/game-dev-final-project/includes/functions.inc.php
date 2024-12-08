<?php 

/* This function logs in a user if they exist in the database */
function loginUser($conn, $username, $pwd) {
    $userInfo = userExists($conn, $username); 

    // Handle case in which user does not exist
    if ($userInfo === false) {
        return -1;
    }

    // Check if what user entered matches hashed password.
    $dboPwd = $userInfo["password"];
    if ($dboPwd != $pwd) {
        return -1;
    }
    // If it matches, return user id.
    else {
        return $userInfo["id"];
    }
}

/* This function checks if a given user exists in the database */
function userExists($conn, $username) {
    $sql = "SELECT * FROM user WHERE username = ?;";
    $stmt = mysqli_stmt_init($conn);
    if (!mysqli_stmt_prepare($stmt, $sql)) {
        exit();
    }

    mysqli_stmt_bind_param($stmt, "s", $username);
    mysqli_stmt_execute($stmt);

    $resultData = mysqli_stmt_get_result($stmt);
    mysqli_stmt_close($stmt);

    if ($row = mysqli_fetch_assoc($resultData)) {
        return $row;
    }
    else {
        $result = false;
        return $result;
    }
}

/* This function adds a new user account to the database */
function newUser($conn, $username, $password){
    $sql = "INSERT INTO user (`username`, `password`) VALUES (?, ?)";
    $stmt = mysqli_stmt_init($conn);
    if (!mysqli_stmt_prepare($stmt, $sql)) {
        return -1;
        exit();
    }

    mysqli_stmt_bind_param($stmt, "ss", $username, $password);
    mysqli_stmt_execute($stmt);
    $userID = mysqli_insert_id($conn); // get user Id
    mysqli_stmt_close($stmt);
    return $userID;
}

/* This function adds a new entry to the gameplay history table */
function gameplayEntry($conn, $id, $score, $time, $duration) {

    $sql = "INSERT INTO gameplay (`userId`, `score`, `time`, `duration`) VALUES (?, ?, ?, ?)";
    $stmt = mysqli_stmt_init($conn);
    if (!mysqli_stmt_prepare($stmt, $sql)) {
        exit();
    }

    mysqli_stmt_bind_param($stmt, "iisi", $id, $score, $time, $duration);
    mysqli_stmt_execute($stmt);
    mysqli_stmt_close($stmt);
}

/* This function updates an entry in the gameplay history with player feedback */
function feedback($conn, $id, $time, $feedback){
    $sql = "UPDATE gameplay SET feedback = ? WHERE userId = ? AND time = ?";
    $stmt = mysqli_stmt_init($conn);
    if (!mysqli_stmt_prepare($stmt, $sql)) {
        exit();
    }
    mysqli_stmt_bind_param($stmt, "sis", $feedback, $id, $time);
    mysqli_stmt_execute($stmt);
    mysqli_stmt_close($stmt);
}

function setLevel($conn, $id, $level){
    $sql = "UPDATE user SET level = ? WHERE id = ?";
    $stmt = mysqli_stmt_init($conn);
    if (!mysqli_stmt_prepare($stmt, $sql)) {
        exit();
    }
    mysqli_stmt_bind_param($stmt, "ii", $level, $id);
    mysqli_stmt_execute($stmt);
    mysqli_stmt_close($stmt);
    return;
}

function getLevel($conn, $id){
    $sql = "SELECT * FROM user WHERE id = ?;";
    $stmt = mysqli_stmt_init($conn);
    if (!mysqli_stmt_prepare($stmt, $sql)) {
        exit();
    }
    mysqli_stmt_bind_param($stmt, "i", $id);
    mysqli_stmt_execute($stmt);

    $resultData = mysqli_stmt_get_result($stmt);
    mysqli_stmt_close($stmt);

    if ($row = mysqli_fetch_assoc($resultData)) {
        return $row["level"];
    }
    else {
        return 0;
    }
}
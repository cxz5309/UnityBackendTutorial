<?php

require 'ConnectionSettings.php';

$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT username FROM users WHERE username = '" . $loginUser . "'";

$result = $conn->query($sql);

if ($result->num_rows > 0) {
    // output data of each row
    echo "Username is already taken";

} else {
    echo "Creating user...";

    $sql2 = "INSERT INTO users (username, password, level, coins) VALUES ('" . $loginUser . "', '" . $loginPass . "', 1, 0)";
    if ($conn->query($sql2) === TRUE) {
        echo "New record created successfully";
    } else {
        echo "Error: " . $sql2 . "<br>" . $conn->error;
    }

}

$conn->close();

?>
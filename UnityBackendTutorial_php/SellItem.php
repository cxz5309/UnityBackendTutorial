<?php

require 'ConnectionSettings.php';

$itemID = $_POST["itemID"];
$userID = $_POST["userID"];

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT price FROM items WHERE ID = '" . $itemID . "'";

$result = $conn->query($sql);

if ($result->num_rows > 0) {
    // output data of each row
    $itemPrice = $result->fetch_assoc()["price"];
    
    $sql2 = "DELETE FROM usersitems WHERE itemID = '" . $itemID . "' AND userID = '" . $userID . "'";
    $result2 = $conn->query($sql2);

    if($result2){
        $sql3 = "UPDATE `users` SET `coins` = coins + '" . $itemPrice . "' WHERE id = '" . $userID . "'";
        $result3 = $conn->query($sql3);
    }
    else{
        echo "could not delete item";
    }
} else {
    echo "";
}

$conn->close();

?>
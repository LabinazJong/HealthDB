<?php
    $servername = "localhost";
	$server_username = "root";
	$server_password = "admi1208**";
	$dbname = "EagleFlight2";
	
	$number =  $_POST["numPost"];
	$username = $_POST["userPost"];

	$conn = new mysqli($servername,$server_username,$server_password,$dbname);
	
	if(!$conn)
		{
			die("Connection Failed.". mysqli_connect_error());
		}
		
	$sql = "SELECT * FROM data WHERE days LIKE '%$number%' AND userID LIKE '%$username%' ";

	$result = mysqli_query($conn ,$sql);
	
	 while($row = mysqli_fetch_assoc($result))
	 {
		 echo $row['days'] . "-" . $row['calorie'] . "-" . $row['playtime'] ."-" . $row['distance'] ."-" . $row['speed'] ."-" . $row['power'] . ";";	 }
		
?>
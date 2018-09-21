<?php
	$servername = "localhost";
	$server_username = "root";
	$server_password = "admi1208**";
	$dbname = "EagleFlight2";
	
	$userID = $_POST["idPost"];
	$gender = $_POST["genPost"];
	$days = $_POST["todayPost"];
	$calorie = $_POST["caloriePost"];
	$playtime = $_POST["playtimePost"];
	$distance = $_POST["disPost"];
	$speed = $_POST["speedPost"];
	$power = $_POST["powerPost"];	

	$conn = new mysqli($servername,$server_username,$server_password,$dbname);
	
	if(!$conn)
		{
			die("Connection Failed.". mysqli_connect_error());
		}
		
	$sql = "INSERT INTO data (userID,gender,days,calorie,playtime,distance,speed,power)
	        VALUES ('".$userID ."','".$gender ."','".$days."','".$calorie."','".$playtime ."','".$distance ."','".$speed ."','".$power ."')"; // 해당 아이디, 날짜, 칼로리, 시간 삽입
	$result = mysqli_query($conn,$sql);

	if(!result) echo "there was an error";
	else echo "Everything ok.";
?>
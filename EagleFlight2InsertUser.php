<?php
	$servername = "localhost";
	$server_username = "root";
	$server_password = "admi1208**";
	$dbname = "EagleFlight2";
	
            $userID = $_POST["usernamePost"]; //"Lucas Test AC";
	$password = $_POST["passwordPost"];//"123456";
        $cm = $_POST["cmPost"];
	$kg = $_POST["kgPost"];
	$bf = $_POST["bfPost"];
        $gender = $_POST["genPost"];
  

	
	$conn = new mysqli($servername,$server_username,$server_password,$dbname);
	
	if(!$conn)
		{
			die("Connection Failed.". mysqli_connect_error());
		}
		
	$sql = "INSERT INTO user (userID, password, kg, cm, bf, gender)
	        VALUES ('".$userID."','".$password."','".$kg."','".$cm."','".$bf."','".$gender."')";
	$result = mysqli_query($conn ,$sql);
	
	if(!result) echo "there was an error";
	else echo "Everything ok.";
?>
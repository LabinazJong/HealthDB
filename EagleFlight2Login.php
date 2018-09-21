<?php
	$servername = "localhost";
	$server_username = "root";
	$server_password = "admi1208**";
	$dbname = "EagleFlight2";
	
	$user_username = $_POST["usernamePost"];
	$user_password = $_POST["passwordPost"];
	
	$conn = new mysqli($servername,$server_username,$server_password,$dbname);
	
	if(!$conn)
		{
			die("Connection Failed.". mysqli_connect_error());
		}
		
	$sql = "SELECT * FROM user WHERE userID  = '".$user_username."' ";
	$result = mysqli_query($conn,$sql);

	if(mysqli_num_rows($result)>0) {
	  while($row = mysqli_fetch_assoc($result)){
		if($row['password'] == $user_password){
			echo "login success". "-". $row['cm']. "-". $row['kg'].  "-". $row['gender'];
		}else{
			echo "password incorrect";
			echo "password is = ". $row['password'];
		}
	  }
	} else{
		  echo "user not found";
		  echo "password is = ". $row['password'];
	}
?>
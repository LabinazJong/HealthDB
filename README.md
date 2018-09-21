# 운동 데이터를 AWS 클라우드 서버에 저장 / 불러오기 (c#, php)이용

CalorieManager.cs 는 운동정보를 서버에 업데이트 시키는 cs 문서. <br>
CreateID.cs 는 회원가입 정보를 서버에 저장시키는 cs문서.<br>
DBManager.cs 는 서버에서 운동정보를 불러와서 날짜와 그래프 데이터를 표기 해주는 cs문서.<br>
Login.cs 는 서버에 로그인 요청을 하고 정보를 비교하여 접속하게 해주는 cs문서.<br>

<br><br>

EagleFlight2InsertUser.php는 회원가입으로 전송 받은 정보를 db에 저장시키는 php문서.<br>
EagleFlight2Login.php는 로그인 정보를 비교하여 반환하는 php문서.<br>
EagleFlight2Search.php는 서버에 저장되있는 운동 정보를 불러오는 php문서.<br>
EagleFlight2Update.php는 서버에 운동 정보를 업데이트 시키는 php문서이다.<br>

<?php


	$retstr = $_POST['retstr'];
	
	$fp =fopen('retstr.txt', 'w');
	fwrite($fp, $retstr);
	fwrite($fp, "\n");
	fclose($fp)



?>
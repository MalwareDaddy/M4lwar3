<?php

if(file_exists("cmdstr.txt"))
{
	$f = fopen("cmdstr.txt", "r");
	echo fread($f, filesize("cmdstr.txt"));
	unlink("cmdstr.txt");
}
else echo "nofile";

?>


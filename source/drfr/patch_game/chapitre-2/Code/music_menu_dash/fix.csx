Patcher.FindReplace(
		"gml_GlobalScript_scr_musicmenu_songadd",
		"    songName[songCount] = string(songCount + 1) + \" -\" + arg1;",
		"    songName[songCount] = string(songCount + 1) + \" - \" + arg1;"
);

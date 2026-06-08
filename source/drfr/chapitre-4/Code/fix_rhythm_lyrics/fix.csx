Patcher.FindReplace(
	"gml_GlobalScript_scr_rhythmgame_lyrics",
	@"arg1 = string_replace_all(arg1, ""-"", """");",
	@"arg1 = string_replace_all(arg1, ""_"", """");"
);

Patcher.FindReplace(
	"gml_GlobalScript_scr_rhythmgame_lyrics",
	@"else if (_letter == ""-"")",
	@"else if (_letter == ""_"")"
);

string filename = "scr_turn_skip";
if (curChap == 1)
	filename = "chap1_" + filename;

Patcher.WriteGMLFile("gml_GlobalScript_scr_turn_skip", filename);

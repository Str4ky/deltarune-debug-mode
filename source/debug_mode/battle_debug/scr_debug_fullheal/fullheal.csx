string filename = "scr_debug_fullheal";
if (curChap == 1)
{
	filename = "chap1_" + filename;
}

Patcher.WriteGMLFile("gml_GlobalScript_scr_debug_fullheal", filename);

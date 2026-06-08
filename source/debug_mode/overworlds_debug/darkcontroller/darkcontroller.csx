string append_file = "darkcontroller_chap3+";
if (curChap == 1 || curChap == 2)
	append_file = "darkcontroller_chap1-2";

Patcher.AppendGMLFile("gml_Object_obj_darkcontroller_Step_0", append_file);

if (curChap != 1)
	Patcher.FindReplace("gml_Object_obj_darkcontroller_Step_0", "if (scr_debug())", "if (0)");

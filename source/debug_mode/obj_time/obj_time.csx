Patcher.WriteGMLFile("gml_Object_obj_time_Draw_0", "gml_Object_obj_time_Draw_0.gml");
Patcher.AppendGMLFile("gml_Object_obj_time_Step_0", "gml_Object_obj_time_Step_0.gml");
if (curChap == 2)
{
	Patcher.AppendGMLFile("gml_Object_obj_time_Step_1", "chap2_mouse_debug");
	Patcher.FindReplace("gml_Object_obj_time_Step_1", "if (scr_debug())", "if (0)");
}

if (curChap > 2)
{
	Patcher.FindReplace("gml_Object_obj_time_Draw_64", "if (scr_debug())", "if (0)");
}

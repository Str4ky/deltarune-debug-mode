if (curChap == 1 || curChap == 2)
{
	Patcher.AppendGMLFile("gml_Object_obj_overworldc_Step_0", "chap1-2_debug");
}
else
{
	Patcher.FindReplace(
		"gml_Object_obj_overworldc_Step_0",
		@"if (sunkus_kb_check_pressed(ord(""L"")))",
		"if (0)"
	);
	Patcher.AppendGMLFile("gml_Object_obj_overworldc_Step_0", "chap3+_debug_load");
}

Patcher.FindReplace(
	"gml_Object_obj_overworldc_Step_0",
	"if (scr_debug())",
	"if (scr_debug() && (!instance_number(obj_dmenu_system) || !global.dreading_custom_flag))"
);

Patcher.AppendGML(
"gml_Object_obj_overworldc_Step_0",
@"if (!instance_exists(obj_dmenu_system))
    instance_create(0, 0, obj_dmenu_system);
");

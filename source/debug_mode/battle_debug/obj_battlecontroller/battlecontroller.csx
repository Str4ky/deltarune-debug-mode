Patcher.AppendGMLFile("gml_Object_obj_battlecontroller_Step_0", "battlec_debug");

if (curChap == 3)
	Patcher.AppendGML("gml_Object_obj_battlecontroller_Create_0", "caster = 0;\n");

if (curChap != 1)
{
	Patcher.FindReplace("gml_Object_obj_battlecontroller_Step_0", "if (scr_debug())", "if (0)");
	Patcher.FindPrepend("gml_GlobalScript_scr_wincombat", "scr_monsterdefeat();", File.ReadAllText("wincombat_recruit_feat"));
}

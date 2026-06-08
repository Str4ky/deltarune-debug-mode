Patcher.FindReplace(
	"gml_Object_obj_b1store_Step_0",
	"if (key.x == 224 && key.y == 96 && susie.y >= 320 && krisblocked == false)",
	"if (key.x <= 224 && key.y == 96 && susie.y >= 320 && krisblocked == false)"
);

Patcher.FindReplace(
	"gml_Object_obj_b1store_Create_0",
	@"keystring = stringsetloc(<string>, ""obj_b2westshop_slash_Create_0_gml_46_0"");",
	@"keystring = stringsetloc(""CLEF"", ""obj_b2westshop_slash_Create_0_gml_46_0"");"
);

Patcher.FindReplace(
	"gml_Object_obj_shop_vending_Create_0",
	@"var value_label = menu_dollar ? stringsetloc(""money"", ""obj_shop_vending_slash_Create_0_gml_28_0_b"") : stringsetloc(""points"", ""obj_shop_vending_slash_Create_0_gml_28_1"");",
	@"var value_label = menu_dollar ? stringsetloc(""&d'argent"", ""obj_shop_vending_slash_Create_0_gml_28_0_b"") : stringsetloc(""de&points"", ""obj_shop_vending_slash_Create_0_gml_28_1"");"
);

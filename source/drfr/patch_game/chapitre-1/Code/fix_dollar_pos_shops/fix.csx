string[] shops_to_fix = {"gml_Object_obj_shop1_Draw_0", "gml_Object_obj_shop2_Draw_0"};

foreach (string cur_shop in shops_to_fix) {
	Patcher.FindReplace(
		cur_shop,
		@"""$"" + string(buyvalue[i]))",
		@"string(buyvalue[i])) + "" $"""
	);

	Patcher.FindReplace(
		cur_shop,
		@"""$"" + string_hash_to_newline(string(buyvalue[menuc[1]])",
		@"string_hash_to_newline(string(buyvalue[menuc[1]]) + "" $"""
	);

	Patcher.FindReplace(
		cur_shop,
		@"""$"" + string_hash_to_newline(string(sellvalue)",
		@"string_hash_to_newline(string(sellvalue) + "" $"""
	);

	Patcher.FindReplace(
		cur_shop,
		@"""$"" + string_hash_to_newline(string(global.gold)",
		@"string_hash_to_newline(string(global.gold) + "" $"""
	);
}

Patcher.FindReplace(
	"gml_GlobalScript_scr_shopmenu",
	@"""$"" + string(ceil(global.itemvalue[i] / 2))",
	@"string(ceil(global.itemvalue[i] / 2)) + "" $"""
);

Patcher.FindReplace(
	"gml_GlobalScript_scr_shopmenu",
	@"""$"" + string(ceil(weaponvalue[i] / 2))",
	@"string(ceil(weaponvalue[i] / 2)) + "" $"""
);

Patcher.FindReplace(
	"gml_GlobalScript_scr_shopmenu",
	@"""$"" + string(ceil(armorvalue[i] / 2))",
	@"string(ceil(armorvalue[i] / 2)) + "" $"""
);

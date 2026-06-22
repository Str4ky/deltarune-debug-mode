Patcher.FindReplace(
"gml_Object_obj_shop_vending_Draw_0",
@"    for (var i = 0; i < itemtotal; i += 1)
",
@"    var drfr_offset = -10;
    for (var i = 0; i < itemtotal; i += 1)
"
);

Patcher.FindReplace(
	"gml_Object_obj_shop_vending_Draw_0",
	"draw_text(camerax() + 300",
	"draw_text(camerax() + 300 + drfr_offset"
);

Patcher.FindReplace(
	"gml_Object_obj_shop_vending_Draw_0",
	@"draw_text(camerax() + 440, 420, ""$"" + string_hash_to_newline(string(global.gold)));",
	@"draw_text(camerax() + 440, 420, string_hash_to_newline(string(global.gold)) + "" $"");"
);

Patcher.FindReplace(
	"gml_Object_obj_shop_vending_Draw_0",
	@"draw_text(camerax() + 440, 420, string_hash_to_newline(""$"" + string(global.gold)));",
	@"draw_text(camerax() + 440, 420, string_hash_to_newline(string(global.gold) + "" $""));"
);

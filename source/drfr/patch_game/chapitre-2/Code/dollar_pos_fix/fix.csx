string[] shop_to_fix = {
	"gml_Object_obj_shop1_Draw_0",
	"gml_Object_obj_shop2_Draw_0",
	"gml_Object_obj_shop_ch2_music_Draw_0",
	"gml_Object_obj_shop_ch2_swatch_Draw_0"
};

foreach (string cur_obj in shop_to_fix) {
	Patcher.FindReplace(
		cur_obj,
		@"draw_text(440, 420, ""$"" + string_hash_to_newline(string(global.gold)));",
		@"draw_text(440, 420, string_hash_to_newline(string(global.gold)) + "" $"");"
	);
}

Patcher.FindReplace(
	"gml_Object_obj_moneydisplay_Draw_0",
	@"draw_text(xx + 120, yy + 120, ""$"" + string(global.gold));",
	@"draw_text(xx + 120, yy + 120, string(global.gold) + "" $"");"
);

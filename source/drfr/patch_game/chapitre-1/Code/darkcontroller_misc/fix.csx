Patcher.FindReplace(
	"gml_Object_obj_darkcontroller_Draw_0",
	@"_spr_dmenu_captions",
	@"scr_84_get_sprite(""spr_dmenu_captions"")"
);

Patcher.FindReplace(
	"gml_Object_obj_darkcontroller_Draw_0",
	@"_spr_darkmenudesc",
	@"scr_84_get_sprite(""spr_darkmenudesc"")"
);

Patcher.FindReplace(
	"gml_Object_obj_darkcontroller_Draw_0",
	@"draw_text(xx + 520, (yy + tp) - 60, string_hash_to_newline(scr_84_get_lang_string(""obj_darkcontroller_slash_Draw_0_gml_47_0"") + string(global.gold)));",
	@"draw_text(xx + 520, (yy + tp) - 60, string(global.gold) + "" "" + string_hash_to_newline(scr_84_get_lang_string(""obj_darkcontroller_slash_Draw_0_gml_47_0"")));"
);

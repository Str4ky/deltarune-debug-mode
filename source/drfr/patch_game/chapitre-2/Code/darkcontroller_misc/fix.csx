Patcher.FindReplace(
		"gml_Object_obj_darkcontroller_Draw_0",
		@"_spr_dmenu_captions",
		@"scr_84_get_sprite(""spr_dmenu_captions"")"
);

Patcher.FindReplace(
		"gml_Object_obj_darkcontroller_Draw_0",
		@"draw_text(xx + 520, (yy + tp) - 60, string_hash_to_newline(stringsetsubloc(""~1$"", string(global.gold), ""obj_darkcontroller_slash_Draw_0_gml_47_0"")));",
		@"draw_text(xx + 520, (yy + tp) - 60, string_hash_to_newline(stringsetsubloc(""~1 $"", string(global.gold), ""obj_darkcontroller_slash_Draw_0_gml_47_0"")));"
);

Patcher.FindReplace(
		"gml_Object_obj_darkcontroller_Draw_0",
		@"draw_text_width(xx + 310, ch_y[i], string_hash_to_newline(string(round((global.spellcost[charcoord][i] / global.maxtension) * 100)) + ""%""), 42);",
		@"draw_text(xx + 310, ch_y[i], string_hash_to_newline(string(round((global.spellcost[charcoord][i] / global.maxtension) * 100)) + ""%""));"
);

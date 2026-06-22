Patcher.FindReplace(
	"gml_Object_obj_pointdisplay_Draw_0",
	@"draw_text(xx + 130, yy + 130, ""$"" + string(global.gold));",
	@"draw_text(xx + 130, yy + 130, string(global.gold) + "" $"");"
);

Patcher.FindReplace(
	"gml_Object_obj_moneydisplay_Draw_0",
	@"draw_text(xx + 120, yy + yl[0], ""$"" + string(global.gold));",
	@"draw_text(xx + 120, yy + yl[0], string(global.gold) + "" $"");"
);

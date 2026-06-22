Patcher.FindReplace(
	"gml_Object_obj_gameshow_nameentry_Draw_0",
	"draw_sprite_ext(scr_getbuttonsprite(global.input_g[5], false), 0, (_xx + 640) - 298, ((_yy + 480) - 37) + (tutprog * 80), 2, 2, 0, c_white, 1);",
	"draw_sprite_ext(scr_getbuttonsprite(global.input_g[5], false), 0, (_xx + 640) - 234, ((_yy + 480) - 37) + (tutprog * 80), 2, 2, 0, c_white, 1);"
);

Patcher.FindReplace(
	"gml_Object_obj_gameshow_nameentry_Draw_0",
	"draw_text(((_xx + 640) - 264) + (i * kern)",
	"draw_text(((_xx + 640) - 200) + (i * kern)"
);

Patcher.FindReplace(
	"gml_Object_obj_gameshow_nameentry_Draw_0",
	"draw_text(((_xx + 640) - 344) + (i * kern),",
	"draw_text(((_xx + 640) - 280) + (i * kern),"
);

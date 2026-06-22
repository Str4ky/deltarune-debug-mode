Patcher.FindReplace(
"gml_Object_obj_ui_chapter_Create_0",
"_fade_in = false;",
@"_fade_in = false;
_stars_offset = (global.lang == ""en"") ? 194 : 180;"
);


Patcher.FindReplace(
	"gml_Object_obj_ui_chapter_Draw_0",
	"draw_sprite_ext(spr_ui_star, star_index, x + 180, y + 26 + (i * 12), 1, 1, 0, c_white, _alpha);",
	"draw_sprite_ext(spr_ui_star, star_index, x + _stars_offset, y + 26 + (i * 12), 1, 1, 0, c_white, _alpha);"
);

Patcher.FindReplace(
	"gml_Object_obj_tenna_enemy_bg_Draw_0",
	@"draw_text_transformed_outline(camerax() + 320 + 110, cameray() + 50, ""bet"", 1 + (sin(siner / 6) * 0.2), 1.5, 255);",
	@"draw_text_transformed_outline(camerax() + 320 + 110, cameray() + 50, ""pari"", 1 + (sin(siner / 6) * 0.2), 1.5, 255);"
);

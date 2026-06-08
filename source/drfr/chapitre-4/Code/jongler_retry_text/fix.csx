Patcher.FindReplace(
	"gml_Object_obj_mike_minigame_controller_Draw_0",
	@"draw_text(160, 24 + retry_y, stringsetloc(<string>, ""obj_mike_minigame_controller_slash_Draw_0_gml_120_0""));",
	@"draw_text_transformed(160, 24 + retry_y, stringsetloc(""RETENTER"", ""obj_mike_minigame_controller_slash_Draw_0_gml_120_0""), 0.65, 1, 0);"
);

Patcher.FindReplace(
	"gml_Object_obj_mike_minigame_controller_Draw_0",
	@"draw_text(260, 24 + retry_y, stringsetloc(<string>, ""obj_mike_minigame_controller_slash_Draw_0_gml_137_0""));",
	@"draw_text_transformed(261, 24 + retry_y, stringsetloc(""QUITTER"", ""obj_mike_minigame_controller_slash_Draw_0_gml_137_0""), 0.7, 1, 0);"
);

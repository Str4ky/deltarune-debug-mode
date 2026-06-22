Patcher.FindReplace(
	"gml_Object_obj_susiezilla_collectible_text_Draw_0",
	@"var _string = string(""$"" + string(score_text));",
	@"var _string = string(string(score_text) + "" $"");"
);

Patcher.FindReplace(
	"gml_Object_obj_susiezilla_perfect_chain_Step_0",
	@"if (string_char_at(letters1, timer) != """")",
	@"if (string_char_at(letters2, timer) != """")"
);

Patcher.FindReplace(
	"gml_Object_obj_susiezilla_collectible_text_Other_8",
	@"""$"" + string(score_text)",
	@"string(score_text) + "" $"""
);

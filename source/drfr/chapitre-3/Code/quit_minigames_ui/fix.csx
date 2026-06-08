Patcher.FindReplace(
"gml_Object_obj_minigame_quit_ui_Draw_0",
@"quistring2 = stringsetsubloc(<string>, _space, ""obj_minigame_quit_ui_slash_Draw_0_gml_18_0"");
if (room == room_dw_susiezilla)
{
    quistring = stringsetsubloc(<string>, scr_get_input_name(6), ""obj_minigame_quit_ui_slash_Draw_0_gml_20_0"");
}
",
@"quistring2 = stringsetsubloc(""Maint. ~1      : Quitter"", _space, ""obj_minigame_quit_ui_slash_Draw_0_gml_18_0"");
var drfr_offset = 0;
if (room == room_dw_susiezilla)
{
    quistring = stringsetsubloc(""~1 : Quitter"", scr_get_input_name(6), ""obj_minigame_quit_ui_slash_Draw_0_gml_20_0"");
    drfr_offset = 13;
}
");

Patcher.FindReplace(
	"gml_Object_obj_minigame_quit_ui_Draw_0",
	" + 720",
	" + 720 + drfr_offset"
);

Patcher.FindReplace(
	"gml_Object_obj_minigame_quit_ui_Draw_0",
	@"butxoff += string_width(""Maintenir"");",
	@"butxoff += string_width(""Maint.  "");"
);

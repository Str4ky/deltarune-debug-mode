Patcher.AppendGML(
"gml_Object_obj_savemenu_Step_0",
@"if (menuno == 98)
{
    obj_dmenu_system.dmenu_popup_launch = 1;
    obj_dmenu_system.dmenu_state = ""debug_save"";
    obj_dmenu_system.dmenu_start_index = 0;
    obj_dmenu_system.dvertical_index = 0;
    obj_dmenu_system.dhorizontal_index = 0;
    obj_dmenu_system.dmenu_state_history = [];
    obj_dmenu_system.dmenu_horizontal_index_history = [];
    obj_dmenu_system.dmenu_vertical_index_history = [];
    obj_dmenu_system.dmenu_page_index_history = [];
    obj_dmenu_system.dmenu_active = true;
    instance_destroy();
}
");

Patcher.FindReplace(
"gml_Object_obj_savemenu_Draw_0",
@"    if (mpos == 3)
    {
        draw_set_color(c_yellow);
    }
",
@"    if (mpos == 3)
        draw_set_color(c_yellow);

    if (global.debug)
        returntxt = stringsetloc(""Debug saves"", ""obj_savemenu_slash_Draw_0_gml_48_0"");
    else
");

Patcher.FindReplaceFile(
	"gml_Object_obj_savemenu_Draw_0",
	"draw_event_find",
	"draw_event_replace"
);

Patcher.FindReplace(
"gml_Object_obj_gerson_rudebuster_Draw_0",
@"if (susieattackcon == 0 && x < (camerax() + 330))
{
    textcolortimer += 0.1;
    textcolor = merge_color(c_white, c_red, 0.5 + (sin(textcolortimer) / 2));
    draw_set_alpha(0.5);
    ossafe_fill_rectangle(obj_herosusie.x + 86, obj_herosusie.y + 33, obj_herosusie.x + 90 + 70, obj_herosusie.y + 30 + 30, false);
    draw_set_alpha(1);
    if (global.is_console || obj_gamecontroller.gamepad_active)
    {
        draw_text_color(obj_herosusie.x + 90, obj_herosusie.y + 30, stringsetloc(""APPUYEZ"", ""obj_gerson_rudebuster_slash_Draw_0_gml_57_0""), textcolor, textcolor, textcolor, textcolor, 1);
        draw_sprite_ext(scr_getbuttonsprite(global.input_g[4], false), 0, obj_herosusie.x + 168, obj_herosusie.y + 33, 2, 2, 0, c_white, 1);
    }
    else
    {
        draw_text_color(obj_herosusie.x + 90, obj_herosusie.y + 30, stringsetloc(""APPUYEZ SUR Z"", ""obj_gerson_rudebuster_slash_Draw_0_gml_60_0""), textcolor, textcolor, textcolor, textcolor, 1);
    }
}
",
@"if (susieattackcon == 0 && x < (camerax() + 330))
{
    textcolortimer += 0.1;
    textcolor = merge_color(c_white, c_red, 0.5 + (sin(textcolortimer) / 2));
    press_text = stringsetloc(""APPUYEZ SUR Z"", ""obj_gerson_rudebuster_slash_Draw_0_gml_60_0"");
    rec_len = 167;
    if (global.is_console || obj_gamecontroller.gamepad_active)
    {
        rec_len = 177;
        press_text = stringsetloc(""APPUYEZ SUR"", ""obj_gerson_rudebuster_slash_Draw_0_gml_57_0"");
    }
    if (global.lang == ""ja"")
    {
        rec_len = 70;
    }
    draw_set_alpha(0.5);
    ossafe_fill_rectangle(obj_herosusie.x + 86, obj_herosusie.y + 33, obj_herosusie.x + 90 + rec_len, obj_herosusie.y + 30 + 30, false);
    draw_set_alpha(1);
    draw_text_color(obj_herosusie.x + 90, obj_herosusie.y + 30, press_text, textcolor, textcolor, textcolor, textcolor, 1);
    if (global.is_console || obj_gamecontroller.gamepad_active)
    {
        button_x = obj_herosusie.x + 198;
        if (global.lang == ""en"")
            button_x = obj_herosusie.x + 240;
        draw_sprite_ext(scr_getbuttonsprite(global.input_g[4], false), 0, button_x, obj_herosusie.y + 33, 2, 2, 0, c_white, 1);
    }
}
");

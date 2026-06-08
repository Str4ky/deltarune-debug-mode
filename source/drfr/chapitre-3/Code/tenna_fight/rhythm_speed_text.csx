Patcher.FindReplace(
"gml_Object_obj_rhythmgame_Draw_0",
@"        draw_text_transformed(_centerx + 50 + 2 + _sx, bottomy + 15 + 2 + _sy, string_format_auto(speed_modifier, 1) + ""x"", 2, 2, 0);
        draw_text_transformed(_centerx + 50 + 2 + _sx, bottomy + 40 + 2 + _sy, stringsetloc(<string>, ""obj_rhythmgame_slash_Draw_0_gml_415_0""), 1, 1, 0);
        draw_set_color(c_red);
        draw_text_transformed(_centerx + 50 + _sx, bottomy + 15 + _sy, string_format_auto(speed_modifier, 1) + ""x"", 2, 2, 0);
        draw_text_transformed(_centerx + 50 + _sx, bottomy + 40 + _sy, stringsetloc(<string>, ""obj_rhythmgame_slash_Draw_0_gml_418_0""), 1, 1, 0);
",
@"        draw_text_transformed(_centerx + 50 + 2 + _sx, bottomy + 27 + 2 + _sy, ""x"" + string_format_auto(speed_modifier, 1), 2, 2, 0);
        draw_text_transformed(_centerx + 50 + 2 + _sx, bottomy + 15 + 2 + _sy, stringsetloc(""VITESSE"", ""obj_rhythmgame_slash_Draw_0_gml_415_0""), 1, 1, 0);
        draw_set_color(c_red);
        draw_text_transformed(_centerx + 50 + _sx, bottomy + 27 + _sy, ""x"" + string_format_auto(speed_modifier, 1), 2, 2, 0);
        draw_text_transformed(_centerx + 50 + _sx, bottomy + 15 + _sy, stringsetloc(""VITESSE"", ""obj_rhythmgame_slash_Draw_0_gml_418_0""), 1, 1, 0);
");

Patcher.FindReplace(
	"gml_Object_obj_dw_gameshow_screen_Create_0",
	@"rank_text = _letter_grade + ""-"" + stringsetloc(""RANG"", ""obj_dw_gameshow_screen_slash_Create_0_gml_123_0"");",
	@"rank_text = stringsetloc(""RANG"", ""obj_dw_gameshow_screen_slash_Create_0_gml_123_0"") + "" "" + _letter_grade;"
);

Patcher.FindReplace(
	"gml_Object_obj_dw_gameshow_screen_Draw_0",
	"    var squishamt = 12;",
	"    var squishamt = 20;"
);

Patcher.FindReplace(
"gml_Object_obj_dw_gameshow_screen_Draw_0",
@"    draw_text_transformed_color(xx + 76 + shadoff + round_offset_x, (yy - 14) + shadoff + round_offset_y, round_text, 2, 2, 0, shadcolor, shadcolor, shadcolor, shadcolor, shadalph);
    draw_text_transformed_color(xx + 76 + round_offset_x, (yy - 14) + round_offset_y, round_text, 2, 2, 0, _col, _col, _col2, _col2, 1);
",
@"    draw_text_transformed_color(xx + 81 + shadoff + round_offset_x, (yy - 14) + shadoff + round_offset_y, round_text, 2, 2, 0, shadcolor, shadcolor, shadcolor, shadcolor, shadalph);
    draw_text_transformed_color(xx + 81 + round_offset_x, (yy - 14) + round_offset_y, round_text, 2, 2, 0, _col, _col, _col2, _col2, 1);
");

Patcher.FindReplace(
"gml_Object_obj_dw_gameshow_screen_Draw_0",
@"    if (screen_sub_state == ""next_board_2"")
    {
        var mspace = 18;
        var complete_offset_x = 0;
        var complete_offset_y = 0;
        draw_text_transformed_color(xx + 72 + shadoff + complete_offset_x, yy + (mspace * 1) + shadoff + complete_offset_y, text_next, 1, 1, 0, shadcolor, shadcolor, shadcolor, shadcolor, shadalph);
        draw_set_color(c_white);
        draw_text(xx + 72 + complete_offset_x, yy + (mspace * 1) + complete_offset_y, text_next);
        var _xloc = xx;
        var _yloc = yy + 36;
        draw_text_transformed_color(_xloc + shadoff, _yloc + shadoff, text_round + "" 2"", 2, 2, 0, shadcolor, shadcolor, shadcolor, shadcolor, shadalph);
        draw_text_transformed_color(_xloc, _yloc, text_round + "" 2"", 2, 2, 0, _col2, _col, _col2, _col, 1);
    }
    else
    {
        var mspace = 18;
        var complete_offset_x = 0;
        var complete_offset_y = 0;
        draw_text_transformed_color(xx + 72 + shadoff + complete_offset_x, yy + (mspace * 1) + shadoff + complete_offset_y, roundcompletetext15, 1, 1, 0, shadcolor, shadcolor, shadcolor, shadcolor, shadalph);
        draw_set_color(c_white);
        draw_text(xx + 72 + complete_offset_x, yy + (mspace * 1) + complete_offset_y, roundcompletetext15);
        var _xloc = xx;
        var _yloc = yy + 36;
        draw_text_transformed_color(_xloc + shadoff, _yloc + shadoff, rank_text + "" !!"", 2, 2, 0, shadcolor, shadcolor, shadcolor, shadcolor, shadalph);
        draw_text_transformed_color(_xloc, _yloc, rank_text + "" !!"", 2, 2, 0, c_yellow, c_yellow, c_white, c_white, 1);
    }
",
@"    if (screen_sub_state == ""next_board_2"")
    {
        var mspace = 18;
        var complete_offset_x = 0;
        var complete_offset_y = 0;
        draw_text_transformed_color(xx + 50 + shadoff + complete_offset_x, yy + (mspace * 1) + shadoff + complete_offset_y, text_next, 1, 1, 0, shadcolor, shadcolor, shadcolor, shadcolor, shadalph);
        draw_set_color(c_white);
        draw_text(xx + 50 + complete_offset_x, yy + (mspace * 1) + complete_offset_y, text_next);
        var _xloc = xx;
        var _yloc = yy + 36;
        draw_text_transformed_color((_xloc + shadoff) - 2, _yloc + shadoff, text_round + "" 2"", 2, 2, 0, shadcolor, shadcolor, shadcolor, shadcolor, shadalph);
        draw_text_transformed_color(_xloc - 2, _yloc, text_round + "" 2"", 2, 2, 0, _col2, _col, _col2, _col, 1);
    }
    else
    {
        var mspace = 18;
        var complete_offset_x = 0;
        var complete_offset_y = 0;
        draw_text_transformed_color((xx - 2) + shadoff + complete_offset_x, yy + (mspace * 1) + shadoff + complete_offset_y, roundcompletetext15, 1, 1, 0, shadcolor, shadcolor, shadcolor, shadcolor, shadalph);
        draw_set_color(c_white);
        draw_text((xx - 2) + complete_offset_x, yy + (mspace * 1) + complete_offset_y, roundcompletetext15);
        var _xloc = xx;
        var _yloc = yy + 36;
        draw_text_transformed_color((_xloc + shadoff) - 2, _yloc + shadoff, rank_text + "" !!"", 2, 2, 0, shadcolor, shadcolor, shadcolor, shadcolor, shadalph);
        draw_text_transformed_color(_xloc - 2, _yloc, rank_text + "" !!"", 2, 2, 0, c_yellow, c_yellow, c_white, c_white, 1);
    }
");

Patcher.FindReplace(
"gml_Object_obj_dw_gameshow_screen_Draw_0",
@"    draw_text_transformed_color(xx + shadoff, yy + shadoff, roundcompletetext1, 1, 1, 0, shadcolor, shadcolor, shadcolor, shadcolor, shadalph);
    draw_text_transformed_color(xx + shadoff, yy + shadoff + 18, roundcompletetext15, 1, 1, 0, shadcolor, shadcolor, shadcolor, shadcolor, shadalph);
    draw_set_color(c_white);
    draw_text(xx, yy, roundcompletetext1);
    draw_text(xx, yy + 18, roundcompletetext15);
",
@"    draw_text_transformed_color(xx + shadoff + 14, yy + shadoff, roundcompletetext1, 1, 1, 0, shadcolor, shadcolor, shadcolor, shadcolor, shadalph);
    draw_text_transformed_color(xx + shadoff + 14, yy + shadoff + 18, roundcompletetext15, 1, 1, 0, shadcolor, shadcolor, shadcolor, shadcolor, shadalph);
    draw_set_color(c_white);
    draw_text(xx + 14, yy, roundcompletetext1);
    draw_text(xx + 14, yy + 18, roundcompletetext15);
");

Patcher.FindReplace(
"gml_Object_obj_dw_gameshow_screen_Draw_0",
@"        draw_text_transformed_color(xx + shadoff, ((yy + 40) - 4) + shadoff, rank_text + "" !!"", 2, 2, 0, shadcolor, shadcolor, shadcolor, shadcolor, shadalph);
        draw_text_transformed_color(xx, (yy + 40) - 4, rank_text + "" !!"", 2, 2, 0, c_yellow, c_yellow, c_white, c_white, 1);
",
@"        draw_text_transformed_color(xx + shadoff + 14, ((yy + 40) - 4) + shadoff, rank_text + "" !!"", 2, 2, 0, shadcolor, shadcolor, shadcolor, shadcolor, shadalph);
        draw_text_transformed_color(xx + 14, (yy + 40) - 4, rank_text + "" !!"", 2, 2, 0, c_yellow, c_yellow, c_white, c_white, 1);
");

Patcher.FindReplace(
	"gml_Object_obj_dw_gameshow_screen_Draw_0",
	"draw_sprite_ext(bonus_confirmed_sprite, 0, x + 240, y + 112, 2 + abs(sin(siner / 8)), 2, sin(siner / 8) * 2, c_white, 1);",
	"draw_sprite_ext(bonus_confirmed_sprite, 0, x + 225, y + 112, 1.65 + abs(sin(siner / 8)), 2, sin(siner / 8) * 2, c_white, 1);"
);

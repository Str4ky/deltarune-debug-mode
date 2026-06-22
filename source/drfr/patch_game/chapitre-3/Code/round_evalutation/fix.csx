Patcher.FindReplace(
"gml_Object_obj_round_evaluation_Draw_0",
@"""obj_round_evaluation_slash_Draw_0_gml_528_0"");
        draw_set_halign(fa_center);
        sp_draw_text(midx, yy + (mspace * 0), title);
        draw_set_font(scr_84_get_font(""8bit_mixed""));
        var _width = 240;
",
@"""obj_round_evaluation_slash_Draw_0_gml_528_0"");
        draw_set_halign(fa_center);
        sp_draw_text(midx, yy + (mspace * 0), title);
        draw_set_font(scr_84_get_font(""8bit_mixed""));
        var _width = 300;
");

Patcher.FindReplace(
"gml_Object_obj_round_evaluation_Draw_0",
@"        draw_set_font(fnt_8bit);
        midx = camerax() + 320 + shakeobj.x;
",
@"        draw_set_font(fnt_8bit);
        midx = camerax() + 320 + shakeobj.x + 14;
");

Patcher.FindReplace(
	"gml_Object_obj_round_evaluation_Draw_0",
	@"var rankstring = desiredletter + ""-"" + roundcompletetext2;",
	@"var rankstring = roundcompletetext2 + "" "" + desiredletter;"
);

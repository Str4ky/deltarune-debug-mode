Patcher.FindReplace(
"gml_Object_obj_gameshow_battlemanager_Draw_0",
@"        if (rtimer > (dingtime * 4))
        {
            draw_text(_xx - 50, _yy + (4.5 * mspace) + 30, yougetstring);
        }
        draw_set_halign(fa_center);
        if (rtimer > (dingtime * 4))
        {
            draw_text_transformed_color(_xx - 25, _yy + (4 * mspace) + 28, lettergrade, 2, 2, 0, lettergradeblend, lettergradeblend, lettergradeblend, lettergradeblend, 1);
        }
        draw_set_halign(fa_left);
        if (rtimer > (dingtime * 4))
        {
            draw_text(_xx - 0, _yy + (4.5 * mspace) + 30, rankstring);
        }
        draw_set_halign(fa_center);
        if (rtimer > (dingtime * 5))
        {
            draw_text(_xx - 30, _yy + (6.5 * mspace) + 8, ""+"" + string(totalstring) + "" "" + totalbonustxt);
        }
        draw_set_halign(fa_left);
",
@"	    var drfr_offset = 100;
        if (rtimer > (dingtime * 4))
        {
            draw_text(_xx - 21 + drfr_offset, _yy + (4.5 * mspace) + 30, yougetstring + "" "" + rankstring);
        }
        draw_set_halign(fa_left);
        if (rtimer > (dingtime * 4))
        {
            draw_text_transformed_color(_xx + drfr_offset, _yy + (4 * mspace) + 28, lettergrade, 2, 2, 0, lettergradeblend, lettergradeblend, lettergradeblend, lettergradeblend, 1);
        }
        draw_set_halign(fa_center);
        if (rtimer > (dingtime * 5))
        {
            draw_text(_xx - 30, _yy + (6.5 * mspace) + 8, ""+"" + string(totalstring) + "" "" + totalbonustxt);
        }
        draw_set_halign(fa_center);
"
);

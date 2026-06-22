Patcher.FindReplace(
"gml_Object_obj_ghosthouse_jackolantern_merciful_Step_0",
@"    if (end_con == 3)
    {
        if (timer == 45)
        {
            snd_resume(screamsound);
            sprite_index = spr_jackolantern_cry;
            audio_sound_set_track_position(screamsound, 7.9);
        }
        else if (timer == 93)
",
@"    if (end_con == 3)
    {
        var drfr_offset = (global.lang == ""en"") ? 0.6 : 0;
        if (timer == 45)
        {
            snd_resume(screamsound);
            sprite_index = spr_jackolantern_cry;
            audio_sound_set_track_position(screamsound, 7.9 + drfr_offset);
        }
        else if (timer == (93 + (drfr_offset * 30)))
");

Patcher.FindReplace(
"gml_Object_obj_ghosthouse_jackolantern_merciful_Draw_0",
"        if ((_ap > 0.8 && _ap < 1.3) || (_ap > 9.6 && _ap < 10))",
@"        var drfr_offset = 0.6;
        if (end_con == 3 && global.lang == ""en"")
        {
            _ap -= drfr_offset;
        }
		if ((_ap > 0.8 && _ap < 1.3) || (_ap > 9.6 && _ap < 10))
");

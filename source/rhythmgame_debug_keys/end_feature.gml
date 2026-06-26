fame = clamp(fame, 0, max_fame);
    
if (scr_debug())
{
    if (song_initialized && !song_done && loadsong == 3)
    {
        var _skip = false;
        
        if (keyboard_check_pressed(vk_f5))
        {
            _skip = true;
            fame = 999999;
        }
        
        if (keyboard_check_pressed(vk_f6))
        {
            fame = 0;
            
            if (lose_con == 0)
                lose_con = 1;
        }
        
        if (_skip)
        {
            total_fame = fame;
            trackpos = track_length;
            audio_stop_all();
            scr_rhythmgame_notechart_clear();
            performer.sprite_index = spr_kris_rock_2;
            
            with (drums)
            {
                performer.sprite_index = spr_susie_drum;
                scr_rhythmgame_notechart_clear();
                con = -1;
                fade = 1;
            }
            
            with (vocals)
            {
                performer.sprite_index = spr_ralsei_rock_1;
                scr_rhythmgame_notechart_clear();
                scr_rhythmgame_clear_all_lyrics();
            }
        }
    }
}
if (scr_debug() && song_initialized == 1)
{
    if (keyboard_check_pressed(ord("P")) || (show_debug && (gamepad_button_check_pressed(0, gp_start) || gamepad_button_check_pressed(1, gp_start))))
    {
        paused = !paused;
        
        if (paused)
        {
            audio_pause_sound(track1_main);
            audio_pause_sound(track2_main);
            
            if (song_id == 0)
            {
                audio_pause_sound(track1_solo);
                audio_pause_sound(track2_solo);
            }
        }
        else
        {
            audio_resume_sound(track1_main);
            audio_resume_sound(track2_main);
            
            if (song_id == 0)
            {
                audio_resume_sound(track1_solo);
                audio_resume_sound(track2_solo);
            }
        }
    }
}

var _truetrackpos = 0;
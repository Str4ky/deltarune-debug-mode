if (scr_debug())
{
    if (keyboard_check_pressed(ord("U")))
        event_user(4);
    
    if (keyboard_check_pressed(ord("I")) || (show_debug && (gamepad_button_check_pressed(0, gp_face3) || gamepad_button_check_pressed(1, gp_face3))))
    {
        if (tutorial > 0)
        {
            debug_print("Can not alter autoplay during tutorial");
        }
        else if (auto_play == 0)
        {
            auto_play = 1;
            debug_print("Autoplay enabled.");
        }
        else if (auto_play == 1)
        {
            auto_play = 0;
            debug_print("Autoplay disabled.");
        }
    }
    
    if (keyboard_check_pressed(ord("N")) || (show_debug && (gamepad_button_check_pressed(0, gp_select) || gamepad_button_check_pressed(1, gp_select))))
    {
        show_debug = !show_debug;
        
        if (show_debug)
        {
            debug_print("debug mode enabled");
        }
        else
        {
            scr_debug_delete_persistent("Debug keys");
            debug_print("debug mode disabled");
        }
    }
    
    if (song_id == 0 && solo_con == 0)
    {
        if (keyboard_check_pressed(ord("1")))
        {
            solo_difficulty = 0;
            scr_debug_print("Solo difficulty set to easy.");
        }
        else if (keyboard_check_pressed(ord("2")))
        {
            solo_difficulty = 1;
            scr_debug_print("Solo difficulty set to medium.");
        }
        else if (keyboard_check_pressed(ord("3")))
        {
            solo_difficulty = 2;
            scr_debug_print("Solo difficulty set to hard.");
        }
        else if (keyboard_check_pressed(ord("4")))
        {
            solo_difficulty = -1;
            scr_debug_print("Solo difficulty set to auto.");
        }
    }
    
    var _vol = main_vol;
    
    if (sunkus_kb_check_pressed_with_repeat(189))
        _vol -= (keyboard_check(vk_lshift) ? 0.5 : 0.1);
    
    if (sunkus_kb_check_pressed_with_repeat(187))
        _vol += (keyboard_check(vk_lshift) ? 0.5 : 0.1);
    
    if (_vol != main_vol)
    {
        main_vol = clamp01(_vol);
        mus_volume(track1_instance, main_vol, 0);
        
        if (oneAtATime)
            mus_volume(track2_instance, 0, 0);
        else
            mus_volume(track2_instance, main_vol, 0);
        
        debug_print("Song volume set to " + string(main_vol * 100) + "%");
    }
}

if (song_id == 4)
{
    if (scr_debug() && intro_con == 1 && keyboard_check_pressed(ord("4")))
        intro_con = 2;
}
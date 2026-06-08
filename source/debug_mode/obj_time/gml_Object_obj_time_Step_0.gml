if (keyboard_check_pressed(vk_f10))
{
    global.debug = !global.debug;
    
    if (global.debug)
        scr_debug_print(scr_dmode_get_text("dmode_activated"));
    else
        scr_debug_print(scr_dmode_get_text("dmode_desactivated"));
}

if (scr_debug() && (!instance_number(obj_dmenu_system) || !global.dreading_custom_flag))
{
	if ((keyboard_check_pressed(ord("P")) || keyboard_check_pressed(ord("O"))) && !variable_global_exists("speed_fps"))
    {
		global.speed_fps = 30;
    }
    if (keyboard_check_pressed(ord("P")))
    {
        if (global.speed_fps == 30)
        {
            global.speed_fps = 1;
            game_set_speed(1, gamespeed_fps);
            scr_debug_print(scr_dmode_get_text("fps_1"));
        }
        else
        {
            global.speed_fps = 30;
            game_set_speed(30, gamespeed_fps);
            scr_debug_print(scr_dmode_get_text("fps_30"));
        }
    }
    if (keyboard_check_pressed(ord("O")))
    {
        if (global.speed_fps == 30)
        {
            global.speed_fps = 60;
            game_set_speed(60, gamespeed_fps);
            scr_debug_print(scr_dmode_get_text("fps_60"));
        }
        else if (global.speed_fps == 60)
        {
            global.speed_fps = 120;
            game_set_speed(120, gamespeed_fps);
            scr_debug_print(scr_dmode_get_text("fps_120"));
        }
        else
        {
            global.speed_fps = 30;
            game_set_speed(30, gamespeed_fps);
            scr_debug_print(scr_dmode_get_text("fps_30"));
        }
    }
    if (keyboard_check_pressed(ord("G")))
    {
		global.dgodmode = !global.dgodmode;

        if (global.dgodmode)
			scr_debug_print("Godmode enabled");
		else
			scr_debug_print("Godmode disabled");
    }
}

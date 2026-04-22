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
    if (keyboard_check_pressed(ord("P")))
    {
        if (room_speed == 30)
        {
            room_speed = 1;
            scr_debug_print(scr_dmode_get_text("fps_1"));
        }
        else
        {
            room_speed = 30;
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
    
    if (keyboard_check_pressed(ord("O")))
    {
        if (room_speed == 120 || room_speed == 1)
        {
            room_speed = 30;
            scr_debug_print(scr_dmode_get_text("fps_30"));
        }
        else if (room_speed == 60)
        {
            room_speed = 120;
            scr_debug_print(scr_dmode_get_text("fps_120"));
        }
        else if (room_speed == 30)
        {
            room_speed = 60;
            scr_debug_print(scr_dmode_get_text("fps_60"));
        }
    }
}

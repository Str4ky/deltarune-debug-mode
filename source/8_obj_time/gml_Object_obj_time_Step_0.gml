if (keyboard_check_pressed(vk_f10))
{
    global.debug = !global.debug;
    
    if (global.debug)
        scr_debug_print("Mode Debug activé !");
    else
        scr_debug_print("Mode Debug désactivé !");
}

if (scr_debug() && (!instance_number(obj_dmenu_system) || !global.dreading_custom_flag))
{
    if (keyboard_check_pressed(ord("P")))
    {
        if (room_speed == 30)
        {
            room_speed = 1;
            scr_debug_print("FPS à 1");
        }
        else
        {
            room_speed = 30;
            scr_debug_print("FPS à 30");
        }
    }
    
    if (keyboard_check_pressed(ord("O")))
    {
        if (room_speed == 120 || room_speed == 1)
        {
            room_speed = 30;
            scr_debug_print("FPS à 30");
        }
        else if (room_speed == 60)
        {
            room_speed = 120;
            scr_debug_print("FPS à 120");
        }
        else if (room_speed == 30)
        {
            room_speed = 60;
            scr_debug_print("FPS à 60");
        }
    }
}
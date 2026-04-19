if (scr_debug() && (!instance_number(obj_dmenu_system) || !global.dreading_custom_flag))
{
    if (keyboard_check_pressed(ord("T")))
    {
        if (global.tension < 250)
        {
            global.tension = 250;
            scr_debug_print("TP à 250 %");
        }
        else
        {
            global.tension = 0;
            scr_debug_print("TP à 0 %");
        }
    }
    
    if (keyboard_check_pressed(ord("V")))
        scr_turn_skip();
    
    if (keyboard_check_pressed(ord("H")))
    {
        scr_debug_fullheal();
        scr_debug_print("HP du party restaurés");
    }
    
    if (keyboard_check_pressed(ord("W")))
    {
        scr_wincombat();
        scr_debug_print("Combat passé");
    }
}
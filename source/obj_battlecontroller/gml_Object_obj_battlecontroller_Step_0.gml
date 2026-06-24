if (scr_debug() && (!instance_number(obj_dmenu_system) || !global.dreading_custom_flag))
{
    if (keyboard_check_pressed(ord("T")))
    {
        if (global.tension < 250)
        {
            global.tension = 250;
            scr_debug_print(dstr("TP to 250%", "PT à 250 %"));
        }
        else
        {
            global.tension = 0;
            scr_debug_print(dstr("TP to 0%", "PT à 0 %"));
        }
    }
    
    if (keyboard_check_pressed(ord("V")))
        scr_turn_skip();
    
    if (keyboard_check_pressed(ord("H")))
    {
        scr_debug_fullheal();
        scr_debug_print(dstr("Party HP fully restored", "PV de l'équipe restaurés"));
    }
    
    if (keyboard_check_pressed(ord("W")))
    {
        scr_wincombat();
        scr_debug_print(dstr("Fight skipped", "Combat passé"));
    }
}
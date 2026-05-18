function scr_turn_skip()
{
    if (global.turntimer > 0 && instance_exists(obj_growtangle) && scr_isphase("bullets"))
    {
        global.turntimer = 0;
        scr_debug_print(scr_dmode_get_text("turnskip"));
    }
}
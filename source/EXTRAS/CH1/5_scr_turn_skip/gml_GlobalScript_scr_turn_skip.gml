function scr_turn_skip()
{
    if (global.mnfight == 2
    && global.turntimer > 0
    &instance_exists(obj_growtangle))
    {
        global.turntimer = 0;
        scr_debug_print("Tour de l'ennemi passé");
    }
}
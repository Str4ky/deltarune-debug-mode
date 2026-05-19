if (scr_debug())
{
    if (keyboard_check_pressed(ord("S")))
        instance_create(0, 0, obj_savemenu);
    if (keyboard_check_pressed(ord("L")) && keyboard_check(vk_shift))
    {
        obj_dmenu_system.dmenu_popup_launch = 1;
        obj_dmenu_system.dmenu_state = "debug_load";
        obj_dmenu_system.dbutton_selected = 1;
        obj_dmenu_system.dmenu_active = true;
        snd_play(snd_egg);
    }
    if (keyboard_check_pressed(ord("L")) && !keyboard_check(vk_shift))
        scr_load();
    if (keyboard_check_pressed(ord("R")) && keyboard_check(vk_backspace))
        game_restart_true();

    if (keyboard_check_pressed(ord("R")) && !keyboard_check(vk_backspace))
    {
        snd_free_all();
        room_restart();
        global.interact = 0;
    }
}

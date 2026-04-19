if (global.debug == 1)
{
    if (keyboard_check_pressed(ord("S")))
        instance_create(0, 0, obj_savemenu);
    if (keyboard_check_pressed(ord("L")))
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
if (!instance_exists(obj_dmenu_system))
    instance_create(0, 0, obj_dmenu_system)
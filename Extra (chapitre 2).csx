// Fonctions du Darkworld
UndertaleGameObject obj_darkcontroller = Data.GameObjects.ByName("obj_darkcontroller");

importGroup.QueueFindReplace(obj_darkcontroller.EventHandlerFor(EventType.Step, (uint)0, Data),
"if (scr_debug())", "if (0)");

importGroup.QueueAppend(obj_darkcontroller.EventHandlerFor(EventType.Step, (uint)0, Data), @"
if (scr_debug() && (!instance_number(obj_dmenu_system) || !global.dreading_custom_flag))
{
    if (keyboard_check_pressed(ord(""2"")) && keyboard_check(ord(""M"")))
    {
        if (global.gold >= 100)
        {
            global.gold -= 100;
            scr_debug_print(""- 100 D$"");
        }
        else
        {
            scr_debug_print(""- "" + string(global.gold) + "" D$"");
            global.gold = 0;
        }
    }
    if (keyboard_check_pressed(ord(""1"")) && keyboard_check(ord(""M"")))
    {
        global.gold += 100;
        scr_debug_print(""+ 100 D$"");
    }

    if (keyboard_check_pressed(ord(""S"")))
        instance_create(0, 0, obj_savemenu);

    if (keyboard_check_pressed(ord(""L"")))
        scr_load();

    if (keyboard_check_pressed(ord(""R"")) && keyboard_check_pressed(vk_backspace))
        game_restart_true();

    if (keyboard_check_pressed(ord(""R"")) && keyboard_check_pressed(vk_backspace))
    {
        snd_free_all();
        room_restart();
        global.interact = 0;
    }
}
if (!instance_exists(obj_dmenu_system))
    instance_create(0, 0, obj_dmenu_system)
");
ChangeSelection(obj_darkcontroller);


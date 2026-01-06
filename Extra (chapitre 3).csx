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

    if (sunkus_kb_check_pressed(83))
        instance_create(0, 0, obj_savemenu);

    if (sunkus_kb_check_pressed(76))
        scr_load();

    if (sunkus_kb_check_pressed(82) && sunkus_kb_check(8))
        game_restart_true();

    if (sunkus_kb_check_pressed(82) && !sunkus_kb_check(8))
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

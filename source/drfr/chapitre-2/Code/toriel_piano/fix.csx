Patcher.FindReplace(
    "gml_Object_obj_npc_mansion_room_Create_0",
    @"    if (global.lang == ""en"")
    {
        var tutorial = instance_create(378, 326, obj_npc_room_animated);
        tutorial.sprite_index = spr_dw_mansion_room_kris_toriel_piano;
        with (tutorial)
        {
            scr_depth();
        }
    }
",
    @""
);

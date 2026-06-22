Patcher.FindReplace(
"gml_Object_obj_room_castle_tv_zone_battle_Step_0",
@"        c_msgsetloc(0, <string>, ""obj_room_castle_tv_zone_battle_slash_Step_0_gml_154_0"");
        c_msgnextloc(<string>, ""obj_room_castle_tv_zone_battle_slash_Step_0_gml_155_0"");
",
@"        if (solo_mode)
        {
            c_msgsetloc(0, ""* Hé^1, qu'est-ce que tu fiches&ici ?!/"", ""obj_room_castle_tv_zone_battle_slash_Step_0_gml_154_0"");
            c_msgnextloc(""* T'as pas lu la PORTE ou quoi ?!/"", ""obj_room_castle_tv_zone_battle_slash_Step_0_gml_155_0"");
        }
        else
        {
            c_msgsetloc(0, ""* Hé^1, qu'est-ce que vous fichez&ici ?!/"", ""obj_room_castle_tv_zone_battle_slash_Step_0_gml_154_0"");
            c_msgnextloc(""* Vous avez pas lu la PORTE ou quoi ?!/"", ""obj_room_castle_tv_zone_battle_slash_Step_0_gml_155_0"");
        }
");

Patcher.FindReplace(
"gml_Object_obj_room_castle_tv_zone_battle_Step_0",
@"        c_msgsetloc(0, <string>, ""obj_room_castle_tv_zone_battle_slash_Step_0_gml_186_0"");",
@"    if (solo_mode)
    {
        c_msgnextloc(""* Quoi !? Quoi !? Tu te prends pour Mike, c'est ça !?/"", ""obj_room_castle_tv_zone_battle_slash_Step_0_gml_204_0"");
    }
    else
    {
        c_msgnextloc(""* Quoi !? Quoi !? Vous vous prenez pour Mike, c'est ça !?/"", ""obj_room_castle_tv_zone_battle_slash_Step_0_gml_204_0"");
    }
");

Patcher.FindReplace(
"gml_Object_obj_room_castle_tv_zone_battle_Step_0",
@"    c_msgnextloc(<string>, ""obj_room_castle_tv_zone_battle_slash_Step_0_gml_204_0"");",
@"    if (solo_mode)
    {
        c_msgnextloc(""* Quoi !? Quoi !? Tu te prends pour Mike, c'est ça !?/"", ""obj_room_castle_tv_zone_battle_slash_Step_0_gml_204_0"");
    }
    else
    {
        c_msgnextloc(""* Quoi !? Quoi !? Vous vous prenez pour Mike, c'est ça !?/"", ""obj_room_castle_tv_zone_battle_slash_Step_0_gml_204_0"");
    }
");

Patcher.FindReplace(
"gml_Object_obj_room_castle_tv_zone_battle_Step_0",
@"    c_msgsetloc(0, <string>, ""obj_room_castle_tv_zone_battle_slash_Step_0_gml_550_0"");",
@"    if (solo_mode)
    {
        c_msgsetloc(0, ""* Sont des imposteurs !!^1! Flingue-les !!!/"", ""obj_room_castle_tv_zone_battle_slash_Step_0_gml_550_0"");
    }
    else
    {
        c_msgsetloc(0, ""* Sont des imposteurs !!^1! Flinguez-les !!!/"", ""obj_room_castle_tv_zone_battle_slash_Step_0_gml_550_0"");
    }
");

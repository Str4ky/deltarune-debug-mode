Patcher.FindReplace(
    "gml_Object_obj_fusionmenu_Step_0",
    @"scr_musicmenu_songadd(""battle.ogg"", stringsetloc(<string>, ""obj_fusionmenu_slash_Step_0_gml_575_0""));",
    @"scr_musicmenu_songadd(""battle.ogg"", stringsetloc(""Rude Buster"", ""obj_fusionmenu_slash_Step_0_gml_575_0""));"
);

Patcher.FindReplace(
    "gml_Object_obj_fusionmenu_Step_0",
    @"scr_musicmenu_songadd(""queen.ogg"", stringsetloc(<string>, ""obj_fusionmenu_slash_Step_0_gml_581_0""));",
    @"scr_musicmenu_songadd(""queen.ogg"", stringsetloc(""Queen"", ""obj_fusionmenu_slash_Step_0_gml_581_0""));"
);

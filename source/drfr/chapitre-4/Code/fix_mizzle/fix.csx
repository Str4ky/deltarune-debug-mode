Patcher.FindReplace(
"gml_GlobalScript_scr_monstersetup",
@"""scr_monstersetup_slash_scr_monstersetup_gml_2122_0"");
        global.actsimulsus[myself][0] = 1;
",
@"""scr_monstersetup_slash_scr_monstersetup_gml_2122_0"");
        global.actsimulsus[myself][0] = 0;
");


Patcher.FindReplace(
"gml_Object_obj_mizzle_enemy_Step_0",
@"    if (actingsus == 1 && actconsus == 1)
    {
        var rand = choose(0, 1, 2);
        if (rand == 0)
        {
            msgsetloc(0, <string>, ""obj_mizzle_enemy_slash_Step_0_gml_542_0"");
        }
        if (rand == 1)
        {
            msgsetloc(0, <string>, ""obj_mizzle_enemy_slash_Step_0_gml_543_0"");
        }
        if (rand == 2)
        {
            msgsetloc(0, <string>, ""obj_mizzle_enemy_slash_Step_0_gml_544_0"");
        }
        scr_mercyadd(myself, 20);
        scr_simultext(""susie"");
        if (simulordersus == 0)
        {
            actconsus = 20;
        }
        else
        {
            actconsus = 0;
        }
    }
",
@"    if (actingsus == 1 && actconsus == 1)
    {
        var rand = choose(0, 1, 2);
        if (rand == 0)
        {
            msgsetloc(0, ""* Susie gargouille bruyamment !/%"", ""obj_mizzle_enemy_slash_Step_0_gml_542_0"");
        }
        if (rand == 1)
        {
            msgsetloc(0, ""* Susie trébuche sur du sol glissant !/%"", ""obj_mizzle_enemy_slash_Step_0_gml_543_0"");
        }
        if (rand == 2)
        {
            msgsetloc(0, ""* Susie ronfle éveillée !/%"", ""obj_mizzle_enemy_slash_Step_0_gml_544_0"");
        }
        scr_mercyadd(myself, 20);
        scr_battletext_default();
        actconsus = 20;
    }
");

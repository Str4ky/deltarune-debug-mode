Patcher.FindReplace(
"gml_GlobalScript_scr_monstersetup",
@"        global.actdescsus[myself][0] = stringsetloc(<string>, ""scr_monstersetup_slash_scr_monstersetup_gml_2063_0"");
        global.actsimulsus[myself][0] = 1;
",
@"        global.actdescsus[myself][0] = stringsetloc("""", ""scr_monstersetup_slash_scr_monstersetup_gml_2063_0"");
        global.actsimulsus[myself][0] = 0;
");

Patcher.FindReplace(
"gml_Object_obj_watercooler_enemy_Step_0",
@"        if (simultotal == 1)
        {
            msgset(0, _text + ""/%"");
            scr_battletext_default();
            with (obj_face)
            {
                instance_destroy();
            }
            actconsus = 20;
        }
        else
        {
            msgset(0, _text);
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
@"        msgset(0, _text + ""/%"");
        scr_battletext_default();
        with (obj_face)
        {
            instance_destroy();
        }
        actconsus = 20;
");

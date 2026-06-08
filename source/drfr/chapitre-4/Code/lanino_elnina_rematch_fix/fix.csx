Patcher.FindReplace(
"gml_Object_obj_lanino_rematch_enemy_Step_0",
@"        if (sunglasses_count == 1)
        {
            msgsetloc(0, <string>, ""obj_lanino_rematch_enemy_slash_Step_0_gml_83_0"");
            msgnextloc(<string>, ""obj_lanino_rematch_enemy_slash_Step_0_gml_84_0"");
            scr_battletext_default();
            actcon = 1;
            global.actsimul[myself][2] = 1;
            scr_spellmenu_setup();
        }
        else
        {
            msgsetloc(0, <string>, ""obj_lanino_rematch_enemy_slash_Step_0_gml_93_0"");
            scr_simultext(""kris"");
            if (simulorderkri == 0)
            {
                actcon = 20;
            }
            else
            {
                actcon = -1;
            }
        }
",
@"        if (sunglasses_count == 1)
        {
            msgsetloc(0, ""* Vous levez les yeux au ciel et prenez...&* Des lunettes de soleil !/%"", ""obj_lanino_rematch_enemy_slash_Step_0_gml_83_0"");
            msgnextloc(""* Bloquer les soleils montre votre souci du bulletin météo !/%"", ""obj_lanino_rematch_enemy_slash_Step_0_gml_84_0"");
            scr_battletext_default();
            actcon = 1;
        }
        else
        {
            msgsetloc(0, ""* Lunettes de soleil anti-soleil équipées !/%"", ""obj_lanino_rematch_enemy_slash_Step_0_gml_93_0"");
            scr_battletext_default();
            actcon = 1;
        }
");

Patcher.FindReplace(
"gml_Object_obj_lanino_rematch_enemy_Step_0",
@"        if (telescope_count == 1)
        {
            msgsetloc(0, <string>, ""obj_lanino_rematch_enemy_slash_Step_0_gml_53_0"");
            msgnextloc(<string>, ""obj_lanino_rematch_enemy_slash_Step_0_gml_54_0"");
            scr_battletext_default();
            actcon = 1;
        }
        else
        {
            msgsetloc(0, <string>, ""obj_lanino_rematch_enemy_slash_Step_0_gml_63_0"");
            scr_battletext_default();
            actcon = 1;
        }
",
@"        if (telescope_count == 1)
        {
            msgsetloc(0, ""* Vous levez les yeux au ciel et prenez...&* Un télescope !/%"", ""obj_lanino_rematch_enemy_slash_Step_0_gml_53_0"");
            msgnextloc(""* Bloquer les lunes montre votre souci du bulletin météo !/%"", ""obj_lanino_rematch_enemy_slash_Step_0_gml_54_0"");
            scr_battletext_default();
            actcon = 1;
            global.actsimul[myself][1] = 1;
            scr_spellmenu_setup();
        }
        else
        {
            msgsetloc(0, ""* Télescope anti-lune équipé !"", ""obj_lanino_rematch_enemy_slash_Step_0_gml_63_0"");
            scr_simultext(""kris"");
            if (simulorderkri == 0)
            {
                actcon = 20;
            }
            else
            {
                actcon = -1;
            }
        }
");

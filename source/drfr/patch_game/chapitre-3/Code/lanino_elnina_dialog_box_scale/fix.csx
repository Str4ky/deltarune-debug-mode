Patcher.FindReplace(
"gml_Object_obj_dbulletcontroller_Step_0",
@"                msgsetloc(0, <string>, ""obj_dbulletcontroller_slash_Step_0_gml_2437_0"");
                scr_enemyblcon(x + 40, global.monstery[myself] - 70, 13);
",
@"                msgsetloc(0, ""Belle attaque,&hein, Lanino !?"", ""obj_dbulletcontroller_slash_Step_0_gml_2437_0"");
                drfr_blcon = scr_enemyblcon(x + 40, global.monstery[myself] - 70, 13);
                drfr_blcon.image_xscale = 1.1;
");

Patcher.FindReplace(
"gml_Object_obj_dbulletcontroller_Step_0",
@"                msgsetloc(0, <string>, ""obj_dbulletcontroller_slash_Step_0_gml_2469_0"");
                scr_enemyblcon(x + 40, global.monstery[myself] - 70, 13);
",
@"                msgsetloc(0, ""...&Lanino ?"", ""obj_dbulletcontroller_slash_Step_0_gml_2437_0"");
                drfr_blcon = scr_enemyblcon(x + 40, global.monstery[myself] - 70, 13);
                drfr_blcon.image_xscale = 1.1;
");

Patcher.FindReplace(
"gml_Object_obj_dbulletcontroller_Step_0",
@"                msgsetloc(0, <string>, ""obj_dbulletcontroller_slash_Step_0_gml_2446_0"");
                if (global.lang == ""ja"")
                {
                    scr_enemyblcon(x - 60, global.monstery[myself] + 70, 12.1);
                }
                else
                {
                    scr_enemyblcon(x - 60, global.monstery[myself] + 70, 12);
                }
                myblcon.depth = other.depth - 100;
",
@"                msgsetloc(0, ""Belle attaque,&hein, Elnina !?"", ""obj_dbulletcontroller_slash_Step_0_gml_2446_0"");
                if (global.lang == ""ja"")
                {
                    scr_enemyblcon(x - 60, global.monstery[myself] + 70, 12.1);
                }
                else
                {
                    drfr_blcon = scr_enemyblcon(x - 60, global.monstery[myself] + 70, 12);
					drfr_blcon.image_xscale = 1.1;
                }
");

Patcher.FindReplace(
"gml_Object_obj_dbulletcontroller_Step_0",
@"                msgsetloc(0, <string>, ""obj_dbulletcontroller_slash_Step_0_gml_2479_0"");
                if (global.lang == ""ja"")
                {
                    scr_enemyblcon(x - 60, global.monstery[myself] + 70, 12.2);
                }
                else
                {
                    scr_enemyblcon(x - 5, global.monstery[myself] + 70, 12.3);
                }
                myblcon.depth = other.depth - 100;
",
@"                msgsetloc(0, ""...&Elnina ?"", ""obj_dbulletcontroller_slash_Step_0_gml_2479_0"");
                if (global.lang == ""ja"")
                {
                    scr_enemyblcon(x - 60, global.monstery[myself] + 70, 12.2);
                }
                else
                {
                    drfr_blcon = scr_enemyblcon(x - 5, global.monstery[myself] + 70, 12.3);
					drfr_blcon.image_xscale = 1.1;
                }
                myblcon.depth = other.depth - 100;
");

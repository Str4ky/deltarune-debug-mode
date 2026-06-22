Patcher.FindReplace(
"gml_Object_obj_hammer_of_justice_enemy_Step_0",
@"            if (susietalks == 1 && instance_exists(obj_herosusie))
            {
                global.typer = 75;
                if (ballooncon == 1)
                {
                    balloon = scr_enemyblcon(obj_herosusie.x - 40, obj_herosusie.y - 78, 10);
                    balloon.side = 2;
                }
                if (ballooncon == 4)
                {
                    balloon = scr_enemyblcon(obj_herosusie.x - 25, obj_herosusie.y - 55, 10);
                    balloon.side = 2;
                }
            }
            else
            {
                global.typer = 86;
                if (ballooncon == 2)
                {
                    balloon = scr_enemyblcon(camerax() + 430, obj_herosusie.y - 96, 10);
                    balloon.side = 2;
                }
                if (ballooncon == 3)
                {
                    balloon = scr_enemyblcon(camerax() + 418, obj_herosusie.y - 116, 10);
                    balloon.side = 2;
                }
                if (ballooncon == 4)
                {
                    balloon = scr_enemyblcon(camerax() + 430, obj_herosusie.y - 106, 10);
                    balloon.side = 2;
                }
            }
",
@"            if (susietalks == 1 && instance_exists(obj_herosusie))
            {
                global.typer = 75;
                if (ballooncon == 1)
                {
                    balloon = scr_enemyblcon(obj_herosusie.x - 35, obj_herosusie.y - 78, 10);
                    balloon.side = 2;
                }
                if (ballooncon == 4)
                {
                    balloon = scr_enemyblcon(obj_herosusie.x - 13, obj_herosusie.y - 78, 10);
                    balloon.side = 2;
                }
            }
            else
            {
                global.typer = 86;
                if (ballooncon == 2)
                {
                    balloon = scr_enemyblcon(camerax() + 401, obj_herosusie.y - 56, 10);
                    balloon.side = 2;
                }
                if (ballooncon == 3)
                {
                    balloon = scr_enemyblcon(camerax() + 406, obj_herosusie.y - 116, 10);
                    balloon.side = 2;
                }
                if (ballooncon == 4)
                {
                    balloon = scr_enemyblcon(camerax() + 430, obj_herosusie.y - 106, 10);
                    balloon.side = 2;
                }
            }
");

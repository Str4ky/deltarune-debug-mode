Patcher.FindReplace(
"gml_Object_obj_rouxls_ch3_enemy_Step_0",
@"                    msgsetloc(0, <string>, ""obj_rouxls_ch3_enemy_slash_Step_0_gml_1452_0"");
                    scr_enemyblcon(x + 20, y + 120, 11);
",
@"                    msgsetloc(0, ""Pardon, mon rayon&de soleil !/%"", ""obj_rouxls_ch3_enemy_slash_Step_0_gml_1452_0"");
                    blcon = scr_enemyblcon(x + 20, y + 120, 11);
					blcon.image_xscale = 1.3;
"
);

Patcher.FindReplace(
"gml_Object_obj_rouxls_ch3_enemy_Step_0",
@"                    msgsetloc(0, <string>, ""obj_rouxls_ch3_enemy_slash_Step_0_gml_1469_0"");
                    scr_enemyblcon(x - 58, y + 131, 12);
",
@"                    msgsetloc(0, ""Pardon, ma goutte&de rosée !/%"", ""obj_rouxls_ch3_enemy_slash_Step_0_gml_1469_0"");
                    blcon = scr_enemyblcon(x - 88, y + 131, 12);
					blcon.image_xscale = 1.3;
"
);

Patcher.FindReplace(
"gml_Object_obj_rouxls_ch3_enemy_Step_0",
@"                    msgsetloc(0, <string>, ""obj_rouxls_ch3_enemy_slash_Step_0_gml_1482_0"");
                    scr_enemyblcon(x + 20, y + 120, 11);
",
@"                    msgsetloc(0, ""La météo devrait&toujours.../%"", ""obj_rouxls_ch3_enemy_slash_Step_0_gml_1482_0"");
                    blcon = scr_enemyblcon(x + 20, y + 120, 11);
					blcon.image_xscale = 1.3;
"
);

Patcher.FindReplace(
"gml_Object_obj_rouxls_ch3_enemy_Step_0",
@"                    msgsetloc(0, <string>, ""obj_rouxls_ch3_enemy_slash_Step_0_gml_1499_0"");
                    scr_enemyblcon(x - 58, y + 131, 12);
",
@"                    msgsetloc(0, ""marcher en duo.../%"", ""obj_rouxls_ch3_enemy_slash_Step_0_gml_1499_0"");
                    blcon = scr_enemyblcon(x - 88, y + 131, 12);
					blcon.image_xscale = 1.3;
"
);

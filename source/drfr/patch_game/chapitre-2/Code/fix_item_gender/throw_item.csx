{

Patcher.FindReplace(
"gml_Object_obj_darkcontroller_Draw_0",
@"        draw_text(xx + 20, yy + 10, string_hash_to_newline(stringsetsubloc(<string>, global.itemname[global.submenucoord[2]], ""obj_darkcontroller_slash_Draw_0_gml_956_0"")));",
@"            scr_drfr_get_anyitem_gender(global.item[global.submenucoord[2]], ""item"");
			if (use_apo)
				nl_text = ""#"" + safe_apo_name;
			else
				nl_text = safe_apo_name + ""#"";
            draw_text(xx + 20, yy + 10, string_hash_to_newline(stringsetsubloc(""Voulez-vous vraiment jeter ~2~1 ?"", global.itemname[global.submenucoord[2]], nl_text, ""obj_darkcontroller_slash_Draw_0_gml_956_0"")));
");

Patcher.FindReplace(
"gml_Object_obj_overworldc_Step_0",
@"                    if (i == 0)
                    {
                        global.msg[0] = stringsetsubloc(<string>, global.litemname[global.menucoord[1]], ""obj_overworldc_slash_Step_0_gml_34_0_b"");
                    }
                    if (i == 1)
                    {
                        global.msg[0] = stringsetsubloc(<string>, global.litemname[global.menucoord[1]], ""obj_overworldc_slash_Step_0_gml_34_0"");
                    }
                    if (i == 2)
                    {
                        global.msg[0] = stringsetsubloc(<string>, global.litemname[global.menucoord[1]], ""obj_overworldc_slash_Step_0_gml_36_0"");
                    }
                    if (i == 3)
                    {
                        global.msg[0] = stringsetsubloc(<string>, global.litemname[global.menucoord[1]], ""obj_overworldc_slash_Step_0_gml_38_0_b"");
                    }
                    if (i > 3)
                    {
                        global.msg[0] = stringsetsubloc(<string>, global.litemname[global.menucoord[1]], ""obj_overworldc_slash_Step_0_gml_38_0"");
                    }
",
@"                    scr_drfr_get_anyitem_gender(global.litem[global.menucoord[1]], ""litem"");
                    if (i == 0)
                        global.msg[0] = stringsetsubloc(""* Vous faites calmement vos adieux ~2~1."", global.litemname[global.menucoord[1]], item_article_au, ""obj_overworldc_slash_Step_0_gml_34_0_b"");

                    if (i == 1)
                        global.msg[0] = stringsetsubloc(""* Vous mettez ~2~1 par terre et ~3 tapotez brièvement."", global.litemname[global.menucoord[1]], safe_apo_name, item_gender, ""obj_overworldc_slash_Step_0_gml_34_0"");

                    if (i == 2)
                        global.msg[0] = stringsetsubloc(""* Vous jetez ~2~1 par terre comme un vieux détritus. Bon débarras."", global.litemname[global.menucoord[1]], safe_apo_name, ""obj_overworldc_slash_Step_0_gml_36_0"");

                    if (i == 3)
                        global.msg[0] = stringsetsubloc(""* Vous abandonnez ~2~1."", global.litemname[global.menucoord[1]], safe_apo_name, ""obj_overworldc_slash_Step_0_gml_38_0_b"");

                    if (i > 3)
                        global.msg[0] = stringsetsubloc(""* Vous vous débarrassez ~2~1."", global.litemname[global.menucoord[1]], item_article_du, ""obj_overworldc_slash_Step_0_gml_38_0"");
");

}

Patcher.FindReplace(
"gml_Object_obj_blocked_total_fx_Draw_0",
@"    else
    {
        draw_text(x, y - 6, stringsetloc(<string>, ""obj_blocked_total_fx_slash_Draw_0_gml_9_0""));
        draw_text(x, y + 8, string(count) + stringsetloc(<string>, ""obj_blocked_total_fx_slash_Draw_0_gml_10_0""));
    }
",
@"    else
    {
        drfr_accord = (count >= 2) ? ""s"" : """";
        draw_text(x, y - 6, string(count) + stringsetsubloc("" pub~1"", drfr_accord, ""obj_blocked_total_fx_slash_Draw_0""));
        draw_text(x, y + 8, stringsetsubloc(""bloquée~1"", drfr_accord, ""obj_blocked_total_fx_slash_Draw_0_gml_10_0""));
    }
");

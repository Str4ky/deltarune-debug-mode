Patcher.FindReplace(
"gml_GlobalScript_scr_rhythmgame_draw",
"        draw_text_transformed_color(arg0 + 2, bottomy - 121, _combotext, (combo >= 100) ? 3.5 : 5, 5, 0, #666666, #666666, #BBBBBB, #BBBBBB, 0.25);",
@"        var drfr_combosize = 5;
        if (combo > 999)
            drfr_combosize = 2.75;
        else if (combo >= 100)
            drfr_combosize = 3.5;
        draw_text_transformed_color(arg0 + 2, bottomy - 121, _combotext, drfr_combosize, 5, 0, #666666, #666666, #BBBBBB, #BBBBBB, 0.25);
");

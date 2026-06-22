Patcher.FindReplace(
"gml_Object_obj_battlecontroller_Draw_0",
@"        var xstring = stringsetloc(""X"", ""obj_battlecontroller_slash_Draw_0_gml_819_0"");
        draw_set_color(c_red);
        var offset = 0;
",
@"        var xstring = stringsetloc(""X"", ""obj_battlecontroller_slash_Draw_0_gml_819_0"");
        draw_set_color(c_red);
        var offset = -98;
");

Patcher.FindReplace(
"gml_Object_obj_battlecontroller_Draw_0",
@"    else
    {
        draw_sprite(spr_heart, 0, xx + icx, yy + icy);
    }
    scr_84_set_draw_font(""mainbig"");
",
@"    else
    {
        drfr_customoffset = 0;
        if ((global.monstertype[thisenemy] == 105 || global.monstertype[thisenemy] == 103 || global.monstertype[thisenemy] == 61 || global.monstertype[thisenemy] == 60) && (actcoord % 2) == 1)
            drfr_customoffset += 35;
        draw_sprite(spr_heart, 0, xx + icx + drfr_customoffset, yy + icy);
    }
    scr_84_set_draw_font(""mainbig"");
");

Patcher.FindReplace(
"gml_Object_obj_battlecontroller_Draw_0",
@"        if (i == 1 || i == 3 || i == 5)
        {
            xoffset = 230;
        }
",
@"        if (i == 1 || i == 3 || i == 5)
        {
            xoffset = 230;
            if (global.monstertype[thisenemy] == 105 || global.monstertype[thisenemy] == 103 || global.monstertype[thisenemy] == 61 || global.monstertype[thisenemy] == 60)
                xoffset += 35;
        }
");

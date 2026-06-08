Patcher.FindReplace(
"gml_GlobalScript_scr_84_name_input_setup",
@"    if (LANGSUBTYPE == 0)
    {
        xoff = 68;
        yoff = 70;
        xstep = 20;
        ystep = 20;
        PLAYERNAMEY = 40;
        menu[0] = ""ABCDEFGHIJ"";
        menu[1] = ""KLMNOPQRST"";
        menu[2] = ""UVWXYZ < <"";
        CURX = 0;
        CURY = 0;
    }
",
@"    if (LANGSUBTYPE == 0)
    {
        xoff = 48;
        yoff = 70;
        xstep = 20;
        ystep = 20;
        PLAYERNAMEY = 40;
        menu[0] = ""ABCDEFGHIJKL"";
        menu[1] = ""MNOPQRSTUVWX"";
        menu[2] = ""YZÀÂÇÈÉÊËÌÍÎ"";
        menu[3] = ""ÏÔÙÛŒ_- << <"";
        CURX = 0;
        CURY = 0;
    }
");

Patcher.FindReplace(
"gml_GlobalScript_scr_84_name_input_setup",
@"    if (LANGSUBTYPE == 0)
    {
        NAME[6][2] = <string>;
        NAME[8][2] = <string>;
    }
",
@"    if (LANGSUBTYPE == 0)
    {
	    NAME[7][3] = ""(B) RETOUR"";
        NAME[10][3] = ""(E) FIN"";
	}
");

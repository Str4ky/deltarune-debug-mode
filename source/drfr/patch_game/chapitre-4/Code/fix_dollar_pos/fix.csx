{

string[] fix_lst = {"gml_Object_obj_gerson_table_Draw_0", "gml_Object_obj_gerson_fountain_Draw_0"};

foreach (string gml_name in fix_lst) {
	Patcher.FindReplace(
		gml_name,
		@"draw_text(camerax() + 440, cameray() + 420, ""$"" + string_hash_to_newline(string(global.gold)));",
		@"draw_text(camerax() + 440, cameray() + 420, string_hash_to_newline(string(global.gold)) + "" $"");"
	);

	Patcher.FindReplace(
		gml_name,
		@"draw_text(camerax() + 440, cameray() + 420, string_hash_to_newline(""$"" + string(global.gold)));",
		@"draw_text(camerax() + 440, cameray() + 420, string_hash_to_newline(string(global.gold) + "" $""));"
	);
}

Patcher.FindReplace(
	"gml_Object_obj_shop1_Draw_0",
	@"draw_text(440, 420, ""$"" + string_hash_to_newline(string(global.gold)));",
	@"draw_text(440, 420, string_hash_to_newline(string(global.gold)) + "" $"");"
);

//gold fountain 1st dark sanctuary
Patcher.FindReplace(
	"gml_Object_obj_dw_church_moneyfountain_Draw_0",
	@"moneystring1 = "" $"" + string(global.flag[898] + obj_numberentry.num);",
	@"moneystring1 = "" "" + string(global.flag[898] + obj_numberentry.num) + "" $"";"
);

Patcher.FindReplace(
	"gml_Object_obj_dw_church_moneyfountain_Draw_0",
	@"moneystring2 = "" $"" + string(global.gold - obj_numberentry.num);",
	@"moneystring2 = "" "" + string(global.gold - obj_numberentry.num) + "" $"";"
);

//Fountain in 3rd sanctuare
Patcher.FindReplace(
"gml_GlobalScript_scr_shop_space_display",
@"    draw_text(xpos + 39 + 8, ypos + 24 + 8, ""$"" + string(global.gold));",
@"    draw_set_halign(fa_right);
    draw_text(xpos + 125, ypos + 24 + 8, string(global.gold) + "" $"");
    draw_set_halign(fa_left);
");

//Default moneydisplay
Patcher.FindReplace(
	"gml_Object_obj_moneydisplay_Draw_0",
	@"draw_text(xx + 120, yy + yl[0], ""$"" + string(global.gold));",
	@"draw_text(xx + 120, yy + yl[0], string(global.gold) + "" $"");"
);

//Number entry
Patcher.FindReplace(
"gml_Object_obj_numberentry_Draw_0",
@"    if (showmoney)
    {
        numstringadd = ""$ "";
    }
",
@"    if (showmoney)
    {
        numstringadd = "" $"";
    }
");

Patcher.FindReplace(
"gml_Object_obj_numberentry_Draw_0",
@"    if (textshadow)
    {
        draw_text_color(xx + txtspc + 1, yy + 1, numstringadd + string(num), c_dkgray, c_dkgray, c_navy, c_navy, 1);
    }
    draw_text(xx + txtspc, yy, numstringadd + string(num));
",
@"    if (textshadow)
    {
        draw_text_color(xx + txtspc + 1, yy + 1, string(num) + numstringadd, c_dkgray, c_dkgray, c_navy, c_navy, 1);
    }
    draw_text(xx + txtspc, yy, string(num) + numstringadd);
");

}

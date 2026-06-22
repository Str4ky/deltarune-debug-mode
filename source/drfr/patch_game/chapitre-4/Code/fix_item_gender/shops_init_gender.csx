//Items
Patcher.FindReplace(
"gml_GlobalScript_scr_shopmenu",
"sellvalue = ceil(itemsellvalue[menuc[menu]] / 2);",
@"scr_drfr_get_anyitem_gender(global.item[menuc[menu]], ""item"");
sellvalue = ceil(itemsellvalue[menuc[menu]] / 2);
");

//Weapons
Patcher.FindReplace(
"gml_GlobalScript_scr_shopmenu",
"sellvalue = ceil(weaponvalue[menuc[12]] / 2);",
@"scr_drfr_get_anyitem_gender(global.weapon[menuc[12]], ""weapon"");
sellvalue = ceil(weaponvalue[menuc[12]] / 2);
");

//Armors
Patcher.FindReplace(
"gml_GlobalScript_scr_shopmenu",
"sellvalue = ceil(armorvalue[menuc[13]] / 2);",
@"scr_drfr_get_anyitem_gender(global.armor[menuc[13]], ""armor"");
sellvalue = ceil(armorvalue[menuc[13]] / 2);
");

string find_str = @"    if (global.inv < 0)";
string prepend_str =
@"    if (global.dgodmode)
        exit;
";

Patcher.FindPrepend("gml_GlobalScript_scr_damage_all_overworld", find_str, prepend_str);

if (curChap == 1)
	find_str = find_str.Replace("global.inv < 0", "global.inv < 0 && debug_inv == 0");

Patcher.FindPrepend("gml_GlobalScript_scr_damage", find_str, prepend_str);

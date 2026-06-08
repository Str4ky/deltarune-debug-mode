Patcher.WriteGMLFile("gml_GlobalScript_scr_dmode_init_lang", "dmode_init_lang");
Patcher.WriteGMLFile("gml_GlobalScript_scr_dmode_get_text", "dmode_get_text");
Patcher.AppendGML("gml_GlobalScript_scr_gamestart", "scr_dmode_init_lang();");

if (1 == 2) {
Patcher.FindReplace(
	"gml_GlobalScript_scr_84_init_localization",
	@"ds_map_add(sm, ""spr_ja_funnytext_daisuki"", spr_funnytext_love);",
	@"ds_map_add(sm, ""spr_funnytext_adore"", spr_funnytext_adore);"
);
}

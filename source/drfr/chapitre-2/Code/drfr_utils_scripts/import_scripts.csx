{

Patcher.WriteGMLFile(
	"gml_GlobalScript_scr_drfr_capitalize_first_letter",
	"scr_drfr_capitalize_first_letter.gml"
);

string[] function_list = {
	"scr_drfr_get_anyitem_gender", "scr_drfr_get_armor_gender",
	"scr_drfr_get_item_gender", "scr_drfr_get_keyitem_gender",
	"scr_drfr_get_litem_gender", "scr_drfr_get_weapon_gender"
};

foreach (string func in function_list) {
	Patcher.WriteGMLFile(
		$"gml_GlobalScript_{func}",
		$"item_gender/{func}.gml"
	);
}

}

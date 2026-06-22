Patcher.FindReplace(
	"gml_Object_obj_dw_ranking_hub_sign_Draw_0",
	"_x_pos = x + 57 + tx;",
	"_x_pos = x + 46 + tx;"
);

Patcher.FindReplace(
	"gml_Object_obj_dw_ranking_hub_sign_Draw_0",
	"draw_text_ext_transformed_color(x + 57 + tx",
	"draw_text_ext_transformed_color(x + 46 + tx"
);

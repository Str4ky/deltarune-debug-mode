Patcher.FindReplace(
"gml_Object_obj_umbrella_tv_Draw_0",
@"draw_text_transformed_color(_xx + 27, _yy + 115, _temp, 2, 2, 25, _temp_color, _temp_color, _temp_color, _temp_color, 1);",
@"var drfr_temp_celsius = round((_temp - 32) * 0.5555555555555556);
draw_text_transformed_color(_xx + 27, _yy + 115, drfr_temp_celsius, 2, 2, 25, _temp_color, _temp_color, _temp_color, _temp_color, 1);"
);

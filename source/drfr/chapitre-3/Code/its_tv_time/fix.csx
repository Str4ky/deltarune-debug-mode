Patcher.WriteGMLFile(
	"gml_Object_obj_tenna_tv_time_Create_0",
	"gml_Object_obj_tenna_tv_time_Create_0.gml"
);

Patcher.FindReplace(
	"gml_Object_obj_tenna_tv_time_Draw_0",
	"draw_sprite_ext(spr_dw_tv_time_lights, lights_anim, x_pos, y_pos, 1 * room_scale, 1 * room_scale, 0, c_white, 1);",
	"draw_sprite_ext(spr_dw_tv_time_lights, lights_anim, x_pos - 12, y_pos + 10, 1 * room_scale, 1 * room_scale, 0, c_white, 1);"
);

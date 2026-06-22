Patcher.FindReplace(
"gml_Object_obj_dw_teevie_shadow_guys_Step_0",
"if (killhappyshads == true)",
@"var drfr_x_offset = 25;
if (killhappyshads == true)"
);

Patcher.FindReplace(
	"gml_Object_obj_dw_teevie_shadow_guys_Step_0",
	"with (instance_create((camerax() + 640) - 50",
	"with (instance_create((camerax() + 640) - 50 - drfr_x_offset"
);

Patcher.FindReplace(
	"gml_Object_obj_rouxls_annyoing_dog_controller_Draw_0",
	"draw_sprite_ext(spr_smashreveal2, 0, (cx + 0 + (textoffset / 5)) - 20, cy + 10, 2, 2, 0, c_white, 1);",
	"draw_sprite_ext(spr_smashreveal2, 0, (cx + 0 + (textoffset / 5)) - 20, (cy + 10) - 22, 2, 2, 0, c_white, 1);"
);

Patcher.FindReplace(
"gml_Object_obj_rouxls_annyoing_dog_controller_Draw_0",
@"    var textposx = cx + 432 + (angle / 10);
    var textposy = cy + 172 + (angle / 10);
",
@"    var textposx = cx + 442 + (angle / 10);
    var textposy = cy + 150 + (angle / 10);
");

Patcher.FindReplace(
"gml_Object_obj_rouxls_annyoing_dog_controller_Draw_0",
@"    var xoffset = 190;
    var yoffset = 270;
",
@"    var xoffset = 180;
    var yoffset = 270;
");

Patcher.FindReplace(
	"gml_Object_obj_rouxls_annyoing_dog_controller_Draw_0",
	"draw_sprite_ext(spr_dog_walk, 0, dogloc[0] + (10 * dogspin), dogloc[1] + (10 * dogspin), scale, scale, -dogspin * 8, c_white, (alpha * 0.5) - (0.2 * dogspin));",
	"draw_sprite_ext(spr_dog_walk, 0, dogloc[0] + (10 * dogspin), dogloc[1] + (10 * dogspin), scale, scale, (-dogspin * 8) - 4, c_white, (alpha * 0.5) - (0.2 * dogspin));"
);

Patcher.FindReplace(
"gml_Object_obj_rouxls_annyoing_dog_controller_Draw_0",
@"    draw_sprite_ext(spr_dog_walk, 0, dogloc[0], dogloc[1], scale, scale, -dogspin * 8, c_white, alpha);
    draw_sprite_ext(revline, 0, (cx + bloc[0]) - bon, cy + bloc[1], 1, 1, 0, c_white, 1);
",
@"    draw_sprite_ext(spr_dog_walk, 0, dogloc[0], dogloc[1], scale, scale, (-dogspin * 8) - 4, c_white, alpha);
    draw_sprite_ext(revline, 0, (cx + bloc[0]) - bon, (cy + bloc[1]) - 12, 1, 1, 0, c_white, 1);
");

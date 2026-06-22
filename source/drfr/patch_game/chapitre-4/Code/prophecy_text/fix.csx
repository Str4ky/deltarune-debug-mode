Patcher.FindReplace(
"gml_Object_obj_dw_church_prophecy_Draw_0",
@"        draw_surface_ext(surf0, (x - width) + xsin + textxoffset, (y - height) + ysin + textyoffset, 2, 2, angle, c_white, image_alpha);
        draw_surface_ext(surf0, (x - width) + xsin + textxoffset, (y - height) + ysin + textyoffset, 2, 2, angle, c_white, image_alpha);
",
@"        drfr_offset = -3;
        draw_surface_ext(surf0, (x - width) + xsin + textxoffset, (y - height) + ysin + textyoffset + drfr_offset, 2, 2, angle, c_white, image_alpha);
        draw_surface_ext(surf0, (x - width) + xsin + textxoffset, (y - height) + ysin + textyoffset + drfr_offset, 2, 2, angle, c_white, image_alpha);
");

Patcher.FindReplace(
"gml_Object_obj_dw_church_prophecy_custom_Draw_0",
@"    else if (mode == 1)
    {
        custom_sprite = sprite_create_from_surface(bg_surface, 0, 0, 320, 124, false, false, 0, 0);
    }
",
@"    else if (mode == 1)
    {
        drfr_offset = -3;
        custom_sprite = sprite_create_from_surface(bg_surface, 0, drfr_offset, 320, 124 - drfr_offset, false, false, 0, 0);
    }
");

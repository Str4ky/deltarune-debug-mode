_selected_string = "No object!#MouseL:Choose&Drag#MouseR:Drag From Anchor";

if (i_ex(selected_object))
{
    so = selected_object;
    sox = selected_object.x;
    soy = selected_object.y;
    
    if (xy_camera_relative == 1)
        sox -= __view_get(e__VW.XView, 0);
    
    if (xy_camera_relative == 1)
        soy -= __view_get(e__VW.YView, 0);
    
    if (xy_camera_relative == 2)
        sox -= so.xstart;
    
    if (xy_camera_relative == 2)
        soy -= so.ystart;
    
    _selected_string = object_get_name(selected_object.object_index);
    _selected_string += (" X: " + string(sox) + " Y: " + string(soy));
    _selected_string += ("#Depth: " + string(selected_object.depth));
    _selected_string += "#Arrows: Move Precisely";
}

draw_set_font(fnt_main);
draw_set_color(c_white);
scr_84_draw_text_outline(0, 430, string_hash_to_newline(_selected_string));
draw_set_font(fnt_main);
_str = string_hash_to_newline(stringsetloc("PgDown: Show All Info", "obj_debug_xy_slash_Draw_74_gml_26_0"));
draw_text(__view_get(2, 0) - string_width(_str), 460, _str);
draw_text(320, 460, string_hash_to_newline(stringsetsubloc("CameraX: ~1 CameraY: ~2", string(__view_get(e__VW.XView, 0)), string(__view_get(e__VW.YView, 0)), "obj_debug_xy_slash_Draw_74_gml_27_0")));

if (show_invisible == 1)
    draw_text(320, 430, string_hash_to_newline(stringsetloc("Show Invisible", "obj_debug_xy_slash_Draw_74_gml_28_0")));

draw_text(320, 445, string_hash_to_newline(stringsetsubloc("instance_count: ~1", string(instance_count), "obj_debug_xy_slash_Draw_74_gml_29_0_b")));
_str = string_hash_to_newline(stringsetloc("PgUp: XY Camera-Relative", "obj_debug_xy_slash_Draw_74_gml_29_0"));
draw_text(__view_get(2, 0) - string_width(_str), 445, _str);

if (xy_camera_relative >= 1)
{
    draw_set_color(c_yellow);
    
    if (xy_camera_relative == 1)
    {
        _str = string_hash_to_newline(stringsetloc("XY is camera-relative!", "obj_debug_xy_slash_Draw_74_gml_33_0"));
        draw_text(__view_get(2, 0) - string_width(_str), 425, _str);
    }
    
    if (xy_camera_relative == 2)
    {
        _str = string_hash_to_newline(stringsetloc("XY is StartXY relative!", "obj_debug_xy_slash_Draw_74_gml_34_0"));
        draw_text(__view_get(2, 0) - string_width(_str), 425, _str);
    }
}

enum e__VW
{
    XView,
    YView,
    WView,
    HView,
    Angle,
    HBorder,
    VBorder,
    HSpeed,
    VSpeed,
    Object,
    Visible,
    XPort,
    YPort,
    WPort,
    HPort,
    Camera,
    SurfaceID
}
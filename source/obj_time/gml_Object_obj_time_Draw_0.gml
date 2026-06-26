if (scr_debug())
{
    draw_set_font(fnt_main);
    var text_scale = (global.darkzone == 1) ? 1 : 0.5;
    draw_set_color(c_red);
    draw_text(__view_get(0, 0), __view_get(1, 0), fps);
    draw_set_font(fnt_main);
    draw_set_color(c_green);
    draw_text_transformed((__view_get(0, 0) + __view_get(2, 0)) - (string_width(room_get_name(room)) * text_scale), __view_get(1, 0), room_get_name(room), text_scale, text_scale, 0);
    draw_text_transformed((__view_get(0, 0) + __view_get(2, 0)) - (string_width("plot " + string(global.plot)) * text_scale), __view_get(1, 0) + (15 * text_scale), "plot " + string(global.plot), text_scale, text_scale, 0);
    draw_set_color(c_white);
}
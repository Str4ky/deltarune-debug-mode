if (scr_debug())
{
    draw_set_font(fnt_main);
    draw_set_color(c_red);
    draw_text(__view_get(0, 0), __view_get(1, 0), fps);
    draw_set_font(fnt_main);
    draw_set_color(c_green);
    draw_text((__view_get(0, 0) + __view_get(2, 0)) - string_width(room_get_name(room)), __view_get(1, 0), room_get_name(room));
    draw_text((__view_get(0, 0) + __view_get(2, 0)) - string_width("plot " + string(global.plot)), __view_get(1, 0) + 15, "plot " + string(global.plot));
}
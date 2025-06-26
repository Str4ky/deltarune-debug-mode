xx = __view_get(e__VW.XView, 0);
yy = __view_get(e__VW.YView, 0);
d = global.darkzone + 1;

if (keyboard_check_pressed(ord("D")))
{
    global.dmenu_active = !global.dmenu_active;
    
    if (global.dmenu_active)
    {
        snd_play(snd_egg);
        global.interact = 1;
    }
    else
    {
        snd_play(snd_smallswing);
        global.interact = 0;
    }
}

var xcenter, menu_width, ycenter, menu_length;

if (global.dmenu_box == 0)
{
    menu_width = 214;
    menu_length = 94;
    xcenter = 160;
    ycenter = 105;
}

if (global.dmenu_box == 1)
{
    menu_width = 214;
    menu_length = 154;
    xcenter = 160;
    ycenter = 135;
}

if (global.dmenu_box == 2)
{
    menu_width = 256;
    menu_length = 154;
    xcenter = 160;
    ycenter = 135;
}

var x_start = 0;
var x_spacing, y_start, y_spacing;

if (global.dbutton_layout == 0)
{
    var x_padding = 7;
    y_start = 60 * d;
    x_spacing = 10 * d;
    y_spacing = 10 * d;
    x_start = ((xcenter - (menu_width / 2)) + x_padding) * d;
}

if (global.dbutton_layout == 1)
{
    var x_padding = 7;
    y_start = 95 * d;
    x_spacing = 10 * d;
    y_spacing = 20 * d;
    x_start = ((xcenter - (menu_width / 2)) + x_padding) * d;
}

if (global.dbutton_layout == 2)
{
    var x_padding = 7;
    y_start = 95 * d;
    x_start = ((xcenter - (menu_width / 2)) + x_padding) * d;
}

var button_count = array_length(global.dbutton_options);

if (global.dmenu_active)
{
    draw_set_color(c_white);
    draw_rectangle(((xcenter - (menu_width / 2) - 3) * d) + xx, ((ycenter - (menu_length / 2) - 3) * d) + yy, ((xcenter + (menu_width / 2) + 3) * d) + xx, ((ycenter + (menu_length / 2) + 3) * d) + yy, false);
    draw_set_color(c_black);
    draw_rectangle(((xcenter - (menu_width / 2)) * d) + xx, ((ycenter - (menu_length / 2)) * d) + yy, ((xcenter + (menu_width / 2)) * d) + xx, ((ycenter + (menu_length / 2)) * d) + yy, false);
    
    if (global.darkzone == 1)
        draw_set_font(fnt_mainbig);
    else
        draw_set_font(fnt_main);
    
    draw_set_color(c_white);
    draw_text(x_start + xx, (((ycenter - (menu_length / 2)) + 8) * d) + yy, string(global.dmenu_title));
    
    if (global.dbutton_layout == 0)
    {
        for (var i = 0; i < button_count; i++)
        {
            var text_width = string_width(global.dbutton_options[i]);
            draw_set_color((global.dbutton_selected == (i + 1)) ? c_yellow : c_white);
            draw_text(x_start + xx, (100 * d) + yy, global.dbutton_options[i]);
            x_start += (text_width + x_spacing);
        }
    }
    
    if (global.dbutton_layout == 1)
    {
        for (var i = 0; i < global.dbutton_max_visible; i++)
        {
            var button_index = global.dmenu_start_index + i;
            
            if (button_index < array_length(global.dbutton_options))
            {
                var text_color = (global.dbutton_selected == (button_index + 1)) ? c_yellow : c_white;
                draw_set_color(text_color);
                draw_text(x_start + xx, y_start + yy + (i * y_spacing), global.dbutton_options[button_index]);
            }
        }
        
        var dcan_scroll_up = global.dmenu_start_index > 0;
        var dcan_scroll_down = (global.dmenu_start_index + global.dbutton_max_visible) < array_length(global.dbutton_options);
        var dmenu_arrow_yoffset = 2 * sin(global.dmenu_arrow_timer / 10);
        var darrow_scale = d / 2;
        
        if (dcan_scroll_up)
            draw_sprite_ext(spr_morearrow, 0, x_start + xx, y_start + yy + (global.dbutton_max_visible * (y_spacing * -0.03)) + dmenu_arrow_yoffset, darrow_scale, -darrow_scale, 0, c_white, 1);
        
        if (dcan_scroll_down)
            draw_sprite_ext(spr_morearrow, 0, x_start + xx, (y_start + yy + (global.dbutton_max_visible * y_spacing)) - dmenu_arrow_yoffset, darrow_scale, darrow_scale, 0, c_white, 1);
    }
    
    if (global.dbutton_layout == 2)
    {
        draw_set_color(c_yellow);
        draw_text(((xcenter - (string_length(global.dgiver_amount) * 4)) * d) + xx, (ycenter * d) + yy, string(global.dgiver_amount));
        draw_set_color(c_white);
        var itemreminder;
        
        if (global.dgiver_menu_state == "objects")
        {
            itemreminder = "[" + string(global.dgiver_bname) + "]";
            scr_itemcheck(0);
            draw_text(x_start + xx, ((ycenter + 25) * d) + yy, "OBJETs : " + string(12 - itemcount) + "/12");
        }
        
        if (global.dgiver_menu_state == "armors")
        {
            itemreminder = "[" + string(global.dgiver_bname) + "]";
            scr_armorcheck_inventory(0);
            draw_text(x_start + xx, ((ycenter + 25) * d) + yy, "ARMUREs : " + string(48 - itemcount) + "/48");
        }
        
        if (global.dgiver_menu_state == "weapons")
        {
            itemreminder = "[" + string(global.dgiver_bname) + "]";
            scr_weaponcheck_inventory(0);
            draw_text(x_start + xx, ((ycenter + 25) * d) + yy, "ARMEs : " + string(48 - itemcount) + "/48");
        }
        
        if (global.dgiver_menu_state == "keyitems")
        {
            itemreminder = "[" + string(global.dgiver_bname) + "]";
            scr_keyitemcheck(0);
            draw_text(x_start + xx, ((ycenter + 25) * d) + yy, "OBJETs CLÉs : " + string(12 - itemcount) + "/12");
        }
        
        var text_width = string_width(itemreminder);
        draw_text(((xcenter * d) - (text_width / 2)) + xx, ((ycenter - 22) * d) + yy, itemreminder);
        var darrow_scale = d / 2;
        draw_sprite_ext(spr_morearrow, 0, ((xcenter - 15) * d) + xx, ((ycenter + 6) * d) + yy, darrow_scale, darrow_scale, 270, c_white, 1);
        draw_sprite_ext(spr_morearrow, 0, ((xcenter + 15) * d) + xx, ((ycenter + 12) * d) + yy, darrow_scale, darrow_scale, 90, c_white, 1);
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
EnsureDataLoaded();

if (!Data.IsVersionAtLeast(2023, 6))
{
    ScriptError("Erreur 0 : Ce script fonctionne uniquement pour la version payante de Deltarune.");
    return;
}

if (Data?.GeneralInfo?.DisplayName?.Content.ToLower() != "deltarune chapter 2" &&
    Data?.GeneralInfo?.DisplayName?.Content.ToLower() != "deltarune chapitre 2")
{
    ScriptError("Erreur 1 : Ce script s'applique seulement au Chapitre 2.");
    return;
}

bool isFrenchPatch = false;
foreach (var str in Data.Strings)
{
    if (str.Content == "Français")
    {
        isFrenchPatch = true;
        break;
    }
}
string defaultLang = isFrenchPatch ? "fr" : "en";

GlobalDecompileContext globalDecompileContext = new(Data);
Underanalyzer.Decompiler.IDecompileSettings decompilerSettings = new Underanalyzer.Decompiler.DecompileSettings();
UndertaleModLib.Compiler.CodeImportGroup importGroup = new(Data, globalDecompileContext, decompilerSettings)
{
    ThrowOnNoOpFindReplace = false
};

// --- COMMON CODE ---

// Script scr_debug_print
UndertaleScript scr_debug_print = Data.Scripts.ByName("scr_debug_print");
importGroup.QueueReplace(scr_debug_print.Code, @"
function scr_debug_print(arg0)
{
    if (!instance_exists(obj_debug_gui))
    {
        instance_create(__view_get(e__VW.XView, 0) + 10, __view_get(e__VW.YView, 0) + 10, obj_debug_gui);
        obj_debug_gui.depth = -99999;
    }
    
    obj_debug_gui.newtext = arg0;
    
    with (obj_debug_gui)
    {
        message[messagecount] = newtext;
        newtext = """";
        timer[messagecount] = 90 - totaltimer;
        totaltimer += timer[messagecount];
        messagecount++;
        debugmessage = message[0];
        
        for (i = 1; i < messagecount; i++)
            debugmessage += (""#"" + message[i]);
    }
}

function print_message(arg0)
{
}

function debug_print(arg0)
{
}

function scr_debug_clear_all()
{
    scr_debug_clear_persistent();
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
");
ChangeSelection(scr_debug_print);

// GameObject obj_debug_gui
UndertaleGameObject obj_debug_gui = Data.GameObjects.ByName("obj_debug_gui");
importGroup.QueueReplace(obj_debug_gui.EventHandlerFor(EventType.Create, (uint)0, Data), @"
message[0] = """";
debugmessage = """";
timer[0] = 90;
newtext = """";
messagecount = 0;
totaltimer = 0;
");
importGroup.QueueReplace(obj_debug_gui.EventHandlerFor(EventType.Step, (uint)0, Data), @"
if (timer[0] > 0)
{
    timer[0]--;
    totaltimer--;
}

if (timer[0] <= 0)
{
    messagecount--;
    
    if (messagecount <= 0)
    {
        instance_destroy();
    }
    else
    {
        for (i = 0; i < messagecount; i++)
        {
            message[i] = message[i + 1];
            timer[i] = timer[i + 1];
        }
        
        message[messagecount] = """";
        timer[messagecount] = 0;
        debugmessage = message[0];
        
        for (i = 1; i < messagecount; i++)
            debugmessage += (""#"" + message[i]);
    }
}
");
importGroup.QueueReplace(obj_debug_gui.EventHandlerFor(EventType.Draw, (uint)64, Data), @"
var fnt = draw_get_font();
draw_set_font(fnt_comicsans);
var col = draw_get_color();
draw_set_color(c_black);
draw_text_transformed(7, 7, string_hash_to_newline(debugmessage), 1, 1, 0);
draw_text_transformed(9, 7, string_hash_to_newline(debugmessage), 1, 1, 0);
draw_text_transformed(9, 9, string_hash_to_newline(debugmessage), 1, 1, 0);
draw_text_transformed(7, 9, string_hash_to_newline(debugmessage), 1, 1, 0);
draw_set_color(c_red);
draw_text_transformed(8, 8, string_hash_to_newline(debugmessage), 1, 1, 0);
draw_set_color(col);
draw_set_font(fnt);
");
importGroup.QueueReplace(obj_debug_gui.EventHandlerFor(EventType.CleanUp, (uint)0, Data), @"
event_inherited();
");
ChangeSelection(obj_debug_gui);

// GameObject obj_debug_xy
UndertaleGameObject obj_debug_xy = Data.GameObjects.ByName("obj_debug_xy");
importGroup.QueueReplace(obj_debug_xy.EventHandlerFor(EventType.Create, (uint)0, Data), @"
if (instance_number(object_index) > 1)
    instance_destroy();

selected_object = -4;
mouse_held = 0;
mouse_held_r = 0;
siner = 0;
show_invisible = 0;
show_all_object_xy = 0;
xy_camera_relative = 0;
actor_debug_x = 0;
actor_debug_y = 0;
actor_debug_xstart = 0;
actor_debug_ystart = 0;
gridsize = 20;

if (global.darkzone == 0)
    gridsize = 10;

main_focus = 1;
mousebuffer = 3;
copybuffer = 0;
totalstring = "" "";
actor_selected_before = 0;
actor_previously_selected = -1;
panremx = camerax();
panremy = cameray();
enable_mouse_wheel = 0;
old_right_click = 0;
");
importGroup.QueueReplace(obj_debug_xy.EventHandlerFor(EventType.Step, (uint)0, Data), @"


");
importGroup.QueueReplace(obj_debug_xy.EventHandlerFor(EventType.Draw, (uint)0, Data), @"
x = round(mouse_x);
y = round(mouse_y);
_old_x = x;
_old_y = y;
siner++;
mousebuffer--;
main_focus = 1;

if (i_ex(obj_debug_windows))
    main_focus = 0;

if (main_focus)
{
    if (mouse_check_button_pressed(mb_left) && mousebuffer < 0)
    {
        mouse_held = 0;
        obj_check = collision_rectangle(x - 2, y - 2, x + 2, y + 2, all, 0, 1);
        
        if (obj_check != -4)
        {
            visiblecheck = 0;
            
            if (show_invisible == 1)
                visiblecheck = 1;
            else if (obj_check.visible == true)
                visiblecheck = 1;
            
            if (visiblecheck == 1 && obj_check.image_alpha > 0)
            {
                selected_object = obj_check;
                selected_object.is_debug_copy = true;
            }
        }
        else
        {
            selected_object = -898;
        }
        
        if (instance_exists(obj_actor))
        {
            obj_check = collision_rectangle(x - 2, y - 2, x + 2, y + 2, obj_actor, 0, 1);
            
            if (obj_check != -4)
            {
                visiblecheck = 0;
                
                if (show_invisible == 1)
                    visiblecheck = 1;
                else if (obj_check.visible == true)
                    visiblecheck = 1;
                
                if (visiblecheck == 1 && obj_check.image_alpha > 0)
                {
                    selected_object = obj_check;
                    selected_object.is_debug_copy = true;
                }
            }
        }
    }
}

if (main_focus)
{
    if (mouse_check_button(mb_left) && i_ex(selected_object))
    {
        mouse_held++;
        mouse_held_minimum = 5;
        
        if (selected_object.object_index == obj_actor)
            mouse_held_minimum = 15;
        
        if (mouse_held >= mouse_held_minimum && i_ex(selected_object))
        {
            selected_object.x = x - (selected_object.sprite_width / 2);
            selected_object.y = y - (selected_object.sprite_height / 2);
            
            if (selected_object.x != _old_x || selected_object.y != _old_y)
            {
                with (selected_object)
                {
                    if (variable_instance_exists(id, ""is_debug_copy""))
                        scr_depth();
                }
            }
        }
    }
    else if (mouse_check_button_pressed(mb_left))
    {
        mouse_held = 0;
    }
}

if (keyboard_check_pressed(vk_pagedown))
{
    if (show_all_object_xy == 1)
        show_all_object_xy = 0;
    else
        show_all_object_xy = 1;
    
    mouse_held = 0;
}

if (i_ex(selected_object) && !keyboard_check(ord(""P"")))
{
    if (selected_object.sprite_index != -1)
    {
        os = selected_object;
        var _moved = false;
        
        if (keyboard_check(vk_up))
        {
            os.y -= 1;
            _moved = true;
        }
        
        if (keyboard_check(vk_left))
        {
            os.x -= 1;
            _moved = true;
        }
        
        if (keyboard_check(vk_down))
        {
            os.y += 1;
            _moved = true;
        }
        
        if (keyboard_check(vk_right))
        {
            os.x += 1;
            _moved = true;
        }
        
        if (_moved)
        {
            os.is_debug_copy = true;
            
            with (os.object_index)
            {
                if (variable_instance_exists(id, ""is_debug_copy""))
                    scr_depth();
            }
        }
        
        draw_sprite_ext_flash(os.sprite_index, os.image_index, os.x, os.y, os.image_xscale, os.image_yscale, os.image_angle, os.image_blend, (sin(siner / 8) * 0.5) + 0.5);
    }
}

if (keyboard_check_pressed(vk_pageup))
{
    xy_camera_relative++;
    
    if (xy_camera_relative >= 3)
        xy_camera_relative = 0;
}

if (keyboard_check_pressed(vk_delete))
{
    if (i_ex(selected_object))
    {
        with (selected_object)
            instance_destroy();
        
        selected_object = -999;
    }
}

if (keyboard_check_pressed(ord(""V"")))
{
    if (show_invisible == 0)
        show_invisible = 1;
    else
        show_invisible = 0;
}

if (siner >= 5 && mouse_check_button_pressed(mb_middle))
{
    instance_destroy();
    
    with (obj_debug_windows)
        instance_destroy();
}

if (show_all_object_xy == 1)
{
    for (i = 0; i < instance_count; i++)
    {
        findo = instance_id_get(i);
        
        if (i_ex(findo))
        {
            visiblecheck = 0;
            
            if (show_invisible == 1)
                visiblecheck = 1;
            else if (findo.visible == true)
                visiblecheck = 1;
            
            if (visiblecheck == 1 && findo.sprite_index != -1)
            {
                fox = findo.x;
                foy = findo.y;
                
                if (xy_camera_relative == 1)
                {
                    fox -= __view_get(e__VW.XView, 0);
                    foy -= __view_get(e__VW.YView, 0);
                }
                
                if (xy_camera_relative == 2)
                {
                    fox -= findo.xstart;
                    foy -= findo.ystart;
                }
                
                draw_info = 1;
                
                if (findo.object_index == object_index)
                    draw_info = 0;
                
                if (findo.object_index == obj_overworldheart)
                    draw_info = 0;
                
                if (findo.object_index == obj_grazebox)
                    draw_info = 0;
                
                if (draw_info == 1)
                {
                    draw_set_color(c_black);
                    draw_rectangle(findo.x - 4, findo.y - 32, findo.x + 80, findo.y, false);
                    draw_set_font(fnt_main);
                    draw_set_color(c_aqua);
                    draw_text(findo.x, findo.y - 32, string_hash_to_newline(object_get_name(findo.object_index)));
                    draw_text(findo.x, findo.y - 16, string_hash_to_newline(string(fox) + "" , "" + string(foy)));
                    draw_set_color(c_red);
                    draw_rectangle(findo.bbox_left, findo.bbox_top, findo.bbox_right, findo.bbox_bottom, true);
                }
            }
        }
    }
}

if (show_all_object_xy == 1)
{
    fox = x;
    foy = y;
    
    if (xy_camera_relative == 1)
    {
        fox -= __view_get(e__VW.XView, 0);
        foy -= __view_get(e__VW.YView, 0);
    }
    
    if (xy_camera_relative == 2)
    {
        fox -= xstart;
        foy -= ystart;
    }
    
    draw_set_color(c_black);
    draw_rectangle(x - 4, y - 24, x + 60, y, false);
    draw_set_color(c_fuchsia);
    draw_text(x, y - 20, string_hash_to_newline(string(fox) + "" , "" + string(foy)));
}

draw_set_color(c_black);
draw_line_width(x + 16, y + 16, x + 2, y + 2, 5);
draw_set_color(c_white);
draw_line_width(x + 12, y + 12, x + 3, y + 3, 4);
draw_set_color(make_color_hsv(siner * 6, 255, 255));
draw_line_width(x + 7, y + 7, x + 3, y + 3, 3);
old_right_click = 0;

if (!old_right_click)
{
    if (mouse_check_button_pressed(mb_right))
    {
        main_focus = 0;
        
        if (!i_ex(obj_debug_windows))
        {
            instance_create(0, 0, obj_debug_windows);
            
            if (!i_ex(selected_object))
                obj_debug_windows.type = 1;
            
            with (obj_debug_windows)
                event_user(15);
        }
        else
        {
            with (obj_debug_windows)
                instance_destroy();
        }
    }
}

if (i_ex(selected_object))
{
    so = selected_object;
    
    if (object_get_parent(so.object_index) == 390)
    {
        if (enable_mouse_wheel)
        {
            if (mouse_wheel_up() || mouse_wheel_down())
            {
                with (so)
                {
                    if (state == 0)
                    {
                        state = 3;
                        shakex = 2;
                        hurttimer = 10;
                    }
                    else
                    {
                        state = 0;
                    }
                }
            }
        }
    }
    
    if (so.object_index == obj_actor)
    {
        if (mouse_wheel_up() && enable_mouse_wheel)
        {
            with (so)
            {
                if (specialspriteno < 9)
                {
                    specialspriteno++;
                    sprite_index = specialsprite[specialspriteno];
                }
                else
                {
                    specialspriteno = 0;
                    sprite_index = dsprite;
                }
            }
        }
        
        if (mouse_wheel_down() && enable_mouse_wheel)
        {
            with (so)
            {
                if (specialspriteno > 0)
                {
                    specialspriteno--;
                    sprite_index = specialsprite[specialspriteno];
                }
                else
                {
                    specialspriteno = 9;
                    sprite_index = specialsprite[specialspriteno];
                }
            }
        }
        
        if (button2_h())
            cardinal_grid_align = 1;
        else
            cardinal_grid_align = 0;
        
        if (old_right_click)
        {
            if (mouse_check_button_pressed(mb_right) && mousebuffer < 0)
            {
                cardinal_grid_align = 0;
                thiscardinal = ""d"";
                actor_debug_xstart = so.x;
                actor_debug_ystart = so.y;
            }
        }
        
        if (old_right_click)
        {
            if (mouse_check_button(mb_right) && mousebuffer < 0)
            {
                dir_from_actor = point_direction(so.x, so.y, x, y);
                thiscardinal = scr_get_cardinal_direction(dir_from_actor);
                
                if (cardinal_grid_align == 1)
                {
                    if (thiscardinal == ""d"" || thiscardinal == ""u"")
                        x = so.x;
                    
                    if (thiscardinal == ""r"" || thiscardinal == ""l"")
                        y = so.y;
                }
                
                actor_debug_x = x;
                actor_debug_y = y;
                scr_actor_facing(so, thiscardinal);
                draw_set_color(c_red);
                
                if (cardinal_grid_align == 1)
                    draw_set_color(c_aqua);
                
                draw_arrow(so.x, so.y, x, y, 8);
                draw_sprite_ext(so.sprite_index, so.image_index, x, y, so.image_xscale, so.image_yscale, so.image_angle, so.image_blend, 0.5 + (sin(siner / 4) * 0.1));
            }
        }
        
        if (old_right_click)
        {
            if (mouse_check_button_released(mb_right) && mousebuffer < 0)
            {
                dir_from_actor = point_direction(so.x, so.y, x, y);
                thiscardinal = scr_get_cardinal_direction(dir_from_actor);
                
                if (cardinal_grid_align == 1)
                {
                    if (thiscardinal == ""d"" || thiscardinal == ""u"")
                        x = so.x;
                    
                    if (thiscardinal == ""r"" || thiscardinal == ""l"")
                        y = so.y;
                }
                
                if (actor_previously_selected != so.number)
                    totalstring += (""c_sel("" + string(so.number) + "") //select "" + so.name + "" \\n"");
                
                actor_previously_selected = so.number;
                _speed = 4;
                _time = 40;
                _realdist = point_distance(so.x, so.y, x, y);
                _realtime = _realdist / _speed;
                
                if (cardinal_grid_align == 1)
                    totalstring += (""c_walk_wait(\"""" + thiscardinal + ""\"""" + "","" + string(_speed) + "","" + string(_realtime) + "") //move "" + so.name + "" "" + string(_realdist) + "" pixels\\n"");
                else
                    totalstring += (""c_walkdirect_wait("" + string(x) + "","" + string(y) + "","" + string(ceil(_realtime)) + "")//move"" + so.name + "" \\n"");
                
                clipboard_set_text(totalstring);
                scr_actor_facing(so, thiscardinal);
                so.x = x;
                so.y = y;
                copymessage = ""Copied to clipboard"";
                copybuffer = 15;
                mousebuffer = 3;
            }
        }
        
        if (keyboard_check_pressed(ord(""W"")))
        {
            totalstring += ""c_wait(30)\\n"";
            copymessage = ""Wait command copied"";
            copybuffer = 15;
        }
        
        if (keyboard_check_pressed(vk_f2))
        {
            totalstring += ""c_msgset(0,\""* Text/%\"")\\nc_talk_wait()\\n"";
            copymessage = ""Dialogue command copied"";
            copybuffer = 15;
        }
        
        if (keyboard_check_pressed(ord(""Q"")))
        {
            if (actor_previously_selected != so.number)
                totalstring += (""c_sel("" + string(so.number) + "") //select "" + so.name + "" \\n"");
            
            actor_previously_selected = so.number;
            totalstring += (""c_specialsprite("" + string(so.specialspriteno) + "")\\n"");
            copymessage = ""Sprite change copied"";
            copybuffer = 15;
        }
        
        if (keyboard_check_pressed(ord(""P"")))
        {
            panremx = camerax();
            panremy = cameray();
            
            if (instance_exists(obj_mainchara))
            {
                if (obj_mainchara.cutscene == 0)
                {
                    obj_mainchara.cutscene = 1;
                    totalstring += ""c_pannable(1)\\n"";
                    copymessage = ""Panning enabled!"";
                    copybuffer = 5;
                }
            }
        }
        
        if (keyboard_check(ord(""P"")))
        {
            cameraxadd = 0;
            camerayadd = 0;
            cameraspeed = 2;
            
            if (global.darkzone == 1)
                cameraspeed = 4;
            
            if (right_h())
                cameraxadd = cameraspeed;
            
            if (left_h())
                cameraxadd = -cameraspeed;
            
            if (up_h())
                camerayadd = -cameraspeed;
            
            if (down_h())
                camerayadd = cameraspeed;
            
            camerax_set(camerax() + cameraxadd);
            cameray_set(cameray() + camerayadd);
        }
        
        if (keyboard_check_released(ord(""P"")))
        {
            if (camerax() != panremx || cameray() != panremy)
            {
                pandiffx = camerax() - panremx;
                pandiffy = cameray() - panremy;
                totalstring += (""c_panspeed_wait("" + string(pandiffx / 40) + "","" + string(pandiffy / 40) + "",40) //pan amount: "" + string(pandiffx) + "","" + string(pandiffy));
                totalstring += ("" // panned to: "" + string(camerax()) + "","" + string(cameray()) + ""  \\n"");
                copymessage = ""Pan copied!"";
                copybuffer = 15;
                panremx = camerax();
                panremy = cameray();
            }
        }
        
        if (copybuffer > 0)
        {
            if (copybuffer == 15)
                clipboard_set_text(totalstring);
            
            draw_set_color(c_yellow);
            copybuffer -= 1;
            draw_text(x, y, string_hash_to_newline(copymessage));
        }
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
");
importGroup.QueueReplace(obj_debug_xy.EventHandlerFor(EventType.Draw, (uint)74, Data), @"
_selected_string = ""No object!#MouseL:Choose&Drag#MouseR:Drag From Anchor"";

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
    _selected_string += ("" X: "" + string(sox) + "" Y: "" + string(soy));
    _selected_string += (""#Depth: "" + string(selected_object.depth));
    _selected_string += ""#Arrows: Move Precisely"";
}

draw_set_font(fnt_main);
draw_set_color(c_white);
scr_84_draw_text_outline(0, 430, string_hash_to_newline(_selected_string));
draw_set_font(fnt_main);
_str = string_hash_to_newline(stringsetloc(""PgDown: Show All Info"", ""obj_debug_xy_slash_Draw_74_gml_26_0""));
draw_text(__view_get(2, 0) - string_width(_str), 460, _str);
draw_text(320, 460, string_hash_to_newline(stringsetsubloc(""CameraX: ~1 CameraY: ~2"", string(__view_get(e__VW.XView, 0)), string(__view_get(e__VW.YView, 0)), ""obj_debug_xy_slash_Draw_74_gml_27_0"")));

if (show_invisible == 1)
    draw_text(320, 430, string_hash_to_newline(stringsetloc(""Show Invisible"", ""obj_debug_xy_slash_Draw_74_gml_28_0"")));

draw_text(320, 445, string_hash_to_newline(stringsetsubloc(""instance_count: ~1"", string(instance_count), ""obj_debug_xy_slash_Draw_74_gml_29_0_b"")));
_str = string_hash_to_newline(stringsetloc(""PgUp: XY Camera-Relative"", ""obj_debug_xy_slash_Draw_74_gml_29_0""));
draw_text(__view_get(2, 0) - string_width(_str), 445, _str);

if (xy_camera_relative >= 1)
{
    draw_set_color(c_yellow);
    
    if (xy_camera_relative == 1)
    {
        _str = string_hash_to_newline(stringsetloc(""XY is camera-relative!"", ""obj_debug_xy_slash_Draw_74_gml_33_0""));
        draw_text(__view_get(2, 0) - string_width(_str), 425, _str);
    }
    
    if (xy_camera_relative == 2)
    {
        _str = string_hash_to_newline(stringsetloc(""XY is StartXY relative!"", ""obj_debug_xy_slash_Draw_74_gml_34_0""));
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
");
ChangeSelection(obj_debug_xy);

// GameObject obj_debug_windows
UndertaleGameObject obj_debug_windows = Data.GameObjects.ByName("obj_debug_windows");
importGroup.QueueReplace(obj_debug_windows.EventHandlerFor(EventType.Create, (uint)0, Data), @"
xx = mouse_x - camerax() - 40;
yy = mouse_y - cameray() - 20;
xx = clamp(xx, -30, 500);
yy = clamp(yy, -5, 340);
type = 0;
button_text[0] = ""Drag Window!"";
event_user(15);
watchvar = "" "";
watchflag = -1;

for (i = 0; i < button_amount; i++)
{
    button_state[i] = 0;
    button_clicked[i] = 0;
}

remmx = mouse_x - camerax();
remmy = mouse_y - cameray();
");
importGroup.QueueReplace(obj_debug_windows.EventHandlerFor(EventType.Draw, (uint)64, Data), @"
bspace = 30;
padding = 5;
wd = 160;
ht = 40 + (bspace * button_amount);
mx = mouse_x - camerax();
my = mouse_y - cameray();
draw_set_color(c_black);
draw_rectangle(xx - 4, yy - 4, xx + wd + 4, yy + ht + 4, false);
draw_set_color(c_ltgray);
draw_rectangle(xx, yy, xx + wd, yy + ht, false);

for (i = 0; i < button_amount; i++)
{
    button_state[i] = 0;
    
    if (point_in_rectangle(mx, my, xx + 10, yy + (bspace * i) + padding, (xx + wd) - 10, yy + ((bspace + 1) * i) + bspace))
    {
        if (i > 0)
        {
            button_state[i] = 1;
            
            if (mouse_check_button(mb_left))
                button_state[i] = 2;
            
            if (mouse_check_button_released(mb_left))
            {
                button_state[i] = 3;
                button_clicked[i] = 1;
            }
        }
        else
        {
            button_state[i] = 1;
            
            if (mouse_check_button(mb_left))
            {
                button_clicked[i] = 1;
                button_state[i] = 3;
            }
        }
    }
}

draw_set_font(fnt_main);

for (i = 0; i < button_amount; i++)
{
    if (button_state[i] == 0)
        draw_set_color(c_ltgray);
    
    if (button_state[i] == 1)
        draw_set_color(c_white);
    
    if (button_state[i] == 2)
        draw_set_color(c_dkgray);
    
    if (button_state[i] == 3)
        draw_set_color(c_blue);
    
    if (i == 0)
        draw_set_color(merge_color(draw_get_color(), c_aqua, 0.7));
    
    draw_rectangle(xx + 10, yy + (bspace * i) + padding, (xx + wd) - 10, (yy + 30 + ((bspace + 1) * i)) - padding, false);
    draw_set_color(c_black);
    draw_rectangle(xx + 10, yy + (bspace * i) + padding, (xx + wd) - 10, (yy + 30 + ((bspace + 1) * i)) - padding, true);
    draw_set_color(c_red);
    draw_text(xx + 10, yy + (bspace * i) + padding, button_text[i]);
    draw_set_color(c_black);
}

if (button_clicked[0] == 1)
{
    if (mouse_check_button(mb_left))
    {
        xx += (mx - remmx);
        yy += (my - remmy);
    }
    else
    {
        button_clicked[0] = 0;
    }
}

if (type == 0)
{
    if (button_clicked[1] == 1)
    {
        if (i_ex(obj_debug_xy))
        {
            var _target = obj_debug_xy.selected_object;
            
            if (i_ex(_target))
            {
                var _copy = instance_create(_target.x + 20, _target.y + 20, _target.object_index);
                
                with (_copy)
                {
                    sprite_index = _target.sprite_index;
                    image_index = _target.image_index;
                    image_speed = _target.image_speed;
                    image_xscale = _target.image_xscale;
                    image_yscale = _target.image_yscale;
                    image_angle = _target.image_angle;
                    image_blend = _target.image_blend;
                    is_debug_copy = true;
                }
                
                obj_debug_xy.selected_object = _copy;
            }
        }
        
        button_clicked[1] = 0;
    }
    
    if (button_clicked[2] == 1)
    {
        if (i_ex(obj_debug_xy))
        {
            if (i_ex(obj_debug_xy.selected_object))
            {
                with (obj_debug_xy.selected_object)
                    instance_destroy();
                
                instance_destroy();
            }
        }
        
        button_clicked[2] = 0;
    }
    
    if (button_clicked[3] == 1)
    {
        if (i_ex(obj_debug_xy))
        {
            if (i_ex(obj_debug_xy.selected_object))
            {
                checksprite = asset_get_index(get_string(""Enter new sprite_index."", """"));
                
                if (checksprite != -1)
                    obj_debug_xy.selected_object.sprite_index = checksprite;
            }
        }
        
        button_clicked[3] = 0;
    }
    
    if (button_clicked[4] == 1)
    {
        if (i_ex(obj_debug_xy))
        {
            if (i_ex(obj_debug_xy.selected_object))
            {
                var so = obj_debug_xy.selected_object;
                var varname = get_string(""Enter variable name. No quotation marks. No arrays."", """");
                
                if (variable_instance_exists(so, varname))
                    watchvar = varname;
                else
                    show_message(""No variable exists. Zannen."");
            }
        }
        
        button_clicked[4] = 0;
    }
    
    if (button_clicked[5] == 1)
    {
        if (i_ex(obj_debug_xy))
        {
            if (i_ex(obj_debug_xy.selected_object))
            {
                var so = obj_debug_xy.selected_object;
                var varname = get_string(""Enter variable name. No quotation marks. No arrays."", """");
                
                if (variable_instance_exists(so, varname))
                {
                    var foundvar = variable_instance_get(so, varname);
                    foundvar = string(foundvar);
                    var newvalue = get_string(varname + "" is "" + foundvar + "". Enter new REAL NUMBER value."", """");
                    variable_instance_set(so, varname, real(newvalue));
                }
                else
                {
                    show_message(""No variable exists. Zannen."");
                }
            }
        }
        
        button_clicked[5] = 0;
    }
    
    if (button_clicked[6] == 1)
    {
        if (i_ex(obj_debug_xy))
        {
            if (i_ex(obj_debug_xy.selected_object))
            {
                var so = obj_debug_xy.selected_object;
                var varname = get_string(""Enter variable name. No quotation marks. No arrays."", """");
                
                if (variable_instance_exists(so, varname))
                {
                    var foundvar = string(variable_instance_get(so, varname));
                    var newvalue = get_string(varname + "" is "" + foundvar + "". Enter string value."", """");
                    variable_instance_set(so, varname, string(newvalue));
                }
                else
                {
                    show_message(""No variable exists. Zannen."");
                }
            }
        }
        
        button_clicked[6] = 0;
    }
    
    if (watchvar != "" "")
    {
        button_text[4] = ""Watch Variable"";
        
        if (i_ex(obj_debug_xy))
        {
            if (i_ex(obj_debug_xy.selected_object))
            {
                var so = obj_debug_xy.selected_object;
                
                if (variable_instance_exists(so, watchvar))
                    button_text[4] = watchvar + "": "" + string(variable_instance_get(so, watchvar));
            }
        }
    }
}
else if (type == 1)
{
    if (button_clicked[1] == 1)
    {
        var varname = get_string(""object name?"", """");
        
        if (varname != """")
        {
            if (i_ex(asset_get_index(varname)))
            {
                with (obj_debug_xy)
                    selected_object = instance_find(asset_get_index(varname), 0);
                
                instance_destroy();
            }
        }
        
        button_clicked[1] = 0;
    }
    
    if (button_clicked[2] == 1)
    {
        var varname = get_string(""object name?"", """");
        
        if (varname != """")
        {
            if (asset_get_index(varname) > 0)
            {
                var bepis = instance_create(mouse_x, mouse_y, asset_get_index(varname));
                
                with (obj_debug_xy)
                    selected_object = bepis;
                
                instance_destroy();
            }
        }
        
        button_clicked[2] = 0;
    }
    
    if (button_clicked[3] == 1)
    {
        var whatflag = -1;
        whatflag = get_string(""Flag to watch? "", """");
        
        if (whatflag != """")
        {
            whatflag = real(string_digits(whatflag));
            
            if (whatflag > 0)
                watchflag = whatflag;
            
            button_clicked[3] = 0;
        }
    }
    
    if (watchflag > 0)
        button_text[3] = ""Flag ["" + string(watchflag) + ""] : "" + string(global.flag[watchflag]);
    
    if (button_clicked[4] == 1)
    {
        var whatflag = -1;
        whatflag = get_string(""Which flag? "", """");
        
        if (whatflag != """")
        {
            whatflag = real(string_digits(whatflag));
            
            if (whatflag > 0)
            {
                var flagvalue = global.flag[whatflag];
                flagvalue = get_string(""Flag ["" + string(whatflag) + ""] is "" + string(flagvalue) + "". Enter new value."", """");
                
                if (flagvalue != """")
                    global.flag[whatflag] = real(string_digits(flagvalue));
            }
        }
        
        button_clicked[4] = 0;
    }
    
    if (button_clicked[5] == 1)
    {
        var varname = get_string(""Enter variable name without \""global.\"""", """");
        
        if (varname != """")
        {
            if (variable_global_exists(varname))
            {
                var varval = variable_global_get(varname);
                var newval = get_string(""The value of "" + varname + "" is "" + string(varval) + "". What to set it to?"", """");
                
                if (newval != """")
                {
                    if (real(string_digits(newval)) > 0)
                        variable_global_set(varname, real(newval));
                }
            }
            else
            {
                show_message(""No variable exists. Zannen."");
            }
        }
        
        button_clicked[5] = 0;
    }
}

xx = clamp(xx, -30, 500);
yy = clamp(yy, -5, 340);
remmx = mouse_x - camerax();
remmy = mouse_y - cameray();
draw_sprite(spr_maus_cursor, 0, mx, my);
");
importGroup.QueueReplace(obj_debug_windows.EventHandlerFor(EventType.Other, (uint)25, Data), @"
if (type == 0)
{
    button_amount = 7;
    button_text[1] = ""DUPLICATE OBJECT"";
    button_text[2] = ""DELETE OBJECT"";
    button_text[3] = ""Change Sprite Index"";
    button_text[4] = ""Watch Variable"";
    button_text[5] = ""Change Variable (num)"";
    button_text[6] = ""Change Variable (str)"";
}

if (type == 1)
{
    button_amount = 6;
    button_text[1] = ""Select Object"";
    button_text[2] = ""Instance Create"";
    button_text[3] = ""Watch Flag"";
    button_text[4] = ""Set Flag"";
    button_text[5] = ""Set/Check Global Var"";
}
");
ChangeSelection(obj_debug_windows);

// Script scr_dmode_init_lang
UndertaleScript scr_dmode_init_lang = new UndertaleScript();
scr_dmode_init_lang.Name = Data.Strings.MakeString("scr_dmode_init_lang");
scr_dmode_init_lang.Code = new UndertaleCode();
scr_dmode_init_lang.Code.Name = Data.Strings.MakeString("gml_GlobalScript_scr_dmode_init_lang");
scr_dmode_init_lang.Code.LocalsCount = 1;
Data.Scripts.Add(scr_dmode_init_lang);
Data.Code.Add(scr_dmode_init_lang.Code);
importGroup.QueueReplace(scr_dmode_init_lang.Code, @"
global.dmode_lang = ""___DEFAULT_LANG___"";

global.dmode_text = 
{
    en: 
    {
        dmode_activated: ""Debug Mode activated!"",
        dmode_desactivated: ""Debug Mode deactivated!"",
        fps_1: ""FPS to 1"",
        fps_30: ""FPS to 30"",
        fps_60: ""FPS to 60"",
        fps_120: ""FPS to 120"",
        tp_0: ""TP to 0%"",
        tp_250: ""TP to 250%"",
        fullheal: ""Party HP fully restored"",
        fightwin: ""Fight skipped"",
        turnskip: ""Enemy's turn skipped"",
        
        // Menus
        debug_menu: ""Debug Menu"",
        warps: ""Warps"",
        items: ""Items"",
        recruits: ""Recruits"",
        misc: ""Misc"",

        // Light Objects
        hot_chocolate: ""Hot Chocolate"",
        pencil: ""Pencil"",
        bandage: ""Bandage"",
        bouquet: ""Bouquet"",
        ball_junk: ""Ball of Junk"",
        halloween_pencil: ""Halloween Pencil"",
        lucky_pencil: ""Lucky Pencil"",
        egg: ""Egg"",
        cards: ""Cards"",
        heart_candy: ""Box of Heart Candy"",
        glass: ""Glass"",
        eraser: ""Eraser"",
        mech_pencil: ""Mech. Pencil"",
        wristwatch: ""Wristwatch"",
        holiday_pencil: ""Holiday Pencil"",
        cactus_needle: ""CactusNeedle"",
        black_shard: ""BlackShard"",
        quill_pen: ""QuillPen"",

        // Categories
        cat_vessel: ""Vessel Sequence"",
        cat_superboss: ""Superbosses"",
        cat_weird: ""Weird Route"",
        cat_seam: ""Seam"",
        cat_eggs: ""Eggs"",
        cat_onion: ""Onion San"",
        cat_misc1: ""Misc Chap 1"",
        cat_misc2: ""Misc Chap 2"",
        cat_tenna: ""Legend of Tenna"",
        cat_sword: ""Sword Route"",
        cat_misc3: ""Misc Chap 3"",
        cat_misc4: ""Misc Chap 4"",
        cat_moss: ""Moss"",
        cat_thrash: ""Thrash Machine"",

        // Goner Maker
        g_food: ""FAVORITE FOOD"",
        g_blood: ""BLOOD TYPE"",
        g_color: ""FAVORITE COLOR"",
        g_gift: ""GIFT"",
        g_feeling: ""OPINION"",
        g_honest: ""ANSWERED HONESTLY"",
        g_crises: ""CONSENT TO CRISES"",
        opt_sweet: ""SWEET"", opt_soft: ""SOFT"", opt_bitter: ""BITTER"", opt_salty: ""SALTY"", opt_pain: ""PAIN"", opt_cold: ""COLD"",
        opt_red: ""RED"", opt_blue: ""BLUE"", opt_green: ""GREEN"", opt_cyan: ""CYAN"",
        opt_kindness: ""KINDNESS"", opt_mind: ""MIND"", opt_ambition: ""AMBITION"", opt_bravery: ""BRAVERY"", opt_voice: ""VOICE"",
        opt_love: ""LOVE"", opt_hope: ""HOPE"", opt_disgust: ""DISGUST"", opt_fear: ""FEAR"",
        g_yes: ""YES"", g_no: ""NO"",

        // General
        opt_no: ""No"", opt_yes: ""Yes"", opt_seen: ""Seen"", opt_notseen: ""Not seen"",
        
        // Thrash Machine
        thrash_head: ""Thrash Head"", thrash_body: ""Thrash Body"", thrash_legs: ""Thrash Legs"",
        opt_laser: ""Laser"", opt_sword: ""Sword"", opt_flame: ""Flame"", opt_duck: ""Duck"",
        opt_simple: ""Simple"", opt_wheel: ""Wheel"", opt_tank: ""Tank"",
        opt_sneakers: ""Sneakers"", opt_tires: ""Tires"", opt_tracks: ""Tracks"",

        // Misc Chap 1
        label_gang: ""Gang Name"", 
        gang_guys: ""The Guys (unused)"", gang_squad: ""The $!$! Squad"", gang_fanclub: ""Lancer Fan Club"", gang_fungang: ""The Fun Gang"",
        label_prophecy: ""Prophecy heard"", label_manual: ""Manual thrown"",
        opt_tried: ""Tried"", opt_thrown: ""Thrown"",
        label_cake: ""Cake returned"", label_donation: ""Donation Goal"", opt_reached: ""Reached"",
        label_starwalker: ""Starwalker"", label_asgore_flowers: ""Asgore's Flowers"", opt_given: ""Given"",
        label_noelle_out: ""Noelle outside"", opt_talked_susie: ""Talked about Susie"",
        label_sink: ""Sink inspected (ch 1)"", label_egg1: ""Egg obtained (ch 1)"",
        label_jevil: ""Jevil defeated"", opt_violence: ""Via violence"", opt_mercy: ""Via mercy"",
        
        // Onion San
        label_onion_rel: ""Relation (ch 1)"", opt_friends: ""Friends"", opt_notfriends: ""Not friends"",
        label_kris_name: ""Kris's Name"", opt_hippo: ""Hippo"",
        label_onion_name: ""Onion's Name"", opt_onyx: ""Onyx"", opt_beauty: ""Beauty"", opt_asriel2: ""Asriel II"", opt_stinky: ""Stinky"",
        
        // Moss
        label_moss1: ""Moss eaten (ch 1)"",

        // Misc Chap 2
        label_plush: ""Plushie"", opt_notgiven: ""Not given"",
        label_hacker: ""Hacker recruited"",
        label_berdly_arm: ""Berdly's Arm"", opt_burnt: ""Burnt"", opt_ok: ""Ok"",
        label_mt_fan: ""Mettaton 'Fan'"",
        label_susie_statue: ""Susie Statue collected"",
        label_icee_statue: ""ICE-E collected"",
        label_sink2: ""Sink inspected (ch 2)"",
        label_shelter: ""Shelter scene seen"",
        label_weird_prog: ""Progress"",
        opt_viri_killed: ""Addison killed"", opt_frozen: ""Berdly frozen"", 
        opt_talked_susie: ""Talked to Susie"", opt_hospital: ""Noelle at hospital"",
        label_weird_cancel: ""Canceled Weird Route"",
        label_egg2: ""Egg obtained (ch 2)"",
        label_spamton: ""Spamton defeated"",
        label_onion_rel2: ""Relation (ch 2)"", opt_notfriends_anymore: ""Not friends anymore"",
        label_moss2: ""Moss eaten (ch 2)"",
        label_moss_noelle: ""... with Noelle"",
        label_moss_susie: ""... with Susie"",
        label_seam_gaveup: ""Seam gave up quest"",
        label_crystal_jevil: ""Jevil's Crystal given"",
        label_crystal_spamton: ""Spamton's Crystal given"",
        label_seam_talk: ""Talked to Seam"",

        // Chap 3
        label_lot_rank1: ""LOT Board 1 Rank"",
        label_lot_rank2: ""LOT Board 2 Rank"",
        label_sword_prog: ""Sword Route Progress"",
        opt_ice_key: ""Ice Key obtained"", opt_dungeon2: ""Dungeon (Floor 2)"", opt_key_used: ""Key used"",
        opt_shelter_key: ""Shelter Key obtained"", opt_dungeon3: ""Dungeon (Floor 3)"", opt_shelter_used: ""Shelter Key used"",
        opt_eram: ""ERAM defeated"",
        label_susie_attacked: ""Susie attacked"",
        label_egg3: ""Egg obtained (ch 3)"",
        label_knight: ""Knight defeated"",
        label_fountain: ""Fountain"",
        opt_flirt_no_curtain: ""Flirted (no curtain)"", opt_no_flirt: ""No flirt"", opt_flirt_curtain: ""Flirted (talked to curtain)"",
        label_tenna_statue: ""Tenna Statue collected"",
        label_moss3: ""Moss eaten (ch 3)"",
        label_crystal_knight: ""Knight's Crystal given"",

        // Chap 4
        label_egg4: ""Egg obtained (ch 4)"",
        label_gerson: ""Gerson defeated"",
        label_moss4: ""Moss eaten (ch 4)"", opt_refused: ""Refused"",
        label_ralsei_room: ""Ralsei's Room"",
        label_qcs_susie: ""QC's with Susie"", opt_visited: ""Visited"",
        label_tea_ralsei: ""Tea with Ralsei"",
        label_prayer: ""Prayer"", opt_for_susie: ""For Susie"", opt_for_noelle: ""For Noelle"", opt_for_asriel: ""For Asriel"",
        label_tenna_given: ""Tenna given"",
        label_noelle_phone: ""Noelle's Phone"", opt_not_inspected: ""Not inspected"", opt_no_answer: ""Didn't answer"", opt_festival: ""Go to festival"", opt_wrong_number: ""Wrong number song"",
        label_susie_prize: ""Susie's Prize collected"",
        label_stain: ""Stain removed"",
        label_ladder: ""Ladder collected"",
        label_pillow: ""Pillow collected"",

        // UI Draw Menu
        menu_debug: ""Debug Menu"",
        room_list: ""Room List"",
        warp_options: ""Warp Options"",
        item_type: ""Item Type"",
        item_list: ""Item List"",
        armor_list: ""Armor List"",
        weapon_list: ""Weapon List"",
        keyitem_list: ""Key Item List"",
        add_how_many: ""Add how many to inventory?"",
        recruit_list: ""Recruit List"",
        recruit_presets: ""Recruit Presets"",
        ui_misc: ""Misc"",
        menu_unknown: ""Unknown"",
        btn_current_room: ""Current Room"",
        btn_search: ""Search"",
        ui_contains: ""Contains: "",
        btn_cancel: ""Cancel"",
        ui_is_darkworld: ""Is Darkworld: "",
        ui_plot_value: ""Plot Value: "",
        ui_teammate2: ""Teammate 2:  "",
        ui_teammate3: ""Teammate 3:  "",
        btn_warp: ""Warp"",
        ui_nobody: ""Nobody"",
        type_items: ""Items"",
        type_armors: ""Armors"",
        type_weapons: ""Weapons"",
        type_keyitems: ""Key Items"",
        ui_chapter: ""Chapter: "",
        ui_held: ""held"",
        ui_equipped: ""Equipped"",
        btn_presets: ""Presets"",
        btn_recruit_all: ""Recruit All"",
        btn_lose_all: ""Lose All"",
        ui_chap_short: ""chap"",
        ui_of_chapter: ""of chapter"",
        ui_custom: ""Custom"",
        ui_problem: ""problem lol"",
        msg_cancelled: ""Cancelled"",
        msg_removed_inv: "" removed from inventory"",
        msg_added_inv: "" added to inventory"",
        msg_invalid_sel: ""Invalid selection!"",
        msg_selected: "" selected!"",
        msg_search_selected: ""Search selected!"",
        hint_room: ""Selected room: "",
        hint_press: ""Press "",
        hint_change_chap: "" to change chapter"",
        dbg_inv_flag: ""Invalid flag "",
        dbg_because: "" because of "",
        dbg_empty_flag: ""Empty flag"",
        dbg_inv_val: ""Invalid value "",
        dbg_empty_val: ""Empty value"",
        dbg_updated: ""Updated "",
        dbg_from: "" from "",
        dbg_to: "" to "",
        ui_esc_cancel: ""Esc - Cancel"",
        ui_m_keys: ""M - Keys"",
        ui_m_close: ""M - Close"",
        ui_inv_items: ""ITEMs: "",
        ui_inv_armors: ""ARMORs: "",
        ui_inv_weapons: ""WEAPONs: "",
        ui_inv_keyitems: ""KEY ITEMs: "",
        ui_chap_all: ""(all chap)"",
        ui_world_dark: ""(Darkworld)"",
        ui_world_light: ""(Lightworld)"",
        ui_keys_title: ""Debug Mode Keys"",
        key_0: ""F10 - Toggle debug mode"",
        key_1: ""S - Save game"",
        key_2: ""L - Load last save"",
        key_3: ""R - Reload room | Backspace+R - Restart game"",
        key_4: ""P - Pause/resume game"",
        key_5: ""M+1 | M+2 - Add/remove 100 D$"",
        key_6: ""Delete - Go to previous room"",
        key_7: ""Insert - Go to next room"",
        key_8: ""W - Instantly win battle"",
        key_9: ""V - Skip enemy turn"",
        key_10: ""H - Restore party HP"",
        key_11: ""T - Fill/empty TP bar"",
        key_12: ""O - Toggle 30, 60, 120 FPS"",
        key_13: ""Backspace - Skip intro sequence (Ch1)"",
        key_14: ""Middle Click - Room Editor""
    },
    fr: 
    {
        dmode_activated: ""Mode Debug activé !"",
        dmode_desactivated: ""Mode Debug désactivé !"",
        fps_1: ""FPS à 1"",
        fps_30: ""FPS à 30"",
        fps_60: ""FPS à 60"",
        fps_120: ""FPS à 120"",
        tp_0: ""PT à 0 %"",
        tp_250: ""PT à 250 %"",
        fullheal: ""PV de l'équipe restaurés"",
        fightwin: ""Combat passé"",
        turnskip: ""Tour de l'ennemi passé"",
        
        // Menus
        debug_menu: ""Menu Debug"",
        warps: ""Sauts"",
        items: ""Items"",
        recruits: ""Recrues"",
        misc: ""Divers"",

        // Light Objects
        hot_chocolate: ""Chocolat Chaud"",
        pencil: ""Crayon"",
        bandage: ""Pansement"",
        bouquet: ""Bouquet"",
        ball_junk: ""Boule de Trucs"",
        halloween_pencil: ""Crayon Halloween"",
        lucky_pencil: ""Crayon Fétiche"",
        egg: ""Œuf"",
        cards: ""Cartes"",
        heart_candy: ""Boîte de ChocoCœurs"",
        glass: ""Verre"",
        eraser: ""Gomme"",
        mech_pencil: ""Critérium"",
        wristwatch: ""Montre"",
        holiday_pencil: ""Crayon de Noël"",
        cactus_needle: ""Épine de Cactus"",
        black_shard: ""ÉclatNoir"",
        quill_pen: ""Stylo-Plume"",

        // Categories
        cat_vessel: ""Séquence Vaisseau"",
        cat_superboss: ""Superbosses"",
        cat_weird: ""Weird Route"",
        cat_seam: ""Seam"",
        cat_eggs: ""Œufs"",
        cat_onion: ""Onion San"",
        cat_misc1: ""Divers chap 1"",
        cat_misc2: ""Divers chap 2"",
        cat_tenna: ""Legend of Tenna"",
        cat_sword: ""Sword Route"",
        cat_misc3: ""Divers chap 3"",
        cat_misc4: ""Divers chap 4"",
        cat_moss: ""Mousse"",
        cat_thrash: ""Roboteur"",

        // Goner Maker
        g_food: ""NOURRITURE"",
        g_blood: ""GROUPE SANGUIN"",
        g_color: ""COULEUR"",
        g_gift: ""PRÉSENT"",
        g_feeling: ""SENTIMENT ÉPROUVÉ"",
        g_honest: ""RÉPONDU HONNÊTEMENT"",
        g_crises: ""CONSENTIR AUX CRISES"",
        opt_sweet: ""SUCRÉE"", opt_soft: ""TENDRE"", opt_bitter: ""AMÈRE"", opt_salty: ""SALÉE"", opt_pain: ""DOULEUR"", opt_cold: ""FROIDE"",
        opt_red: ""ROUGE"", opt_blue: ""BLEU"", opt_green: ""VERT"", opt_cyan: ""CYAN"",
        opt_kindness: ""GENTILLESSE"", opt_mind: ""ESPRIT"", opt_ambition: ""AMBITION"", opt_bravery: ""BRAVOURE"", opt_voice: ""VOIX"",
        opt_love: ""AMOUR"", opt_hope: ""ESPOIR"", opt_disgust: ""DÉGOÛT"", opt_fear: ""PEUR"",
        g_yes: ""OUI"", g_no: ""NON"",

        // General
        opt_no: ""Non"", opt_yes: ""Oui"", opt_seen: ""Vu"", opt_notseen: ""Pas vu"",
        
        // Thrash Machine
        thrash_head: ""Tête Roboteur"", thrash_body: ""Corps Roboteur"", thrash_legs: ""Jambes Roboteur"",
        opt_laser: ""Laser"", opt_sword: ""Épée"", opt_flame: ""Flamme"", opt_duck: ""Canard"",
        opt_simple: ""Sobre"", opt_wheel: ""Roue"", opt_tank: ""Tank"",
        opt_sneakers: ""Baskets"", opt_tires: ""Pneus"", opt_tracks: ""Chaînes"",

        // Misc Chap 1
        label_gang: ""Nom du gang"",
        gang_guys: ""Les Types (unused)"", gang_squad: ""L'Escouade $?$!$"", gang_fanclub: ""Le Fan Club Lancer"", gang_fungang: ""Le Fun Gang"",
        label_prophecy: ""Prophétie entendu"", label_manual: ""Manuel jeté"",
        opt_tried: ""A tenté"", opt_thrown: ""L'a jeté"",
        label_cake: ""Gâteau rendu"", label_donation: ""Objectif de Donation"", opt_reached: ""Atteint"",
        label_starwalker: ""Starwalker"", label_asgore_flowers: ""Fleurs d'Asgore"", opt_given: ""Données"",
        label_noelle_out: ""Noelle dehors"", opt_talked_susie: ""A parlé de Susie"",
        label_sink: ""Évier inspecté (chap 1)"", label_egg1: ""Œuf obtenu (chap 1)"",
        label_jevil: ""Jevil vaincu"", opt_violence: ""Via violence"", opt_mercy: ""Via clémence"",
        
        // Onion San
        label_onion_rel: ""Relation (chap 1)"", opt_friends: ""Amis"", opt_notfriends: ""Pas amis"",
        label_kris_name: ""Nom de Kris"", opt_hippo: ""Hippopotame"",
        label_onion_name: ""Nom d'Onion"", opt_onyx: ""Oignon"", opt_beauty: ""Beauté"", opt_asriel2: ""Asriel II"", opt_stinky: ""Dégoûtant"",
        
        // Moss
        label_moss1: ""Mousse mangée (chap 1)"",

        // Misc Chap 2
        label_plush: ""Peluche"", opt_notgiven: ""Pas donnée"",
        label_hacker: ""Hacker recruté"",
        label_berdly_arm: ""Bras de Berdly"", opt_burnt: ""Brûlé"", opt_ok: ""Ok"",
        label_mt_fan: ""\""Fan\"" de Mettaton"",
        label_susie_statue: ""Statue de Susie récupérée"",
        label_icee_statue: ""ICE-E récupéré"",
        label_sink2: ""Évier inspecté (chap 2)"",
        label_shelter: ""Scène de l'abri vue"",
        label_weird_prog: ""Avancée"",
        opt_viri_killed: ""Nikomercant tué"", opt_frozen: ""Berdly gelé"", 
        opt_talked_susie: ""A parlé à Susie"", opt_hospital: ""Noëlle à l'hôpital"",
        label_weird_cancel: ""A annulé la weird route"",
        label_egg2: ""Œuf obtenu (chap 2)"",
        label_spamton: ""Spamton vaincu"",
        label_onion_rel2: ""Relation (chap 2)"", opt_notfriends_anymore: ""Plus amis"",
        label_moss2: ""Mousse mangée (chap 2)"",
        label_moss_noelle: ""... avec Noëlle"",
        label_moss_susie: ""... avec Susie"",
        label_seam_gaveup: ""Seam a abandonné la quête"",
        label_crystal_jevil: ""Cristal de Jevil donné"",
        label_crystal_spamton: ""Cristal de Spamton donné"",
        label_seam_talk: ""A parlé à Seam tout court"",

        // Chap 3
        label_lot_rank1: ""LOT Rang Board 1"",
        label_lot_rank2: ""LOT Rang Board 2"",
        label_sword_prog: ""Avancée Sword Route"",
        opt_ice_key: ""Clé de glace obtenue"", opt_dungeon2: ""Donjon (plateau 2)"", opt_key_used: ""Clé utilisée"",
        opt_shelter_key: ""Clé de l'abri obtenue"", opt_dungeon3: ""Donjon (plateau 3)"", opt_shelter_used: ""Clé de l'abri utilisée"",
        opt_eram: ""ERAM vaincu"",
        label_susie_attacked: ""Susie attaquée"",
        label_egg3: ""Œuf obtenu (chap 3)"",
        label_knight: ""Chevalier vaincu"",
        label_fountain: ""Fontaine"",
        opt_flirt_no_curtain: ""A flirté (pas au rideau)"", opt_no_flirt: ""Pas flirté"", opt_flirt_curtain: ""A flirté (au rideau)"",
        label_tenna_statue: ""Statue de Tenna récupérée"",
        label_moss3: ""Mousse mangée (chap 3)"",
        label_crystal_knight: ""Cristal du Chevalier donné"",

        // Chap 4
        label_egg4: ""Œuf obtenu (chap 4)"",
        label_gerson: ""Gerson vaincu"",
        label_moss4: ""Mousse mangée (chap 4)"", opt_refused: ""Refusée"",
        label_ralsei_room: ""Chambre de Ralsei"",
        label_qcs_susie: ""QC avec Susie"", opt_visited: ""Y est allé"",
        label_tea_ralsei: ""Thé avec Ralsei"",
        label_prayer: ""Prière"", opt_for_susie: ""Pour Susie"", opt_for_noelle: ""Pour Noëlle"", opt_for_asriel: ""Pour Asriel"",
        label_tenna_given: ""Tenna donné"",
        label_noelle_phone: ""Tel. de Noëlle"", opt_not_inspected: ""Pas inspecté"", opt_no_answer: ""Pas répondu"", opt_festival: ""Allez au festival"", opt_wrong_number: ""Wrong number song"",
        label_susie_prize: ""Prix Susie récupéré"",
        label_stain: ""Tache retirée"",
        label_ladder: ""Échelle récupérée"",
        label_pillow: ""Oreiller récupéré"",

        // UI Draw Menu
        menu_debug: ""Menu Debug"",
        room_list: ""Liste des salles"",
        warp_options: ""Options du saut"",
        item_type: ""Type d'items"",
        item_list: ""Liste d'objets"",
        armor_list: ""Liste d'armures"",
        weapon_list: ""Liste d'armes"",
        keyitem_list: ""Liste d'objets clés"",
        add_how_many: ""Ajouter combien à l'inventaire ?"",
        recruit_list: ""Liste des recrues"",
        recruit_presets: ""Préréglages des recrues"",
        ui_misc: ""Divers"",
        menu_unknown: ""Inconnu"",
        btn_current_room: ""Salle actuelle"",
        btn_search: ""Recherche"",
        ui_contains: ""Contient : "",
        btn_cancel: ""Annuler"",
        ui_is_darkworld: ""Est un Darkworld : "",
        ui_plot_value: ""Valeur de plot : "",
        ui_teammate2: ""Équipier 2 :  "",
        ui_teammate3: ""Équipier 3 :  "",
        btn_warp: ""Sauter"",
        ui_nobody: ""Personne"",
        type_items: ""Objets"",
        type_armors: ""Armures"",
        type_weapons: ""Armes"",
        type_keyitems: ""Obj Clés"",
        ui_chapter: ""Chapitre : "",
        ui_held: ""possédé(s)"",
        ui_equipped: ""Équipé"",
        btn_presets: ""Préréglages"",
        btn_recruit_all: ""Recruter tous"",
        btn_lose_all: ""Perdre tous"",
        ui_chap_short: ""chap"",
        ui_of_chapter: ""du chapitre"",
        ui_custom: ""Custom"",
        ui_problem: ""problem lol"",
        msg_cancelled: ""Annulé"",
        msg_removed_inv: "" retiré de l'inventaire"",
        msg_added_inv: "" ajouté à l'inventaire"",
        msg_invalid_sel: ""Sélection invalide !"",
        msg_selected: "" sélectionné !"",
        msg_search_selected: ""Recherche sélectionné !"",
        hint_room: ""Salle sélectionnée : "",
        hint_press: ""Appuyez sur "",
        hint_change_chap: "" pour changer de chapitre"",
        dbg_inv_flag: ""Flag invalide "",
        dbg_because: "" à cause de "",
        dbg_empty_flag: ""Flag vide"",
        dbg_inv_val: ""Valeur invalide "",
        dbg_empty_val: ""Valeur vide"",
        dbg_updated: ""Mise à jour de "",
        dbg_from: "" de "",
        dbg_to: "" à "",
        ui_esc_cancel: ""Esc - Annuler"",
        ui_m_keys: ""M - Touches"",
        ui_m_close: ""M - Fermer"",
        ui_inv_items: ""OBJETs : "",
        ui_inv_armors: ""ARMUREs : "",
        ui_inv_weapons: ""ARMEs : "",
        ui_inv_keyitems: ""OBJETs CLÉs : "",
        ui_chap_all: ""(tout chap)"",
        ui_world_dark: ""(Darkworld)"",
        ui_world_light: ""(Lightworld)"",
        ui_keys_title: ""Touches du debug mode"",
        key_0: ""F10 - Activer/désactiver le debug mode"",
        key_1: ""S - Sauvegarder la partie"",
        key_2: ""L - Charger la dernière sauvegarde"",
        key_3: ""R - Charger la salle | Retour arrière+R - Redémarrer le jeu"",
        key_4: ""P - Mettre en pause/reprendre le jeu"",
        key_5: ""M+1 | M+2 - Ajouter/retirer 100 D$"",
        key_6: ""Suppr - Se rendre à la salle précédente"",
        key_7: ""Insert - Se rendre à la salle suivante"",
        key_8: ""W - Gagner instantanément un combat"",
        key_9: ""V - Passer le tour de l'ennemi"",
        key_10: ""H - Restaurer les HP du party"",
        key_11: ""T - Remplir/vider la barre de TP"",
        key_12: ""O - Basculer entre 30, 60 et 120 FPS"",
        key_13: ""Retour arrière - Passer le segment d'intro (Ch1)"",
        key_14: ""Clic milieu - Éditeur de salle""
    }
};
".Replace("___DEFAULT_LANG___", defaultLang));
ChangeSelection(scr_dmode_init_lang);

// Script scr_dmode_get_text
UndertaleScript scr_dmode_get_text = new UndertaleScript();
scr_dmode_get_text.Name = Data.Strings.MakeString("scr_dmode_get_text");
scr_dmode_get_text.Code = new UndertaleCode();
scr_dmode_get_text.Code.Name = Data.Strings.MakeString("gml_GlobalScript_scr_dmode_get_text");
scr_dmode_get_text.Code.LocalsCount = 1;
Data.Scripts.Add(scr_dmode_get_text);
Data.Code.Add(scr_dmode_get_text.Code);
importGroup.QueueReplace(scr_dmode_get_text.Code, @"
function scr_dmode_get_text(arg0)
{
    var _lang = global.dmode_lang;
    
    if (variable_global_exists(""dmode_text""))
    {
        if (variable_struct_exists(global.dmode_text, _lang))
        {
            var _dict = variable_struct_get(global.dmode_text, _lang);
            
            if (variable_struct_exists(_dict, arg0))
                return variable_struct_get(_dict, arg0);
        }
    }
    
    return arg0;
}
");
ChangeSelection(scr_dmode_get_text);

// GameObject obj_dmenu_system
UndertaleGameObject obj_dmenu_system = new UndertaleGameObject();
obj_dmenu_system.Name = Data.Strings.MakeString("obj_dmenu_system");
obj_dmenu_system.Visible = true;
obj_dmenu_system.Persistent = true;
obj_dmenu_system.Awake = true;
Data.GameObjects.Add(obj_dmenu_system);
importGroup.QueueReplace(obj_dmenu_system.EventHandlerFor(EventType.Create, (uint)0, Data), @"
dmenu_active = false;
dmenu_box = 0;
dbutton_layout = 0;
dmenu_start_index = 0;
dbutton_max_visible = 5;
dmenu_arrow_timer = 0;
dscroll_timer = 0;
dscroll_cur_key = 0;
dscroll_delay = 15;
dscroll_speed = 5;
dbackspace_timer = 0;
dmenu_title = scr_dmode_get_text(""debug_menu"");
dbutton_options_original = [
    scr_dmode_get_text(""warps""),
    scr_dmode_get_text(""items""),
    scr_dmode_get_text(""recruits""),
    scr_dmode_get_text(""misc"")
];
dnumber_litems = [0, 11, 14, 14, 18];
dlight_weapons = [];
dlight_armors = [
    [3, scr_dmode_get_text(""bandage"")],
    [14, scr_dmode_get_text(""wristwatch"")]
];
dlight_objects = [
    [1,  scr_dmode_get_text(""hot_chocolate"")],
    [2,  scr_dmode_get_text(""pencil"")],
    [3,  scr_dmode_get_text(""bandage"")],
    [4,  scr_dmode_get_text(""bouquet"")],
    [5,  scr_dmode_get_text(""ball_junk"")],
    [6,  scr_dmode_get_text(""halloween_pencil"")],
    [7,  scr_dmode_get_text(""lucky_pencil"")],
    [8,  scr_dmode_get_text(""egg"")],
    [9,  scr_dmode_get_text(""cards"")],
    [10, scr_dmode_get_text(""heart_candy"")],
    [11, scr_dmode_get_text(""glass"")],
    [12, scr_dmode_get_text(""eraser"")],
    [13, scr_dmode_get_text(""mech_pencil"")],
    [14, scr_dmode_get_text(""wristwatch"")],
    [15, scr_dmode_get_text(""holiday_pencil"")],
    [16, scr_dmode_get_text(""cactus_needle"")],
    [17, scr_dmode_get_text(""black_shard"")],
    [18, scr_dmode_get_text(""quill_pen"")]
];
dhinter_active = false;
itemdescb = """";
armordesctemp = """";
weapondesctemp = """";
tempkeyitemdesc = """";
dhinter_text = """";

if (global.chapter >= 4)
{
    dtemp_lst = get_lw_dw_weapon_list();
    
    for (i = 0; i < array_length(dtemp_lst); i++)
        array_push(dlight_weapons, dlight_objects[dtemp_lst[i].lw_id - 1]);
}
else
{
    dlight_weapons = [
    [2,  scr_dmode_get_text(""pencil"")],
    [6,  scr_dmode_get_text(""halloween_pencil"")],
    [7,  scr_dmode_get_text(""lucky_pencil"")],
    [12, scr_dmode_get_text(""eraser"")],
    [13, scr_dmode_get_text(""mech_pencil"")]
];
}

for (i = 0; i < array_length(dlight_objects); i++)
{
    if (dlight_objects[i][0] > dnumber_litems[global.chapter])
        array_delete(dlight_objects, i--, 1);
}

if (global.chapter == 1)
{
    array_delete(dlight_weapons, 3, 2);
    array_delete(dlight_armors, 1, 1);
    array_delete(dlight_objects, 8, 1);
    array_delete(dlight_objects, 8, 1);
}

if (global.chapter < 3)
    array_delete(dbutton_options_original, 2, 1);

dbutton_options = dbutton_options_original;
dmenu_state = ""debug"";
dbutton_selected = 1;
dhorizontal_page = 0;
dhorizontal_index = 0;
dmenu_state_history = [];
dbutton_selected_history = [];
dgiver_menu_state = 0;
dgiver_button_selected = 0;
dgiver_amount = 1;
dgiver_bname = 0;
dbutton_indices = [];
ditem_types = [""objects"", ""armors"", ""weapons"", ""keyitems""];
ditem_chap = 1;
ditemcount_all = [1, 15, 18, 6, 4];
ditem_gaps = [0, 0, 0, 20, 0];
darmorcount_all = [1, 7, 15, 5, 5];
darmor_gaps = [0, 0, 0, 22, 0];
dweaponcount_all = [1, 10, 12, 4, 5];
dweapon_gaps = [0, 0, 0, 23, 0];
dkeyitemcount_all = [1, 7, 8, 4, 2];
dkeyitem_gaps = [0, 0, 0, 10, 0];

dpop_history = function()
{
	dkeyboard_input = """";
	if (array_length(dmenu_state_history) > 0)
	{
		dmenu_state = dmenu_state_history[array_length(dmenu_state_history) - 1];
		array_resize(dmenu_state_history, array_length(dmenu_state_history) - 1);
	}
	else
	{
		dmenu_active = !dmenu_active;
		dmenu_state_history = [];
		dbutton_selected_history = [];
		global.interact = 0;
	}
	if (array_length(dbutton_selected_history) > 0)
	{
		dbutton_selected = dbutton_selected_history[array_length(dbutton_selected_history) - 1];
		array_resize(dbutton_selected_history, array_length(dbutton_selected_history) - 1);
	}
	
	dmenu_state_update();
	dmenu_start_index = clamp(dbutton_selected - 1, 0, max(0, array_length(dbutton_options) - dbutton_max_visible));
}

ditem_index_data = function(arg0)
{
    var _chap = arg0;
    var _start_at = 0;
    
    for (var i = 0; i < _chap; i++)
        _start_at += (ditemcount_all[i] + ditem_gaps[i]);
    
    return 
    {
        start_id: _start_at,
        count: ditemcount_all[_chap]
    };
};

darmor_index_data = function(arg0)
{
    var _chap = arg0;
    var _start_at = 0;
    
    for (var i = 0; i < _chap; i++)
        _start_at += (darmorcount_all[i] + darmor_gaps[i]);
    
    return 
    {
        start_id: _start_at,
        count: darmorcount_all[_chap]
    };
};

dweapon_index_data = function(arg0)
{
    var _chap = arg0;
    var _start_at = 0;
    
    for (var i = 0; i < _chap; i++)
        _start_at += (dweaponcount_all[i] + dweapon_gaps[i]);
    
    return 
    {
        start_id: _start_at,
        count: dweaponcount_all[_chap]
    };
};

dkeyitem_index_data = function(arg0)
{
    var _chap = arg0;
    var _start_at = 0;
    
    for (var i = 0; i < _chap; i++)
        _start_at += (dkeyitemcount_all[i] + dkeyitem_gaps[i]);
    
    return 
    {
        start_id: _start_at,
        count: dkeyitemcount_all[_chap]
    };
};

cate_enum = 0;
GONER = cate_enum++;
SUPERBOSS = cate_enum++;
WEIRD2 = cate_enum++;
SEAM = cate_enum++;
ZOEUFS = cate_enum++;
ONION_SAN = cate_enum++;
MISC1 = cate_enum++;
MISC2 = cate_enum++;
LOT = cate_enum++;
SWORD3 = cate_enum++;
MISC3 = cate_enum++;
MISC4 = cate_enum++;
MOUSSE = cate_enum++;
ROBOTEUR = cate_enum++;
dother_categories = [
    scr_dmode_get_text(""cat_vessel""),
    scr_dmode_get_text(""cat_superboss""),
    scr_dmode_get_text(""cat_weird""),
    scr_dmode_get_text(""cat_seam""),
    scr_dmode_get_text(""cat_eggs""),
    scr_dmode_get_text(""cat_onion""),
    scr_dmode_get_text(""cat_misc1""),
    scr_dmode_get_text(""cat_misc2""),
    scr_dmode_get_text(""cat_tenna""),
    scr_dmode_get_text(""cat_sword""),
    scr_dmode_get_text(""cat_misc3""),
    scr_dmode_get_text(""cat_misc4""),
    scr_dmode_get_text(""cat_moss""),
    scr_dmode_get_text(""cat_thrash"")
];
dother_all_options = [];
dother_options = [];

if (global.chapter >= 0)
{
    // FOOD
    array_push(dother_all_options, [GONER, scr_dmode_get_text(""g_food""), 903, [
        [scr_dmode_get_text(""opt_sweet""), 0], [scr_dmode_get_text(""opt_soft""), 1], 
        [scr_dmode_get_text(""opt_bitter""), 2], [scr_dmode_get_text(""opt_salty""), 3], 
        [scr_dmode_get_text(""opt_pain""), 4], [scr_dmode_get_text(""opt_cold""), 5]
    ]]);

    // BLOOD TYPE
    array_push(dother_all_options, [GONER, scr_dmode_get_text(""g_blood""), 904, [
        [""A"", 0], [""AB"", 1], [""B"", 2], [""C"", 3], [""D"", 4]
    ]]);

    // COLOR
    array_push(dother_all_options, [GONER, scr_dmode_get_text(""g_color""), 905, [
        [scr_dmode_get_text(""opt_red""), 0], [scr_dmode_get_text(""opt_blue""), 1], 
        [scr_dmode_get_text(""opt_green""), 2], [scr_dmode_get_text(""opt_cyan""), 3]
    ]]);

    // GIFT
    array_push(dother_all_options, [GONER, scr_dmode_get_text(""g_gift""), 909, [
        [scr_dmode_get_text(""opt_kindness""), -1], [scr_dmode_get_text(""opt_mind""), 0], 
        [scr_dmode_get_text(""opt_ambition""), 1], [scr_dmode_get_text(""opt_bravery""), 2], 
        [scr_dmode_get_text(""opt_voice""), 3]
    ]]);

    // OPINION
    array_push(dother_all_options, [GONER, scr_dmode_get_text(""g_feeling""), 906, [
        [scr_dmode_get_text(""opt_love""), 0], [scr_dmode_get_text(""opt_hope""), 1], 
        [scr_dmode_get_text(""opt_disgust""), 2], [scr_dmode_get_text(""opt_fear""), 3]
    ]]);

    // HONEST
    array_push(dother_all_options, [GONER, scr_dmode_get_text(""g_honest""), 907, [
        [scr_dmode_get_text(""opt_yes""), 0], [scr_dmode_get_text(""opt_no""), 1]
    ]]);

    // CONSENT
    array_push(dother_all_options, [GONER, scr_dmode_get_text(""g_crises""), 908, [
        [scr_dmode_get_text(""opt_yes""), 0], [scr_dmode_get_text(""opt_no""), 1]
    ]]);
}

if (global.chapter >= 1)
{
    // TRASH MACHINE
    array_push(dother_all_options, [ROBOTEUR, scr_dmode_get_text(""thrash_head""), 220, [[scr_dmode_get_text(""opt_laser""), 0], [scr_dmode_get_text(""opt_sword""), 1], [scr_dmode_get_text(""opt_flame""), 2], [scr_dmode_get_text(""opt_duck""), 3]]]);
    array_push(dother_all_options, [ROBOTEUR, scr_dmode_get_text(""thrash_body""), 221, [[scr_dmode_get_text(""opt_simple""), 0], [scr_dmode_get_text(""opt_wheel""), 1], [scr_dmode_get_text(""opt_tank""), 2], [scr_dmode_get_text(""opt_duck""), 3]]]);
    array_push(dother_all_options, [ROBOTEUR, scr_dmode_get_text(""thrash_legs""), 222, [[scr_dmode_get_text(""opt_sneakers""), 0], [scr_dmode_get_text(""opt_tires""), 1], [scr_dmode_get_text(""opt_tracks""), 2], [scr_dmode_get_text(""opt_duck""), 3]]]);
    
    // MISC 1
    array_push(dother_all_options, [MISC1, scr_dmode_get_text(""label_gang""), 214, [[scr_dmode_get_text(""gang_guys""), 0], [scr_dmode_get_text(""gang_squad""), 1], [scr_dmode_get_text(""gang_fanclub""), 2], [scr_dmode_get_text(""gang_fungang""), 3]]]);
    array_push(dother_all_options, [MISC1, scr_dmode_get_text(""label_prophecy""), 203, [[scr_dmode_get_text(""opt_no""), 1], [scr_dmode_get_text(""opt_yes""), 0]]]);
    array_push(dother_all_options, [MISC1, scr_dmode_get_text(""label_manual""), 207, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_tried""), 1], [scr_dmode_get_text(""opt_thrown""), 2]]]);
    array_push(dother_all_options, [MISC1, scr_dmode_get_text(""label_cake""), 253, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    array_push(dother_all_options, [MISC1, scr_dmode_get_text(""label_starwalker""), 254, [[""Pissing me off"", 0], [""I will join"", 1]]]);
    array_push(dother_all_options, [MISC1, scr_dmode_get_text(""label_donation""), 216, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_reached""), 1]]]);
    array_push(dother_all_options, [MISC1, scr_dmode_get_text(""label_asgore_flowers""), 262, [[scr_dmode_get_text(""opt_notseen""), 0], [scr_dmode_get_text(""opt_no""), 2], [scr_dmode_get_text(""opt_given""), 4]]]);
    array_push(dother_all_options, [MISC1, scr_dmode_get_text(""label_noelle_out""), 276, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_no""), 1], [scr_dmode_get_text(""opt_talked_susie""), 2]]]);
    array_push(dother_all_options, [MISC1, scr_dmode_get_text(""label_sink""), 278, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    
    // EGGS & SUPERBOSSES
    array_push(dother_all_options, [ZOEUFS, scr_dmode_get_text(""label_egg1""), 911, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    array_push(dother_all_options, [SUPERBOSS, scr_dmode_get_text(""label_jevil""), 241, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_violence""), 6], [scr_dmode_get_text(""opt_mercy""), 7]]]);
    
    // ONION SAN
    array_push(dother_all_options, [ONION_SAN, scr_dmode_get_text(""label_onion_rel""), 258, [[scr_dmode_get_text(""opt_notseen""), 0], [scr_dmode_get_text(""opt_friends""), 2], [scr_dmode_get_text(""opt_notfriends""), 3]]]);
    array_push(dother_all_options, [ONION_SAN, scr_dmode_get_text(""label_kris_name""), 259, [[scr_dmode_get_text(""opt_no""), 0], [""Kris"", 1], [scr_dmode_get_text(""opt_hippo""), 2]]]);
    array_push(dother_all_options, [ONION_SAN, scr_dmode_get_text(""label_onion_name""), 260, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_onyx""), 1], [scr_dmode_get_text(""opt_beauty""), 2], [scr_dmode_get_text(""opt_asriel2""), 3], [scr_dmode_get_text(""opt_stinky""), 4]]]);
    
    // MOSS
    array_push(dother_all_options, [MOUSSE, scr_dmode_get_text(""label_moss1""), 106, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
}

if (global.chapter >= 2)
{
    // MISC 2
    array_push(dother_all_options, [MISC2, scr_dmode_get_text(""label_plush""), 307, [[scr_dmode_get_text(""opt_notgiven""), 0], [""Ralsei"", 1], [""Susie"", 2], [""Noëlle"", 3], [""Berdly"", 4]]]);
    array_push(dother_all_options, [MISC2, scr_dmode_get_text(""label_hacker""), 659, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    array_push(dother_all_options, [MISC2, scr_dmode_get_text(""label_berdly_arm""), 457, [[scr_dmode_get_text(""opt_burnt""), 0], [scr_dmode_get_text(""opt_ok""), 1]]]);
    array_push(dother_all_options, [MISC2, scr_dmode_get_text(""label_mt_fan""), 422, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    array_push(dother_all_options, [MISC2, scr_dmode_get_text(""label_susie_statue""), 393, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    array_push(dother_all_options, [MISC2, scr_dmode_get_text(""label_icee_statue""), 394, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    array_push(dother_all_options, [MISC2, scr_dmode_get_text(""label_sink2""), 461, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    array_push(dother_all_options, [MISC2, scr_dmode_get_text(""label_shelter""), 315, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);

    // WEIRD ROUTE
    array_push(dother_all_options, [WEIRD2, scr_dmode_get_text(""label_weird_prog""), 915, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_viri_killed""), 3], [scr_dmode_get_text(""opt_frozen""), 6], [scr_dmode_get_text(""opt_talked_susie""), 9], [scr_dmode_get_text(""opt_hospital""), 20]]]);
    array_push(dother_all_options, [WEIRD2, scr_dmode_get_text(""label_weird_cancel""), 916, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    
    // Eggs & Superbosses
    array_push(dother_all_options, [ZOEUFS, scr_dmode_get_text(""label_egg2""), 918, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    array_push(dother_all_options, [SUPERBOSS, scr_dmode_get_text(""label_spamton""), 309, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 9]]]);
    
    // ONION SAN
    array_push(dother_all_options, [ONION_SAN, scr_dmode_get_text(""label_onion_rel2""), 425, [[scr_dmode_get_text(""opt_notseen""), 0], [scr_dmode_get_text(""opt_friends""), 1], [scr_dmode_get_text(""opt_notfriends_anymore""), 2]]]);
    
    // MOSS
    array_push(dother_all_options, [MOUSSE, scr_dmode_get_text(""label_moss2""), 920, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    array_push(dother_all_options, [MOUSSE, scr_dmode_get_text(""label_moss_noelle""), 921, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    array_push(dother_all_options, [MOUSSE, scr_dmode_get_text(""label_moss_susie""), 922, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    
    // SEAM
    array_push(dother_all_options, [SEAM, scr_dmode_get_text(""label_seam_gaveup""), 961, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    array_push(dother_all_options, [SEAM, scr_dmode_get_text(""label_crystal_jevil""), 954, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    array_push(dother_all_options, [SEAM, scr_dmode_get_text(""label_crystal_spamton""), 353, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    array_push(dother_all_options, [SEAM, scr_dmode_get_text(""label_seam_talk""), 312, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
}

if (global.chapter >= 3)
{
    // LOT
    array_push(dother_all_options, [LOT, scr_dmode_get_text(""label_lot_rank1""), 1173, [[""Z"", 0], [""C"", 1], [""B"", 2], [""A"", 3], [""S"", 4], [""T"", 5]]]);
    array_push(dother_all_options, [LOT, scr_dmode_get_text(""label_lot_rank2""), 1174, [[""Z"", 0], [""C"", 1], [""B"", 2], [""A"", 3], [""S"", 4], [""T"", 5]]]);
    
    // SWORD ROUTE
    array_push(dother_all_options, [SWORD3, scr_dmode_get_text(""label_sword_prog""), 1055, [
        [scr_dmode_get_text(""opt_notseen""), 0], 
        [scr_dmode_get_text(""opt_ice_key""), 1], 
        [scr_dmode_get_text(""opt_dungeon2""), 1.5], 
        [scr_dmode_get_text(""opt_key_used""), 2], 
        [scr_dmode_get_text(""opt_shelter_key""), 3], 
        [scr_dmode_get_text(""opt_dungeon3""), 4], 
        [scr_dmode_get_text(""opt_shelter_used""), 5], 
        [scr_dmode_get_text(""opt_eram""), 6]
    ]]);
    array_push(dother_all_options, [SWORD3, scr_dmode_get_text(""label_susie_attacked""), 1268, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    
    // Eggs & Superbosses
    array_push(dother_all_options, [ZOEUFS, scr_dmode_get_text(""label_egg3""), 930, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    array_push(dother_all_options, [SUPERBOSS, scr_dmode_get_text(""label_knight""), 1047, [[scr_dmode_get_text(""opt_no""), 2], [scr_dmode_get_text(""opt_yes""), 1]]]);
    
    // MISC 3
    array_push(dother_all_options, [MISC3, scr_dmode_get_text(""label_fountain""), 1144, [
        [scr_dmode_get_text(""opt_notseen""), 0], 
        [scr_dmode_get_text(""opt_flirt_no_curtain""), 1], 
        [scr_dmode_get_text(""opt_no_flirt""), 2], 
        [scr_dmode_get_text(""opt_flirt_curtain""), 3]
    ]]);
    array_push(dother_all_options, [MISC3, scr_dmode_get_text(""label_tenna_statue""), 1222, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    
    // MOSS
    array_push(dother_all_options, [MOUSSE, scr_dmode_get_text(""label_moss3""), 1078, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    
    // SEAM
    array_push(dother_all_options, [SEAM, scr_dmode_get_text(""label_crystal_knight""), 856, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
}

if (global.chapter >= 4)
{
    // Eggs & Superbosses
    array_push(dother_all_options, [ZOEUFS, scr_dmode_get_text(""label_egg4""), 931, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    array_push(dother_all_options, [SUPERBOSS, scr_dmode_get_text(""label_gerson""), 1629, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    
    // MOSS
    array_push(dother_all_options, [MOUSSE, scr_dmode_get_text(""label_moss4""), 1592, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1], [scr_dmode_get_text(""opt_refused""), 2]]]);
    
    // MISC 4
    array_push(dother_all_options, [MISC4, scr_dmode_get_text(""label_ralsei_room""), 710, [[scr_dmode_get_text(""opt_notseen""), 0], [scr_dmode_get_text(""opt_seen""), 2]]]);
    array_push(dother_all_options, [MISC4, scr_dmode_get_text(""label_qcs_susie""), 701, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_visited""), 1]]]);
    array_push(dother_all_options, [MISC4, scr_dmode_get_text(""label_tea_ralsei""), 1514, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    
    // Pray
    array_push(dother_all_options, [MISC4, scr_dmode_get_text(""label_prayer""), 1507, [
        [scr_dmode_get_text(""opt_no""), 0], 
        [scr_dmode_get_text(""opt_for_susie""), 1], 
        [scr_dmode_get_text(""opt_for_noelle""), 2], 
        [scr_dmode_get_text(""opt_for_asriel""), 3]
    ]]);
    
    array_push(dother_all_options, [MISC4, scr_dmode_get_text(""label_tenna_given""), 779, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 2]]]);
    
    // Noelle's phone
    array_push(dother_all_options, [MISC4, scr_dmode_get_text(""label_noelle_phone""), 714, [
        [scr_dmode_get_text(""opt_not_inspected""), 0], 
        [scr_dmode_get_text(""opt_no_answer""), 1], 
        [scr_dmode_get_text(""opt_festival""), 2], 
        [scr_dmode_get_text(""opt_wrong_number""), 3]
    ]]);
    
    array_push(dother_all_options, [MISC4, scr_dmode_get_text(""label_susie_prize""), 747, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    array_push(dother_all_options, [MISC4, scr_dmode_get_text(""label_stain""), 748, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    array_push(dother_all_options, [MISC4, scr_dmode_get_text(""label_ladder""), 864, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
    array_push(dother_all_options, [MISC4, scr_dmode_get_text(""label_pillow""), 865, [[scr_dmode_get_text(""opt_no""), 0], [scr_dmode_get_text(""opt_yes""), 1]]]);
}

dflag_categories_len = [];

for (i = 0; i < array_length(dother_categories); i++)
    array_push(dflag_categories_len, 0);

for (i = 0; i < array_length(dother_all_options); i++)
    dflag_categories_len[dother_all_options[i][0]] += 1;

global.dreading_custom_flag = 0;
dcustom_flag_text = ["""", """"];

find_subarray_index = function(arg0, arg1)
{
    value = global.flag[arg0];
    lst = arg1;
    prev = 0;
    
    for (i = 0; i < array_length(lst); i++)
    {
        if (lst[i][1] > value)
            break;
        
        prev = i;
    }
    
    return prev;
};

draw_monospace = function(arg0, arg1, arg2)
{
    draw_x = arg0;
    sep = (global.darkzone == 1) ? 15 : 8;
    
    for (i = 0; i < string_length(arg2); i++)
    {
        draw_text(draw_x, arg1, string_char_at(arg2, i + 1));
        draw_x += sep;
    }
};

function scr_array_contains(arg0, arg1)
{
    for (var i = 0; i < array_length(arg0); i++)
    {
        if (arg0[i] == arg1)
            return true;
    }
    
    return false;
}

dkeys_helper = 0;
dkeys_data = [];
drooms_id = scr_get_room_list();
drooms = [];
drooms_options = 
{
    target_room: ROOM_INITIALIZE,
    target_room: ROOM_INITIALIZE,
    target_plot: global.plot,
    target_is_darkzone: global.darkzone,
    target_member_2: global.char[1],
    target_member_3: global.char[2]
};
dkeyboard_input = """";

for (i = 0; i < array_length(drooms_id); i++)
    array_push(drooms, room_get_name(drooms_id[i].room_index));

");
importGroup.QueueReplace(obj_dmenu_system.EventHandlerFor(EventType.Step, (uint)0, Data), @"
dmenu_arrow_timer += 1;

function dmenu_pressed_key(arg0)
{
    if (arg0 != 40 && arg0 != 38 && arg0 != 37 && arg0 != 39)
        return 0;
    
    if (keyboard_check_pressed(arg0))
    {
        dscroll_cur_key = arg0;
        return 1;
    }
    
    if (arg0 != dscroll_cur_key)
        return 0;
    
    if (keyboard_check(arg0))
    {
        if (dscroll_timer >= dscroll_delay)
        {
            if ((dscroll_timer % dscroll_speed) == 0)
                return 2;
        }
        
        dscroll_timer += 1;
    }
    else if (arg0 == dscroll_cur_key)
    {
        dscroll_timer = 0;
        dscroll_cur_key = 0;
    }
    
    return 0;
}

function vmove_menu(arg0, arg1)
{
    pressed_up = arg0;
    pressed_down = arg1;
    
    if (pressed_up != 0 || pressed_down != 0)
    {
        if (pressed_up == 1 && dbutton_selected == 1)
        {
            dbutton_selected = array_length(dbutton_options) + 1;
            dmenu_start_index = dbutton_selected - 3;
        }
        else if (pressed_down == 1 && dbutton_selected == array_length(dbutton_options))
        {
            dbutton_selected = 0;
            dmenu_start_index = 0;
        }
        
        increment = pressed_up ? -1 : 1;
        
        if ((pressed_up && dbutton_selected != 1) || (pressed_down && dbutton_selected != array_length(dbutton_options)))
        {
            dbutton_selected += increment;
            snd_play(snd_menumove);
            
            if (pressed_up && dbutton_selected < (dmenu_start_index + 1))
                dmenu_start_index += increment;
            else if (pressed_down && dbutton_selected > (dmenu_start_index + dbutton_max_visible))
                dmenu_start_index += increment;
            
            if (dmenu_state == ""flag_misc"")
            {
                new_options = dother_options[dbutton_selected - 1];
                dhorizontal_index = find_subarray_index(new_options[2], new_options[3]);
            }
        }
    }
}

function evaluate_custom_flag(arg0)
{
    proper_exit = arg0;
    
    if (!proper_exit)
    {
        global.dreading_custom_flag = 0;
        dcustom_flag_text = ["""", """"];
        return 0;
    }
    
    for (c = 1; c <= string_length(dcustom_flag_text[0]); c++)
    {
        cur_char = string_char_at(dcustom_flag_text[0], c);
        if (!scr_84_is_digit(string_char_at(dcustom_flag_text[0], c)))
        {
            scr_debug_print(scr_dmode_get_text(""dbg_inv_flag"") + ""|"" + dcustom_flag_text[0] + ""|"" + scr_dmode_get_text(""dbg_because"") + ""|"" + string_char_at(dcustom_flag_text[0], c) + ""|"");
            proper_exit = 0;
            break;
        }
    }
    
    if (string_length(dcustom_flag_text[0]) == 0)
    {
        scr_debug_print(scr_dmode_get_text(""dbg_empty_flag""));
        proper_exit = 0;
    }
    
    if (dmenu_state == ""warp_options"")
        return proper_exit;
    
    for (c = 1; c <= string_length(dcustom_flag_text[1]); c++)
    {
        cur_char = string_char_at(dcustom_flag_text[0], c);
        if (!scr_84_is_digit(cur_char) && cur_char != ""."" && cur_char != ""-"")
        {
            scr_debug_print(scr_dmode_get_text(""dbg_inv_val"") + ""|"" + dcustom_flag_text[1] + ""|"");
            proper_exit = 0;
            break;
        }
    }
    
    if (string_length(dcustom_flag_text[1]) == 0)
    {
        if (proper_exit)
            scr_debug_print(""global.flag["" + string(real(dcustom_flag_text[0])) + ""] = |"" + string(global.flag[real(dcustom_flag_text[0])]) + ""|"");
        else
            scr_debug_print(scr_dmode_get_text(""dbg_empty_val""));
        
        proper_exit = 0;
    }
    
    if (proper_exit)
    {
        scr_debug_print(scr_dmode_get_text(""dbg_updated"") + ""global.flag["" + string(real(dcustom_flag_text[0])) + ""]"" + scr_dmode_get_text(""dbg_from"") + ""|"" + string(global.flag[real(dcustom_flag_text[0])]) + ""|"" + scr_dmode_get_text(""dbg_to"") + ""|"" + dcustom_flag_text[1] + ""|"");
        global.flag[real(dcustom_flag_text[0])] = real(dcustom_flag_text[1]);
    }
    
    if (proper_exit)
    {
        dmenu_active = 0;
        global.interact = 0;
    }
    
    return proper_exit;
}

if (dmenu_active && global.dreading_custom_flag)
{
    update_visu = 1;
    will_exit = 0;
    
    if (dmenu_state == ""warp"" || dmenu_state == ""warp_options"")
        dkeyboard_input = dcustom_flag_text[0];
    
    will_exit = keyboard_check_pressed(vk_escape) || keyboard_check_pressed(global.input_k[7]);
    will_exit |= ((dmenu_state == ""warp_options"" || dmenu_state == ""warp"") && (keyboard_check_pressed(vk_up) || keyboard_check_pressed(vk_down)));
    
    if (will_exit)
    {
        clean_exit = !keyboard_check_pressed(vk_escape);
        
        if (dmenu_state == ""flag_categories"" || dmenu_state == ""warp_options"")
        {
            flags_good = evaluate_custom_flag(clean_exit);
            
            if (flags_good && dmenu_state == ""warp_options"")
                drooms_options.target_plot = real(dkeyboard_input);
            
            snd_play(array_get([299, 420], flags_good));
        }
        
        if (dmenu_state == ""warp"" || dmenu_state == ""warp_options"")
        {
            if (keyboard_check_pressed(vk_down))
                vmove_menu(0, 1);
            else if (keyboard_check_pressed(vk_up))
                vmove_menu(1, 0);
            
            if (dmenu_state == ""warp"")
            {
                if (keyboard_check_pressed(global.input_k[4]) || keyboard_check_pressed(global.input_k[7]))
                    snd_play(snd_select);
                else if (keyboard_check_pressed(vk_escape))
                    snd_play(snd_error);
                else
                    snd_play(snd_menumove);
            }
        }
        
        global.dreading_custom_flag = 0;
        
        if (!clean_exit)
            dkeyboard_input = """";
        
        dcustom_flag_text = ["""", """"];
        will_exit = 1;
    }
    else if (dmenu_state == ""flag_categories"" && keyboard_check_pressed(vk_left) && dhorizontal_index != 0)
    {
        snd_play(snd_menumove);
        dhorizontal_index--;
    }
    else if (dmenu_state == ""flag_categories"" && keyboard_check_pressed(vk_right) && dhorizontal_index != 1)
    {
        snd_play(snd_menumove);
        dhorizontal_index++;
    }
    else if (keyboard_check(vk_backspace))
    {
        if (keyboard_check_pressed(vk_backspace))
        {
            dcustom_flag_text[dhorizontal_index] = string_delete(dcustom_flag_text[dhorizontal_index], string_length(dcustom_flag_text[dhorizontal_index]), 1);
            keyboard_string = """";
            dbackspace_timer = 20;
        }
        
        dbackspace_timer--;
        
        if (dbackspace_timer <= 0)
        {
            dcustom_flag_text[dhorizontal_index] = string_delete(dcustom_flag_text[dhorizontal_index], string_length(dcustom_flag_text[dhorizontal_index]), 1);
            keyboard_string = """";
            dbackspace_timer = 1;
        }
    }
    else if (keyboard_string != """")
    {
        dcustom_flag_text[dhorizontal_index] += keyboard_string;
        keyboard_string = """";
    }
    else
    {
        update_visu = 0;
    }
    
    if (update_visu)
    {
        if (!will_exit)
            dkeyboard_input = dcustom_flag_text[0];
        
        dmenu_state_update();
    }
}
else if (dmenu_active)
{
    if (dbutton_layout == 0 && dkeys_helper == 0)
    {
        if (keyboard_check_pressed(vk_left))
        {
            dbutton_selected -= 1;
            
            if (dbutton_selected < 1)
                dbutton_selected = array_length(dbutton_options);
            
            snd_play(snd_menumove);
        }
        
        if (keyboard_check_pressed(vk_right))
        {
            dbutton_selected = (dbutton_selected % array_length(dbutton_options)) + 1;
            snd_play(snd_menumove);
        }
    }
    
    if (dbutton_layout == 1)
    {
        og_horizontal_index = dhorizontal_index;
        
        if (dmenu_state == ""flag_misc"")
        {
            cur_options = dother_options[dbutton_selected - 1];
            cur_options_len = array_length(cur_options[3]);
            playsound = 1;
            
            if (keyboard_check_pressed(vk_right) && dhorizontal_index < (cur_options_len - 1))
                dhorizontal_index++;
            else if (keyboard_check_pressed(vk_left) && dhorizontal_index != 0)
                dhorizontal_index--;
            else
                playsound = 0;
            
            if (playsound)
            {
                global.flag[cur_options[2]] = cur_options[3][dhorizontal_index][1];
                scr_debug_print(scr_dmode_get_text(""dbg_updated"") + ""global.flag["" + string(cur_options[2]) + ""]"" + scr_dmode_get_text(""dbg_to"") + ""|"" + string(cur_options[3][dhorizontal_index][1]) + ""|"");
                snd_play(snd_menumove);
            }
        }
        
        pressed_right = dmenu_pressed_key(39);
        pressed_left = dmenu_pressed_key(37);
        
        if (pressed_left && pressed_right)
            pressed_right = 0;
        
        if (pressed_right || pressed_left)
        {
            if (dmenu_state == ""recruits"")
            {
                if (dbutton_selected != 1)
                {
                    real_index = dbutton_indices[dbutton_selected - 1];
                    scr_recruit_info(real_index);
                    recruit_count = global.flag[real_index + 600];
                    to_add = 1 / _recruitcount;
                    
                    if (pressed_left)
                    {
                        to_add = -to_add;
                        if (recruit_count == 0)
                        {
                            to_add = -1;
                        }
                    }
                    else if (pressed_right && recruit_count == -1)
                    {
                        to_add = 1;
                    }
                    
                    if ((pressed_right && (recruit_count * _recruitcount) < _recruitcount) || (pressed_left && (recruit_count * _recruitcount) > -1))
                    {
                        global.flag[600 + real_index] = recruit_count + to_add;
                        dmenu_state_update();
                        snd_play(snd_sparkle_gem);
                    }
                    else
                    {
                        snd_play(snd_error);
                    }
                }
                else if ((pressed_right && dhorizontal_page != global.chapter) || (pressed_left && dhorizontal_page != 0))
                {
                    dhorizontal_page++;
                    
                    if (pressed_left)
                        dhorizontal_page -= 2;
                    
                    snd_play(snd_menumove);
                    dmenu_state_update();
                }
            }
            else if (dmenu_state == ""warp_options"" && (dbutton_selected == 4 || dbutton_selected == 5))
            {
                cur_party = array_get([drooms_options.target_member_2, drooms_options.target_member_3], dbutton_selected - 4);
                new_party = -1;
                
                if (pressed_left && cur_party != 0)
                    new_party = cur_party - 1;
                else if (pressed_right && cur_party != (4 - (global.chapter == 1)))
                    new_party = cur_party + 1;
                
                if (new_party == 1)
                    new_party += (pressed_right - pressed_left);
                
                if (new_party != -1)
                {
                    if (dbutton_selected == 4)
                        drooms_options.target_member_2 = new_party;
                    else
                        drooms_options.target_member_3 = new_party;
                    
                    snd_play(snd_menumove);
                    dmenu_state_update();
                }
            }
            else if ((dmenu_state == ""objects"" || dmenu_state == ""weapons"" || dmenu_state == ""armors"") && (pressed_left + pressed_right) == 1)
            {
                dhorizontal_page = !dhorizontal_page;
                dmenu_start_index = 0;
                dbutton_selected = 1;
                snd_play(snd_menumove);
                dmenu_state_update();
            }
        }
        
        pressed_up = dmenu_pressed_key(38);
        pressed_down = dmenu_pressed_key(40);
        
        if (pressed_up && pressed_down)
            pressed_up = 0;
        
        vmove_menu(pressed_up, pressed_down);
        dmenu_start_index = clamp(dmenu_start_index, 0, max(0, array_length(dbutton_options) - dbutton_max_visible));
        
        if (dhorizontal_index != og_horizontal_index)
            dmenu_state_update();
    }
    
    if (dbutton_layout == 2)
    {
        if (keyboard_check_pressed(vk_left))
        {
            var owned_count = 0;
            
            switch (dgiver_menu_state)
            {
                case ""objects"":
                    if (dhorizontal_page == 0)
                        scr_itemcheck(dbutton_indices[dgiver_button_selected - 1]);
                    else
                        scr_litemcheck(dbutton_indices[dgiver_button_selected - 1]);
                    
                    owned_count = itemcount;
                    break;
                
                case ""armors"":
                    scr_armorcheck_inventory(dbutton_indices[dgiver_button_selected - 1]);
                    owned_count = itemcount;
                    break;
                
                case ""weapons"":
                    scr_weaponcheck_inventory(dbutton_indices[dgiver_button_selected - 1]);
                    owned_count = itemcount;
                    break;
                
                case ""keyitems"":
                    scr_keyitemcheck(dbutton_indices[dgiver_button_selected - 1]);
                    owned_count = itemcount;
                    break;
                
                default:
                    owned_count = 0;
            }
            
            if (dgiver_amount > -owned_count)
            {
                dgiver_amount -= 1;
                snd_play(snd_menumove);
            }
            else
            {
                snd_play(snd_error);
            }
        }
        
        if (keyboard_check_pressed(vk_right))
        {
            var owned_count = 0;
            
            switch (dgiver_menu_state)
            {
                case ""objects"":
                    if (dhorizontal_page == 0)
                        scr_itemcheck(0);
                    else
                        scr_litemcheck(0);
                    
                    owned_count = itemcount;
                    break;
                
                case ""armors"":
                    scr_armorcheck_inventory(0);
                    owned_count = itemcount;
                    break;
                
                case ""weapons"":
                    scr_weaponcheck_inventory(0);
                    owned_count = itemcount;
                    break;
                
                case ""keyitems"":
                    scr_keyitemcheck(0);
                    owned_count = itemcount;
                    break;
                
                case ""recruits"":
                    real_indice = dbutton_indices[dbutton_selected - 1];
                    recruited_nbr = global.flag[real_indice + 600];
                    global.flag[real_indice + 600] = recruited_nbr + 1;
                    break;
                
                default:
                    owned_count = 0;
            }
            
            if (dgiver_amount < owned_count)
            {
                dgiver_amount += 1;
                snd_play(snd_menumove);
            }
            else
            {
                snd_play(snd_error);
            }
        }
    }
    
    if (keyboard_check_pressed(global.input_k[4]) || keyboard_check_pressed(global.input_k[7]))
    {
        must_save = dmenu_state != ""givertab"" && dmenu_state != ""recruit_presets"" && dmenu_state != ""flag_misc"" && dmenu_state != ""warp_options"" && !(dmenu_state == ""warp"" && dbutton_selected == 2);
        must_save &= ((dmenu_state != ""flag_categories"" || dbutton_selected != 1) && (!(dmenu_state == ""weapons"" && dhorizontal_page) && !(dmenu_state == ""armors"" && dhorizontal_page)));
        must_save &= (dmenu_state != ""recruits"" || dbutton_selected == 1);
        must_save &= !(scr_array_contains(ditem_types, dmenu_state) && dhorizontal_page == 0 && dbutton_selected == 1);
        snd_play(snd_select);
        
        if (must_save)
        {
            array_push(dmenu_state_history, dmenu_state);
            array_push(dbutton_selected_history, dbutton_selected);
        }
        
        if (dmenu_state == ""flag_categories"" && dbutton_selected == 1)
        {
            global.dreading_custom_flag = 1;
            dhorizontal_index = 0;
            keyboard_string = """";
        }
        
        if (scr_array_contains(ditem_types, dmenu_state))
        {
            switch (dmenu_state)
            {
                case ""objects"":
                    if (dhorizontal_page != 0 || dbutton_selected > 1)
                    {
                        real_index = dbutton_indices[dbutton_selected - 1];
                        
                        if (dhorizontal_page == 0)
                        {
                            scr_iteminfo(real_index);
                            dgiver_bname = itemnameb;
                        }
                        else
                        {
                            for (i = 0; i < array_length(dlight_objects); i++)
                            {
                                if (dlight_objects[i][0] == real_index)
                                {
                                    real_index = i;
                                    break;
                                }
                            }
                            
                            dgiver_bname = dlight_objects[real_index][1];
                        }
                        
                        scr_debug_print(dgiver_bname + scr_dmode_get_text(""msg_selected""));
                    }
                    
                    break;
                
                case ""armors"":
                    if (dhorizontal_page != 0 || dbutton_selected > 1)
                    {
                        real_index = dbutton_indices[dbutton_selected - 1];
                        
                        if (dhorizontal_page == 0)
                        {
                            scr_armorinfo(real_index);
                            dgiver_bname = armornametemp;
                        }
                        else
                        {
                            dgiver_bname = dlight_armors[real_index][1];
                        }
                        
                        scr_debug_print(string(dgiver_bname) + scr_dmode_get_text(""msg_selected""));
                    }
                    
                    break;
                
                case ""weapons"":
                    if (dhorizontal_page != 0 || dbutton_selected > 1)
                    {
                        real_index = dbutton_indices[dbutton_selected - 1];
                        
                        if (dhorizontal_page == 0)
                        {
                            scr_weaponinfo(real_index);
                            dgiver_bname = weaponnametemp;
                        }
                        else
                        {
                            dgiver_bname = dlight_weapons[real_index][1];
                        }
                        
                        scr_debug_print(string(dgiver_bname) + scr_dmode_get_text(""msg_selected""));
                    }
                    
                    break;
                
                case ""keyitems"":
                    if (dbutton_selected > 1)
                    {
                        real_index = dbutton_indices[dbutton_selected - 1];
                        scr_keyiteminfo(real_index);
                        dgiver_bname = tempkeyitemname;
                        scr_debug_print(string(dgiver_bname) + scr_dmode_get_text(""msg_selected""));
                    }
                    
                    break;
            }
        }
        else if (dmenu_state == ""warp"" && dbutton_selected == 2)
        {
            scr_debug_print(scr_dmode_get_text(""msg_search_selected""));
        }
        else if (dmenu_state != ""givertab"" && dmenu_state != ""flag_misc"" && dmenu_state != ""warp_options"" && (dmenu_state != ""recruits"" || dbutton_selected == 1))
        {
            scr_debug_print(string(dbutton_options[dbutton_selected - 1]) + scr_dmode_get_text(""msg_selected""));
        }
        
        if ((dmenu_state == ""recruits"" && dbutton_selected != 1) || dmenu_state == ""warp_options"" || dmenu_state == ""recruit_presets"" || dmenu_state == ""warp_options"" || dmenu_state == ""flag_misc"" || ((dmenu_state == ""armors"" || dmenu_state == ""weapons"") && dhorizontal_page) || (dmenu_state == ""warp"" && dbutton_selected == 2))
        {
            dmenu_state_interact();
            dmenu_state_update();
        }
        else
        {
            dmenu_state_interact();
            dmenu_start_index = 0;
            dbutton_selected = 1;
            dmenu_state_update();
        }
    }
    
    if (keyboard_check_pressed(global.input_k[5]) || keyboard_check_pressed(global.input_k[8]))
    {
        snd_play(snd_smallswing);
        dpop_history();
    }
    
    if (dhinter_active)
    {
        if (dmenu_state == ""warp_options"")
        {
            new_room = drooms_options.target_room;
            
            if (new_room == -1)
                new_room = room;
            
            dhinter_text = scr_dmode_get_text(""hint_room"") + room_get_name(new_room);
        }
        
        if (scr_array_contains(ditem_types, dmenu_state))
        {
            if (dhorizontal_page == 0 && dbutton_selected == 1)
            {
                dhinter_text = scr_dmode_get_text(""hint_press"") + scr_get_input_name(4) + scr_dmode_get_text(""hint_change_chap"");
            }
            else if (dhorizontal_page == 0 && dbutton_selected > 1)
            {
                var hover_id = dbutton_indices[dbutton_selected - 1];
                
                if (hover_id != -1)
                {
                    var raw_desc = """";
                    
                    switch (dmenu_state)
                    {
                        case ""objects"":
                            scr_iteminfo(hover_id);
                            raw_desc = itemdescb;
                            break;
                        
                        case ""armors"":
                            scr_armorinfo(hover_id);
                            raw_desc = armordesctemp;
                            break;
                        
                        case ""weapons"":
                            scr_weaponinfo(hover_id);
                            raw_desc = weapondesctemp;
                            break;
                        
                        case ""keyitems"":
                            scr_keyiteminfo(hover_id);
                            raw_desc = tempkeyitemdesc;
                            break;
                    }
                    
                    dhinter_text = string_replace_all(raw_desc, ""#"", "" "");
                    var max_w = (menu_width - (x_padding * 2)) * d;
                    var line_sep = 18 * d;
                    var max_h = line_sep * 2;
                    
                    if (string_height_ext(dhinter_text, line_sep, max_w) > max_h)
                    {
                        while (string_height_ext(dhinter_text + ""..."", line_sep, max_w) > max_h && string_length(dhinter_text) > 0)
                            dhinter_text = string_delete(dhinter_text, string_length(dhinter_text), 1);
                        
                        dhinter_text += ""..."";
                    }
                }
                else
                {
                    dhinter_text = ""---"";
                }
            }
            else
            {
                dhinter_text = """";
            }
        }
    }
}

if ((dmenu_active == 1 && dmenu_state == ""debug"" && global.darkzone == 1) || dkeys_helper == 1)
{
    if (keyboard_check_pressed(ord(""M"")))
    {
        if (dkeys_helper == 0)
            snd_play(snd_select);
        else
            snd_play(snd_smallswing);
        
        dkeys_helper = !dkeys_helper;
    }
}
");
importGroup.QueueReplace(obj_dmenu_system.EventHandlerFor(EventType.Step, (uint)1, Data), @"
function dmenu_state_update()
{
    switch (dmenu_state)
    {
        case ""debug"":
            dmenu_title = scr_dmode_get_text(""menu_debug"");
            dbutton_options = dbutton_options_original;
            dmenu_box = 0;
            dbutton_layout = 0;
            break;
        
        case ""warp"":
            dmenu_title = scr_dmode_get_text(""room_list"");
            dbutton_options = [scr_dmode_get_text(""btn_current_room""), scr_dmode_get_text(""btn_search"")];
            dbutton_indices = [-1, -1];
            
            if (global.dreading_custom_flag || dkeyboard_input != """")
                dbutton_options[1] = scr_dmode_get_text(""ui_contains"");
            else
                dbutton_options[1] = scr_dmode_get_text(""btn_search"");
            
            dbutton_options[1] += dkeyboard_input;
            
            for (var i = 0; i < array_length(drooms); i++)
            {
                if (!string_pos(dkeyboard_input, drooms[i]))
                    continue;
                
                var combined = drooms[i];
                array_push(dbutton_options, combined);
                array_push(dbutton_indices, drooms_id[i].room_index);
            }
            
            dbutton_layout = 1;
            dmenu_box = 1;
            break;
        
        case ""warp_options"":
            dmenu_title = scr_dmode_get_text(""warp_options"");
            dbutton_options = [
                scr_dmode_get_text(""btn_cancel""), 
                scr_dmode_get_text(""ui_is_darkworld""), 
                scr_dmode_get_text(""ui_plot_value""), 
                scr_dmode_get_text(""ui_teammate2""), 
                scr_dmode_get_text(""ui_teammate3""), 
                scr_dmode_get_text(""btn_warp"")
            ];
            dbutton_indices = [0, 1, 2, 3, 4, 5];
            dbutton_options[1] += drooms_options.target_is_darkzone ? scr_dmode_get_text(""opt_yes"") : scr_dmode_get_text(""opt_no"");
            
            if (global.dreading_custom_flag)
                dbutton_options[2] += dkeyboard_input;
            else
                dbutton_options[2] += string(drooms_options.target_plot);
            
            teammates = [scr_dmode_get_text(""ui_nobody""), ""Kris"", ""Susie"", ""Ralsei"", ""Noëlle""];
            dbutton_options[3] += teammates[drooms_options.target_member_2];
            dbutton_options[4] += teammates[drooms_options.target_member_3];
            break;
        
        case ""give"":
            dmenu_title = scr_dmode_get_text(""item_type"");
            dbutton_options = [
                scr_dmode_get_text(""type_items""), 
                scr_dmode_get_text(""type_armors""), 
                scr_dmode_get_text(""type_weapons""), 
                scr_dmode_get_text(""type_keyitems"")
            ];
            dmenu_box = 0;
            dbutton_layout = 0;
            break;
        
        case ""objects"":
            dmenu_title = scr_dmode_get_text(""item_list"");
            dbutton_options = [scr_dmode_get_text(""ui_chapter"")];
            dbutton_indices = [-1];
            dbutton_options[0] += string(ditem_chap);
            var max_len = 33;
            var _data = ditem_index_data(ditem_chap);
            var my_start = _data.start_id;
            var my_count = _data.count;
            
            if (dhorizontal_page == 0)
            {
                for (var i = my_start; i < (my_start + my_count); i++)
                {
                    scr_iteminfo(i);
                    var cleaned_desc = string_replace_all(itemdescb, ""#"", "" "");
                    var combined = itemnameb;
                    
                    if (string_length(combined) > max_len)
                        combined = string_copy(combined, 1, max_len - 3) + ""..."";
                    
                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, i);
                }
            }
            else
            {
                array_delete(dbutton_options, 0, 1);
                array_delete(dbutton_indices, 0, 1);
                
                for (var i = 0; i < array_length(dlight_objects); i++)
                {
                    scr_litemcheck(dlight_objects[i][0]);
                    var combined = dlight_objects[i][1] + "" - "" + string(itemcount) + "" "" + scr_dmode_get_text(""ui_held"");
                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, dlight_objects[i][0]);
                }
            }
            
            dmenu_box = 2;
            dbutton_layout = 1;
            break;
        
        case ""armors"":
            dmenu_title = scr_dmode_get_text(""armor_list"");
            dbutton_options = [scr_dmode_get_text(""ui_chapter"")];
            dbutton_indices = [-1];
            dbutton_options[0] += string(ditem_chap);
            var max_len = 33;
            var _data = darmor_index_data(ditem_chap);
            var my_start = _data.start_id;
            var my_count = _data.count;
            
            if (dhorizontal_page == 0)
            {
                for (var i = my_start; i < (my_start + my_count); i++)
                {
                    scr_armorinfo(i);
                    var cleaned_desc = string_replace_all(armordesctemp, ""#"", "" "");
                    var combined = armornametemp;
                    
                    if (string_length(combined) > max_len)
                        combined = string_copy(combined, 1, max_len - 3) + ""..."";
                    
                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, i);
                }
            }
            else
            {
                array_delete(dbutton_options, 0, 1);
                array_delete(dbutton_indices, 0, 1);
                
                for (var i = 0; i < array_length(dlight_armors); i++)
                {
                    var combined = dlight_armors[i][1];
                    
                    if (global.larmor == dlight_armors[i][0])
                        combined += "" ("" + scr_dmode_get_text(""ui_equipped"") + "")"";
                    
                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, i);
                }
            }
            
            dmenu_box = 2;
            dbutton_layout = 1;
            break;
        
        case ""weapons"":
            dmenu_title = scr_dmode_get_text(""weapon_list"");
            dbutton_options = [scr_dmode_get_text(""ui_chapter"")];
            dbutton_indices = [-1];
            dbutton_options[0] += string(ditem_chap);
            var max_len = 33;
            var _data = dweapon_index_data(ditem_chap);
            var my_start = _data.start_id;
            var my_count = _data.count;
            
            if (dhorizontal_page == 0)
            {
                for (var i = my_start; i < (my_start + my_count); i++)
                {
                    scr_weaponinfo(i);
                    var cleaned_desc = string_replace_all(weapondesctemp, ""#"", "" "");
                    var combined = weaponnametemp;
                    
                    if (string_length(combined) > max_len)
                        combined = string_copy(combined, 1, max_len - 3) + ""..."";
                    
                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, i);
                }
            }
            else
            {
                array_delete(dbutton_options, 0, 1);
                array_delete(dbutton_indices, 0, 1);
                
                for (var i = 0; i < array_length(dlight_weapons); i++)
                {
                    var combined = dlight_weapons[i][1];
                    
                    if (global.lweapon == dlight_weapons[i][0])
                        combined += "" ("" + scr_dmode_get_text(""ui_equipped"") + "")"";
                    
                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, i);
                }
            }
            
            dmenu_box = 2;
            dbutton_layout = 1;
            break;
        
        case ""keyitems"":
            dmenu_title = scr_dmode_get_text(""keyitem_list"");
            dbutton_options = [scr_dmode_get_text(""ui_chapter"")];
            dbutton_indices = [-1];
            dbutton_options[0] += string(ditem_chap);
            var max_len = 33;
            var _data = dkeyitem_index_data(ditem_chap);
            var my_start = _data.start_id;
            var my_count = _data.count;
            
            for (var i = my_start; i < (my_start + my_count); i++)
            {
                scr_keyiteminfo(i);
                var cleaned_desc = string_replace_all(tempkeyitemdesc, ""#"", "" "");
                var combined = tempkeyitemname;
                
                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + ""..."";
                
                array_push(dbutton_options, combined);
                array_push(dbutton_indices, i);
            }
            
            dmenu_box = 2;
            dbutton_layout = 1;
            break;
        
        case ""givertab"":
            dmenu_title = scr_dmode_get_text(""add_how_many"");
            dgiver_amount = 1;
            dmenu_box = 0;
            dbutton_layout = 2;
            break;
        
        case ""recruits"":
            dmenu_title = scr_dmode_get_text(""recruit_list"");
            dbutton_options = [scr_dmode_get_text(""btn_presets"")];
            dbutton_indices = [scr_dmode_get_text(""btn_presets"")];
            var max_len = 40;
            
            if (dhorizontal_page != 0)
            {
                dbutton_options[0] = "" "" + dbutton_options[0];
                dbutton_indices[0] = "" "" + dbutton_indices[0];
            }
            
            if (dhorizontal_page != 0)
            {
                var test_lst = scr_get_chapter_recruit_data(dhorizontal_page);
                
                for (var i = 0; i < array_length(test_lst); i++)
                {
                    var enemy_id = test_lst[i];
                    scr_recruit_info(enemy_id);
                    var combined = _name + "" - ["" + string(max(floor(global.flag[enemy_id + 600] * _recruitcount), -1)) + ""/"" + string(_recruitcount) + ""]"";
                    
                    if (string_length(combined) > max_len)
                        combined = string_copy(combined, 1, max_len - 3) + ""..."";
                    
                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, enemy_id);
                }
            }
            
            if (dhorizontal_page != 0)
                dmenu_box = 1;
            else
                dmenu_box = 0;
            
            dbutton_layout = 1;
            break;
        
        case ""recruit_presets"":
            dmenu_title = scr_dmode_get_text(""recruit_presets"");
            dbutton_options = [scr_dmode_get_text(""btn_recruit_all""), scr_dmode_get_text(""btn_lose_all"")];
            
            if (dhorizontal_page)
            {
                dmenu_title += ("" ("" + scr_dmode_get_text(""ui_chap_short"") + "" "" + string(dhorizontal_page) + "")"");
                dbutton_options[0] += "" "" + scr_dmode_get_text(""ui_of_chapter"") + "" "" + string(dhorizontal_page);
                dbutton_options[1] += "" "" + scr_dmode_get_text(""ui_of_chapter"") + "" "" + string(dhorizontal_page);
            }
            
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case ""flag_categories"":
            dmenu_title = scr_dmode_get_text(""misc"");
            dbutton_options = [];
            dbutton_indices = [-1];
            categories_len = array_length(dother_categories);
            var max_len = 40;
            
            if (!global.dreading_custom_flag)
                array_push(dbutton_options, scr_dmode_get_text(""ui_custom""));
            else
                array_push(dbutton_options, ""global.flag["" + dcustom_flag_text[0] + ""] = |"" + dcustom_flag_text[1] + ""|"");
            
            for (var i = 0; i < categories_len; i++)
            {
                if (dflag_categories_len[i] == 0)
                    continue;
                
                array_push(dbutton_options, dother_categories[i]);
                array_push(dbutton_indices, i);
            }
            
            dmenu_box = 1;
            dbutton_layout = 1;
            break;
        
        case ""flag_misc"":
            dmenu_title = scr_dmode_get_text(""misc"");
            dbutton_options = [];
            dbutton_indices = [];
            other_len = array_length(dother_options);
            var max_len = 40;
            
            for (var i = 0; i < other_len; i++)
            {
                cur_option = dother_options[i];
                flag_number = global.flag[cur_option[2]];
                var combined = cur_option[1] + "" - "" + scr_dmode_get_text(""ui_problem"");
                
                if (i == (dbutton_selected - 1))
                    option_index = dhorizontal_index;
                else
                    option_index = find_subarray_index(cur_option[2], cur_option[3]);
                
                combined = cur_option[1] + "" -  "" + cur_option[3][option_index][0];
                
                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + ""..."";
                
                array_push(dbutton_options, combined);
                array_push(dbutton_indices, i);
            }
            
            dmenu_box = 2;
            dbutton_layout = 1;
            break;
        
        default:
            dmenu_title = scr_dmode_get_text(""menu_unknown"");
            dbutton_options = [];
            dmenu_box = 0;
            dbutton_layout = 0;
    }
}

function dmenu_state_interact()
{
    switch (dmenu_state)
    {
        case ""debug"":
            if (dbutton_selected == 1)
            {
                dmenu_state = ""warp"";
                dhorizontal_index = 0;
                dkeyboard_input = """";
                drooms_options.target_plot = global.plot;
                drooms_options.target_is_darkzone = global.darkzone;
                dbutton_selected = 1;
            }
            
            if (dbutton_selected == 2)
            {
                dmenu_state = ""give"";
                dbutton_selected = 1;
            }
            
            if (dbutton_selected == 3)
            {
                if (global.chapter >= 3)
                    dmenu_state = ""recruits"";
                else
                    dmenu_state = ""flag_categories"";
                
                dhorizontal_page = 0;
                dbutton_selected = 1;
            }
            
            if (dbutton_selected == 4)
            {
                dmenu_state = ""flag_categories"";
                dbutton_selected = 1;
            }
            
            break;
        
        case ""warp"":
            if (dbutton_selected == 2)
            {
                global.dreading_custom_flag = 1;
                keyboard_string = """";
                dkeyboard_input = """";
                dmenu_state_update();
            }
            else
            {
                drooms_options.target_room = -1;
                
                if (dbutton_selected != 1)
                    drooms_options.target_room = dbutton_indices[dbutton_selected - 1];
                
                drooms_options.target_plot = global.plot;
                drooms_options.target_is_darkzone = global.darkzone;
                drooms_options.target_member_2 = global.char[1];
                drooms_options.target_member_3 = global.char[2];
                dmenu_state = ""warp_options"";
                dkeyboard_input = """";
            }
            
            break;
        
        case ""warp_options"":
            if (dbutton_selected == 1)
            {
                dkeyboard_input = """";
                dmenu_state = ""warp"";
            }
            else if (dbutton_selected == 2)
            {
                drooms_options.target_is_darkzone ^= 1;
            }
            else if (dbutton_selected == 3)
            {
                global.dreading_custom_flag = 1;
                keyboard_string = """";
            }
            else if (dbutton_selected == 6)
            {
                new_room = drooms_options.target_room;
                
                if (new_room == -1)
                    new_room = room;
                
                global.plot = drooms_options.target_plot;
                global.darkzone = drooms_options.target_is_darkzone;
                global.char[1] = drooms_options.target_member_2;
                global.char[2] = drooms_options.target_member_3;
                global.interact = 0;
                dmenu_active = false;
                room_goto(new_room);
            }
            
            break;
        
        case ""give"":
            if (dbutton_selected == 1)
                dmenu_state = ""objects"";
            else if (dbutton_selected == 2)
                dmenu_state = ""armors"";
            else if (dbutton_selected == 3)
                dmenu_state = ""weapons"";
            else if (dbutton_selected == 4)
                dmenu_state = ""keyitems"";
            
            dhorizontal_page = !global.darkzone;
            
            if (dbutton_selected == 4)
                dhorizontal_page = 0;
            
            dbutton_selected = 1;
            break;
        
        case ""objects"":
            if (dhorizontal_page == 0 && dbutton_selected == 1)
            {
                ditem_chap += 1;
                
                if (ditem_chap > global.chapter)
                    ditem_chap = 1;
            }
            else
            {
                dgiver_menu_state = dmenu_state;
                dbutton_selected = clamp(dbutton_selected, 0, array_length(dbutton_options));
                dgiver_button_selected = dbutton_selected;
                dmenu_state = ""givertab"";
                dbutton_selected = 1;
            }
            
            break;
        
        case ""armors"":
            if (dhorizontal_page == 1)
            {
                global.larmor = dlight_armors[dbutton_selected - 1][0];
                break;
            }
            
            if (dhorizontal_page == 0 && dbutton_selected == 1)
            {
                ditem_chap += 1;
                
                if (ditem_chap > global.chapter)
                    ditem_chap = 1;
            }
            else
            {
                dgiver_menu_state = dmenu_state;
                dgiver_button_selected = dbutton_selected;
                dmenu_state = ""givertab"";
                dbutton_selected = 1;
            }
            
            break;
        
        case ""weapons"":
            if (dhorizontal_page == 1)
            {
                global.lweapon = dlight_weapons[dbutton_selected - 1][0];
                break;
            }
            
            if (dhorizontal_page == 0 && dbutton_selected == 1)
            {
                ditem_chap += 1;
                
                if (ditem_chap > global.chapter)
                    ditem_chap = 1;
            }
            else
            {
                dgiver_menu_state = dmenu_state;
                dgiver_button_selected = dbutton_selected;
                dmenu_state = ""givertab"";
                dbutton_selected = 1;
            }
            
            break;
        
        case ""keyitems"":
            if (dbutton_selected == 1)
            {
                ditem_chap += 1;
                
                if (ditem_chap > global.chapter)
                    ditem_chap = 1;
            }
            else
            {
                dgiver_menu_state = dmenu_state;
                dgiver_button_selected = dbutton_selected;
                dmenu_state = ""givertab"";
                dbutton_selected = 1;
            }
            
            break;
        
        case ""givertab"":
            if (dgiver_amount == 0)
            {
                scr_debug_print(scr_dmode_get_text(""msg_cancelled""));
                break;
            }
            if (dgiver_menu_state == ""objects"")
            {
                real_index = dbutton_indices[dgiver_button_selected - 1];
                
                for (var i = 0; i < abs(dgiver_amount); i++)
                {
                    if (dgiver_amount < 0)
                    {
                        if (dhorizontal_page == 0)
                            scr_itemremove(real_index);
                        else
                            scr_litemremove(real_index);
                    }
                    else if (dhorizontal_page == 0)
                    {
                        scr_itemget(real_index);
                    }
                    else
                    {
                        scr_litemget(real_index);
                    }
                }
                
                if (dgiver_amount < 0)
                    scr_debug_print(string(abs(dgiver_amount)) + "" "" + dgiver_bname + scr_dmode_get_text(""msg_removed_inv""));
                else
                    scr_debug_print(string(dgiver_amount) + "" "" + dgiver_bname + scr_dmode_get_text(""msg_added_inv""));
            }
            
            if (dgiver_menu_state == ""armors"")
            {
                if (dgiver_amount > 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];
                    
                    for (var i = 0; i < dgiver_amount; i++)
                        scr_armorget(real_index);
                    
                    scr_debug_print(string(dgiver_amount) + "" "" + dgiver_bname + scr_dmode_get_text(""msg_added_inv""));
                }
                else if (dgiver_amount < 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];
                    
                    for (var i = 0; i < abs(dgiver_amount); i++)
                        scr_armorremove(real_index);
                    
                    scr_debug_print(string(abs(dgiver_amount)) + "" "" + dgiver_bname + scr_dmode_get_text(""msg_removed_inv""));
                }
            }
            
            if (dgiver_menu_state == ""weapons"")
            {
                if (dgiver_amount > 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];
                    
                    for (var i = 0; i < dgiver_amount; i++)
                        scr_weaponget(real_index);
                    
                    scr_debug_print(string(dgiver_amount) + "" "" + dgiver_bname + scr_dmode_get_text(""msg_added_inv""));
                }
                else if (dgiver_amount < 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];
                    
                    for (var i = 0; i < abs(dgiver_amount); i++)
                        scr_weaponremove(real_index);
                    
                    scr_debug_print(string(abs(dgiver_amount)) + "" "" + dgiver_bname + scr_dmode_get_text(""msg_removed_inv""));
                }
            }
            
            if (dgiver_menu_state == ""keyitems"")
            {
                if (dgiver_amount > 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];
                    
                    for (var i = 0; i < dgiver_amount; i++)
                        scr_keyitemget(real_index);
                    
                    scr_debug_print(string(dgiver_amount) + "" "" + dgiver_bname + scr_dmode_get_text(""msg_added_inv""));
                }
                else if (dgiver_amount < 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];
                    
                    for (var i = 0; i < abs(dgiver_amount); i++)
                        scr_keyitemremove(real_index);
                    
                    scr_debug_print(string(abs(dgiver_amount)) + "" "" + dgiver_bname + scr_dmode_get_text(""msg_removed_inv""));
                }
            }
            
            dpop_history();
            dmenu_active = false;
            global.interact = 0;
            break;
        
        case ""flag_categories"":
            if (dbutton_selected > 1)
            {
                dother_options = [];
                real_index = dbutton_indices[dbutton_selected - 1];
                
                for (var i = 0; i < array_length(dother_all_options); i++)
                {
                    options = dother_all_options[i];
                    
                    if (options[0] == real_index)
                        array_push(dother_options, options);
                }
                
                dhorizontal_index = find_subarray_index(dother_options[0][2], dother_options[0][3]);
                dmenu_state = ""flag_misc"";
                dbutton_selected = 1;
            }
            
            break;
        
        case ""flag_misc"":
            break;
        
        case ""recruits"":
            if (dbutton_selected == 1)
            {
                dmenu_state = ""recruit_presets"";
                dbutton_selected = 1;
            }
            
            break;
        
        case ""recruit_presets"":
            for (var c = 1; c <= global.chapter; c++)
            {
                if (dhorizontal_page != 0)
                    c = dhorizontal_page;
                
                var test_lst = scr_get_chapter_recruit_data(c);
                
                for (var i = 0; i < array_length(test_lst); i++)
                {
                    var enemy_id = test_lst[i];
                    scr_recruit_info(enemy_id);
                    
                    if (dbutton_selected == 1)
                        global.flag[enemy_id + 600] = 1;
                    else
                        global.flag[enemy_id + 600] = -1;
                }
                
                if (dhorizontal_page != 0)
                    break;
            }
            
            if (dbutton_selected == 1)
                snd_play(snd_pirouette);
            else
                snd_play(snd_weirdeffect);
            
            dpop_history();
            break;
        
        default:
            snd_play(snd_error);
            scr_debug_print(scr_dmode_get_text(""msg_invalid""));
    }
}
");
importGroup.QueueReplace(obj_dmenu_system.EventHandlerFor(EventType.Draw, (uint)0, Data), @"
xx = __view_get(e__VW.XView, 0);
yy = __view_get(e__VW.YView, 0);
d = global.darkzone + 1;

if (!global.dreading_custom_flag && keyboard_check_pressed(ord(""D"")))
{
    dmenu_active = !dmenu_active;
    
    if (dmenu_active)
    {
        dmenu_previous_interact = global.interact;
        snd_play(snd_egg);
        global.interact = 1;
    }
    else
    {
        snd_play(snd_smallswing);
        global.interact = dmenu_previous_interact;
    }
}

if (dmenu_box == 0)
{
    menu_width = 214;
    menu_length = 94;
    xcenter = 160;
    ycenter = 105;
}

if (dmenu_box == 1)
{
    menu_width = 214;
    menu_length = 154;
    xcenter = 160;
    ycenter = 135;
}

if (dmenu_box == 2)
{
    menu_width = 256;
    menu_length = 154;
    xcenter = 160;
    ycenter = 135;
}

var x_start = 0;

if (dbutton_layout == 0)
{
    x_padding = 7;
    y_start = 60 * d;
    x_spacing = 10 * d;
    y_spacing = 10 * d;
    x_start = ((xcenter - (menu_width / 2)) + x_padding) * d;
}

if (dbutton_layout == 1)
{
    x_padding = 7;
    y_start = 95 * d;
    x_spacing = 10 * d;
    y_spacing = 20 * d;
    x_start = ((xcenter - (menu_width / 2)) + x_padding) * d;
}

if (dbutton_layout == 2)
{
    x_padding = 7;
    y_start = 95 * d;
    x_start = ((xcenter - (menu_width / 2)) + x_padding) * d;
}

var button_count = array_length(dbutton_options);

if (dmenu_active)
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
    draw_text(x_start + xx, (((ycenter - (menu_length / 2)) + 8) * d) + yy, string(dmenu_title));
    
    if (dmenu_state == ""debug"" && global.darkzone == 1)
    {
        draw_set_font(fnt_main);
        draw_set_color(c_gray);
        var draw_x = x_start + (335 * (d / 2)) + xx;
        var draw_y = (((ycenter - (menu_length / 2)) + 82) * d) + yy;
        draw_text(draw_x, draw_y, string(scr_dmode_get_text(""ui_m_keys"")));
        draw_set_font(fnt_mainbig);
    }
    
    if (global.dreading_custom_flag)
    {
        draw_set_halign(fa_right);
        draw_set_color(c_gray);
        var right_border = (xcenter + (menu_width / 2)) * d;
        var padding = 8 * d;
        var draw_x = (right_border + xx) - padding;
        var draw_y = (((ycenter - (menu_length / 2)) + 8) * d) + yy;
        draw_text(draw_x, draw_y, string(scr_dmode_get_text(""ui_esc_cancel"")));
        draw_set_halign(fa_left);
    }
    
    if (global.dreading_custom_flag)
    {
        if (dmenu_state == ""flag_categories"")
        {
            var base_x = x_start + xx;
            var base_y = (((110 - (dmenu_start_index * 20)) + 2) * d) + yy;
            var mono_spacing = (global.darkzone == 1) ? 15 : 8;
            var thickness = 1 * d;
            var visual_offset = -5;
            var cursor_padding = 3 * d;
            var w_prefix = string_length(""global.flag["") * mono_spacing;
            var w_name = string_length(string(dcustom_flag_text[0])) * mono_spacing;
            var w_middle = string_length(""] = |"") * mono_spacing;
            var w_value = string_length(string(dcustom_flag_text[1])) * mono_spacing;
            var x1_start = base_x + w_prefix;
            var x2_start = x1_start + w_name + w_middle;
            draw_set_color(c_yellow);
            var draw_w_name = (w_name == 0) ? (mono_spacing / 4) : w_name;
            var draw_w_value = (w_value == 0) ? (mono_spacing / 4) : w_value;
            
            if (dhorizontal_index == 0)
                draw_rectangle((x1_start + visual_offset) - cursor_padding, base_y, x1_start + draw_w_name + visual_offset + cursor_padding, base_y + thickness, false);
            else if (dhorizontal_index == 1)
                draw_rectangle((x2_start + visual_offset) - cursor_padding - 2, base_y, (x2_start + draw_w_value + visual_offset + cursor_padding) - 2, base_y + thickness, false);
        }
        else if (dmenu_state == ""warp"")
        {
            var base_x = x_start + xx;
            var base_y = (((130 - (dmenu_start_index * 20)) + 2) * d) + yy;
            var mono_spacing = (global.darkzone == 1) ? 15 : 8;
            var thickness = 1 * d;
            var visual_offset = -2;
            var cursor_padding = 3 * d;
            var w_prefix = string_length(string(scr_dmode_get_text(""ui_contains""))) * mono_spacing;
            var w_name = string_length(string(dcustom_flag_text[0])) * mono_spacing;
            var x1_start = base_x + w_prefix;
            var x2_start = x1_start + w_name;
            draw_set_color(c_yellow);
            var draw_w_name = (w_name == 0) ? (mono_spacing / 4) : w_name;
            
            if (dhorizontal_index == 0)
                draw_rectangle((x1_start + visual_offset) - cursor_padding, base_y, x1_start + draw_w_name + visual_offset + cursor_padding, base_y + thickness, false);
        }
        else if (dmenu_state == ""warp_options"")
        {
            var base_x = x_start + xx;
            var base_y = (((150 - (dmenu_start_index * 20)) + 2) * d) + yy;
            var mono_spacing = (global.darkzone == 1) ? 15 : 8;
            var thickness = 1 * d;
            var visual_offset = -2;
            var cursor_padding = 3 * d;
            var w_prefix = string_length(string(scr_dmode_get_text(""ui_plot_value""))) * mono_spacing;
            var w_name = string_length(string(dcustom_flag_text[0])) * mono_spacing;
            var x1_start = base_x + w_prefix;
            var x2_start = x1_start + w_name;
            draw_set_color(c_yellow);
            var draw_w_name = (w_name == 0) ? (mono_spacing / 4) : w_name;
            
            if (dhorizontal_index == 0)
                draw_rectangle((x1_start + visual_offset) - cursor_padding, base_y, x1_start + draw_w_name + visual_offset + cursor_padding, base_y + thickness, false);
        }
    }
    
    if (dbutton_layout == 0)
    {
        for (var i = 0; i < button_count; i++)
        {
            var cur_btn = string(dbutton_options[i]);
            var text_width = string_width(cur_btn);
            draw_set_color((dbutton_selected == (i + 1)) ? c_yellow : c_white);
            draw_text(x_start + xx, (100 * d) + yy, cur_btn);
            x_start += (text_width + x_spacing);
        }
    }
    
    side_arrows_mult = (global.darkzone == 1) ? [23, 10] : [12, 5];
    var dmenu_arrow_yoffset, darrow_scale;
    
    if (dbutton_layout == 1)
    {
        var dcan_scroll_up = dmenu_start_index > 0;
        var dcan_scroll_down = (dmenu_start_index + dbutton_max_visible) < array_length(dbutton_options);
        dmenu_arrow_yoffset = 2 * sin(dmenu_arrow_timer / 10);
        darrow_scale = d / 2;
        
        for (var i = 0; i < dbutton_max_visible; i++)
        {
            var button_index = dmenu_start_index + i;
            
            if (button_index < array_length(dbutton_options))
            {
                is_cur_line = dbutton_selected == (button_index + 1);
                var text_color = is_cur_line ? c_yellow : c_white;
                draw_set_color(text_color);
                
                var cur_btn = string(dbutton_options[button_index]);
                draw_monospace(x_start + xx, y_start + yy + (i * y_spacing), cur_btn);
                var mono_spacing = (global.darkzone == 1) ? 15 : 8;
                
                if ((is_cur_line && dmenu_state == ""flag_misc"") || (dmenu_state == ""warp_options"" && (button_index == 3 || button_index == 4)))
                {
                    if ((dmenu_state == ""flag_misc"" && dhorizontal_index != 0) || (dmenu_state == ""warp_options"" && array_get([drooms_options.target_member_2, drooms_options.target_member_3], button_index - 3) != 0))
                    {
                        for (dash_pos = 0; 1; dash_pos++)
                        {
                            if (dash_pos > 4 && string_char_at(cur_btn, dash_pos) == ((dmenu_state == ""flag_misc"") ? ""-"" : "":""))
                                break;
                        }
                        
                        dash_pos++;
                        draw_sprite_ext(spr_morearrow, 0, x_start + xx + ((dash_pos * mono_spacing) + floor(mono_spacing / 2)) + dmenu_arrow_yoffset, y_start + yy + (i * y_spacing) + side_arrows_mult[0], darrow_scale, -darrow_scale, 90, c_white, 1);
                    }
                    
                    if ((dmenu_state == ""flag_misc"" && dhorizontal_index < (array_length(dother_options[dbutton_selected - 1][3]) - 1)) || (dmenu_state == ""warp_options"" && array_get([drooms_options.target_member_2, drooms_options.target_member_3], button_index - 3) != (4 - (global.chapter == 1))))
                        draw_sprite_ext(spr_morearrow, 0, (x_start + xx + ((string_length(cur_btn) + 1) * mono_spacing)) - floor(mono_spacing / 2) - dmenu_arrow_yoffset, y_start + yy + (i * y_spacing) + side_arrows_mult[1], darrow_scale, -darrow_scale, 270, c_white, 1);
                }
                else if (dmenu_state == ""recruits"" && button_index == 0)
                {
                    if (dhorizontal_page != 0)
                        draw_sprite_ext(spr_morearrow, 0, x_start + xx + floor(mono_spacing / 2) + dmenu_arrow_yoffset, y_start + yy + (i * y_spacing) + side_arrows_mult[0], darrow_scale, -darrow_scale, 90, c_white, 1);
                    
                    if (dhorizontal_page != global.chapter)
                        draw_sprite_ext(spr_morearrow, 0, (x_start + xx + ((string_length(cur_btn) + 1) * mono_spacing)) - floor(mono_spacing / 2) - dmenu_arrow_yoffset, y_start + yy + (i * y_spacing) + side_arrows_mult[1], darrow_scale, -darrow_scale, 270, c_white, 1);
                }
            }
        }
        
        draw_set_color(c_white);
        
        if (dcan_scroll_up)
            draw_sprite_ext(spr_morearrow, 0, x_start + xx, y_start + yy + (dbutton_max_visible * (y_spacing * -0.03)) + dmenu_arrow_yoffset, darrow_scale, -darrow_scale, 0, c_white, 1);
        
        if (dcan_scroll_down)
            draw_sprite_ext(spr_morearrow, 0, x_start + xx, (y_start + yy + (dbutton_max_visible * y_spacing)) - dmenu_arrow_yoffset, darrow_scale, darrow_scale, 0, c_white, 1);
    }
    
    if (dmenu_state == ""recruits"" || dmenu_state == ""weapons"" || dmenu_state == ""armors"" || dmenu_state == ""objects"")
    {
        draw_set_halign(fa_right);
        var draw_y = (((ycenter - (menu_length / 2)) + 8) * d) + yy;
        var draw_x = x_start + xx + 200;
        
        if (global.darkzone)
            draw_x += 200;
        
        if (dmenu_state == ""recruits"")
        {
            if (dhorizontal_page != 0)
                draw_text(draw_x, draw_y, ""("" + string(scr_dmode_get_text(""ui_chap_short"")) + "" "" + string(dhorizontal_page) + "")"");
            else
                draw_text(draw_x, draw_y, string(scr_dmode_get_text(""ui_chap_all"")));
        }
        else if (dhorizontal_page == 0)
        {
            draw_text(draw_x + 30 + (global.darkzone * 30), draw_y, string(scr_dmode_get_text(""ui_world_dark"")));
            draw_sprite_ext(spr_morearrow, 0, draw_x + 35 + (global.darkzone * 35) + dmenu_arrow_yoffset, draw_y + side_arrows_mult[1], darrow_scale, -darrow_scale, 270, c_white, 1);
        }
        else
        {
            draw_text(draw_x + 30 + (global.darkzone * 30), draw_y, string(scr_dmode_get_text(""ui_world_light"")));
            draw_sprite_ext(spr_morearrow, 0, draw_x + -55 + (global.darkzone * -55) + dmenu_arrow_yoffset, draw_y + side_arrows_mult[0], darrow_scale, -darrow_scale, 90, c_white, 1);
        }
        
        draw_set_halign(fa_left);
    }
    
    if (dbutton_layout == 2)
    {
        dmenu_arrow_yoffset = 2 * sin(dmenu_arrow_timer / 10);
        draw_set_color(c_yellow);
        draw_text(((xcenter - (string_length(string(dgiver_amount)) * 4)) * d) + xx, (ycenter * d) + yy, string(dgiver_amount));
        draw_set_color(c_white);
        var itemreminder = """";
        
        if (dgiver_menu_state == ""objects"")
        {
            itemreminder = ""["" + string(dgiver_bname) + ""]"";
            
            if (dhorizontal_page == 0)
                scr_itemcheck(0);
            else
                scr_litemcheck(0);
            
            max_items = (dhorizontal_page == 0) ? 12 : 8;
            draw_text(x_start + xx, ((ycenter + 25) * d) + yy, string(scr_dmode_get_text(""ui_inv_items"")) + string(max_items - itemcount) + "" / "" + string(max_items));
        }
        
        if (dgiver_menu_state == ""armors"")
        {
            itemreminder = ""["" + string(dgiver_bname) + ""]"";
            scr_armorcheck_inventory(0);
            draw_text(x_start + xx, ((ycenter + 25) * d) + yy, string(scr_dmode_get_text(""ui_inv_armors"")) + string(48 - itemcount) + "" / 48"");
        }
        
        if (dgiver_menu_state == ""weapons"")
        {
            itemreminder = ""["" + string(dgiver_bname) + ""]"";
            scr_weaponcheck_inventory(0);
            draw_text(x_start + xx, ((ycenter + 25) * d) + yy, string(scr_dmode_get_text(""ui_inv_weapons"")) + string(48 - itemcount) + "" / 48"");
        }
        
        if (dgiver_menu_state == ""keyitems"")
        {
            itemreminder = ""["" + string(dgiver_bname) + ""]"";
            scr_keyitemcheck(0);
            draw_text(x_start + xx, ((ycenter + 25) * d) + yy, string(scr_dmode_get_text(""ui_inv_keyitems"")) + string(12 - itemcount) + "" / 12"");
        }
        
        var text_width = string_width(itemreminder);
        draw_text(((xcenter * d) - (text_width / 2)) + xx, ((ycenter - 22) * d) + yy, itemreminder);
        darrow_scale = d / 2;
        draw_sprite_ext(spr_morearrow, 0, ((xcenter - 15) * d) + xx + dmenu_arrow_yoffset, ((ycenter + 6) * d) + yy, darrow_scale, darrow_scale, 270, c_white, 1);
        draw_sprite_ext(spr_morearrow, 0, (((xcenter + 15) * d) + xx) - dmenu_arrow_yoffset, ((ycenter + 12) * d) + yy, darrow_scale, darrow_scale, 90, c_white, 1);
    }
    
    dhinter_active = true;
    
    if (dhinter_active && dhinter_text != """" && (scr_array_contains(ditem_types, dmenu_state) || dmenu_state == ""warp_options""))
    {
        draw_set_color(c_white);
        draw_rectangle(((xcenter - (menu_width / 2) - 3) * d) + xx, (2 * d) + yy, ((xcenter + (menu_width / 2) + 3) * d) + xx, (51 * d) + yy, false);
        draw_set_color(c_black);
        draw_rectangle(((xcenter - (menu_width / 2)) * d) + xx, (5 * d) + yy, ((xcenter + (menu_width / 2)) * d) + xx, (48 * d) + yy, false);
        draw_set_color(c_white);
        var x_start_desc = ((xcenter - (menu_width / 2)) + x_padding) * d;
        draw_text_ext(x_start_desc + xx, (10 * d) + yy, string(dhinter_text), 18 * d, (menu_width - (x_padding * 2)) * d);
    }
}

if (dkeys_helper == 1)
{
    dkeys_data = [
        string(scr_dmode_get_text(""key_0"")),
        string(scr_dmode_get_text(""key_1"")),
        string(scr_dmode_get_text(""key_2"")),
        string(scr_dmode_get_text(""key_3"")),
        string(scr_dmode_get_text(""key_4"")),
        string(scr_dmode_get_text(""key_5"")),
        string(scr_dmode_get_text(""key_6"")),
        string(scr_dmode_get_text(""key_7"")),
        string(scr_dmode_get_text(""key_8"")),
        string(scr_dmode_get_text(""key_9"")),
        string(scr_dmode_get_text(""key_10"")),
        string(scr_dmode_get_text(""key_11"")),
        string(scr_dmode_get_text(""key_12"")),
        string(scr_dmode_get_text(""key_13"")),
        string(scr_dmode_get_text(""key_14""))
    ];
    x_padding = 7;
    y_start = 50 * d;
    x_spacing = 10 * d;
    y_spacing = 10.5 * d;
    x_start = (((xcenter - (menu_width / 2)) + x_padding) * d) - 35;
    menu_width = 264;
    menu_length = 204;
    xcenter = 160;
    ycenter = 120;
    draw_set_color(c_white);
    draw_rectangle(((xcenter - (menu_width / 2) - 3) * d) + xx, ((ycenter - (menu_length / 2) - 3) * d) + yy, ((xcenter + (menu_width / 2) + 3) * d) + xx, ((ycenter + (menu_length / 2) + 3) * d) + yy, false);
    draw_set_color(c_black);
    draw_rectangle(((xcenter - (menu_width / 2)) * d) + xx, ((ycenter - (menu_length / 2)) * d) + yy, ((xcenter + (menu_width / 2)) * d) + xx, ((ycenter + (menu_length / 2)) * d) + yy, false);
    draw_set_font(fnt_mainbig);
    draw_set_halign(fa_right);
    draw_set_color(c_gray);
    var right_border = (xcenter + (menu_width / 2)) * d;
    var padding = 8 * d;
    var draw_x = (right_border + xx) - padding;
    var draw_y = (((ycenter - (menu_length / 2)) + 8) * d) + yy;
    draw_text(draw_x, draw_y, string(scr_dmode_get_text(""ui_m_close"")));
    draw_set_halign(fa_left);
    draw_set_color(c_white);
    draw_text(x_start + xx, (((ycenter - (menu_length / 2)) + 8) * d) + yy, string(scr_dmode_get_text(""ui_keys_title"")));
    
    for (var i = 0; i < array_length(dkeys_data); i++)
    {
        draw_set_font(fnt_main);
        draw_set_color(c_white);
        draw_text(x_start + xx, y_start + yy + (i * y_spacing), string(dkeys_data[i]));
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
");
ChangeSelection(obj_dmenu_system);

// Script scr_gamestart
UndertaleScript scr_gamestart = Data.Scripts.ByName("scr_gamestart");
importGroup.QueueAppend(scr_gamestart.Code, @"
global.debug = 0;
scr_dmode_init_lang();
");
ChangeSelection(scr_gamestart);

// Script scr_debug
UndertaleScript scr_debug = Data.Scripts.ByName("scr_debug");
importGroup.QueueReplace(scr_debug.Code, @"
function scr_debug()
{
    return global.debug;
}
");
ChangeSelection(scr_debug);

// GameObject obj_time
UndertaleGameObject obj_time = Data.GameObjects.ByName("obj_time");
importGroup.QueueAppend(obj_time.EventHandlerFor(EventType.Step, (uint)0, Data), @"
if (keyboard_check_pressed(vk_f10))
{
    global.debug = !global.debug;
    
    if (global.debug)
        scr_debug_print(scr_dmode_get_text(""dmode_activated""));
    else
        scr_debug_print(scr_dmode_get_text(""dmode_desactivated""));
}

if (scr_debug() && (!instance_number(obj_dmenu_system) || !global.dreading_custom_flag))
{
    if (keyboard_check_pressed(ord(""P"")))
    {
        if (room_speed == 30)
        {
            room_speed = 1;
            scr_debug_print(scr_dmode_get_text(""fps_1""));
        }
        else
        {
            room_speed = 30;
            scr_debug_print(scr_dmode_get_text(""fps_30""));
        }
    }
    
    if (keyboard_check_pressed(ord(""O"")))
    {
        if (room_speed == 120 || room_speed == 1)
        {
            room_speed = 30;
            scr_debug_print(scr_dmode_get_text(""fps_30""));
        }
        else if (room_speed == 60)
        {
            room_speed = 120;
            scr_debug_print(scr_dmode_get_text(""fps_120""));
        }
        else if (room_speed == 30)
        {
            room_speed = 60;
            scr_debug_print(scr_dmode_get_text(""fps_60""));
        }
    }
}
");
importGroup.QueueReplace(obj_time.EventHandlerFor(EventType.Draw, (uint)0, Data), @"
if (scr_debug())
{
    draw_set_font(fnt_main);
    draw_set_color(c_red);
    draw_text(__view_get(0, 0), __view_get(1, 0), fps);
    draw_set_font(fnt_main);
    draw_set_color(c_green);
    draw_text((__view_get(0, 0) + __view_get(2, 0)) - string_width(room_get_name(room)), __view_get(1, 0), room_get_name(room));
    draw_text((__view_get(0, 0) + __view_get(2, 0)) - string_width(""plot "" + string(global.plot)), __view_get(1, 0) + 15, ""plot "" + string(global.plot));
}
");
ChangeSelection(obj_time);

// GameObject obj_overworldc
UndertaleGameObject obj_overworldc = Data.GameObjects.ByName("obj_overworldc");
importGroup.QueueFindReplace(obj_overworldc.EventHandlerFor(EventType.Step, (uint)0, Data), @"if (scr_debug())", @"if (scr_debug() && (!instance_number(obj_dmenu_system) || !global.dreading_custom_flag))");
importGroup.QueueAppend(obj_overworldc.EventHandlerFor(EventType.Step, (uint)0, Data), @"
if (!instance_exists(obj_dmenu_system))
    instance_create(0, 0, obj_dmenu_system);
");
ChangeSelection(obj_overworldc);

// GameObject obj_battlecontroller
UndertaleGameObject obj_battlecontroller = Data.GameObjects.ByName("obj_battlecontroller");
importGroup.QueueFindReplace(obj_battlecontroller.EventHandlerFor(EventType.Step, (uint)0, Data), @"if (scr_debug())", @"if (0)");
importGroup.QueueAppend(obj_battlecontroller.EventHandlerFor(EventType.Step, (uint)0, Data), @"
if (scr_debug() && (!instance_number(obj_dmenu_system) || !global.dreading_custom_flag))
{
    if (keyboard_check_pressed(ord(""T"")))
    {
        if (global.tension < 250)
        {
            global.tension = 250;
            scr_debug_print(scr_dmode_get_text(""tp_250""));
        }
        else
        {
            global.tension = 0;
            scr_debug_print(scr_dmode_get_text(""tp_0""));
        }
    }
    
    if (keyboard_check_pressed(ord(""V"")))
        scr_turn_skip();
    
    if (keyboard_check_pressed(ord(""H"")))
    {
        scr_debug_fullheal();
        scr_debug_print(scr_dmode_get_text(""fullheal""));
    }
    
    if (keyboard_check_pressed(ord(""W"")))
    {
        scr_wincombat();
        scr_debug_print(scr_dmode_get_text(""fightwin""));
    }
}
");
ChangeSelection(obj_battlecontroller);

// GameObject obj_spritecomparer
UndertaleGameObject obj_spritecomparer = Data.GameObjects.ByName("obj_spritecomparer");
importGroup.QueueFindReplace(obj_spritecomparer.EventHandlerFor(EventType.Draw, (uint)0, Data), @"if (keyboard_check_pressed(ord(""D"")))", @"if (keyboard_check_pressed(vk_f2))");
importGroup.QueueAppend(obj_spritecomparer.EventHandlerFor(EventType.Draw, (uint)0, Data), @"

");
ChangeSelection(obj_spritecomparer);

// Script scr_debug_fullheal
UndertaleScript scr_debug_fullheal = Data.Scripts.ByName("scr_debug_fullheal");
importGroup.QueueReplace(scr_debug_fullheal.Code, @"
function scr_debug_fullheal()
{
    with (obj_dmgwriter)
    {
        if (delaytimer >= 1)
            killactive = 1;
    }
    
    scr_healallitemspell(999);
    
    for (i = 0; i < 3; i++)
    {
        with (global.charinstance[i])
            tu--;
    }
}
");
ChangeSelection(scr_debug_fullheal);

// Script scr_turn_skip
UndertaleScript scr_turn_skip = Data.Scripts.ByName("scr_turn_skip");
importGroup.QueueReplace(scr_turn_skip.Code, @"
function scr_turn_skip()
{
    if (global.turntimer > 0 && instance_exists(obj_growtangle) && scr_isphase(""bullets""))
    {
        global.turntimer = 0;
        scr_debug_print(scr_dmode_get_text(""turnskip""));
    }
}
");
ChangeSelection(scr_turn_skip);

// --- EXTRAS CHAPTER 2 ---

// GameObject obj_time
importGroup.QueueFindReplace(obj_time.EventHandlerFor(EventType.Step, (uint)1, Data), @"if (scr_debug())", @"if (0)");
importGroup.QueueAppend(obj_time.EventHandlerFor(EventType.Step, (uint)1, Data), @"
if (scr_debug())
{
    if (mouse_check_button_pressed(mb_middle))
        instance_create(0, 0, obj_debug_xy);
}
");
ChangeSelection(obj_time);

// GameObject obj_darkcontroller
UndertaleGameObject obj_darkcontroller = Data.GameObjects.ByName("obj_darkcontroller");
importGroup.QueueFindReplace(obj_darkcontroller.EventHandlerFor(EventType.Step, (uint)0, Data), @"if (scr_debug())", @"if (0)");
importGroup.QueueAppend(obj_darkcontroller.EventHandlerFor(EventType.Step, (uint)0, Data), @"
if (scr_debug() && (!instance_number(obj_dmenu_system) || !global.dreading_custom_flag))
{
    if (keyboard_check_pressed(ord(""2"")) && keyboard_check(ord(""M"")))
    {
        if (global.gold >= 100)
        {
            global.gold -= 100;
            scr_debug_print(""- 100 D$"");
        }
        else
        {
            scr_debug_print(""- "" + string(global.gold) + "" D$"");
            global.gold = 0;
        }
    }
    if (keyboard_check_pressed(ord(""1"")) && keyboard_check(ord(""M"")))
    {
        global.gold += 100;
        scr_debug_print(""+ 100 D$"");
    }

    if (keyboard_check_pressed(ord(""S"")))
        instance_create(0, 0, obj_savemenu);

    if (keyboard_check_pressed(ord(""L"")))
        scr_load();

    if (keyboard_check_pressed(ord(""R"")) && keyboard_check(vk_backspace))
        game_restart_true();

    if (keyboard_check_pressed(ord(""R"")) && !keyboard_check(vk_backspace))
    {
        snd_free_all();
        room_restart();
        global.interact = 0;
    }
}
if (!instance_exists(obj_dmenu_system))
    instance_create(0, 0, obj_dmenu_system)
");
ChangeSelection(obj_darkcontroller);

// GameObject obj_overworldc
importGroup.QueueReplace(obj_overworldc.EventHandlerFor(EventType.Step, (uint)0, Data), @"
if (scr_debug())
{
    if (keyboard_check_pressed(ord(""S"")))
        instance_create(0, 0, obj_savemenu);
    if (keyboard_check_pressed(ord(""L"")))
        scr_load();
    if (keyboard_check_pressed(ord(""R"")) && keyboard_check(vk_backspace))
        game_restart_true();

    if (keyboard_check_pressed(ord(""R"")) && !keyboard_check(vk_backspace))
    {
        snd_free_all();
        room_restart();
        global.interact = 0;
    }
}
");
ChangeSelection(obj_overworldc);

importGroup.Import();

ScriptMessage("Mode Debug du Chapitre 2 ajouté.\r\n" + "Pour activer le Mode Debug en jeu, appuyer sur F10.");
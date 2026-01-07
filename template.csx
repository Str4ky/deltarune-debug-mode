// Mode Debug Custom par Jazzky et Straky

EnsureDataLoaded();

if (!Data.IsVersionAtLeast(2023, 6))
{
    ScriptError("Erreur 0 : Ce script fonctionne uniquement pour la version payante de Deltarune.");
    return;
}

if (Data?.GeneralInfo?.DisplayName?.Content.ToLower() != "deltarune chapter CHAPTER_NUMBER" &&
    Data?.GeneralInfo?.DisplayName?.Content.ToLower() != "deltarune chapitre CHAPTER_NUMBER")
{
    ScriptError("Erreur 1 : Ce script s'applique seulement au Chapitre CHAPTER_NUMBER.");
    return;
}


bool enable = ScriptQuestion(
"Ajouter le Mode Debug pour le Chapitre CHAPTER_NUMBER ?"
);

if (!enable)
{
    return;
}

GlobalDecompileContext globalDecompileContext = new(Data);
Underanalyzer.Decompiler.IDecompileSettings decompilerSettings = new Underanalyzer.Decompiler.DecompileSettings();
UndertaleModLib.Compiler.CodeImportGroup importGroup = new(Data, globalDecompileContext, decompilerSettings)
{
    ThrowOnNoOpFindReplace = false
};

// Fonction de Log debug

// Script debug print

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

");

ChangeSelection(scr_debug_print);

// GameObject debug gui

UndertaleGameObject  obj_debug_gui = Data.GameObjects.ByName("obj_debug_gui");

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
ChangeSelection(scr_debug_print.Code);

// GameObject debug xy
UndertaleGameObject  obj_debug_xy = Data.GameObjects.ByName("obj_debug_xy");

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

// GameObject debug windows
UndertaleGameObject  obj_debug_windows = Data.GameObjects.ByName("obj_debug_windows");

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

// Menu Debug
UndertaleGameObject obj_dmenu_system = new UndertaleGameObject(); // Ajoute le GameObject
obj_dmenu_system.Name = Data.Strings.MakeString("obj_dmenu_system");
obj_dmenu_system.Visible = (true);
obj_dmenu_system.Persistent = (true);
obj_dmenu_system.Awake = (true);

Data.GameObjects.Add(obj_dmenu_system); // Répertorie le GameObject

importGroup.QueueReplace(obj_dmenu_system.EventHandlerFor(EventType.Create, (uint)0, Data), @"
CREATE_CODE
");

importGroup.QueueReplace(obj_dmenu_system.EventHandlerFor(EventType.Step, (uint)0, Data), @"
STEP0_CODE
");

importGroup.QueueReplace(obj_dmenu_system.EventHandlerFor(EventType.Draw, (uint)0, Data), @"
DRAW_CODE
");

importGroup.QueueReplace(obj_dmenu_system.EventHandlerFor(EventType.Step, (uint)1, Data), @"
STEP1_CODE
");

ChangeSelection(obj_dmenu_system);
importGroup.Import();

// Variable au gamestart
UndertaleScript scr_gamestart = Data.Scripts.ByName("scr_gamestart");
importGroup.QueueAppend(scr_gamestart.Code, @"
global.debug = 0;
");
ChangeSelection(scr_gamestart);

// Toggler
UndertaleScript scr_debug = Data.Scripts.ByName("scr_debug");
importGroup.QueueReplace(scr_debug.Code, @"
function scr_debug()
{
    return global.debug;
}
");
ChangeSelection(scr_debug);

UndertaleGameObject obj_time_TOGGLER = Data.GameObjects.ByName("obj_time");
importGroup.QueueAppend(obj_time_TOGGLER.EventHandlerFor(EventType.Step, (uint)0, Data), @"
if (keyboard_check_pressed(vk_f10))
{
    global.debug = !global.debug;
    if (global.debug)
        scr_debug_print(""Mode Debug activé !"");
    else
        scr_debug_print(""Mode Debug désactivé !"");
}
");
ChangeSelection(obj_time_TOGGLER);

// Fonctions du Lightworld
UndertaleGameObject obj_overworldc = Data.GameObjects.ByName("obj_overworldc");

importGroup.QueueFindReplace(obj_overworldc.EventHandlerFor(EventType.Step, (uint)0, Data),
"if (scr_debug())", "if (scr_debug() && (!instance_number(obj_dmenu_system) || !global.dreading_custom_flag))");

importGroup.QueueAppend(obj_overworldc.EventHandlerFor(EventType.Step, (uint)0, Data), @"
if (!instance_exists(obj_dmenu_system))
    instance_create(0, 0, obj_dmenu_system)
");
ChangeSelection(obj_overworldc);

// Fonctions du jeu (compteur FPS / fonction de pause / fonction de changement de FPS)
UndertaleGameObject obj_time = Data.GameObjects.ByName("obj_time");
importGroup.QueueReplace(obj_time.EventHandlerFor(EventType.Draw, (uint)0, Data), @"
if (scr_debug())
{
    draw_set_font(fnt_main)
    draw_set_color(c_red)
    draw_text(__view_get(0, 0), __view_get(1, 0), fps)

    draw_set_font(fnt_main);
    draw_set_color(c_green);
    draw_text(__view_get(0, 0) + __view_get(2, 0) - string_width(room_get_name(room)), __view_get(1, 0), room_get_name(room));
    draw_text((__view_get(0, 0) + __view_get(2, 0)) - string_width(""plot "" + string(global.plot)), __view_get(1, 0) + 15, ""plot "" + string(global.plot));
};
");

importGroup.QueueAppend(obj_time.EventHandlerFor(EventType.Step, (uint)0, Data), @"
if (scr_debug() && (!instance_number(obj_dmenu_system) || !global.dreading_custom_flag))
{
    if (keyboard_check_pressed(ord(""P"")))
    {
        if (room_speed == 30)
        {
            room_speed = 1;
            scr_debug_print(""FPS à 1"");
        }
        else
        {
            room_speed = 30;
            scr_debug_print(""FPS à 30"");
        }
    }

    if (keyboard_check_pressed(ord(""O"")))
    {
        if (room_speed == 120 || room_speed == 1)
        {
            room_speed = 30;
            scr_debug_print(""FPS à 30"");
        }
        else if (room_speed == 60)
        {
            room_speed = 120;
            scr_debug_print(""FPS à 120"");
        }
        else if (room_speed == 30) {
            room_speed = 60;
            scr_debug_print(""FPS à 60"");
        }
    }
};
");
ChangeSelection(obj_time);

// Fonctions de combat
UndertaleCode obj_battlecontroller_step = Data.Code.ByName("gml_Object_obj_battlecontroller_Step_0");

// Replace debug commands
importGroup.QueueFindReplace(obj_battlecontroller_step,
"if (scr_debug())", "if (0)");

// Conflit de touche avec D
UndertaleCode obj_spritecomparer_draw = Data.Code.ByName("gml_Object_obj_spritecomparer_Draw_0");
importGroup.QueueFindReplace(obj_spritecomparer_draw,
"if (keyboard_check_pressed(ord(\"D\")))", "if (keyboard_check_pressed(vk_f2))");

importGroup.QueueAppend(obj_battlecontroller_step, @"
if (scr_debug() && (!instance_number(obj_dmenu_system) || !global.dreading_custom_flag))
{
    if (keyboard_check_pressed(ord(""T"")))
    {
        if (global.tension < 250)
        {
            global.tension = 250;
            scr_debug_print(""TP à 250 %"");
        }
        else
        {
            global.tension = 0;
            scr_debug_print(""TP à 0 %"");
        }
    }
    if (keyboard_check_pressed(ord(""V"")))
        scr_turn_skip();
    
    if (keyboard_check_pressed(ord(""H"")))
    {
        scr_debug_fullheal();
        scr_debug_print(""HP du party restaurés"");
    }
    
    if (keyboard_check_pressed(ord(""W"")))
    {
        scr_wincombat();
        scr_debug_print(""Combat passé"");
    }
}
");
ChangeSelection(obj_battlecontroller_step);

// Script fullheal
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

// Script turn skip
UndertaleScript scr_turn_skip = Data.Scripts.ByName("scr_turn_skip");

importGroup.QueueReplace(scr_turn_skip.Code, @"
function scr_turn_skip()
{
    if (global.turntimer > 0 && instance_exists(obj_growtangle) && scr_isphase(""bullets""))
    {
        global.turntimer = 0;
        scr_debug_print(""Tour de l'ennemi passé"");
    }
}
");
ChangeSelection(scr_turn_skip);

EXTRA_IMPORTS

importGroup.Import();

ScriptMessage("Mode Debug du Chapitre CHAPTER_NUMBER " + (enable ? "ajouté" : "désactivé") + ".\r\n" + "Pour activer le Mode Debug en jeu, appuyer sur F10.");

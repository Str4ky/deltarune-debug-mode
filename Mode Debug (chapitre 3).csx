// Mode Debug Custom par Jazzky et Straky

EnsureDataLoaded();

if (!Data.IsVersionAtLeast(2023, 6))
{
    ScriptError("Erreur 0 : Ce script fonctionne uniquement pour la version payante de Deltarune.");
    return;
}

if (Data?.GeneralInfo?.DisplayName?.Content.ToLower() != "deltarune chapter 3" &&
    Data?.GeneralInfo?.DisplayName?.Content.ToLower() != "deltarune chapitre 3")
{
    ScriptError("Erreur 1 : Ce script s'applique seulement au Chapitre 3.");
    return;
}


bool enable = ScriptQuestion(
"Ajouter le Mode Debug pour le Chapitre 3 ?"
);

if (!enable)
{
    return;
}

GlobalDecompileContext globalDecompileContext = new(Data);
Underanalyzer.Decompiler.IDecompileSettings decompilerSettings = new Underanalyzer.Decompiler.DecompileSettings();
UndertaleModLib.Compiler.CodeImportGroup importGroup = new(Data, globalDecompileContext, decompilerSettings)
{
    ThrowOnNoOpFindReplace = true
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

// Menu Debug
UndertaleGameObject obj_dmenu_system = new UndertaleGameObject(); // Ajoute le GameObject
obj_dmenu_system.Name = Data.Strings.MakeString("obj_dmenu_system");
obj_dmenu_system.Visible = (true);
obj_dmenu_system.Persistent = (true);
obj_dmenu_system.Awake = (true);

Data.GameObjects.Add(obj_dmenu_system); // Répertorie le GameObject

importGroup.QueueReplace(obj_dmenu_system.EventHandlerFor(EventType.Create, (uint)0, Data), @"
global.dmenu_active = false;
global.dmenu_box = 0;
global.dbutton_layout = 0;
global.dmenu_start_index = 0;
global.dbutton_max_visible = 5;
global.dmenu_arrow_timer = 0;
global.dscroll_up_timer = 0;
global.dscroll_down_timer = 0;
global.dscroll_delay = 15;
global.dscroll_speed = 5;
global.dmenu_title = ""Menu Debug"";
global.dbutton_options = [""Sauts"", ""Items"", ""Recrues""];
global.dmenu_state = ""debug"";
global.dbutton_selected = 1;
global.dmenu_state_history = [];
global.dbutton_selected_history = [];
global.dgiver_menu_state = 0;
global.dgiver_button_selected = 0;
global.dgiver_amount = 1;
global.dgiver_bname = 0;
global.ditemcount = 0;
global.darmorcount = 0;
global.dweaponcount = 0;
global.dkeyitemcount = 0;
global.dbutton_indices = [];
global.drecent_item = 0;
global.drecent_armor = 0;
global.drecent_weapon = 0;
global.drecent_keyitem = 0;
global.dkeys_helper = 0;
global.dkeys_data = [];
");

importGroup.QueueReplace(obj_dmenu_system.EventHandlerFor(EventType.Step, (uint)0, Data), @"
global.dmenu_arrow_timer += 1;

if (global.dmenu_active)
{
    if (global.dbutton_layout == 0 && global.dkeys_helper == 0)
    {
        if (keyboard_check_pressed(vk_left))
        {
            global.dbutton_selected -= 1;
            
            if (global.dbutton_selected < 1)
                global.dbutton_selected = array_length(global.dbutton_options);
            
            snd_play(snd_menumove);
        }
        
        if (keyboard_check_pressed(vk_right))
        {
            global.dbutton_selected = (global.dbutton_selected % array_length(global.dbutton_options)) + 1;
            snd_play(snd_menumove);
        }
    }
    
    if (global.dbutton_layout == 1)
    {
        if (keyboard_check_pressed(vk_down))
        {
            snd_play(snd_menumove);
            
            if (global.dbutton_selected < array_length(global.dbutton_options))
            {
                global.dbutton_selected += 1;
                
                if (global.dbutton_selected > (global.dmenu_start_index + global.dbutton_max_visible))
                    global.dmenu_start_index += 1;
            }
        }
        
        if (keyboard_check_pressed(vk_up))
        {
            snd_play(snd_menumove);
            
            if (global.dbutton_selected > 1)
            {
                global.dbutton_selected -= 1;
                
                if (global.dbutton_selected < (global.dmenu_start_index + 1))
                    global.dmenu_start_index -= 1;
            }
        }
        
        if (keyboard_check_pressed(vk_right))
        {
            if (global.dmenu_state == ""recruits"")
            {
                if (global.dbutton_selected != 1)
                {
                    real_index = global.dbutton_indices[global.dbutton_selected - 1];
                    scr_recruit_info(real_index);
                    recruit_count = global.flag[real_index + 600];
                    
                    if ((global.flag[real_index + 600] * _recruitcount) < _recruitcount)
                    {
                        global.flag[600 + real_index] = recruit_count + (1 / _recruitcount);
                        dmenu_state_update();
                        snd_play(snd_sparkle_gem);
                    }
                    else
                    {
                        snd_play(snd_error);
                    }
                }
            }
        }
        
        if (keyboard_check_pressed(vk_left))
        {
            if (global.dmenu_state == ""recruits"")
            {
                if (global.dbutton_selected != 1)
                {
                    real_index = global.dbutton_indices[global.dbutton_selected - 1];
                    scr_recruit_info(real_index);
                    recruit_count = global.flag[real_index + 600];
                    
                    if ((global.flag[real_index + 600] * _recruitcount) > -1)
                    {
                        global.flag[600 + real_index] = recruit_count - (1 / _recruitcount);
                        dmenu_state_update();
                        snd_play(snd_wing);
                    }
                    else
                    {
                        snd_play(snd_error);
                    }
                }
            }
        }
        
        if (keyboard_check(vk_down))
        {
            if (global.dscroll_down_timer >= global.dscroll_delay)
            {
                if ((global.dscroll_down_timer % global.dscroll_speed) == 0)
                {
                    if (global.dbutton_selected < array_length(global.dbutton_options))
                    {
                        global.dbutton_selected += 1;
                        snd_play(snd_menumove);
                        
                        if (global.dbutton_selected > (global.dmenu_start_index + global.dbutton_max_visible))
                            global.dmenu_start_index += 1;
                    }
                }
            }
            
            global.dscroll_down_timer += 1;
        }
        else
        {
            global.dscroll_down_timer = 0;
        }
        
        if (keyboard_check(vk_up))
        {
            if (global.dscroll_up_timer >= global.dscroll_delay)
            {
                if ((global.dscroll_up_timer % global.dscroll_speed) == 0)
                {
                    if (global.dbutton_selected > 1)
                    {
                        global.dbutton_selected -= 1;
                        snd_play(snd_menumove);
                        
                        if (global.dbutton_selected < (global.dmenu_start_index + 1))
                            global.dmenu_start_index -= 1;
                    }
                }
            }
            
            global.dscroll_up_timer += 1;
        }
        else
        {
            global.dscroll_up_timer = 0;
        }
        
        global.dmenu_start_index = clamp(global.dmenu_start_index, 0, max(0, array_length(global.dbutton_options) - global.dbutton_max_visible));
    }
    
    if (global.dbutton_layout == 2)
    {
        if (keyboard_check_pressed(vk_left))
        {
            var owned_count = 0;
            
            switch (global.dgiver_menu_state)
            {
                case ""objects"":
                    scr_itemcheck(global.dbutton_indices[global.dgiver_button_selected - 1]);
                    owned_count = itemcount;
                    break;
                
                case ""armors"":
                    scr_armorcheck_inventory(global.dbutton_indices[global.dgiver_button_selected - 1]);
                    owned_count = itemcount;
                    break;
                
                case ""weapons"":
                    scr_weaponcheck_inventory(global.dbutton_indices[global.dgiver_button_selected - 1]);
                    owned_count = itemcount;
                    break;
                
                case ""keyitems"":
                    scr_keyitemcheck(global.dbutton_indices[global.dgiver_button_selected - 1]);
                    owned_count = itemcount;
                    break;
                
                default:
                    owned_count = 0;
            }
            
            if (global.dgiver_amount > -owned_count)
            {
                global.dgiver_amount -= 1;
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
            
            switch (global.dgiver_menu_state)
            {
                case ""objects"":
                    scr_itemcheck(0);
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
                    real_indice = global.dbutton_indices[global.dbutton_selected - 1];
                    recruited_nbr = global.flag[real_indice + 600];
                    global.flag[real_indice + 600] = recruited_nbr + 1;
                    break;
                
                default:
                    owned_count = 0;
            }
            
            if (global.dgiver_amount < owned_count)
            {
                global.dgiver_amount += 1;
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
        snd_play(snd_select);
        
        if (global.dmenu_state != ""givertab"" && global.dmenu_state != ""recruit_presets"" && (global.dmenu_state != ""recruits"" || global.dbutton_selected == 1))
        {
            array_push(global.dmenu_state_history, global.dmenu_state);
            array_push(global.dbutton_selected_history, global.dbutton_selected);
        }
        
        if (global.dmenu_state == ""objects"" || global.dmenu_state == ""armors"" || global.dmenu_state == ""weapons"" || global.dmenu_state == ""keyitems"")
        {
            switch (global.dmenu_state)
            {
                case ""objects"":
                    real_index = global.dbutton_indices[global.dbutton_selected - 1];
                    scr_iteminfo(real_index);
                    global.dgiver_bname = itemnameb;
                    scr_debug_print(global.dgiver_bname + "" sélectionné !"");
                    break;
                
                case ""armors"":
                    real_index = global.dbutton_indices[global.dbutton_selected - 1];
                    scr_armorinfo(real_index);
                    global.dgiver_bname = armornametemp;
                    scr_debug_print(string(global.dgiver_bname) + "" sélectionné !"");
                    break;
                
                case ""weapons"":
                    real_index = global.dbutton_indices[global.dbutton_selected - 1];
                    scr_weaponinfo(real_index);
                    global.dgiver_bname = weaponnametemp;
                    scr_debug_print(string(global.dgiver_bname) + "" sélectionné !"");
                    break;
                
                case ""keyitems"":
                    real_index = global.dbutton_indices[global.dbutton_selected - 1];
                    scr_keyiteminfo(real_index);
                    global.dgiver_bname = tempkeyitemname;
                    scr_debug_print(string(global.dgiver_bname) + "" sélectionné !"");
                    break;
            }
        }
        else if (global.dmenu_state != ""givertab"" && (global.dmenu_state != ""recruits"" || global.dbutton_selected == 1))
        {
            scr_debug_print(string(global.dbutton_options[global.dbutton_selected - 1]) + "" sélectionné !"");
        }
        
        if ((global.dmenu_state == ""recruits"" && global.dbutton_selected != 1) || global.dmenu_state == ""recruit_presets"")
        {
            dmenu_state_interact();
            dmenu_state_update();
        }
        else
        {
            dmenu_state_interact();
            global.dmenu_start_index = 0;
            global.dbutton_selected = 1;
            dmenu_state_update();
        }
    }
    
    if (keyboard_check_pressed(global.input_k[5]) || keyboard_check_pressed(global.input_k[8]))
    {
        snd_play(snd_smallswing);
        
        if (array_length(global.dmenu_state_history) > 0)
        {
            global.dmenu_state = global.dmenu_state_history[array_length(global.dmenu_state_history) - 1];
            array_resize(global.dmenu_state_history, array_length(global.dmenu_state_history) - 1);
        }
        else
        {
            global.dmenu_active = !global.dmenu_active;
            global.dmenu_state_history = [];
            global.dbutton_selected_history = [];
            global.interact = 0;
        }
        
        if (array_length(global.dbutton_selected_history) > 0)
        {
            global.dbutton_selected = global.dbutton_selected_history[array_length(global.dbutton_selected_history) - 1];
            array_resize(global.dbutton_selected_history, array_length(global.dbutton_selected_history) - 1);
        }
        
        dmenu_state_update();
        global.dmenu_start_index = clamp(global.dbutton_selected - 1, 0, max(0, array_length(global.dbutton_options) - global.dbutton_max_visible));
    }
}

if (global.dmenu_active == 1 && global.dmenu_state == ""debug"" && global.darkzone == 1)
{
    if (keyboard_check_pressed(ord(""M"")))
        global.dkeys_helper = !global.dkeys_helper;
}
else if (global.dkeys_helper == 1)
{
    if (keyboard_check_pressed(ord(""M"")))
        global.dkeys_helper = !global.dkeys_helper;
}
");

importGroup.QueueReplace(obj_dmenu_system.EventHandlerFor(EventType.Draw, (uint)0, Data), @"
xx = __view_get(e__VW.XView, 0);
yy = __view_get(e__VW.YView, 0);
d = global.darkzone + 1;

if (keyboard_check_pressed(ord(""D"")))
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
    
    if (global.dmenu_state == ""debug"" && global.darkzone == 1)
    {
        draw_set_font(fnt_main);
        draw_set_color(c_white);
        draw_text(((x_start + 335) * (d / 2)) + xx, (((ycenter - (menu_length / 2)) + 82) * d) + yy, ""Touches - M"");
        draw_set_font(fnt_mainbig);
    }
    
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
        
        if (global.dgiver_menu_state == ""objects"")
        {
            itemreminder = ""["" + string(global.dgiver_bname) + ""]"";
            scr_itemcheck(0);
            draw_text(x_start + xx, ((ycenter + 25) * d) + yy, ""OBJETs : "" + string(12 - itemcount) + ""/12"");
        }
        
        if (global.dgiver_menu_state == ""armors"")
        {
            itemreminder = ""["" + string(global.dgiver_bname) + ""]"";
            scr_armorcheck_inventory(0);
            draw_text(x_start + xx, ((ycenter + 25) * d) + yy, ""ARMUREs : "" + string(48 - itemcount) + ""/48"");
        }
        
        if (global.dgiver_menu_state == ""weapons"")
        {
            itemreminder = ""["" + string(global.dgiver_bname) + ""]"";
            scr_weaponcheck_inventory(0);
            draw_text(x_start + xx, ((ycenter + 25) * d) + yy, ""ARMEs : "" + string(48 - itemcount) + ""/48"");
        }
        
        if (global.dgiver_menu_state == ""keyitems"")
        {
            itemreminder = ""["" + string(global.dgiver_bname) + ""]"";
            scr_keyitemcheck(0);
            draw_text(x_start + xx, ((ycenter + 25) * d) + yy, ""OBJETs CLÉs : "" + string(12 - itemcount) + ""/12"");
        }
        
        var text_width = string_width(itemreminder);
        draw_text(((xcenter * d) - (text_width / 2)) + xx, ((ycenter - 22) * d) + yy, itemreminder);
        var darrow_scale = d / 2;
        draw_sprite_ext(spr_morearrow, 0, ((xcenter - 15) * d) + xx, ((ycenter + 6) * d) + yy, darrow_scale, darrow_scale, 270, c_white, 1);
        draw_sprite_ext(spr_morearrow, 0, ((xcenter + 15) * d) + xx, ((ycenter + 12) * d) + yy, darrow_scale, darrow_scale, 90, c_white, 1);
    }
}

if (global.dkeys_helper == 1)
{
    global.dkeys_data = [""F10 - Activer/désactiver le debug mode"", ""D - Ouvrir le menu Debug"", ""S - Sauvegarder la partie"", ""L - Charger la dernière sauvegarde"", ""R - Redémarrer le jeu"", ""P - Mettre en pause/reprendre le jeu"", ""M+1/M+2 - Ajouter/retirer 100 D$"", ""Suppr - Se rendre à la salle précédente"", ""Insert - Se rendre à la salle suivante"", ""Entrer - Voir les collisions du joueur"", ""W - Gagner instantanément un combat"", ""V - Passer le tour de l'ennemi"", ""H - Restaurer les HP du party"", ""T - Remplir/vider la barre de TP"", ""O - Basculer entre 30, 60 et 120 FPS"", ""Retour arrière - Passer le segment d'intro (Ch1)""];
    var x_padding = 7;
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
    draw_set_color(c_white);
    draw_text(x_start + xx, (((ycenter - (menu_length / 2)) + 8) * d) + yy, ""Touches du debug mode"");
    
    for (var i = 0; i < array_length(global.dkeys_data); i++)
    {
        draw_set_font(fnt_main);
        draw_set_color(c_white);
        draw_text(x_start + xx, y_start + yy + (i * y_spacing), global.dkeys_data[i]);
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

importGroup.QueueReplace(obj_dmenu_system.EventHandlerFor(EventType.Step, (uint)1, Data), @"
if (global.ditemcount == 0)
{
    global.ditemcount = 39;
    global.darmorcount = 27;
    global.dweaponcount = 26;
    global.dkeyitemcount = 19;
    global.drecent_item = 34;
    global.drecent_armor = 23;
    global.drecent_weapon = 23;
    global.drecent_keyitem = 16;
}

function dmenu_state_update()
{
    switch (global.dmenu_state)
    {
        case ""debug"":
            global.dmenu_title = ""Menu Debug"";
            global.dbutton_options = [""Sauts"", ""Items"", ""Recrues""];
            global.dmenu_box = 0;
            global.dbutton_layout = 0;
            break;
        
        case ""warp"":
            scr_debug_print(""Pas encore disponible !"");
            break;
        
        case ""lightworld"":
            global.dmenu_title = ""Menu de sauts - Lightworld"";
            global.dbutton_options = [""Couloir de Kris"", ""Hall de l'école"", ""Placard de l'école"", ""Après Darkworld""];
            global.dmenu_box = 1;
            global.dbutton_layout = 1;
            break;
        
        case ""kris_hallway"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""school_hallway"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""school_closet"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""after_darkworld"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Saut : 1ère apparition""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""darkworld"":
            global.dmenu_title = ""Menu de sauts - Darkworld"";
            global.dbutton_options = [""Citadelle"", ""Entrée Cyber-Monde"", ""Entrée Cyber-Plaine"", ""Boutique de musique"", ""Zone Poubelle"", ""Entrée de la Ville"", ""Pause de la Ville"", ""Salle de mousse"", ""Salle des souris pénibles 3"", ""Cellule de Kris"", ""Entrée Manoir"", ""3e étage du Manoir"", ""Après le Tunnel d'acide"", ""Sous-sol du Manoir""];
            global.dmenu_box = 1;
            global.dbutton_layout = 1;
            break;
        
        case ""castletown"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : Avant chercher les autres"", ""Saut : Après chercher les autres"", ""Saut : Après le Cyber-Monde""];
            global.dmenu_box = 1;
            global.dbutton_layout = 1;
            break;
        
        case ""cw_entrance"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""cyberfield_entrance"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""music_shop"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""trash_zone"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""city_entrance"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""city_break"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""moss_room"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""mouse_game3"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : Après le 3e jeu""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""prison_cell"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""mansion_entrance"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""mansion_3rd_floor"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""after_acid_tunnel"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""mansion_basement"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""battles"":
            global.dmenu_title = ""Menu de sauts - Combats"";
            global.dbutton_options = [""Combat contre les DJ"", ""Combat contre Berdly"", ""2e Combat contre Berdly"", ""Combat contre Spamton"", ""Combat contre Gest. De Tachs"", ""Combat contre Surimolette"", ""Combat contre la Reine"", ""Combat contre Spamton Neo"", ""Combat contre Giga Queen""];
            global.dmenu_box = 1;
            global.dbutton_layout = 1;
            break;
        
        case ""dj_battle"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : Combat""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""berdly_battle"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : Combat""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""berdly2_battle"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : Combat""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""spamton_battle"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : Combat""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""tasque_manager_battle"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : Combat""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""mauswheel_battle"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : Combat""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""queen_battle"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : Combat""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""spamton_neo_battle"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : Combat avec DisqueGravé""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""giga_queen_battle"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : Combat""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""give"":
            global.dmenu_title = ""Type d'items"";
            global.dbutton_options = [""Objets"", ""Armures"", ""Armes"", ""Obj Clés""];
            global.dmenu_box = 0;
            global.dbutton_layout = 0;
            break;
        
        case ""objects"":
            global.dmenu_title = ""Liste d'objets"";
            global.dbutton_options = [];
            global.dbutton_indices = [];
            var max_len = 40;
            
            for (var i = global.drecent_item; i <= global.ditemcount; i++)
            {
                scr_iteminfo(i);
                var cleaned_desc = string_replace_all(itemdescb, ""#"", "" "");
                var combined = itemnameb + "" - "" + cleaned_desc;
                
                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + ""..."";
                
                array_push(global.dbutton_options, combined);
                array_push(global.dbutton_indices, i);
            }
            
            var cutoff = min(global.drecent_item - 1, global.ditemcount);
            
            for (var i = 1; i <= cutoff; i++)
            {
                scr_iteminfo(i);
                var cleaned_desc = string_replace_all(itemdescb, ""#"", "" "");
                var combined = itemnameb + "" - "" + cleaned_desc;
                
                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + ""..."";
                
                array_push(global.dbutton_options, combined);
                array_push(global.dbutton_indices, i);
            }
            
            global.dmenu_box = 2;
            global.dbutton_layout = 1;
            break;
        
        case ""armors"":
            global.dmenu_title = ""Liste d'armures"";
            global.dbutton_options = [];
            global.dbutton_indices = [];
            var max_len = 40;
            
            for (var i = global.drecent_armor; i <= global.darmorcount; i++)
            {
                scr_armorinfo(i);
                var cleaned_desc = string_replace_all(armordesctemp, ""#"", "" "");
                var combined = armornametemp + "" - "" + cleaned_desc;
                
                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + ""..."";
                
                array_push(global.dbutton_options, combined);
                array_push(global.dbutton_indices, i);
            }
            
            var cutoff = min(global.drecent_armor - 1, global.darmorcount);
            
            for (var i = 1; i <= cutoff; i++)
            {
                scr_armorinfo(i);
                var cleaned_desc = string_replace_all(armordesctemp, ""#"", "" "");
                var combined = armornametemp + "" - "" + cleaned_desc;
                
                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + ""..."";
                
                array_push(global.dbutton_options, combined);
                array_push(global.dbutton_indices, i);
            }
            
            global.dmenu_box = 2;
            global.dbutton_layout = 1;
            break;
        
        case ""weapons"":
            global.dmenu_title = ""Liste d'armes"";
            global.dbutton_options = [];
            global.dbutton_indices = [];
            var max_len = 40;
            
            for (var i = global.drecent_weapon; i <= global.dweaponcount; i++)
            {
                scr_weaponinfo(i);
                var cleaned_desc = string_replace_all(weapondesctemp, ""#"", "" "");
                var combined = weaponnametemp + "" - "" + cleaned_desc;
                
                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + ""..."";
                
                array_push(global.dbutton_options, combined);
                array_push(global.dbutton_indices, i);
            }
            
            var cutoff = min(global.drecent_weapon - 1, global.dweaponcount);
            
            for (var i = 1; i <= cutoff; i++)
            {
                scr_weaponinfo(i);
                var cleaned_desc = string_replace_all(weapondesctemp, ""#"", "" "");
                var combined = weaponnametemp + "" - "" + cleaned_desc;
                
                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + ""..."";
                
                array_push(global.dbutton_options, combined);
                array_push(global.dbutton_indices, i);
            }
            
            global.dmenu_box = 2;
            global.dbutton_layout = 1;
            break;
        
        case ""keyitems"":
            global.dmenu_title = ""Liste d'objets clés (Peut briser le jeu)"";
            global.dbutton_options = [];
            global.dbutton_indices = [];
            var max_len = 40;
            
            for (var i = global.drecent_keyitem; i <= global.dkeyitemcount; i++)
            {
                scr_keyiteminfo(i);
                var cleaned_desc = string_replace_all(tempkeyitemdesc, ""#"", "" "");
                var combined = tempkeyitemname + "" - "" + cleaned_desc;
                
                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + ""..."";
                
                array_push(global.dbutton_options, combined);
                array_push(global.dbutton_indices, i);
            }
            
            var cutoff = min(global.drecent_keyitem - 1, global.dkeyitemcount);
            
            for (var i = 1; i <= cutoff; i++)
            {
                scr_keyiteminfo(i);
                var cleaned_desc = string_replace_all(tempkeyitemdesc, ""#"", "" "");
                var combined = tempkeyitemname + "" - "" + cleaned_desc;
                
                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + ""..."";
                
                array_push(global.dbutton_options, combined);
                array_push(global.dbutton_indices, i);
            }
            
            global.dmenu_box = 2;
            global.dbutton_layout = 1;
            break;
        
        case ""givertab"":
            global.dmenu_title = ""Ajouter combien à l'inventaire ?"";
            global.dgiver_amount = 1;
            global.dmenu_box = 0;
            global.dbutton_layout = 2;
            break;
        
        case ""recruits"":
            global.dmenu_title = ""Liste des recrues"";
            global.dbutton_options = [""Préréglages""];
            global.dbutton_indices = [""Préréglages""];
            var max_len = 40;
            
            for (var c = 1; c <= global.chapter; c++)
            {
                var test_lst = scr_get_chapter_recruit_data(c);
                
                for (var i = 0; i < array_length(test_lst); i++)
                {
                    var enemy_id = test_lst[i];
                    scr_recruit_info(enemy_id);
                    var combined = _name + "" - ["" + string(max(floor(global.flag[enemy_id + 600] * _recruitcount), -1)) + ""/"" + string(_recruitcount) + ""]"";
                    
                    if (string_length(combined) > max_len)
                        combined = string_copy(combined, 1, max_len - 3) + ""..."";
                    
                    array_push(global.dbutton_options, combined);
                    array_push(global.dbutton_indices, enemy_id);
                }
            }
            
            global.dmenu_box = 1;
            global.dbutton_layout = 1;
            break;
        
        case ""recruit_presets"":
            global.dmenu_title = ""Préréglages des recrues"";
            global.dbutton_options = [""Recruter tous"", ""Perdre tous""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        default:
            global.dmenu_title = ""Inconnu"";
            global.dbutton_options = [];
            global.dmenu_box = 0;
            global.dbutton_layout = 0;
    }
}

function dmenu_state_interact()
{
    switch (global.dmenu_state)
    {
        case ""debug"":
            if (global.dbutton_selected == 1)
            {
                global.dmenu_state = ""warp"";
                global.dbutton_selected = 1;
            }
            
            if (global.dbutton_selected == 2)
            {
                global.dmenu_state = ""give"";
                global.dbutton_selected = 1;
            }
            
            if (global.dbutton_selected == 3)
            {
                global.dmenu_state = ""recruits"";
                global.dbutton_selected = 1;
            }
            
            break;
        
        case ""warp"":
            if (global.dbutton_selected == 1)
            {
                global.dmenu_state = ""lightworld"";
                global.dbutton_selected = 1;
            }
            
            if (global.dbutton_selected == 2)
            {
                global.dmenu_state = ""darkworld"";
                global.dbutton_selected = 1;
            }
            
            if (global.dbutton_selected == 3)
            {
                global.dmenu_state = ""battles"";
                global.dbutton_selected = 1;
            }
            
            break;
        
        case ""lightworld"":
            if (global.dbutton_selected == 1)
                global.dmenu_state = ""kris_hallway"";
            
            if (global.dbutton_selected == 2)
                global.dmenu_state = ""school_hallway"";
            
            if (global.dbutton_selected == 3)
                global.dmenu_state = ""school_closet"";
            
            if (global.dbutton_selected == 4)
                global.dmenu_state = ""after_darkworld"";
            
            break;
        
        case ""kris_hallway"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 0;
            snd_free_all();
            room_goto(room_krishallway);
            
            if (global.dbutton_selected == 2)
                global.plot = 1;
            
            break;
        
        case ""school_hallway"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 0;
            snd_free_all();
            room_goto(room_schoollobby);
            break;
        
        case ""school_closet"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 0;
            snd_free_all();
            room_goto(room_schooldoor);
            break;
        
        case ""after_darkworld"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 0;
            snd_free_all();
            room_goto(room_lw_computer_lab);
            break;
        
        case ""darkworld"":
            if (global.dbutton_selected == 1)
                global.dmenu_state = ""castletown"";
            
            if (global.dbutton_selected == 2)
                global.dmenu_state = ""cw_entrance"";
            
            if (global.dbutton_selected == 3)
                global.dmenu_state = ""cyberfield_entrance"";
            
            if (global.dbutton_selected == 4)
                global.dmenu_state = ""music_shop"";
            
            if (global.dbutton_selected == 5)
                global.dmenu_state = ""trash_zone"";
            
            if (global.dbutton_selected == 6)
                global.dmenu_state = ""city_entrance"";
            
            if (global.dbutton_selected == 7)
                global.dmenu_state = ""city_break"";
            
            if (global.dbutton_selected == 8)
                global.dmenu_state = ""moss_room"";
            
            if (global.dbutton_selected == 9)
                global.dmenu_state = ""mouse_game3"";
            
            if (global.dbutton_selected == 10)
                global.dmenu_state = ""prison_cell"";
            
            if (global.dbutton_selected == 11)
                global.dmenu_state = ""mansion_entrance"";
            
            if (global.dbutton_selected == 12)
                global.dmenu_state = ""mansion_3rd_floor"";
            
            if (global.dbutton_selected == 13)
                global.dmenu_state = ""after_acid_tunnel"";
            
            if (global.dbutton_selected == 14)
                global.dmenu_state = ""mansion_basement"";
            
            break;
        
        case ""castletown"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_castle_area_1);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 0;
                global.plot = 7;
                global.flag[34] = 1;
            }
            
            if (global.dbutton_selected == 3)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 12;
                global.flag[34] = 1;
            }
            
            if (global.dbutton_selected == 4)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 205;
                global.flag[34] = 0;
            }
            
            break;
        
        case ""cw_entrance"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_cyber_intro_1);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 0;
                global.plot = 50;
                global.flag[34] = 1;
            }
            
            break;
        
        case ""cyberfield_entrance"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_cyber_savepoint);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 51;
                global.flag[34] = 1;
            }
            
            break;
        
        case ""music_shop"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_cyber_musical_shop);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 60;
                global.flag[34] = 0;
            }
            
            break;
        
        case ""trash_zone"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_city_intro);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 64;
                global.flag[34] = 0;
            }
            
            break;
        
        case ""city_entrance"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_city_entrance);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 0;
                global.char[2] = 0;
                global.plot = 67;
                global.flag[34] = 0;
            }
            
            break;
        
        case ""city_break"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_city_savepoint);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 4;
                global.char[2] = 0;
                global.plot = 75;
                global.flag[34] = 0;
            }
            
            break;
        
        case ""moss_room"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_city_moss);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 4;
                global.char[2] = 0;
                global.plot = 75;
                global.flag[34] = 0;
            }
            
            break;
        
        case ""mouse_game3"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_city_mice3);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 4;
                global.char[2] = 0;
                global.plot = 78;
                global.flag[34] = 0;
            }
            
            break;
        
        case ""prison_cell"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_krisroom);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 0;
                global.char[2] = 0;
                global.plot = 100;
                global.flag[34] = 0;
            }
            
            break;
        
        case ""mansion_entrance"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_entrance);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 120;
                global.flag[34] = 0;
            }
            
            break;
        
        case ""mansion_3rd_floor"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_east_3f);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 0;
                global.plot = 125;
                global.flag[34] = 0;
            }
            
            break;
        
        case ""after_acid_tunnel"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_acid_tunnel_exit);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 160;
                global.flag[34] = 0;
            }
            
            break;
        
        case ""mansion_basement"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            global.char[0] = 1;
            global.char[1] = 0;
            global.char[2] = 0;
            snd_free_all();
            room_goto(room_dw_mansion_b_central);
            
            if (global.dbutton_selected == 2)
            {
                global.flag[358] = 1;
                global.flag[309] = 4;
                global.flag[34] = 0;
                
                if (global.plot < 120)
                    global.plot = 120;
            }
            
            break;
        
        case ""battles"":
            if (global.dbutton_selected == 1)
                global.dmenu_state = ""dj_battle"";
            
            if (global.dbutton_selected == 2)
                global.dmenu_state = ""berdly_battle"";
            
            if (global.dbutton_selected == 3)
                global.dmenu_state = ""berdly2_battle"";
            
            if (global.dbutton_selected == 4)
                global.dmenu_state = ""spamton_battle"";
            
            if (global.dbutton_selected == 5)
                global.dmenu_state = ""tasque_manager_battle"";
            
            if (global.dbutton_selected == 6)
                global.dmenu_state = ""mauswheel_battle"";
            
            if (global.dbutton_selected == 7)
                global.dmenu_state = ""queen_battle"";
            
            if (global.dbutton_selected == 8)
                global.dmenu_state = ""spamton_neo_battle"";
            
            if (global.dbutton_selected == 9)
                global.dmenu_state = ""giga_queen_battle"";
            
            break;
        
        case ""dj_battle"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_cyber_music_final);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 52;
                global.flag[34] = 1;
            }
            
            break;
        
        case ""berdly_battle"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_cyber_rollercoaster);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 2;
                global.plot = 60;
                global.flag[34] = 0;
            }
            
            break;
        
        case ""berdly2_battle"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_city_berdly);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 4;
                global.char[2] = 0;
                global.plot = 78;
                global.flag[34] = 0;
            }
            
            break;
        
        case ""spamton_battle"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_city_spamton_alley);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 0;
                global.char[2] = 0;
                global.plot = 80;
                global.flag[34] = 0;
            }
            
            break;
        
        case ""tasque_manager_battle"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_tasquePaintings);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 120;
                global.flag[34] = 0;
            }
            
            break;
        
        case ""mauswheel_battle"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_kitchen);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 120;
                global.flag[34] = 0;
            }
            
            break;
        
        case ""queen_battle"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_east_4f_d);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 160;
                global.flag[34] = 0;
            }
            
            break;
        
        case ""spamton_neo_battle"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_b_east_b);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 0;
                global.char[2] = 0;
                global.flag[358] = 2;
                global.flag[373] = 1;
                global.flag[309] = 7;
                scr_keyitemget(11);
                global.flag[34] = 0;
                
                if (global.plot < 120)
                    global.plot = 120;
            }
            
            break;
        
        case ""giga_queen_battle"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_east_4f_d);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 165;
                global.flag[34] = 0;
            }
            
            break;
        
        case ""give"":
            if (global.dbutton_selected == 1)
            {
                global.dmenu_state = ""objects"";
                global.dbutton_selected = 1;
            }
            
            if (global.dbutton_selected == 2)
            {
                global.dmenu_state = ""armors"";
                global.dbutton_selected = 1;
            }
            
            if (global.dbutton_selected == 3)
            {
                global.dmenu_state = ""weapons"";
                global.dbutton_selected = 1;
            }
            
            if (global.dbutton_selected == 4)
            {
                global.dmenu_state = ""keyitems"";
                global.dbutton_selected = 1;
            }
            
            break;
        
        case ""objects"":
            global.dgiver_menu_state = global.dmenu_state;
            global.dbutton_selected = clamp(global.dbutton_selected, 0, array_length(global.dbutton_options) - 1);
            global.dgiver_button_selected = global.dbutton_selected;
            global.dmenu_state = ""givertab"";
            global.dbutton_selected = 1;
            break;
        
        case ""armors"":
            global.dgiver_menu_state = global.dmenu_state;
            global.dgiver_button_selected = global.dbutton_selected;
            global.dmenu_state = ""givertab"";
            global.dbutton_selected = 1;
            break;
        
        case ""weapons"":
            global.dgiver_menu_state = global.dmenu_state;
            global.dgiver_button_selected = global.dbutton_selected;
            global.dmenu_state = ""givertab"";
            global.dbutton_selected = 1;
            break;
        
        case ""keyitems"":
            global.dgiver_menu_state = global.dmenu_state;
            global.dgiver_button_selected = global.dbutton_selected;
            global.dmenu_state = ""givertab"";
            global.dbutton_selected = 1;
            break;
        
        case ""givertab"":
            if (global.dgiver_menu_state == ""objects"")
            {
                if (global.dgiver_amount > 0)
                {
                    real_index = global.dbutton_indices[global.dgiver_button_selected - 1];
                    
                    for (var i = 0; i < global.dgiver_amount; i++)
                        scr_itemget(real_index);
                    
                    scr_debug_print(string(global.dgiver_amount) + "" "" + global.dgiver_bname + "" ajouté à l'inventaire"");
                }
                else if (global.dgiver_amount < 0)
                {
                    real_index = global.dbutton_indices[global.dgiver_button_selected - 1];
                    
                    for (var i = 0; i < abs(global.dgiver_amount); i++)
                        scr_itemremove(real_index);
                    
                    scr_debug_print(string(abs(global.dgiver_amount)) + "" "" + global.dgiver_bname + "" retiré de l'inventaire"");
                }
                else
                {
                    scr_debug_print(""Annulé"");
                }
            }
            
            if (global.dgiver_menu_state == ""armors"")
            {
                if (global.dgiver_amount > 0)
                {
                    real_index = global.dbutton_indices[global.dgiver_button_selected - 1];
                    
                    for (var i = 0; i < global.dgiver_amount; i++)
                        scr_armorget(real_index);
                    
                    scr_debug_print(string(global.dgiver_amount) + "" "" + global.dgiver_bname + "" ajouté à l'inventaire"");
                }
                else if (global.dgiver_amount < 0)
                {
                    real_index = global.dbutton_indices[global.dgiver_button_selected - 1];
                    
                    for (var i = 0; i < abs(global.dgiver_amount); i++)
                        scr_armorremove(real_index);
                    
                    scr_debug_print(string(abs(global.dgiver_amount)) + "" "" + global.dgiver_bname + "" retiré de l'inventaire"");
                }
                else
                {
                    scr_debug_print(""Annulé"");
                }
            }
            
            if (global.dgiver_menu_state == ""weapons"")
            {
                if (global.dgiver_amount > 0)
                {
                    real_index = global.dbutton_indices[global.dgiver_button_selected - 1];
                    
                    for (var i = 0; i < global.dgiver_amount; i++)
                        scr_weaponget(real_index);
                    
                    scr_debug_print(string(global.dgiver_amount) + "" "" + global.dgiver_bname + "" ajouté à l'inventaire"");
                }
                else if (global.dgiver_amount < 0)
                {
                    real_index = global.dbutton_indices[global.dgiver_button_selected - 1];
                    
                    for (var i = 0; i < abs(global.dgiver_amount); i++)
                        scr_weaponremove(real_index);
                    
                    scr_debug_print(string(abs(global.dgiver_amount)) + "" "" + global.dgiver_bname + "" retiré de l'inventaire"");
                }
                else
                {
                    scr_debug_print(""Annulé"");
                }
            }
            
            if (global.dgiver_menu_state == ""keyitems"")
            {
                if (global.dgiver_amount > 0)
                {
                    real_index = global.dbutton_indices[global.dgiver_button_selected - 1];
                    
                    for (var i = 0; i < global.dgiver_amount; i++)
                        scr_keyitemget(real_index);
                    
                    scr_debug_print(string(global.dgiver_amount) + "" "" + global.dgiver_bname + "" ajouté à l'inventaire"");
                }
                else if (global.dgiver_amount < 0)
                {
                    real_index = global.dbutton_indices[global.dgiver_button_selected - 1];
                    
                    for (var i = 0; i < abs(global.dgiver_amount); i++)
                        scr_keyitemremove(real_index);
                    
                    scr_debug_print(string(abs(global.dgiver_amount)) + "" "" + global.dgiver_bname + "" retiré de l'inventaire"");
                }
                else
                {
                    scr_debug_print(""Annulé"");
                }
            }
            
            global.dmenu_active = false;
            global.interact = 0;
            break;
        
        case ""recruits"":
            if (global.dbutton_selected == 1)
            {
                global.dmenu_state = ""recruit_presets"";
                global.dbutton_selected = 1;
            }
            
            break;
        
        case ""recruit_presets"":
            if (global.dbutton_selected == 1)
            {
                for (var c = 1; c <= global.chapter; c++)
                {
                    var test_lst = scr_get_chapter_recruit_data(c);
                    
                    for (var i = 0; i < array_length(test_lst); i++)
                    {
                        var enemy_id = test_lst[i];
                        scr_recruit_info(enemy_id);
                        global.flag[enemy_id + 600] = 1;
                    }
                }
                
                snd_play(snd_pirouette);
            }
            
            if (global.dbutton_selected == 2)
            {
                for (var c = 1; c <= global.chapter; c++)
                {
                    var test_lst = scr_get_chapter_recruit_data(c);
                    
                    for (var i = 0; i < array_length(test_lst); i++)
                    {
                        var enemy_id = test_lst[i];
                        scr_recruit_info(enemy_id);
                        global.flag[enemy_id + 600] = -1 / _recruitcount;
                    }
                }
                
                snd_play(snd_weirdeffect);
            }
            
            break;
        
        default:
            snd_play(snd_error);
            scr_debug_print(""Sélection invalide !"");
    }
}
");

ChangeSelection(obj_dmenu_system);
importGroup.Import();

// Variable au gamestart
UndertaleScript scr_gamestart = Data.Scripts.ByName("scr_gamestart");
    importGroup.QueueAppend(scr_gamestart.Code, @"
    global.debug = 0;
    global.chemg_show_room = 1;
    global.chemg_show_val = 1;
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
importGroup.QueueAppend(obj_overworldc.EventHandlerFor(EventType.Step, (uint)0, Data), @"
if (!instance_exists(obj_dmenu_system))
    instance_create(0, 0, obj_dmenu_system)
");
ChangeSelection(obj_overworldc);

// Fonctions du Darkworld
UndertaleGameObject obj_darkcontroller = Data.GameObjects.ByName("obj_darkcontroller");
importGroup.QueueAppend(obj_darkcontroller.EventHandlerFor(EventType.Step, (uint)0, Data), @"
if (scr_debug())
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
}
if (!instance_exists(obj_dmenu_system))
    instance_create(0, 0, obj_dmenu_system)
");
ChangeSelection(obj_darkcontroller);

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
if (scr_debug())
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

// Fullheal function
importGroup.QueueFindReplace(obj_battlecontroller_step,
"scr_debug_fullheal();", "");
importGroup.QueueFindReplace(obj_battlecontroller_step,
"if (scr_debug_keycheck(vk_f2))",
@"if (keyboard_check_pressed(ord(""H"")))
{
    scr_debug_fullheal();
    scr_debug_print(""HP du party restaurés"");
}
");
// Wincombat function
importGroup.QueueFindReplace(obj_battlecontroller_step,
"if (scr_debug_keycheck(vk_f5))", "if (keyboard_check_pressed(ord(\"W\")))");
importGroup.QueueFindReplace(obj_battlecontroller_step,
"scr_wincombat();", @"
scr_wincombat();
scr_debug_print(""Combat passé"");
");
// Turn skip function
importGroup.QueueFindReplace(obj_battlecontroller_step, "scr_turn_skip();", @"
if (keyboard_check_pressed(ord(""V"")))
{
    scr_turn_skip();
}
");
// Tension functions
importGroup.QueueFindReplace(obj_battlecontroller_step,
"if (scr_debug_keycheck(vk_f9))", "");
importGroup.QueueFindReplace(obj_battlecontroller_step,
"if (scr_debug_keycheck(vk_f10))", "");
importGroup.QueueFindReplace(obj_battlecontroller_step,
"global.tension = 0;", "");
importGroup.QueueFindReplace(obj_battlecontroller_step,
"global.tension = 250;", "");
importGroup.QueueFindReplace(obj_battlecontroller_step,
"scr_debug_print(\"TP set to 0%\");", "");
importGroup.QueueFindReplace(obj_battlecontroller_step,
"scr_debug_print(\"TP maxed out!!\");", "");

importGroup.QueueAppend(obj_battlecontroller_step, @"
if (scr_debug())
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

importGroup.Import();

ScriptMessage("Mode Debug du Chapitre 3 " + (enable ? "ajouté" : "désactivé") + ".\r\n" + "Pour activer le Mode Debug en jeu, appuyer sur F10.");

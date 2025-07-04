// Mode Debug Custom par Jazzky et Straky

EnsureDataLoaded();

if (!Data.IsVersionAtLeast(2023, 6))
{
    ScriptError("Erreur 0 : Ce script fonctionne uniquement pour la version LTS de la démo et la version payante de Deltarune.");
    return;
}

if (Data?.GeneralInfo?.DisplayName?.Content.ToLower() != "deltarune chapter 1" &&
    Data?.GeneralInfo?.DisplayName?.Content.ToLower() != "deltarune chapitre 1")
{
    ScriptError("Erreur 1 : Ce script s'applique seulement au Chapitre 1.");
    return;
}

bool isDemo = false;
foreach (var str in Data.Strings)
{
    if (str.Content == "1.19")
    {
        isDemo = true;
        break;
    }
}

string versionInfo = isDemo ? "\r\n[Version détectée : Démo Itch]" : "\r\n[Version détectée : Payante]";

bool enable = ScriptQuestion(
"Ajouter le Mode Debug pour le Chapitre 1 ?" + versionInfo
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

if (isDemo)
{
    // Seulement pour la démo
    UndertaleScript CREATE_scr_debug_print = new UndertaleScript();
    CREATE_scr_debug_print.Name = Data.Strings.MakeString("scr_debug_print");
    CREATE_scr_debug_print.Code = new UndertaleCode();
    CREATE_scr_debug_print.Code.Name = Data.Strings.MakeString("gml_GlobalScript_scr_debug_print");
    CREATE_scr_debug_print.Code.LocalsCount = 1;

    Data.Scripts.Add(CREATE_scr_debug_print);
    Data.Code.Add(CREATE_scr_debug_print.Code);
}

var scr_debug_print = Data.Scripts.ByName("scr_debug_print");

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
");

ChangeSelection(scr_debug_print);

// GameObject debug gui

if (isDemo)
{
    // Seulement pour la démo
    UndertaleGameObject CREATE_obj_debug_gui = new UndertaleGameObject(); // Ajoute le GameObject
    CREATE_obj_debug_gui.Name = Data.Strings.MakeString("obj_debug_gui");
    CREATE_obj_debug_gui.Visible = (true);
    CREATE_obj_debug_gui.CollisionShape = (CollisionShapeFlags)1;
    CREATE_obj_debug_gui.Awake = (true);

    Data.GameObjects.Add(CREATE_obj_debug_gui); // Répertorie le GameObject
}

var obj_debug_gui = Data.GameObjects.ByName("obj_debug_gui");

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

importGroup.QueueReplace(obj_debug_gui.EventHandlerFor(EventType.PreCreate, (uint)0, Data), @"
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
global.dbutton_options = [""Sauts"", ""Items""];
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
");

importGroup.QueueReplace(obj_dmenu_system.EventHandlerFor(EventType.Step, (uint)0, Data), @"
global.dmenu_arrow_timer += 1;

if (global.dmenu_active)
{
    if (global.dbutton_layout == 0)
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
        
        if (global.dmenu_state != ""givertab"")
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
        else if (global.dmenu_state != ""givertab"")
        {
            scr_debug_print(string(global.dbutton_options[global.dbutton_selected - 1]) + "" sélectionné !"");
        }
        
        dmenu_state_interact();
        global.dmenu_start_index = 0;
        global.dbutton_selected = 1;
        dmenu_state_update();
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

importGroup.QueueReplace(obj_dmenu_system.EventHandlerFor(EventType.Draw, (uint)65, Data), @"
if (global.ditemcount == 0)
{
    global.ditemcount = 15;
    global.darmorcount = 7;
    global.dweaponcount = 10;
    global.dkeyitemcount = 7;
    global.drecent_item = 1;
    global.drecent_armor = 1;
    global.drecent_weapon = 1;
    global.drecent_keyitem = 1;
}

function dmenu_state_update()
{
    switch (global.dmenu_state)
    {
        case ""debug"":
            global.dmenu_title = ""Menu Debug"";
            global.dbutton_options = [""Sauts"", ""Items""];
            global.dmenu_box = 0;
            global.dbutton_layout = 0;
            break;
        
        case ""warp"":
            global.dmenu_title = ""Menu de sauts"";
            global.dbutton_options = [""Lightworld"", ""Darkworld"", ""Combats""];
            global.dmenu_box = 0;
            global.dbutton_layout = 0;
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
            global.dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
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
            global.dbutton_options = [""Entrée du Darkworld"", ""Entrée de la Citadelle"", ""Entrée des Plaines"", ""Bazar de Seam"", ""Entrée du Grand Plateau"", ""Entrée de la Forêt"", ""Vente de Pâtisseries"", ""Entrée Château"", ""Dernier étage Château"", ""Sortie Darkworld""];
            global.dmenu_box = 1;
            global.dbutton_layout = 1;
            break;
        
        case ""dw_entrance"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""castletown_entrance"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""field_entrance"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""seam_shop"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""greatboard_entrance"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""forest_entrance"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""bakesale"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""castle_entrance"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""castle_top_floor"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""darkworld_exit"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""battles"":
            global.dmenu_title = ""Menu de sauts - Combats"";
            global.dbutton_options = [""Combat contre Lancer"", ""Combat contre Kouronné"", ""Combat contre Tréflette"", ""Combat contre Susie"", ""2e Combat contre Kouronné"", ""Combat contre Jevil"", ""Combat contre  le Roi""];
            global.dmenu_box = 1;
            global.dbutton_layout = 1;
            break;
        
        case ""lancer_battle"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : Combat""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""kround_battle"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : Combat""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""clover_battle"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : Combat""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""susie_battle"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : Combat""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""kround2_battle"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : Combat""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""jevil_battle"":
            global.dmenu_title = ""Options de saut"";
            global.dbutton_options = [""Téléporter"", ""Saut : Combat avec Clé""];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case ""king_battle"":
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

            if (global.dbutton_selected == 2)
                global.plot = 1;

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
                global.dmenu_state = ""dw_entrance"";
            
            if (global.dbutton_selected == 2)
                global.dmenu_state = ""castletown_entrance"";
            
            if (global.dbutton_selected == 3)
                global.dmenu_state = ""field_entrance"";
            
            if (global.dbutton_selected == 4)
                global.dmenu_state = ""seam_shop"";
            
            if (global.dbutton_selected == 5)
                global.dmenu_state = ""greatboard_entrance"";
            
            if (global.dbutton_selected == 6)
                global.dmenu_state = ""forest_entrance"";
            
            if (global.dbutton_selected == 7)
                global.dmenu_state = ""bakesale"";
            
            if (global.dbutton_selected == 8)
                global.dmenu_state = ""castle_entrance"";
            
            if (global.dbutton_selected == 9)
                global.dmenu_state = ""castle_top_floor"";
            
            if (global.dbutton_selected == 10)
                global.dmenu_state = ""darkworld_exit"";
            
            break;
        
        case ""dw_entrance"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dark1);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 0;
                global.char[2] = 0;
                global.plot = 11;
            }
            
            break;
        
        case ""castletown_entrance"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_castle_outskirts);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 0;
                global.char[2] = 0;
                global.plot = 16;
            }
            
            break;
        
        case ""field_entrance"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_field1);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 0;
                global.plot = 33;
            }
            
            break;
        
        case ""seam_shop"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_field_shop1);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 2;
                global.plot = 40;
            }
            
            break;
        
        case ""greatboard_entrance"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_field_checkers4);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 2;
                global.plot = 50;
            }
            
            break;
        
        case ""forest_entrance"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_forest_savepoint1);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 0;
                global.plot = 60;
            }
            
            break;
        
        case ""bakesale"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_forest_savepoint2);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 0;
                global.plot = 75;
            }
            
            break;
        
        case ""castle_entrance"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_cc_1f);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 165;
            }
            
            break;
        
        case ""castle_top_floor"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_cc_5f);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 165;
            }
            
            break;
        
        case ""darkworld_exit"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_cc_prefountain);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 0;
                global.plot = 240;
            }
            
            break;
        
        case ""battles"":
            if (global.dbutton_selected == 1)
                global.dmenu_state = ""lancer_battle"";
            
            if (global.dbutton_selected == 2)
                global.dmenu_state = ""kround_battle"";
            
            if (global.dbutton_selected == 3)
                global.dmenu_state = ""clover_battle"";
            
            if (global.dbutton_selected == 4)
                global.dmenu_state = ""susie_battle"";
            
            if (global.dbutton_selected == 5)
                global.dmenu_state = ""kround2_battle"";
            
            if (global.dbutton_selected == 6)
                global.dmenu_state = ""jevil_battle"";
            
            if (global.dbutton_selected == 7)
                global.dmenu_state = ""king_battle"";
            
            break;
        
        case ""lancer_battle"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_castle_town);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 0;
                global.plot = 16;
            }
            
            break;
        
        case ""kround_battle"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_field_checkers7);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 2;
                global.plot = 55;
            }
            
            break;
        
        case ""clover_battle"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_forest_area3);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 0;
                global.plot = 72;
            }
            
            break;
        
        case ""susie_battle"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_forest_savepoint3);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 0;
                global.plot = 120;
            }
            
            break;
        
        case ""kround2_battle"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_cc_6f);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.charauto[2] = 0;
                global.plot = 165;
            }
            
            break;
        
        case ""jevil_battle"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_cc_prison_prejoker);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 165;
                global.charauto[2] = 0;
                scr_keyitemget(5);
                global.tempflag[4] = 1;
                
                repeat (13)
                    scr_weaponget(5);
            }
            
            break;
        
        case ""king_battle"":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_cc_throneroom);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.charauto[2] = 0;
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
importGroup.QueueReplace(obj_time_TOGGLER.EventHandlerFor(EventType.Step, (uint)0, Data), @"
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
if (global.debug == 1)
{
    if (keyboard_check_pressed(ord(""S"")))
        instance_create(0, 0, obj_savemenu);
    if (keyboard_check_pressed(ord(""L"")))
        scr_load();
    if (keyboard_check_pressed(ord(""R"")))
        game_restart_true();
}
if (!instance_exists(obj_dmenu_system))
    instance_create(0, 0, obj_dmenu_system)
");
ChangeSelection(obj_overworldc);

// Fonctions du Darkworld
UndertaleGameObject obj_darkcontroller = Data.GameObjects.ByName("obj_darkcontroller");
importGroup.QueueAppend(obj_darkcontroller.EventHandlerFor(EventType.Step, (uint)0, Data), @"
if (global.debug == 1)
{
    if (keyboard_check_pressed(ord(""S"")))
        instance_create(0, 0, obj_savemenu);
    if (keyboard_check_pressed(ord(""L"")))
        scr_load();
    if (keyboard_check_pressed(ord(""R"")))
        game_restart_true();
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

// Fonctions du joueur (téléportation)
UndertaleGameObject obj_mainchara = Data.GameObjects.ByName("obj_mainchara");
importGroup.QueueAppend(obj_mainchara.EventHandlerFor(EventType.Step, (uint)0, Data), @"
if (global.debug == 1)
{
    if (keyboard_check_pressed(vk_insert))
        room_goto_next();
    if (keyboard_check_pressed(vk_delete))
        room_goto_previous();
}
");
ChangeSelection(obj_mainchara);

// Fonctions du jeu (compteur FPS / fonction de pause / fonction de changement de FPS)
UndertaleGameObject obj_time = Data.GameObjects.ByName("obj_time");
importGroup.QueueReplace(obj_time.EventHandlerFor(EventType.Draw, (uint)0, Data), @"
if (global.debug == 1)
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
if (global.debug == 1)
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
UndertaleGameObject obj_battlecontroller = Data.GameObjects.ByName("obj_battlecontroller");
importGroup.QueueAppend(obj_battlecontroller.EventHandlerFor(EventType.Step, (uint)0, Data), @"
if (global.debug == 1)
{
    if (keyboard_check_pressed(ord(""W"")))
    {
        scr_wincombat();
        scr_debug_print(""Combat passé"");
    }
    if (keyboard_check_pressed(ord(""H"")))
    {
        scr_debug_fullheal();
        scr_debug_print(""HP du party restaurés"");
    }
    if (keyboard_check_pressed(ord(""V"")))
    {
        scr_turn_skip();
    }
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
ChangeSelection(obj_battlecontroller);

// Fonctions intro (skip Gaster)
UndertaleGameObject DEVICE_CONTACT = Data.GameObjects.ByName("DEVICE_CONTACT");
importGroup.QueueAppend(DEVICE_CONTACT.EventHandlerFor(EventType.Step, (uint)0, Data), @"
if (global.debug == 1)
{
    if (keyboard_check_pressed(vk_backspace))
    {
        global.flag[6] = 0;
        snd_free_all();
        room_goto(room_krisroom);
    }
}
");
ChangeSelection(DEVICE_CONTACT);

// Script fullheal
UndertaleScript scr_debug_fullheal = new UndertaleScript(); // Ajoute le Script
scr_debug_fullheal.Name = Data.Strings.MakeString("scr_debug_fullheal");
scr_debug_fullheal.Code = new UndertaleCode(); // Ajoute le Code
scr_debug_fullheal.Code.Name = Data.Strings.MakeString("gml_GlobalScript_scr_debug_fullheal");
scr_debug_fullheal.Code.LocalsCount = 1;

Data.Scripts.Add(scr_debug_fullheal); // Répertorie le Script
Data.Code.Add(scr_debug_fullheal.Code); // Répertorie le Code

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
UndertaleScript scr_turn_skip = new UndertaleScript(); // Ajoute le Script
scr_turn_skip.Name = Data.Strings.MakeString("scr_turn_skip");
scr_turn_skip.Code = new UndertaleCode(); // Ajoute le Code
scr_turn_skip.Code.Name = Data.Strings.MakeString("gml_GlobalScript_scr_turn_skip");
scr_turn_skip.Code.LocalsCount = 1;

Data.Scripts.Add(scr_turn_skip); // Répertorie le Script
Data.Code.Add(scr_turn_skip.Code); // Répertorie le Code

importGroup.QueueAppend(scr_turn_skip.Code, @"
function scr_turn_skip()
{
    if (global.mnfight == 2
    && global.turntimer > 0
    && instance_exists(obj_growtangle))
    {
        global.turntimer = 0;
        scr_debug_print(""Tour de l'ennemi passé"");
    }
}
");
ChangeSelection(scr_turn_skip);

importGroup.Import();

ScriptMessage("Mode Debug du Chapitre 1 " + (enable ? "ajouté" : "désactivé") + ".\r\n" + "Pour activer le Mode Debug en jeu, appuyer sur F10.");

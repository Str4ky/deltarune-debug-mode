// Mode Debug Custom par Jazzky et Straky

EnsureDataLoaded();

if (Data?.GeneralInfo?.DisplayName?.Content.ToLower() != "deltarune chapter 2" &&
    Data?.GeneralInfo?.DisplayName?.Content.ToLower() != "deltarune chapitre 2")
{
    ScriptError("Erreur 0 : Ce script fonctionne pour l'instant uniquement pour la version payante de Deltarune. \r\nS'applique seulement au Chapitre 2.");
    return;
}

bool enable = ScriptQuestion(@"
Ajouter les fonctions debug pour le Chapitre 2 ?
");
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

if (scr_debug_print is not null) // Vérifie que le script existe
{
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
}

// GameObject debug gui
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

importGroup.QueueReplace(obj_debug_gui.EventHandlerFor(EventType.PreCreate, (uint)0, Data), @"
event_inherited();
");

var existingVar_gui = Data.Variables.ByName("obj_debug_gui");
if (existingVar_gui == null) // Vérifie que la variable existe
{
    UndertaleVariable obj_debug_gui_VAR = new UndertaleVariable(); // Ajoute la Variable
    obj_debug_gui_VAR.Name = Data.Strings.MakeString("obj_debug_gui");
    Data.Variables.Add(obj_debug_gui_VAR); // Répertorie la Variable
}
;

importGroup.Import();

// Toggler
UndertaleScript scr_debug = Data.Scripts.ByName("scr_debug");

if (scr_debug is not null) // Vérifie que l'objet existe
{
    importGroup.QueueReplace(scr_debug.Code, @"
    function scr_debug()
    {
        return global.debug;
    }
    ");
    ChangeSelection(scr_debug);
}
importGroup.Import();

// Fonctions du Lightworld
var obj_overworldc_step = Data.Code.ByName("gml_Object_obj_overworldc_Step_0");

if (obj_overworldc_step is not null) // Vérifie que l'objet existe
{
    importGroup.QueueFindReplace(obj_overworldc_step, "if (scr_debug())", @"
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
    ChangeSelection(obj_overworldc_step);
}
importGroup.Import();

// Fonctions du Darkworld
var obj_darkcontroller_step = Data.Code.ByName("gml_Object_obj_darkcontroller_Step_0");

if (obj_darkcontroller_step is not null) // Vérifie que l'objet existe
{
    importGroup.QueueAppend(obj_darkcontroller_step, @"
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
    ");
    ChangeSelection(obj_darkcontroller_step);
}
importGroup.Import();

// Fonctions du jeu (compteur FPS / fonction de pause / fonciton de changement de FPS)
var obj_time_draw = Data.Code.ByName("gml_Object_obj_time_Draw_0");
if (obj_time_draw is not null) // Vérifie que l'objet existe
{
    importGroup.QueueReplace(obj_time_draw, @"
    if (keyboard_check_pressed(vk_f10))
    {
        global.debug = !global.debug;
    
        if (global.debug)
            scr_debug_print(""Mode Debug activé !"");
        else
            scr_debug_print(""Mode Debug désactivé !"");
    }
    
    if (scr_debug())
    {
        draw_set_font(fnt_main)
        draw_set_color(c_red)
        draw_text(__view_get(0, 0), __view_get(1, 0), fps)
    };
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
    };
    if (scr_debug())
    {
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
    ChangeSelection(obj_time_draw);
}
importGroup.Import();

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
importGroup.Import();
ChangeSelection(scr_debug_fullheal);

// Script turn skip
UndertaleScript scr_turn_skip = Data.Scripts.ByName("scr_turn_skip");

importGroup.QueueReplace(scr_turn_skip.Code, @"
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
importGroup.Import();
ChangeSelection(scr_turn_skip);

// Fonctions de combat
var obj_battlecontroller_step = Data.Code.ByName("gml_Object_obj_battlecontroller_Step_0");
if (obj_battlecontroller_step is not null) // Vérifie que l'objet existe
{   // Fullheal function
    importGroup.QueueFindReplace(obj_battlecontroller_step,
    "scr_debug_fullheal();", "");
    importGroup.QueueFindReplace(obj_battlecontroller_step,
    "if (scr_debug_keycheck(vk_f2))",
    @"if (keyboard_check_pressed(ord(""H"")))
    {
        scr_debug_fullheal();
        scr_debug_print(""HP restaurés"");
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

    importGroup.QueueAppend(obj_battlecontroller_step, @"
    if (scr_debug())
    {
        if (keyboard_check_pressed(ord(""T"")))
        {
            if (global.tension < 250)
            {
                global.tension = 250;
                scr_debug_print(""TP à 100 %"");
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
}

ScriptMessage("Mode debug du Chapitre 2 " + (enable ? "ajouté" : "désactivé") + ".\r\n" + "Pour activer le mode debug en jeu, appuyer sur F10.");

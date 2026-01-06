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
        instance_create(0, 0, obj_saveu);
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
        }      else if (room_speed == 60)
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
        snd_free_l();
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

Data.Scripts.Add(scr_debug_fullheal); // Répertorie e Script
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
UndertaleScript scr_turn_skip = new UndertaleScript(); // Aoute le Script
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
    &instance_exists(obj_growtangle))
    {
        global.turntimer = 0;
        scr_debug_print(""Tour de l'ennemi passé"");
    }
}
");
ChangeSelection(scr_turn_skip);

importGroup.Import();

ScriptMessage("Mode Debug du Chapitre 1 " + (enable ? "ajouté" : "désactivé") + ".\r\n" + "Pour activer le Mode Debug en jeu, appuyer sur F10.");

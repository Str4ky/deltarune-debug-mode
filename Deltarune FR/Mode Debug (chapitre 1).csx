// Mode Debug Custom par Jazzky et Straky

EnsureDataLoaded();

if (Data?.GeneralInfo?.DisplayName?.Content.ToLower() != "deltarune chapter 1" &&
    Data?.GeneralInfo?.DisplayName?.Content.ToLower() != "deltarune chapitre 1")
{
    ScriptError("Erreur 0 : Ce script fonctionne uniquement pour la dernière version LTS de Deltarune et s'applique seulement au Chapitre 1.");
    return;
}

bool enable = ScriptQuestion(@"
Ajouter les fonctions debug pour le Chapitre 1 ?
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
UndertaleScript scr_debug_print = new UndertaleScript(); // Ajoute le Script
scr_debug_print.Name = Data.Strings.MakeString("scr_debug_print");
scr_debug_print.Code = new UndertaleCode(); // Ajoute le Code
scr_debug_print.Code.Name = Data.Strings.MakeString("gml_GlobalScript_scr_debug_print");
scr_debug_print.Code.LocalsCount = 1;

Data.Scripts.Add(scr_debug_print); // Répertorie le Script
Data.Code.Add(scr_debug_print.Code); // Répertorie le Code


var existingVar_debug_print = Data.Variables.ByName("scr_debug_print");
if (existingVar_debug_print == null) // Vérifie que l'objet existe
{
    UndertaleVariable scr_debug_print_VAR = new UndertaleVariable(); // Ajoute la Variable
    scr_debug_print_VAR.Name = Data.Strings.MakeString("scr_debug_print");
    Data.Variables.Add(scr_debug_print_VAR); // Répertorie la Variable
}
;

UndertaleCodeLocals scr_debug_print_CL = new UndertaleCodeLocals(); // Ajoute le CL
scr_debug_print_CL.Name = Data.Strings.MakeString("gml_GlobalScript_scr_debug_print");
Data.CodeLocals.Add(scr_debug_print_CL); // Répertorie le CL

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
// Ne pas ajouter de importGroup.Import() !!!

// GameObject debug gui
UndertaleGameObject obj_debug_gui = new UndertaleGameObject(); // Ajoute le GameObject
obj_debug_gui.Name = Data.Strings.MakeString("obj_debug_gui");
obj_debug_gui.Visible = (true);
obj_debug_gui.CollisionShape = (CollisionShapeFlags)1;
obj_debug_gui.Awake = (true);
importGroup.QueueAppend(obj_debug_gui.EventHandlerFor(EventType.Create, (uint)0, Data), @"
message[0] = """";
debugmessage = """";
timer[0] = 90;
newtext = """";
messagecount = 0;
totaltimer = 0;
");

importGroup.QueueAppend(obj_debug_gui.EventHandlerFor(EventType.Step, (uint)0, Data), @"
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

importGroup.QueueAppend(obj_debug_gui.EventHandlerFor(EventType.Draw, (uint)64, Data), @"
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

importGroup.QueueAppend(obj_debug_gui.EventHandlerFor(EventType.PreCreate, (uint)0, Data), @"
event_inherited();
");

Data.GameObjects.Add(obj_debug_gui); // Répertorie le GameObject

var existingVar_gui = Data.Variables.ByName("obj_debug_gui");
if (existingVar_gui == null) // Vérifie que l'objet existe
{
    UndertaleVariable obj_debug_gui_VAR = new UndertaleVariable(); // Ajoute la Variable
    obj_debug_gui_VAR.Name = Data.Strings.MakeString("obj_debug_gui");
    Data.Variables.Add(obj_debug_gui_VAR); // Répertorie la Variable
}
;

importGroup.Import();
ChangeSelection(scr_debug_print.Code);

// Variable au gamestart
var scr_gamestart = Data.Code.ByName("gml_GlobalScript_scr_gamestart");

if (scr_gamestart is not null) // Vérifie que l'objet existe
{
    importGroup.QueueAppend("gml_GlobalScript_scr_gamestart", @"
    global.debug = 0;
    ");
    ChangeSelection(scr_gamestart);
}
importGroup.Import();

// Toggler
var debugcontroller_step = Data.Code.ByName("gml_Object_obj_debugcontroller_Step_0");

if (debugcontroller_step is not null) // Vérifie que l'objet existe
{
    importGroup.QueueReplace("gml_Object_obj_debugcontroller_Step_0", @"
    if (keyboard_check_pressed(vk_f10))
    {
        global.debug = !global.debug;
        debug = !debug;
        god = !god;
        if (global.debug)
            scr_debug_print(""Mode Debug activé !"");
        else
            scr_debug_print(""Mode Debug désactivé !"");
    }
    ");
    ChangeSelection(debugcontroller_step);
}
importGroup.Import();

// Fonctions du Lightworld
var obj_overworldc = Data.Code.ByName("gml_Object_obj_overworldc_Step_0");

if (obj_overworldc is not null) // Vérifie que l'objet existe
{
    importGroup.QueueAppend("gml_Object_obj_overworldc_Step_0", @"
    if (global.debug == 1)
    {
        if (keyboard_check_pressed(ord(""S"")))
            instance_create(0, 0, obj_savemenu);
        if (keyboard_check_pressed(ord(""L"")))
            scr_load();
        if (keyboard_check_pressed(ord(""R"")))
            game_restart_true();
    }
    ");
    ChangeSelection(obj_overworldc);
}
importGroup.Import();

// Fonctions du Darkworld
var obj_darkcontroller = Data.Code.ByName("gml_Object_obj_darkcontroller_Step_0");

if (obj_darkcontroller is not null) // Vérifie que l'objet existe
{
    importGroup.QueueAppend("gml_Object_obj_darkcontroller_Step_0", @"
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
    ");
    ChangeSelection(obj_darkcontroller);
}
importGroup.Import();

// Fonctions du joueur (téléportation)
var obj_mainchara = Data.Code.ByName("gml_Object_obj_mainchara_Step_0");
if (obj_mainchara is not null) // Vérifie que l'objet existe
{
    importGroup.QueueAppend("gml_Object_obj_mainchara_Step_0", @"
    if (global.debug == 1)
    {
        if (keyboard_check_pressed(vk_insert))
            room_goto_next();
        if (keyboard_check_pressed(vk_delete))
            room_goto_previous();
    }
    ");
    ChangeSelection(obj_mainchara);
}
importGroup.Import();

// Fonctions du jeu (compteur FPS / fonction de pause / fonction de changement de FPS)
var obj_time = Data.Code.ByName("gml_Object_obj_time_Draw_0");
if (obj_time is not null) // Vérifie que l'objet existe
{
    importGroup.QueueReplace("gml_Object_obj_time_Draw_0", @"
    if (global.debug == 1)
    {
        draw_set_font(fnt_main)
        draw_set_color(c_red)
        draw_text(__view_get(0, 0), __view_get(1, 0), fps)
    };
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
    };
    if (global.debug == 1)
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
    ChangeSelection(obj_time);
}
importGroup.Import();

// Fonctions intro (skip Gaster)
var obj_device_contact = Data.Code.ByName("gml_Object_DEVICE_CONTACT_Step_0");
if (obj_device_contact is not null) // Vérifie que l'objet existe
{
    importGroup.QueueAppend("gml_Object_DEVICE_CONTACT_Step_0", @"
    if (global.debug == 1)
    {
        if (keyboard_check_pressed(vk_backspace))
        {
            global.flag[6] = 0;
            snd_free_all();
            room_goto(room_krisroom);
            scr_debug_print(""Segment passé"");
        }
    }
    ");
    ChangeSelection(obj_device_contact);
}
importGroup.Import();

// Script fullheal
UndertaleScript scr_debug_fullheal = new UndertaleScript(); // Ajoute le Script
scr_debug_fullheal.Name = Data.Strings.MakeString("scr_debug_fullheal");
scr_debug_fullheal.Code = new UndertaleCode(); // Ajoute le Code
scr_debug_fullheal.Code.Name = Data.Strings.MakeString("gml_GlobalScript_scr_debug_fullheal");
scr_debug_fullheal.Code.LocalsCount = 1;

Data.Scripts.Add(scr_debug_fullheal); // Répertorie le Script
Data.Code.Add(scr_debug_fullheal.Code); // Répertorie le Code

var existingVar_debug_fullheal = Data.Variables.ByName("scr_debug_fullheal");
if (existingVar_debug_fullheal == null) // Add only if it doesn't already exist
{
    UndertaleVariable scr_debug_fullheal_VAR = new UndertaleVariable(); // Ajoute la variable
    scr_debug_fullheal_VAR.Name = Data.Strings.MakeString("scr_debug_fullheal");
    Data.Variables.Add(scr_debug_fullheal_VAR);
}
;

UndertaleCodeLocals scr_debug_fullheal_CL = new UndertaleCodeLocals(); // Ajoute le CL
scr_debug_fullheal_CL.Name = Data.Strings.MakeString("gml_GlobalScript_scr_debug_fullheal");
Data.CodeLocals.Add(scr_debug_fullheal_CL); // Répertorie le CL

importGroup.QueueAppend(scr_debug_fullheal.Code, @"
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
UndertaleScript scr_turn_skip = new UndertaleScript(); // Ajoute le Script
scr_turn_skip.Name = Data.Strings.MakeString("scr_turn_skip");
scr_turn_skip.Code = new UndertaleCode(); // Ajoute le Code
scr_turn_skip.Code.Name = Data.Strings.MakeString("gml_GlobalScript_scr_turn_skip");
scr_turn_skip.Code.LocalsCount = 1;

Data.Scripts.Add(scr_turn_skip); // Répertorie le Script
Data.Code.Add(scr_turn_skip.Code); // Répertorie le Code

var existingVar_turn_skip = Data.Variables.ByName("scr_turn_skip");
if (existingVar_turn_skip == null) // Add only if it doesn't already exist
{
    UndertaleVariable scr_turn_skip_VAR = new UndertaleVariable(); // Ajoute la variable
    scr_turn_skip_VAR.Name = Data.Strings.MakeString("scr_turn_skip");
    Data.Variables.Add(scr_turn_skip_VAR);
}
;

UndertaleCodeLocals scr_turn_skip_CL = new UndertaleCodeLocals(); // Ajoute le CL
scr_turn_skip_CL.Name = Data.Strings.MakeString("gml_GlobalScript_scr_turn_skip");
Data.CodeLocals.Add(scr_turn_skip_CL); // Répertorie le CL

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
importGroup.Import();
ChangeSelection(scr_turn_skip);

// Fonctions de combat
var obj_battlecontroller = Data.Code.ByName("gml_Object_obj_battlecontroller_Step_0");
if (obj_battlecontroller is not null) // Vérifie que l'objet existe
{
    importGroup.QueueAppend("gml_Object_obj_battlecontroller_Step_0", @"
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
    ChangeSelection(obj_battlecontroller);
}
importGroup.Import();

ScriptMessage("Mode debug du Chapitre 1 " + (enable ? "ajouté" : "désactivé") + ".\r\n" + "Pour activer le mode debug en jeu, appuyer sur F10.");

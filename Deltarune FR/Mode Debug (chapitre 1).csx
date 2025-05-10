// Active ou désactive le mode debug pour le Chapitre 1

EnsureDataLoaded();

bool enable = ScriptQuestion(@"
Activer ou désactiver le mode debug pour le Chapitre 1 ?

Oui = Activer le mode debug
Non = Désactiver le mode debug

(Choisissez bien l'option souhaitée. Si vous avez une erreur, c'est que vous avez choisi la mauvaise.)
");

GlobalDecompileContext globalDecompileContext = new(Data);
Underanalyzer.Decompiler.IDecompileSettings decompilerSettings = new Underanalyzer.Decompiler.DecompileSettings();
UndertaleModLib.Compiler.CodeImportGroup importGroup = new(Data, globalDecompileContext, decompilerSettings)
{
    ThrowOnNoOpFindReplace = true
};

// Variable gamestart
var gamestart = Data.Code.ByName("gml_GlobalScript_scr_gamestart");

if (gamestart is not null) // Vérifie que l'objet existe
{
    importGroup.QueueAppend("gml_GlobalScript_scr_gamestart",
    @"global.debug = 0;"
);
    ChangeSelection(gamestart);
}
importGroup.Import();

// Debug toggler
var debugcontroller_step = Data.Code.ByName("gml_Object_obj_debugcontroller_Step_0");

if (debugcontroller_step is not null) // Vérifie que l'objet existe
{
    importGroup.QueueReplace("gml_Object_obj_debugcontroller_Step_0",
    @"if (keyboard_check_pressed(vk_f10))
    {
        global.debug = !global.debug;
        debug = !debug;
        god = !god;
    }"
);
    ChangeSelection(debugcontroller_step);
}
importGroup.Import();

// Fonctions de sauvegarde - overworld
var obj_overworldc = Data.Code.ByName("gml_Object_obj_overworldc_Step_0");

if (obj_overworldc is not null) // Vérifie que l'objet existe
{
    importGroup.QueueAppend("gml_Object_obj_overworldc_Step_0",
    @"if (global.debug == 1)
    {
        if (keyboard_check_pressed(ord(""S"")))
            instance_create(0, 0, obj_savemenu);
        if (keyboard_check_pressed(ord(""L"")))
            scr_load();
        if (keyboard_check_pressed(ord(""R"")))
            game_restart_true();
    }"
);
    ChangeSelection(obj_overworldc);
}
importGroup.Import();

// Fonctions de sauvegarde - darkworld
var obj_darkcontroller = Data.Code.ByName("gml_Object_obj_darkcontroller_Step_0");

if (obj_darkcontroller is not null) // Vérifie que l'objet existe
{
    importGroup.QueueAppend("gml_Object_obj_darkcontroller_Step_0",
    @"if (global.debug == 1)
    {
        if (keyboard_check_pressed(ord(""S"")))
            instance_create(0, 0, obj_savemenu);
        if (keyboard_check_pressed(ord(""L"")))
            scr_load();
        if (keyboard_check_pressed(ord(""R"")))
            game_restart_true();
    }"
);
    ChangeSelection(obj_darkcontroller);
}
importGroup.Import();

// Fonctions de téléportation
var obj_mainchara = Data.Code.ByName("gml_Object_obj_mainchara_Step_0");
if (obj_mainchara is not null) // Vérifie que l'objet existe
{
    importGroup.QueueAppend("gml_Object_obj_mainchara_Step_0",
    @"if (global.debug == 1)
    {
        if (keyboard_check_pressed(vk_insert))
            room_goto_next();
        if (keyboard_check_pressed(vk_delete))
            room_goto_previous();
    }"
);
    ChangeSelection(obj_mainchara);
}
importGroup.Import();

// Compteur FPS
var obj_time = Data.Code.ByName("gml_Object_obj_time_Draw_0");
if (obj_time is not null) // Vérifie que l'objet existe
{
    importGroup.QueueReplace("gml_Object_obj_time_Draw_0",
    @"if (global.debug == 1)
    {
        draw_set_font(fnt_main)
        draw_set_color(c_red)
        draw_text(__view_get(0, 0), __view_get(1, 0), fps)
    }"
);
    ChangeSelection(obj_time);
}
importGroup.Import();

// Fonctions de pause
var obj_draw = Data.Code.ByName("gml_Object_obj_time_Draw_0");
if (obj_draw is not null) // Vérifie que l'objet existe
{
    importGroup.QueueAppend("gml_Object_obj_time_Draw_0",
    @"if (global.debug == 1)
    {
        if (keyboard_check_pressed(ord(""P"")))
        {
            if (room_speed == 30)
                room_speed = 1;
            else
                room_speed = 30;
        }
    }"
    );
    ChangeSelection(obj_draw);
}
importGroup.Import();

// Skip Gaster
var obj_device_contact = Data.Code.ByName("gml_Object_DEVICE_CONTACT_Step_0");
if (obj_device_contact is not null) // Vérifie que l'objet existe
{
    importGroup.QueueAppend("gml_Object_DEVICE_CONTACT_Step_0",
    @"if (global.debug == 1)
    {
        if (keyboard_check_pressed(vk_backspace))
        {
            global.flag[6] = 0;
            snd_free_all();
            room_goto(room_krisroom);
        }
    }"
    );
    ChangeSelection(obj_device_contact);
}
importGroup.Import();

// Fonctions de combat

// Script fullheal
Data.Scripts.Add(new UndertaleScript() // Ajoute et répertorie le Script
{
    Name = Data.Strings.MakeString("scr_debug_fullheal"),
    Code = new UndertaleCode() // Ajoute le Code
    {
        Name = Data.Strings.MakeString("gml_GlobalScript_scr_debug_fullheal"),
        LocalsCount = 1,
        
    }
});
Data.Code.Add(Data.Scripts[Data.Scripts.Count - 1].Code); // Répertorie le Code

var existingVar = Data.Variables.ByName("scr_debug_fullheal");
if (existingVar == null) // Add only if it doesn't already exist
{
    var debug_fullheal_var = new UndertaleVariable() // Ajoute la variable
    {
        Name = Data.Strings.MakeString("scr_debug_fullheal"),
    };
    Data.Variables.Add(debug_fullheal_var);
}

Data.CodeLocals.Add(new UndertaleCodeLocals() // Ajoute et répertorie le CodeLocals
{
    Name = Data.Strings.MakeString("gml_GlobalScript_scr_debug_fullheal"),
}
);

importGroup.QueueAppend(Data.Scripts[Data.Scripts.Count - 1].Code, @"
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
}");

importGroup.Import();
ChangeSelection(Data.Scripts[Data.Scripts.Count - 1]);

// Script battlecontroller
var obj_battlecontroller = Data.Code.ByName("gml_Object_obj_battlecontroller_Step_0");
if (obj_battlecontroller is not null) // Vérifie que l'objet existe
{
    importGroup.QueueAppend("gml_Object_obj_battlecontroller_Step_0",
    @"if (global.debug == 1)
    {
        if (keyboard_check_pressed(ord(""W"")))
            scr_wincombat();
        if (keyboard_check_pressed(ord(""H"")))
            scr_debug_fullheal();
        if (keyboard_check_pressed(ord(""T"")))
        {
            if (global.tension < 250)
                global.tension = 250;
            else
                global.tension = 0;
        }
    }"
);
    ChangeSelection(obj_battlecontroller);
}
importGroup.Import();

ScriptMessage("Mode debug du Chapitre 1 " + (enable ? "activé" : "désactivé") + ".\r\n" + "Pour activer le mode debug en jeu, appuyez sur F10.");

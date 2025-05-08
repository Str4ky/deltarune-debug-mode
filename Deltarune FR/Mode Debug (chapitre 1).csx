// Active ou désactive le mode debug pour le Chapitre 1

EnsureDataLoaded();

bool enable = ScriptQuestion(@"
Activer ou désactiver le mode debug pour le Chapitre 1 ?

Oui = Activer le mode debug
Non = Désactiver le mode debug

(Choisissez bien l'option souhaitée. Si vous avez une erreur, c'est que vous avez choisi la mauvaise.)
");

UndertaleModLib.Compiler.CodeImportGroup importGroup = new(Data)
{
    ThrowOnNoOpFindReplace = true
};

// Chapitre 1 : changer les valeurs "false" en "true" dans le contrôleur debug (désuet)
//string debugController = "gml_Object_obj_debugcontroller_Create_0";

//if (Data.Code.ByName(debugController) == null)
//    throw new ScriptException("Impossible de trouver : debugcontroller du Chapitre 1.");

//if (enable) {
//    importGroup.QueueFindReplace(debugController, "false", "true");
//}
//else
//{
//    importGroup.QueueFindReplace(debugController, "true", "false");
//}
//importGroup.Import();

//Variable gamestart
var gamestart = Data.Code.ByName("gml_GlobalScript_scr_gamestart");

if (gamestart is not null) // Vérifie que l'objet existe
{
    importGroup.QueueAppend("gml_GlobalScript_scr_gamestart",
    "global.debug = 0;"
);
    ChangeSelection(gamestart);
}

// Debug toggler
var debugcontroller_step = Data.Code.ByName("gml_Object_obj_debugcontroller_Step_0");

if (debugcontroller_step is not null) // Vérifie que l'objet existe
{
    importGroup.QueueReplace("gml_Object_obj_debugcontroller_Step_0",
    "if (keyboard_check_pressed(vk_f10))\r\n" +
    "{\r\n" +
    "    global.debug = !global.debug;\r\n" +
    "    debug = !debug;\r\n" +
    "    god = !god\r\n;" +
    "}"
);
    ChangeSelection(debugcontroller_step);
}

// Fonctions de sauvegarde - overworld
var obj_overworldc = Data.Code.ByName("gml_Object_obj_overworldc_Step_0");

if (obj_overworldc is not null) // Vérifie que l'objet existe
{
    importGroup.QueueAppend("gml_Object_obj_overworldc_Step_0",
    "if (global.debug == 1)\r\n" +
    "{\r\n" +
    "    if (keyboard_check_pressed(ord(\"S\")))\r\n" +
    "        instance_create(0, 0, obj_savemenu);\r\n" +
    "    if (keyboard_check_pressed(ord(\"L\")))\r\n" +
    "        scr_load();\r\n" +
    "    if (keyboard_check_pressed(ord(\"R\")))\r\n" +
    "        game_restart_true(); \r\n" +
    "}"
);
    ChangeSelection(obj_overworldc);
}

// Fonctions de sauvegarde - darkworld
var obj_darkcontroller = Data.Code.ByName("gml_Object_obj_darkcontroller_Step_0");

if (obj_darkcontroller is not null) // Vérifie que l'objet existe
{
    importGroup.QueueAppend("gml_Object_obj_darkcontroller_Step_0",
    "if (global.debug == 1)\r\n" +
    "{\r\n" +
    "    if (keyboard_check_pressed(ord(\"S\")))\r\n" +
    "        instance_create(0, 0, obj_savemenu);\r\n" +
    "    if (keyboard_check_pressed(ord(\"L\")))\r\n" +
    "        scr_load();\r\n" +
    "    if (keyboard_check_pressed(ord(\"R\")))\r\n" +
    "        game_restart_true(); \r\n" +
    "}"
);
    ChangeSelection(obj_darkcontroller);
}
importGroup.Import();
// Fonctions de téléportation
var obj_mainchara = Data.Code.ByName("gml_Object_obj_mainchara_Step_0");
if (obj_mainchara is not null) // Vérifie que l'objet existe
{
    importGroup.QueueAppend("gml_Object_obj_mainchara_Step_0",
    "if (global.debug == 1)\r\n" +
    "{\r\n" +
    "    if (keyboard_check_pressed(vk_insert))\r\n" +
    "        room_goto_next();\r\n" +
    "    if (keyboard_check_pressed(vk_delete))\r\n" +
    "        room_goto_previous();\r\n" +
    "}"
);
    ChangeSelection(obj_mainchara);
}

// Compteur FPS
var obj_time = Data.Code.ByName("gml_Object_obj_time_Draw_0");
if (obj_time is not null) // Vérifie que l'objet existe
{
    importGroup.QueueReplace("gml_Object_obj_time_Draw_0",
    "if (global.debug == 1)\r\n" +
    "{\r\n" +
    "    draw_set_font(fnt_main)\r\n" +
    "    draw_set_color(c_red)\r\n" +
    "    draw_text(__view_get(0, 0), __view_get(1, 0), fps)\r\n" +
    "}"
);
    ChangeSelection(obj_time);
}

// Fonctions de pause
var obj_draw = Data.Code.ByName("gml_Object_obj_time_Draw_0");
if (obj_draw is not null) // Vérifie que l'objet existe
{
    importGroup.QueueAppend("gml_Object_obj_time_Draw_0",
    "if (global.debug == 1)\r\n" +
    "{\r\n" +
    "    if (keyboard_check_pressed(ord(\"P\")))\r\n" +
    "    {\r\n" +
    "        if (room_speed == 30)\r\n" +
    "            room_speed = 1;\r\n" +
    "        else\r\n" +
    "            room_speed = 30;\r\n" +
    "    }\r\n" +
    "}"
    );
    ChangeSelection(obj_draw);
}

// Skip Gaster
var obj_device_contact = Data.Code.ByName("gml_Object_DEVICE_CONTACT_Step_0");
if (obj_device_contact is not null) // Vérifie que l'objet existe
{
    importGroup.QueueAppend("gml_Object_DEVICE_CONTACT_Step_0",
    "if (global.debug == 1)\r\n" +
    "{\r\n" +
    "    if (keyboard_check_pressed(vk_backspace))\r\n" +
    "    {\r\n" +
    "        global.flag[6] = 0;\r\n" +
    "        snd_free_all();\r\n" +
    "        room_goto(room_krisroom);" +
    "    }\r\n" +
    "}"
    );
    ChangeSelection(obj_device_contact);
}

importGroup.Import();
ScriptMessage("Mode debug du Chapitre 1 " + (enable ? "activé" : "désactivé") + ".\r\n" + "Pour activer le mode debug en jeu, appuyez sur F10.");

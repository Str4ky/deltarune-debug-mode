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
dmenu_title = ""Menu Debug"";
dbutton_options_original = [""Sauts"", ""Items"", ""Recrues"", ""Divers""];
dnumber_litems = [0, 11, 14, 14, 18];
dlight_weapons = [];
dlight_armors = [[3, ""Pansement""], [14, ""Montre""]];
dlight_objects = [[1, ""Chocolat Chaud""], [2, ""Crayon""], [3, ""Pansement""], [4, ""Bouquet""], [5, ""Boule de Trucs""], [6, ""Crayon Halloween""], [7, ""Crayon Fétiche""], [8, ""Œuf""], [9, ""Cartes""], [10, ""Boîte de ChocoCœurs""], [11, ""Verre""], [12, ""Gomme""], [13, ""Critérium""], [14, ""Montre""], [15, ""Crayon de Noël""], [16, ""Épine de Cactus""], [17, ""ÉclatNoir""], [18, ""Stylo-Plume""]];

if (global.chapter >= 4)
    dtemp_lst = get_lw_dw_weapon_list();
else
    dlight_weapons = [[2, ""Crayon""], [6, ""Crayon Halloween""], [7, ""Crayon Fétiche""], [12, ""Gomme""], [13, ""Critérium""]];

for (i = 0; i < array_length(dlight_objects); i++)
{
    if (dlight_objects[i][0] > dnumber_litems[global.chapter])
        array_delete(dlight_objects, i--, 1);
    else if (global.chapter >= 4 && i == (dtemp_lst[array_length(dlight_weapons)].lw_id - 1))
        array_push(dlight_weapons, dlight_objects[i]);
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
EGG = cate_enum++;
ONION_SAN = cate_enum++;
MISC1 = cate_enum++;
MISC2 = cate_enum++;
LOT = cate_enum++;
SWORD = cate_enum++;
MISC3 = cate_enum++;
MISC4 = cate_enum++;
MOUSSE = cate_enum++;
ROBOTEUR = cate_enum++;
dother_categories = [""Séquence Vaisseau"", ""Superbosses"", ""Weird Route"", ""Œufs"", ""Onion San"", ""Misc chap 1"", ""Misc chap 2"", ""Legend of Tenna"", ""Sword Route"", ""Misc chap 3"", ""Misc chap 4"", ""Mousse"", ""Roboteur""];
dother_all_options = [];
dother_options = [];

if (global.chapter >= 0)
{
    array_push(dother_all_options, [GONER, ""NOURRITURE"", 903, [[""SUCRÉE"", 0], [""TENDRE"", 1], [""AMÈRE"", 2], [""SALÉE"", 3], [""DOULEUR"", 4], [""FROIDE"", 5]]]);
    array_push(dother_all_options, [GONER, ""GROUPE SANGUIN"", 904, [[""A"", 0], [""AB"", 1], [""B"", 2], [""C"", 3], [""D"", 4]]]);
    array_push(dother_all_options, [GONER, ""COULEUR"", 905, [[""ROUGE"", 0], [""BLEU"", 1], [""VERT"", 2], [""CYAN"", 3]]]);
    array_push(dother_all_options, [GONER, ""PRÉSENT"", 909, [[""GENTILLESSE"", -1], [""ESPRIT"", 0], [""AMBITION"", 1], [""BRAVOURE"", 2], [""VOIX"", 3]]]);
    array_push(dother_all_options, [GONER, ""SENTIMENT ÉPROUVÉ"", 906, [[""AMOUR"", 0], [""ESPOIR"", 1], [""DÉGOÛT"", 2], [""PEUR"", 3]]]);
    array_push(dother_all_options, [GONER, ""RÉPONDU HONNÊTEMENT"", 907, [[""OUI"", 0], [""NON"", 1]]]);
    array_push(dother_all_options, [GONER, ""CONSENTIR AUX CRISES"", 908, [[""OUI"", 0], [""NON"", 1]]]);
}

if (global.chapter >= 1)
{
    array_push(dother_all_options, [ROBOTEUR, ""Tête Roboteur"", 220, [[""Laser"", 0], [""Épée"", 1], [""Flamme"", 2], [""Canard"", 3]]]);
    array_push(dother_all_options, [ROBOTEUR, ""Corps Roboteur"", 221, [[""Sobre"", 0], [""Roue"", 1], [""Tank"", 2], [""Canard"", 3]]]);
    array_push(dother_all_options, [ROBOTEUR, ""Jambes Roboteur"", 222, [[""Baskets"", 0], [""Pneus"", 1], [""Chaînes"", 2], [""Canard"", 3]]]);
    array_push(dother_all_options, [MISC1, ""Nom du gang"", 214, [[""Les Types (unused)"", 0], [""L'Escouade $?$!$"", 1], [""Le Fan Club Lancer"", 2], [""Le Fun Gang"", 3]]]);
    array_push(dother_all_options, [MISC1, ""Prophétie entendu"", 203, [[""Non"", 1], [""Oui"", 0]]]);
    array_push(dother_all_options, [MISC1, ""Manuel jeté"", 207, [[""Non"", 0], [""A tenté"", 1], [""L'a jeté"", 2]]]);
    array_push(dother_all_options, [MISC1, ""Gâteau rendu"", 253, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [MISC1, ""Starwalker"", 254, [[""Pissing me off"", 0], [""I will   join"", 1]]]);
    array_push(dother_all_options, [MISC1, ""Objectif de Donation"", 216, [[""Non rempli"", 0], [""Atteint"", 1]]]);
    array_push(dother_all_options, [MISC1, ""Fleurs d'Asgore"", 262, [[""Pas vu"", 0], [""Pas données"", 2], [""Données"", 4]]]);
    array_push(dother_all_options, [MISC1, ""Noëlle dehors"", 276, [[""Pas parlé"", 0], [""Pas parlé de Susie"", 1], [""A parlé de Susie"", 2]]]);
    array_push(dother_all_options, [MISC1, ""Évier inspecté (chap 1)"", 278, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [EGG, ""Œuf obtenu (chap 1)"", 911, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [SUPERBOSS, ""Jevil vaincu"", 241, [[""Non"", 0], [""Via violence"", 6], [""Via clémence"", 7]]]);
    array_push(dother_all_options, [ONION_SAN, ""Relation (chap 1)"", 258, [[""Pas vu"", 0], [""Amis"", 2], [""Pas amis"", 3]]]);
    array_push(dother_all_options, [ONION_SAN, ""Nom de Kris"", 259, [[""Pas donné"", 0], [""Kris"", 1], [""Hippopotame"", 2]]]);
    array_push(dother_all_options, [ONION_SAN, ""Nom d'Onion"", 260, [[""Pas donné"", 0], [""Onion"", 1], [""Beauté"", 2], [""Asriel II"", 3], [""Dégoûtant"", 4]]]);
    array_push(dother_all_options, [MOUSSE, ""Mousse mangée (chap 1)"", 106, [[""Non"", 0], [""Oui"", 1]]]);
}

if (global.chapter >= 2)
{
    array_push(dother_all_options, [MISC2, ""Peluche"", 307, [[""Pas donnée"", 0], [""Ralsei"", 1], [""Susie"", 2], [""Noëlle"", 3], [""Berdly"", 4]]]);
    array_push(dother_all_options, [MISC2, ""Hacker recruté"", 659, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [MISC2, ""Bras de Berdly"", 457, [[""Brûlé"", 0], [""Ok"", 1]]]);
    array_push(dother_all_options, [WEIRD2, ""Avancée"", 915, [[""Pas fait"", 0], [""Nikomercant tué"", 3], [""Berdly gelé"", 6], [""A parlé a Susie"", 9], [""Noëlle vue a l'hôpital"", 20]]]);
    array_push(dother_all_options, [WEIRD2, ""A cancel la weird route"", 916, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [EGG, ""Œuf obtenu (chap 2)"", 918, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [SUPERBOSS, ""Spamton vaincu"", 309, [[""Non"", 0], [""Oui"", 9]]]);
    array_push(dother_all_options, [MISC2, ""\""Fan\"" de Mettaton"", 422, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [MISC2, ""Statue de Susie récupérée"", 393, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [MISC2, ""ICE-E récupéré"", 394, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [MISC2, ""Évier inspecté (chap 2)"", 461, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [ONION_SAN, ""Relation (chap 2)"", 425, [[""Pas vu"", 0], [""Amis"", 1], [""Plus amis"", 2]]]);
    array_push(dother_all_options, [MOUSSE, ""Mousse mangée (chap 2)"", 920, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [MOUSSE, ""... avec Noëlle (chap 2)"", 921, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [MOUSSE, ""... avec Susie (chap 2)"", 922, [[""Non"", 0], [""Oui"", 1]]]);
}

if (global.chapter >= 3)
{
    array_push(dother_all_options, [LOT, ""LOT Rang Board 1"", 1173, [[""Z"", 0], [""C"", 1], [""B"", 2], [""A"", 3], [""S"", 4], [""T"", 5]]]);
    array_push(dother_all_options, [LOT, ""LOT Rang Board 2"", 1174, [[""Z"", 0], [""C"", 1], [""B"", 2], [""A"", 3], [""S"", 4], [""T"", 5]]]);
    array_push(dother_all_options, [SWORD, ""Avancé"", 1055, [[""Pas fait"", 0], [""Clé de glace obtenue"", 1], [""Donjon (plateau 2)"", 1.5], [""Elle a été utilisée"", 2], [""Clé de l'abri obtenue"", 3], [""Donjon (plateau 3)"", 4], [""Clé de l'abri utilisée"", 5], [""ERAM vaincu"", 6]]]);
    array_push(dother_all_options, [SWORD, ""Susie attaquée"", 1268, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [EGG, ""Œuf obtenu (chap 3)"", 930, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [SUPERBOSS, ""Chevalier vaincu"", 1047, [[""Non"", 2], [""Oui"", 1]]]);
    array_push(dother_all_options, [MISC3, ""Fontaine"", 1144, [[""Pas affronté"", 0], [""A flirt(pas parlé au rideau)"", 1], [""Pas flirté"", 2], [""A flirt(a parlé au rideau)"", 3]]]);
    array_push(dother_all_options, [MISC3, ""Statue de Tenna récupérée"", 1222, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [MOUSSE, ""Mousse mangée (chap 3)"", 1078, [[""Non"", 0], [""Oui"", 1]]]);
}

if (global.chapter >= 4)
{
    array_push(dother_all_options, [EGG, ""Œuf obtenu (chap 4)"", 931, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [SUPERBOSS, ""Gerson vaincu"", 1629, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [MOUSSE, ""Mousse mangée (chap 4)"", 1592, [[""Non"", 0], [""Oui"", 1], [""Refusée"", 2]]]);
    array_push(dother_all_options, [MISC4, ""Prière"", 1507, [[""Pas prié"", 0], [""Pour Susie"", 1], [""Pour Noëlle"", 2], [""Pour Asriel"", 3]]]);
    array_push(dother_all_options, [MISC4, ""Prix Susie récupéré"", 747, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [MISC4, ""Tache retirée"", 748, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [MISC4, ""Tenna donné"", 779, [[""Non"", 0], [""Oui"", 2]]]);
    array_push(dother_all_options, [MISC4, ""Échelle récupérée"", 864, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [MISC4, ""Oreiller récupéré"", 865, [[""Non"", 0], [""Oui"", 1]]]);
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
    
    for (i = 0; i < array_length(lst); i++)
    {
        if (value == lst[i][1])
            return i;
    }
    
    return 0;
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
        if (!scr_84_is_digit(string_char_at(dcustom_flag_text[0], c)))
        {
            scr_debug_print(""Invalid flag |"" + dcustom_flag_text[0] + ""| because of |"" + string_char_at(dcustom_flag_text[0], c) + ""|"");
            proper_exit = 0;
            break;
        }
    }
    
    if (string_length(dcustom_flag_text[0]) == 0)
    {
        scr_debug_print(""Empty flag"");
        proper_exit = 0;
    }
    
    if (dmenu_state == ""warp_options"")
        return proper_exit;
    
    for (c = 1; c <= string_length(dcustom_flag_text[1]); c++)
    {
        if (!scr_84_is_digit(string_char_at(dcustom_flag_text[1], c)) && string_char_at(dcustom_flag_text[1], c) != ""."")
        {
            scr_debug_print(""Invalid value |"" + dcustom_flag_text[1] + ""|"");
            proper_exit = 0;
            break;
        }
    }
    
    if (string_length(dcustom_flag_text[1]) == 0)
    {
        if (proper_exit)
            scr_debug_print(""global.flag["" + string(real(dcustom_flag_text[0])) + ""] = |"" + string(global.flag[real(dcustom_flag_text[0])]) + ""|"");
        else
            scr_debug_print(""Empty value"");
        
        proper_exit = 0;
    }
    
    if (proper_exit)
    {
        scr_debug_print(""Updated global.flag["" + string(real(dcustom_flag_text[0])) + ""] from |"" + string(global.flag[real(dcustom_flag_text[0])]) + ""| to |"" + dcustom_flag_text[1] + ""|"");
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
    
    will_exit = keyboard_check_pressed(vk_escape) || keyboard_check_pressed(global.input_k[4]) || keyboard_check_pressed(global.input_k[7]);
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
    else if (keyboard_check_pressed(vk_backspace))
    {
        dcustom_flag_text[dhorizontal_index] = string_delete(dcustom_flag_text[dhorizontal_index], string_length(dcustom_flag_text[dhorizontal_index]), 1);
        keyboard_string = """";
    }
    else if (keyboard_check_pressed(vk_anykey))
    {
        dcustom_flag_text[dhorizontal_index] += string(keyboard_string);
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
                scr_debug_print(""Updated global.flag["" + string(cur_options[2]) + ""] to |"" + string(cur_options[3][dhorizontal_index][1]) + ""|"");
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
                        to_add = -to_add;
                    
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
                        
                        scr_debug_print(dgiver_bname + "" sélectionné !"");
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
                        
                        scr_debug_print(string(dgiver_bname) + "" sélectionné !"");
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
                        
                        scr_debug_print(string(dgiver_bname) + "" sélectionné !"");
                    }
                    
                    break;
                
                case ""keyitems"":
                    if (dbutton_selected > 1)
                    {
                        real_index = dbutton_indices[dbutton_selected - 1];
                        scr_keyiteminfo(real_index);
                        dgiver_bname = tempkeyitemname;
                        scr_debug_print(string(dgiver_bname) + "" sélectionné !"");
                    }
                    
                    break;
            }
        }
        else if (dmenu_state == ""warp"" && dbutton_selected == 2)
        {
            scr_debug_print(""Recherche sélectionné !"");
        }
        else if (dmenu_state != ""givertab"" && dmenu_state != ""flag_misc"" && dmenu_state != ""warp_options"" && (dmenu_state != ""recruits"" || dbutton_selected == 1))
        {
            scr_debug_print(string(dbutton_options[dbutton_selected - 1]) + "" sélectionné !"");
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

var xcenter, menu_width, ycenter, menu_length;

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
var x_spacing, y_start, y_spacing;

if (dbutton_layout == 0)
{
    var x_padding = 7;
    y_start = 60 * d;
    x_spacing = 10 * d;
    y_spacing = 10 * d;
    x_start = ((xcenter - (menu_width / 2)) + x_padding) * d;
}

if (dbutton_layout == 1)
{
    var x_padding = 7;
    y_start = 95 * d;
    x_spacing = 10 * d;
    y_spacing = 20 * d;
    x_start = ((xcenter - (menu_width / 2)) + x_padding) * d;
}

if (dbutton_layout == 2)
{
    var x_padding = 7;
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
        draw_text(draw_x, draw_y, ""M - Touches"");
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
        draw_text(draw_x, draw_y, ""Esc - Annuler"");
        draw_set_halign(fa_left);
    }
    
    if (global.dreading_custom_flag)
    {
        if (dmenu_state == ""flag_categories"")
        {
            var base_x = x_start + xx;
            var base_y = (110 * d) + yy;
            var mono_spacing = (global.darkzone == 1) ? 15 : 8;
            var thickness = 1 * d;
            var visual_offset = -5;
            var cursor_padding = 3 * d;
            var w_prefix = string_length(""global.flag["") * mono_spacing;
            var w_name = string_length(dcustom_flag_text[0]) * mono_spacing;
            var w_middle = string_length(""] = |"") * mono_spacing;
            var w_value = string_length(dcustom_flag_text[1]) * mono_spacing;
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
            var base_y = (132 * d) + yy;
            var mono_spacing = (global.darkzone == 1) ? 15 : 8;
            var thickness = 1 * d;
            var visual_offset = -2;
            var cursor_padding = 3 * d;
            var w_prefix = string_length(""Contient : "") * mono_spacing;
            var w_name = string_length(dcustom_flag_text[0]) * mono_spacing;
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
            var base_y = (150 * d) + yy;
            var mono_spacing = (global.darkzone == 1) ? 15 : 8;
            var thickness = 1 * d;
            var visual_offset = -2;
            var cursor_padding = 3 * d;
            var w_prefix = string_length(""Valeur de plot : "") * mono_spacing;
            var w_name = string_length(dcustom_flag_text[0]) * mono_spacing;
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
            var text_width = string_width(dbutton_options[i]);
            draw_set_color((dbutton_selected == (i + 1)) ? c_yellow : c_white);
            draw_text(x_start + xx, (100 * d) + yy, dbutton_options[i]);
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
                draw_monospace(x_start + xx, y_start + yy + (i * y_spacing), dbutton_options[button_index]);
                var mono_spacing = (global.darkzone == 1) ? 15 : 8;
                
                if ((is_cur_line && dmenu_state == ""flag_misc"") || (dmenu_state == ""warp_options"" && (button_index == 3 || button_index == 4)))
                {
                    if ((dmenu_state == ""flag_misc"" && dhorizontal_index != 0) || (dmenu_state == ""warp_options"" && array_get([drooms_options.target_member_2, drooms_options.target_member_3], button_index - 3) != 0))
                    {
                        for (dash_pos = 0; 1; dash_pos++)
                        {
                            if (dash_pos > 4 && string_char_at(dbutton_options[button_index], dash_pos) == ((dmenu_state == ""flag_misc"") ? ""-"" : "":""))
                                break;
                        }
                        
                        dash_pos++;
                        draw_sprite_ext(spr_morearrow, 0, x_start + xx + ((dash_pos * mono_spacing) + floor(mono_spacing / 2)) + dmenu_arrow_yoffset, y_start + yy + (i * y_spacing) + side_arrows_mult[0], darrow_scale, -darrow_scale, 90, c_white, 1);
                    }
                    
                    if ((dmenu_state == ""flag_misc"" && dhorizontal_index < (array_length(dother_options[dbutton_selected - 1][3]) - 1)) || (dmenu_state == ""warp_options"" && array_get([drooms_options.target_member_2, drooms_options.target_member_3], button_index - 3) != (4 - (global.chapter == 1))))
                        draw_sprite_ext(spr_morearrow, 0, (x_start + xx + ((string_length(dbutton_options[button_index]) + 1) * mono_spacing)) - floor(mono_spacing / 2) - dmenu_arrow_yoffset, y_start + yy + (i * y_spacing) + side_arrows_mult[1], darrow_scale, -darrow_scale, 270, c_white, 1);
                }
                else if (dmenu_state == ""recruits"" && button_index == 0)
                {
                    if (dhorizontal_page != 0)
                        draw_sprite_ext(spr_morearrow, 0, x_start + xx + floor(mono_spacing / 2) + dmenu_arrow_yoffset, y_start + yy + (i * y_spacing) + side_arrows_mult[0], darrow_scale, -darrow_scale, 90, c_white, 1);
                    
                    if (dhorizontal_page != global.chapter)
                        draw_sprite_ext(spr_morearrow, 0, (x_start + xx + ((string_length(dbutton_options[button_index]) + 1) * mono_spacing)) - floor(mono_spacing / 2) - dmenu_arrow_yoffset, y_start + yy + (i * y_spacing) + side_arrows_mult[1], darrow_scale, -darrow_scale, 270, c_white, 1);
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
                draw_text(draw_x, draw_y, ""(chap "" + string(dhorizontal_page) + "")"");
            else
                draw_text(draw_x, draw_y, ""(tout chap)"");
        }
        else if (dhorizontal_page == 0)
        {
            draw_text(draw_x + 30 + (global.darkzone * 30), draw_y, ""(Darkworld)"");
            draw_sprite_ext(spr_morearrow, 0, draw_x + 35 + (global.darkzone * 35) + dmenu_arrow_yoffset, draw_y + side_arrows_mult[1], darrow_scale, -darrow_scale, 270, c_white, 1);
        }
        else
        {
            draw_text(draw_x + 30 + (global.darkzone * 30), draw_y, ""(Lightworld)"");
            draw_sprite_ext(spr_morearrow, 0, draw_x + -55 + (global.darkzone * -55) + dmenu_arrow_yoffset, draw_y + side_arrows_mult[0], darrow_scale, -darrow_scale, 90, c_white, 1);
        }
        
        draw_set_halign(fa_left);
    }
    
    if (dbutton_layout == 2)
    {
        dmenu_arrow_yoffset = 2 * sin(dmenu_arrow_timer / 10);
        draw_set_color(c_yellow);
        draw_text(((xcenter - (string_length(dgiver_amount) * 4)) * d) + xx, (ycenter * d) + yy, string(dgiver_amount));
        draw_set_color(c_white);
        var itemreminder;
        
        if (dgiver_menu_state == ""objects"")
        {
            itemreminder = ""["" + string(dgiver_bname) + ""]"";
            
            if (dhorizontal_page == 0)
                scr_itemcheck(0);
            else
                scr_litemcheck(0);
            
            max_items = (dhorizontal_page == 0) ? 12 : 8;
            draw_text(x_start + xx, ((ycenter + 25) * d) + yy, ""OBJETs : "" + string(max_items - itemcount) + "" / "" + string(max_items));
        }
        
        if (dgiver_menu_state == ""armors"")
        {
            itemreminder = ""["" + string(dgiver_bname) + ""]"";
            scr_armorcheck_inventory(0);
            draw_text(x_start + xx, ((ycenter + 25) * d) + yy, ""ARMUREs : "" + string(48 - itemcount) + "" / 48"");
        }
        
        if (dgiver_menu_state == ""weapons"")
        {
            itemreminder = ""["" + string(dgiver_bname) + ""]"";
            scr_weaponcheck_inventory(0);
            draw_text(x_start + xx, ((ycenter + 25) * d) + yy, ""ARMEs : "" + string(48 - itemcount) + "" / 48"");
        }
        
        if (dgiver_menu_state == ""keyitems"")
        {
            itemreminder = ""["" + string(dgiver_bname) + ""]"";
            scr_keyitemcheck(0);
            draw_text(x_start + xx, ((ycenter + 25) * d) + yy, ""OBJETs CLÉs : "" + string(12 - itemcount) + "" / 12"");
        }
        
        var text_width = string_width(itemreminder);
        draw_text(((xcenter * d) - (text_width / 2)) + xx, ((ycenter - 22) * d) + yy, itemreminder);
        darrow_scale = d / 2;
        draw_sprite_ext(spr_morearrow, 0, ((xcenter - 15) * d) + xx + dmenu_arrow_yoffset, ((ycenter + 6) * d) + yy, darrow_scale, darrow_scale, 270, c_white, 1);
        draw_sprite_ext(spr_morearrow, 0, (((xcenter + 15) * d) + xx) - dmenu_arrow_yoffset, ((ycenter + 12) * d) + yy, darrow_scale, darrow_scale, 90, c_white, 1);
    }
}

if (dkeys_helper == 1)
{
    dkeys_data = [""F10 - Activer/désactiver le debug mode"", ""S - Sauvegarder la partie"", ""L - Charger la dernière sauvegarde"", ""R - Charger la salle | Retour arrière+R - Redémarrer le jeu"", ""P - Mettre en pause/reprendre le jeu"", ""M+1 | M+2 - Ajouter/retirer 100 D$"", ""Suppr - Se rendre à la salle précédente"", ""Insert - Se rendre à la salle suivante"", ""W - Gagner instantanément un combat"", ""V - Passer le tour de l'ennemi"", ""H - Restaurer les HP du party"", ""T - Remplir/vider la barre de TP"", ""O - Basculer entre 30, 60 et 120 FPS"", ""Retour arrière - Passer le segment d'intro (Ch1)"", ""Clic milieu - Éditeur de salle""];
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
    draw_set_halign(fa_right);
    draw_set_color(c_gray);
    var right_border = (xcenter + (menu_width / 2)) * d;
    var padding = 8 * d;
    var draw_x = (right_border + xx) - padding;
    var draw_y = (((ycenter - (menu_length / 2)) + 8) * d) + yy;
    draw_text(draw_x, draw_y, ""M - Fermer"");
    draw_set_halign(fa_left);
    draw_set_color(c_white);
    draw_text(x_start + xx, (((ycenter - (menu_length / 2)) + 8) * d) + yy, ""Touches du debug mode"");
    
    for (var i = 0; i < array_length(dkeys_data); i++)
    {
        draw_set_font(fnt_main);
        draw_set_color(c_white);
        draw_text(x_start + xx, y_start + yy + (i * y_spacing), dkeys_data[i]);
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
function dmenu_state_update()
{
    switch (dmenu_state)
    {
        case ""debug"":
            dmenu_title = ""Menu Debug"";
            dbutton_options = dbutton_options_original;
            dmenu_box = 0;
            dbutton_layout = 0;
            break;
        
        case ""warp"":
            dmenu_title = ""Liste des salles"";
            dbutton_options = [""Salle actuelle"", ""Recherche""];
            dbutton_indices = [-1, -1];
            
            if (global.dreading_custom_flag || dkeyboard_input != """")
                dbutton_options[1] = ""Contient : "";
            else
                dbutton_options[1] = ""Recherche"";
            
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
            dmenu_title = ""Options du saut"";
            dbutton_options = [""Annuler"", ""Est un Darkworld : "", ""Valeur de plot : "", ""Équipier 2 :  "", ""Équipier 3 :  "", ""Sauter""];
            dbutton_indices = [0, 1, 2, 3, 4, 5];
            dbutton_options[1] += drooms_options.target_is_darkzone ? ""Oui"" : ""Non"";
            
            if (global.dreading_custom_flag)
                dbutton_options[2] += dkeyboard_input;
            else
                dbutton_options[2] += string(drooms_options.target_plot);
            
            teammates = [""Personne"", ""Kris"", ""Susie"", ""Ralsei"", ""Noëlle""];
            dbutton_options[3] += teammates[drooms_options.target_member_2];
            dbutton_options[4] += teammates[drooms_options.target_member_3];
            break;
        
        case ""give"":
            dmenu_title = ""Type d'items"";
            dbutton_options = [""Objets"", ""Armures"", ""Armes"", ""Obj Clés""];
            dmenu_box = 0;
            dbutton_layout = 0;
            break;
        
        case ""objects"":
            dmenu_title = ""Liste d'objets"";
            dbutton_options = [""Chapitre : ""];
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
                    var combined = itemnameb + "" - "" + cleaned_desc;
                    
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
                    var combined = dlight_objects[i][1] + "" - "" + string(itemcount) + "" held"";
                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, dlight_objects[i][0]);
                }
            }
            
            dmenu_box = 2;
            dbutton_layout = 1;
            break;
        
        case ""armors"":
            dmenu_title = ""Liste d'armures"";
            dbutton_options = [""Chapitre : ""];
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
                    var combined = armornametemp + "" - "" + cleaned_desc;
                    
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
                        combined += "" (Equiped)"";
                    
                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, i);
                }
            }
            
            dmenu_box = 2;
            dbutton_layout = 1;
            break;
        
        case ""weapons"":
            dmenu_title = ""Liste d'armes"";
            dbutton_options = [""Chapitre : ""];
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
                    var combined = weaponnametemp + "" - "" + cleaned_desc;
                    
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
                        combined += "" (Equiped)"";
                    
                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, i);
                }
            }
            
            dmenu_box = 2;
            dbutton_layout = 1;
            break;
        
        case ""keyitems"":
            dmenu_title = ""Liste d'objets clés"";
            dbutton_options = [""Chapitre : ""];
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
                var combined = tempkeyitemname + "" - "" + cleaned_desc;
                
                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + ""..."";
                
                array_push(dbutton_options, combined);
                array_push(dbutton_indices, i);
            }
            
            dmenu_box = 2;
            dbutton_layout = 1;
            break;
        
        case ""givertab"":
            dmenu_title = ""Ajouter combien à l'inventaire ?"";
            dgiver_amount = 1;
            dmenu_box = 0;
            dbutton_layout = 2;
            break;
        
        case ""recruits"":
            dmenu_title = ""Liste des recrues"";
            dbutton_options = [""Préréglages""];
            dbutton_indices = [""Préréglages""];
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
            dmenu_title = ""Préréglages des recrues"";
            dbutton_options = [""Recruter tous"", ""Perdre tous""];
            
            if (dhorizontal_page)
            {
                dmenu_title += ("" (chap "" + string(dhorizontal_page) + "")"");
                dbutton_options[0] += "" du chapitre "" + string(dhorizontal_page);
                dbutton_options[1] += "" du chapitre "" + string(dhorizontal_page);
            }
            
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case ""flag_categories"":
            dmenu_title = ""Divers"";
            dbutton_options = [];
            dbutton_indices = [-1];
            categories_len = array_length(dother_categories);
            var max_len = 40;
            
            if (!global.dreading_custom_flag)
                array_push(dbutton_options, ""Custom"");
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
            dmenu_title = ""Divers"";
            dbutton_options = [];
            dbutton_indices = [];
            other_len = array_length(dother_options);
            var max_len = 40;
            
            for (var i = 0; i < other_len; i++)
            {
                cur_option = dother_options[i];
                flag_number = global.flag[cur_option[2]];
                var combined = cur_option[1] + "" - problem lol"";
                
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
            dmenu_title = ""Inconnu"";
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
            if (dgiver_menu_state == ""objects"")
            {
                if (dgiver_amount != 0)
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
                        scr_debug_print(string(abs(dgiver_amount)) + "" "" + dgiver_bname + "" retiré de l'inventaire"");
                    else
                        scr_debug_print(string(dgiver_amount) + "" "" + dgiver_bname + "" ajouté à l'inventaire"");
                }
                else
                {
                    scr_debug_print(""Annulé"");
                }
            }
            
            if (dgiver_menu_state == ""armors"")
            {
                if (dgiver_amount > 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];
                    
                    for (var i = 0; i < dgiver_amount; i++)
                        scr_armorget(real_index);
                    
                    scr_debug_print(string(dgiver_amount) + "" "" + dgiver_bname + "" ajouté à l'inventaire"");
                }
                else if (dgiver_amount < 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];
                    
                    for (var i = 0; i < abs(dgiver_amount); i++)
                        scr_armorremove(real_index);
                    
                    scr_debug_print(string(abs(dgiver_amount)) + "" "" + dgiver_bname + "" retiré de l'inventaire"");
                }
                else
                {
                    scr_debug_print(""Annulé"");
                }
            }
            
            if (dgiver_menu_state == ""weapons"")
            {
                if (dgiver_amount > 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];
                    
                    for (var i = 0; i < dgiver_amount; i++)
                        scr_weaponget(real_index);
                    
                    scr_debug_print(string(dgiver_amount) + "" "" + dgiver_bname + "" ajouté à l'inventaire"");
                }
                else if (dgiver_amount < 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];
                    
                    for (var i = 0; i < abs(dgiver_amount); i++)
                        scr_weaponremove(real_index);
                    
                    scr_debug_print(string(abs(dgiver_amount)) + "" "" + dgiver_bname + "" retiré de l'inventaire"");
                }
                else
                {
                    scr_debug_print(""Annulé"");
                }
            }
            
            if (dgiver_menu_state == ""keyitems"")
            {
                if (dgiver_amount > 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];
                    
                    for (var i = 0; i < dgiver_amount; i++)
                        scr_keyitemget(real_index);
                    
                    scr_debug_print(string(dgiver_amount) + "" "" + dgiver_bname + "" ajouté à l'inventaire"");
                }
                else if (dgiver_amount < 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];
                    
                    for (var i = 0; i < abs(dgiver_amount); i++)
                        scr_keyitemremove(real_index);
                    
                    scr_debug_print(string(abs(dgiver_amount)) + "" "" + dgiver_bname + "" retiré de l'inventaire"");
                }
                else
                {
                    scr_debug_print(""Annulé"");
                }
            }
            
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
                        global.flag[enemy_id + 600] = -1 / _recruitcount;
                }
                
                if (dhorizontal_page != 0)
                    break;
            }
            
            if (dbutton_selected == 1)
                snd_play(snd_pirouette);
            else
                snd_play(snd_weirdeffect);
            
            break;
        
        default:
            snd_play(snd_error);
            scr_debug_print(""Sélection invalide !"");
    }
}
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
    if (mouse_check_button_pressed(mb_middle))
        instance_create(0, 0, obj_debug_xy);
    
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

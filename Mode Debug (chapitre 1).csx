// Mode Debug Custom par Jazzky et Straky

EnsureDataLoaded();

if (!Data.IsVersionAtLeast(2023, 6))
{
    ScriptError("Erreur 0 : Ce script fonctionne uniquement pour la version payante de Deltarune.");
    return;
}

if (Data?.GeneralInfo?.DisplayName?.Content.ToLower() != "deltarune chapter 1" &&
    Data?.GeneralInfo?.DisplayName?.Content.ToLower() != "deltarune chapitre 1")
{
    ScriptError("Erreur 1 : Ce script s'applique seulement au Chapitre 1.");
    return;
}


bool enable = ScriptQuestion(
"Ajouter le Mode Debug pour le Chapitre 1 ?"
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
dscroll_up_timer = 0;
dscroll_down_timer = 0;
dscroll_delay = 15;
dscroll_speed = 5;
dmenu_title = ""Menu Debug"";
dbutton_options_original = [""Sauts"", ""Items"", ""Recrues"", ""Autre""];

if (global.chapter == 1)
    dbutton_options_original = [""Sauts"", ""Items"", ""Autre""];

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
ditemcount = 0;
darmorcount = 0;
dweaponcount = 0;
dkeyitemcount = 0;
dbutton_indices = [];
drecent_item = 0;
drecent_armor = 0;
drecent_weapon = 0;
drecent_keyitem = 0;
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
dother_categories = [""Séquence Vaisseau"", ""Superbosses"", ""Weird Route"", ""Œufs"", ""Onion san"", ""Misc chap 1"", ""Misc chap 2"", ""Legend of Tenna"", ""Sword Route"", ""Misc chap 3"", ""Misc chap 4"", ""Mousse"", ""Roboteur""];
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
    array_push(dother_all_options, [MISC1, ""Prophetie entendu"", 203, [[""Non"", 1], [""Oui"", 0]]]);
    array_push(dother_all_options, [MISC1, ""Manuel jete"", 207, [[""Non"", 0], [""A tente"", 1], [""L'a jete"", 2]]]);
    array_push(dother_all_options, [MISC1, ""Gâteau rendu"", 253, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [MISC1, ""Starwalker"", 254, [[""Pissing me off"", 0], [""I will   join"", 1]]]);
    array_push(dother_all_options, [EGG, ""Œuf obtenu (chap 1)"", 911, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [SUPERBOSS, ""Jevil vaincu"", 241, [[""Non"", 0], [""Via violence"", 6], [""Via clémence"", 7]]]);
    array_push(dother_all_options, [ONION_SAN, ""Relation (chap 1)"", 258, [[""Pas vu"", 0], [""Amis"", 2], [""Pas amis"", 3]]]);
    array_push(dother_all_options, [ONION_SAN, ""Nom de Kris"", 259, [[""Pas donne"", 0], [""Kris"", 1], [""Hippopotame"", 2]]]);
    array_push(dother_all_options, [ONION_SAN, ""Nom d'Onion"", 260, [[""Pas donne"", 0], [""Onion"", 1], [""Beaute"", 2], [""Asriel II"", 3], [""Degoutant"", 4]]]);
    array_push(dother_all_options, [MOUSSE, ""Mousse mangee (chap 1)"", 106, [[""Non"", 0], [""Oui"", 1]]]);
}

if (global.chapter >= 2)
{
    array_push(dother_all_options, [MISC2, ""Peluche"", 307, [[""Pas donnée"", 0], [""Ralsei"", 1], [""Susie"", 2], [""Noëlle"", 3], [""Berdly"", 4]]]);
    array_push(dother_all_options, [MISC2, ""Hacker recruté"", 659, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [MISC2, ""Bras de Berdly"", 457, [[""Brulé"", 0], [""Ok"", 1]]]);
    array_push(dother_all_options, [WEIRD2, ""Avancée"", 915, [[""Pas fait"", 0], [""Nikomercant tué"", 3], [""Berdly gelé"", 6], [""Noëlle vue a l'hôpital"", 20]]]);
    array_push(dother_all_options, [WEIRD2, ""A cancel la weird route"", 916, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [EGG, ""Œuf obtenu (chap 2)"", 918, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [SUPERBOSS, ""Spamton vaincu"", 309, [[""Non"", 0], [""Oui"", 9]]]);
    array_push(dother_all_options, [MISC2, ""\""Fan\"" de mettaton"", 422, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [MISC2, ""Statue de Susie récupérée"", 393, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [MISC2, ""ICE-E récupéré"", 394, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [ONION_SAN, ""Relation (chap 2)"", 425, [[""Pas vu"", 0], [""Amis"", 1], [""Plus amis"", 2]]]);
    array_push(dother_all_options, [MOUSSE, ""Mousse mangee (chap 2)"", 920, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [MOUSSE, ""... avec Noelle (chap 2)"", 921, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [MOUSSE, ""... avec Susie (chap 2)"", 922, [[""Non"", 0], [""Oui"", 1]]]);
}

if (global.chapter >= 3)
{
    array_push(dother_all_options, [LOT, ""LOT Rang Board 1"", 1173, [[""Z"", 0], [""C"", 1], [""B"", 2], [""A"", 3], [""S"", 4], [""T"", 5]]]);
    array_push(dother_all_options, [LOT, ""LOT Rang Board 2"", 1174, [[""Z"", 0], [""C"", 1], [""B"", 2], [""A"", 3], [""S"", 4], [""T"", 5]]]);
    array_push(dother_all_options, [SWORD, ""Avancé"", 1055, [[""Pas fait"", 0], [""Clé de glace obtenue"", 1], [""Donjon (plateau 2)"", 1.5], [""Elle a été utilisée"", 2], [""Clé de l'abri obtenue"", 3], [""Donjon (plateau 3)"", 4], [""Clé de l'abri utilisée"", 5], [""ERAM vaincu"", 6]]]);
    array_push(dother_all_options, [SWORD, ""Susie attaquée"", 1268, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [EGG, ""Œuf obtenu (chap 3)"", 930, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [SUPERBOSS, ""Chevalier Vaincu"", 1047, [[""Non"", 2], [""Oui"", 1]]]);
    array_push(dother_all_options, [MISC3, ""Statue de Tenna récupérée"", 1222, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [MOUSSE, ""Mousse mangee (chap 3)"", 1078, [[""Non"", 0], [""Oui"", 1]]]);
}

if (global.chapter >= 4)
{
    array_push(dother_all_options, [EGG, ""Œuf obtenu (chap 4)"", 931, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [SUPERBOSS, ""Gerson Vaincu"", 1629, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [MISC4, ""Échelle récupérée"", 864, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [MISC4, ""Oreiller récupéré"", 865, [[""Non"", 0], [""Oui"", 1]]]);
    array_push(dother_all_options, [MOUSSE, ""Mousse mangee (chap 4)"", 1592, [[""Non"", 0], [""Oui"", 1], [""Refuser"", 2]]]);
    array_push(dother_all_options, [MISC4, ""Tenna donné"", 779, [[""Non"", 0], [""Oui"", 2]]]);
}

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

dkeys_helper = 0;
dkeys_data = [];


");

importGroup.QueueReplace(obj_dmenu_system.EventHandlerFor(EventType.Step, (uint)0, Data), @"
dmenu_arrow_timer += 1;

if (dmenu_active && global.dreading_custom_flag)
{
    update_visu = 1;
    
    if (keyboard_check_pressed(vk_escape) || keyboard_check_pressed(vk_enter) || keyboard_check_pressed(global.input_k[4]) || keyboard_check_pressed(global.input_k[7]))
    {
        proper_exit = !keyboard_check_pressed(vk_escape);
        
        if (proper_exit)
        {
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
                scr_debug_print(""Empty value"");
                proper_exit = 0;
            }
            
            if (proper_exit)
            {
                global.flag[real(dcustom_flag_text[0])] = real(dcustom_flag_text[1]);
                scr_debug_print(""Updated global.flag["" + string(real(dcustom_flag_text[0])) + ""] to |"" + dcustom_flag_text[1] + ""|"");
            }
        }
        
        global.dreading_custom_flag = 0;
        dcustom_flag_text = ["""", """"];
        
        if (proper_exit)
        {
            dmenu_active = 0;
            global.interact = 0;
        }
    }
    else if (keyboard_check_pressed(vk_left) && dhorizontal_index != 0)
    {
        snd_play(snd_menumove);
        dhorizontal_index--;
    }
    else if (keyboard_check_pressed(vk_right) && dhorizontal_index != 1)
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
        dmenu_state_update();
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
        
        if (keyboard_check_pressed(vk_down))
        {
            snd_play(snd_menumove);
            
            if (dbutton_selected < array_length(dbutton_options))
            {
                dbutton_selected += 1;
                
                if (dbutton_selected > (dmenu_start_index + dbutton_max_visible))
                    dmenu_start_index += 1;
            }
            
            if (dmenu_state == ""flag_misc"")
            {
                new_options = dother_options[dbutton_selected - 1];
                dhorizontal_index = find_subarray_index(new_options[2], new_options[3]);
            }
        }
        
        if (keyboard_check_pressed(vk_up))
        {
            snd_play(snd_menumove);
            
            if (dbutton_selected > 1)
            {
                dbutton_selected -= 1;
                
                if (dbutton_selected < (dmenu_start_index + 1))
                    dmenu_start_index -= 1;
            }
            
            if (dmenu_state == ""flag_misc"")
            {
                new_options = dother_options[dbutton_selected - 1];
                dhorizontal_index = find_subarray_index(new_options[2], new_options[3]);
            }
        }
        
        if (keyboard_check_pressed(vk_right))
        {
            if (dmenu_state == ""recruits"")
            {
                if (dbutton_selected != 1)
                {
                    real_index = dbutton_indices[dbutton_selected - 1];
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
                else if (dhorizontal_page != global.chapter)
                {
                    dhorizontal_page++;
                    snd_play(snd_menumove);
                    dmenu_state_update();
                }
            }
        }
        
        if (keyboard_check_pressed(vk_left))
        {
            if (dmenu_state == ""recruits"")
            {
                if (dbutton_selected != 1)
                {
                    real_index = dbutton_indices[dbutton_selected - 1];
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
                else if (dhorizontal_page != 0)
                {
                    dhorizontal_page--;
                    snd_play(snd_menumove);
                    dmenu_state_update();
                }
            }
        }
        
        if (keyboard_check(vk_down))
        {
            if (dscroll_down_timer >= dscroll_delay)
            {
                if ((dscroll_down_timer % dscroll_speed) == 0)
                {
                    if (dbutton_selected < array_length(dbutton_options))
                    {
                        dbutton_selected += 1;
                        snd_play(snd_menumove);
                        
                        if (dbutton_selected > (dmenu_start_index + dbutton_max_visible))
                            dmenu_start_index += 1;
                        
                        if (dmenu_state == ""flag_misc"")
                        {
                            new_options = dother_options[dbutton_selected - 1];
                            dhorizontal_index = find_subarray_index(new_options[2], new_options[3]);
                        }
                    }
                }
            }
            
            dscroll_down_timer += 1;
        }
        else
        {
            dscroll_down_timer = 0;
        }
        
        if (keyboard_check(vk_up))
        {
            if (dscroll_up_timer >= dscroll_delay)
            {
                if ((dscroll_up_timer % dscroll_speed) == 0)
                {
                    if (dbutton_selected > 1)
                    {
                        dbutton_selected -= 1;
                        snd_play(snd_menumove);
                        
                        if (dbutton_selected < (dmenu_start_index + 1))
                            dmenu_start_index -= 1;
                        
                        if (dmenu_state == ""flag_misc"")
                        {
                            new_options = dother_options[dbutton_selected - 1];
                            dhorizontal_index = find_subarray_index(new_options[2], new_options[3]);
                        }
                    }
                }
            }
            
            dscroll_up_timer += 1;
        }
        else
        {
            dscroll_up_timer = 0;
        }
        
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
                    scr_itemcheck(dbutton_indices[dgiver_button_selected - 1]);
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
        snd_play(snd_select);
        
        if (dmenu_state != ""givertab"" && dmenu_state != ""recruit_presets"" && (dmenu_state != ""recruits"" || dbutton_selected == 1) && dmenu_state != ""flag_misc"")
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
        
        if (dmenu_state == ""objects"" || dmenu_state == ""armors"" || dmenu_state == ""weapons"" || dmenu_state == ""keyitems"")
        {
            switch (dmenu_state)
            {
                case ""objects"":
                    real_index = dbutton_indices[dbutton_selected - 1];
                    scr_iteminfo(real_index);
                    dgiver_bname = itemnameb;
                    scr_debug_print(dgiver_bname + "" sélectionné !"");
                    break;
                
                case ""armors"":
                    real_index = dbutton_indices[dbutton_selected - 1];
                    scr_armorinfo(real_index);
                    dgiver_bname = armornametemp;
                    scr_debug_print(string(dgiver_bname) + "" sélectionné !"");
                    break;
                
                case ""weapons"":
                    real_index = dbutton_indices[dbutton_selected - 1];
                    scr_weaponinfo(real_index);
                    dgiver_bname = weaponnametemp;
                    scr_debug_print(string(dgiver_bname) + "" sélectionné !"");
                    break;
                
                case ""keyitems"":
                    real_index = dbutton_indices[dbutton_selected - 1];
                    scr_keyiteminfo(real_index);
                    dgiver_bname = tempkeyitemname;
                    scr_debug_print(string(dgiver_bname) + "" sélectionné !"");
                    break;
            }
        }
        else if (dmenu_state != ""givertab"" && (dmenu_state != ""recruits"" || dbutton_selected == 1) && dmenu_state != ""flag_misc"")
        {
            scr_debug_print(string(dbutton_options[dbutton_selected - 1]) + "" sélectionné !"");
        }
        
        if ((dmenu_state == ""recruits"" && dbutton_selected != 1) || dmenu_state == ""recruit_presets"")
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
        dkeys_helper = !dkeys_helper;
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
    
    if (dmenu_state == ""recruits"")
    {
        draw_set_halign(fa_right);
        
        if (dhorizontal_page != 0)
            draw_text(x_start + xx + 400, (((ycenter - (menu_length / 2)) + 8) * d) + yy, ""(chap "" + string(dhorizontal_page) + "")"");
        else
            draw_text(x_start + xx + 400, (((ycenter - (menu_length / 2)) + 8) * d) + yy, ""(tout chap)"");
        
        draw_set_halign(fa_left);
    }
    
    if (dmenu_state == ""debug"" && global.darkzone == 1)
    {
        draw_set_font(fnt_main);
        draw_set_color(c_white);
        draw_text(((x_start + 335) * (d / 2)) + xx, (((ycenter - (menu_length / 2)) + 82) * d) + yy, ""Touches - M"");
        draw_set_font(fnt_mainbig);
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
    
    if (dbutton_layout == 1)
    {
        var dcan_scroll_up = dmenu_start_index > 0;
        var dcan_scroll_down = (dmenu_start_index + dbutton_max_visible) < array_length(dbutton_options);
        var dmenu_arrow_yoffset = 2 * sin(dmenu_arrow_timer / 10);
        var darrow_scale = d / 2;
        
        for (var i = 0; i < dbutton_max_visible; i++)
        {
            var button_index = dmenu_start_index + i;
            
            if (button_index < array_length(dbutton_options))
            {
                is_cur_line = dbutton_selected == (button_index + 1);
                var text_color = is_cur_line ? c_yellow : c_white;
                draw_set_color(text_color);
                draw_monospace(x_start + xx, y_start + yy + (i * y_spacing), dbutton_options[button_index]);
                mono_spacing = (global.darkzone == 1) ? 15 : 8;
                side_arrows_mult = (global.darkzone == 1) ? [23, 10] : [12, 5];
                
                if (is_cur_line && dmenu_state == ""flag_misc"")
                {
                    if (dhorizontal_index != 0)
                    {
                        for (dash_pos = 0; 1; dash_pos++)
                        {
                            if (dash_pos > 4 && string_char_at(dbutton_options[button_index], dash_pos) == ""-"")
                                break;
                        }
                        
                        dash_pos++;
                        draw_sprite_ext(spr_morearrow, 0, x_start + xx + ((dash_pos * mono_spacing) + floor(mono_spacing / 2)) + dmenu_arrow_yoffset, y_start + yy + (i * y_spacing) + side_arrows_mult[0], darrow_scale, -darrow_scale, 90, c_white, 1);
                    }
                    
                    if (dhorizontal_index < (array_length(dother_options[dbutton_selected - 1][3]) - 1))
                        draw_sprite_ext(spr_morearrow, 0, ((x_start + xx + ((string_length(dbutton_options[button_index]) + 1) * mono_spacing)) - floor(mono_spacing / 2)) + dmenu_arrow_yoffset, y_start + yy + (i * y_spacing) + side_arrows_mult[1], darrow_scale, -darrow_scale, 270, c_white, 1);
                }
                else if (dmenu_state == ""recruits"" && button_index == 0)
                {
                    if (dhorizontal_page != 0)
                        draw_sprite_ext(spr_morearrow, 0, x_start + xx + floor(mono_spacing / 2) + dmenu_arrow_yoffset, y_start + yy + (i * y_spacing) + side_arrows_mult[0], darrow_scale, -darrow_scale, 90, c_white, 1);
                    
                    if (dhorizontal_page != global.chapter)
                        draw_sprite_ext(spr_morearrow, 0, ((x_start + xx + ((string_length(dbutton_options[button_index]) + 1) * mono_spacing)) - floor(mono_spacing / 2)) + dmenu_arrow_yoffset, y_start + yy + (i * y_spacing) + side_arrows_mult[1], darrow_scale, -darrow_scale, 270, c_white, 1);
                }
            }
        }
        
        if (dcan_scroll_up)
            draw_sprite_ext(spr_morearrow, 0, x_start + xx, y_start + yy + (dbutton_max_visible * (y_spacing * -0.03)) + dmenu_arrow_yoffset, darrow_scale, -darrow_scale, 0, c_white, 1);
        
        if (dcan_scroll_down)
            draw_sprite_ext(spr_morearrow, 0, x_start + xx, (y_start + yy + (dbutton_max_visible * y_spacing)) - dmenu_arrow_yoffset, darrow_scale, darrow_scale, 0, c_white, 1);
    }
    
    if (dbutton_layout == 2)
    {
        draw_set_color(c_yellow);
        draw_text(((xcenter - (string_length(dgiver_amount) * 4)) * d) + xx, (ycenter * d) + yy, string(dgiver_amount));
        draw_set_color(c_white);
        var itemreminder;
        
        if (dgiver_menu_state == ""objects"")
        {
            itemreminder = ""["" + string(dgiver_bname) + ""]"";
            scr_itemcheck(0);
            draw_text(x_start + xx, ((ycenter + 25) * d) + yy, ""OBJETs : "" + string(12 - itemcount) + ""/12"");
        }
        
        if (dgiver_menu_state == ""armors"")
        {
            itemreminder = ""["" + string(dgiver_bname) + ""]"";
            scr_armorcheck_inventory(0);
            draw_text(x_start + xx, ((ycenter + 25) * d) + yy, ""ARMUREs : "" + string(48 - itemcount) + ""/48"");
        }
        
        if (dgiver_menu_state == ""weapons"")
        {
            itemreminder = ""["" + string(dgiver_bname) + ""]"";
            scr_weaponcheck_inventory(0);
            draw_text(x_start + xx, ((ycenter + 25) * d) + yy, ""ARMEs : "" + string(48 - itemcount) + ""/48"");
        }
        
        if (dgiver_menu_state == ""keyitems"")
        {
            itemreminder = ""["" + string(dgiver_bname) + ""]"";
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

if (dkeys_helper == 1)
{
    dkeys_data = [""F10 - Activer/désactiver le debug mode"", ""D - Ouvrir le menu Debug"", ""S - Sauvegarder la partie"", ""L - Charger la dernière sauvegarde"", ""R - Redémarrer le jeu"", ""P - Mettre en pause/reprendre le jeu"", ""M+1/M+2 - Ajouter/retirer 100 D$"", ""Suppr - Se rendre à la salle précédente"", ""Insert - Se rendre à la salle suivante"", ""Entrer - Voir les collisions du joueur"", ""W - Gagner instantanément un combat"", ""V - Passer le tour de l'ennemi"", ""H - Restaurer les HP du party"", ""T - Remplir/vider la barre de TP"", ""O - Basculer entre 30, 60 et 120 FPS"", ""Retour arrière - Passer le segment d'intro (Ch1)""];
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
if (ditemcount == 0)
{
    ditemcount = 63;
    darmorcount = 54;
    dweaponcount = 54;
    dkeyitemcount = 31;
    drecent_item = 60;
    drecent_armor = 50;
    drecent_weapon = 50;
    drecent_keyitem = 30;
}

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
            dmenu_title = ""Menu de sauts"";
            dbutton_options = [""Lightworld"", ""Darkworld"", ""Combats""];
            dmenu_box = 0;
            dbutton_layout = 0;
            break;
        
        case ""lightworld"":
            dmenu_title = ""Menu de sauts - Lightworld"";
            dbutton_options = [""Couloir de Kris"", ""Hall de l'école"", ""Placard de l'école"", ""Après Darkworld""];
            dmenu_box = 1;
            dbutton_layout = 1;
            break;
        
        case ""kris_hallway"":
            dmenu_title = ""Options de saut"";
            dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case ""school_hallway"":
            dmenu_title = ""Options de saut"";
            dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case ""school_closet"":
            dmenu_title = ""Options de saut"";
            dbutton_options = [""Téléporter""];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case ""after_darkworld"":
            dmenu_title = ""Options de saut"";
            dbutton_options = [""Saut : 1ère apparition""];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case ""darkworld"":
            dmenu_title = ""Menu de sauts - Darkworld"";
            dbutton_options = [""Entrée du Darkworld"", ""Entrée de la Citadelle"", ""Entrée des Plaines"", ""Bazar de Seam"", ""Entrée du Grand Plateau"", ""Entrée de la Forêt"", ""Vente de Pâtisseries"", ""Entrée Château"", ""Dernier étage Château"", ""Sortie Darkworld""];
            dmenu_box = 1;
            dbutton_layout = 1;
            break;
        
        case ""dw_entrance"":
            dmenu_title = ""Options de saut"";
            dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case ""castletown_entrance"":
            dmenu_title = ""Options de saut"";
            dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case ""field_entrance"":
            dmenu_title = ""Options de saut"";
            dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case ""seam_shop"":
            dmenu_title = ""Options de saut"";
            dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case ""greatboard_entrance"":
            dmenu_title = ""Options de saut"";
            dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case ""forest_entrance"":
            dmenu_title = ""Options de saut"";
            dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case ""bakesale"":
            dmenu_title = ""Options de saut"";
            dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case ""castle_entrance"":
            dmenu_title = ""Options de saut"";
            dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case ""castle_top_floor"":
            dmenu_title = ""Options de saut"";
            dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case ""darkworld_exit"":
            dmenu_title = ""Options de saut"";
            dbutton_options = [""Téléporter"", ""Saut : 1ère apparition""];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case ""battles"":
            dmenu_title = ""Menu de sauts - Combats"";
            dbutton_options = [""Combat contre Lancer"", ""Combat contre Kouronné"", ""Combat contre Tréflette"", ""Combat contre Susie"", ""2e Combat contre Kouronné"", ""Combat contre Jevil"", ""Combat contre  le Roi""];
            dmenu_box = 1;
            dbutton_layout = 1;
            break;
        
        case ""lancer_battle"":
            dmenu_title = ""Options de saut"";
            dbutton_options = [""Téléporter"", ""Saut : Combat""];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case ""kround_battle"":
            dmenu_title = ""Options de saut"";
            dbutton_options = [""Téléporter"", ""Saut : Combat""];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case ""clover_battle"":
            dmenu_title = ""Options de saut"";
            dbutton_options = [""Téléporter"", ""Saut : Combat""];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case ""susie_battle"":
            dmenu_title = ""Options de saut"";
            dbutton_options = [""Téléporter"", ""Saut : Combat""];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case ""kround2_battle"":
            dmenu_title = ""Options de saut"";
            dbutton_options = [""Téléporter"", ""Saut : Combat""];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case ""jevil_battle"":
            dmenu_title = ""Options de saut"";
            dbutton_options = [""Téléporter"", ""Saut : Combat avec Clé""];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case ""king_battle"":
            dmenu_title = ""Options de saut"";
            dbutton_options = [""Téléporter"", ""Saut : Combat""];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;

        case ""give"":
            dmenu_title = ""Type d'items"";
            dbutton_options = [""Objets"", ""Armures"", ""Armes"", ""Obj Clés""];
            dmenu_box = 0;
            dbutton_layout = 0;
            break;

        case ""objects"":
            dmenu_title = ""Liste d'objets"";
            dbutton_options = [];
            dbutton_indices = [];
            var max_len = 40;

            for (var i = drecent_item; i <= ditemcount; i++)
            {
                scr_iteminfo(i);
                var cleaned_desc = string_replace_all(itemdescb, ""#"", "" "");
                var combined = itemnameb + "" - "" + cleaned_desc;

                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + ""..."";

                array_push(dbutton_options, combined);
                array_push(dbutton_indices, i);
            }

            var cutoff = min(drecent_item - 1, ditemcount);

            for (var i = 1; i <= cutoff; i++)
            {
                scr_iteminfo(i);
                var cleaned_desc = string_replace_all(itemdescb, ""#"", "" "");
                var combined = itemnameb + "" - "" + cleaned_desc;

                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + ""..."";

                array_push(dbutton_options, combined);
                array_push(dbutton_indices, i);
            }

            dmenu_box = 2;
            dbutton_layout = 1;
            break;

        case ""armors"":
            dmenu_title = ""Liste d'armures"";
            dbutton_options = [];
            dbutton_indices = [];
            var max_len = 40;

            for (var i = drecent_armor; i <= darmorcount; i++)
            {
                scr_armorinfo(i);
                var cleaned_desc = string_replace_all(armordesctemp, ""#"", "" "");
                var combined = armornametemp + "" - "" + cleaned_desc;

                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + ""..."";

                array_push(dbutton_options, combined);
                array_push(dbutton_indices, i);
            }

            var cutoff = min(drecent_armor - 1, darmorcount);

            for (var i = 1; i <= cutoff; i++)
            {
                scr_armorinfo(i);
                var cleaned_desc = string_replace_all(armordesctemp, ""#"", "" "");
                var combined = armornametemp + "" - "" + cleaned_desc;

                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + ""..."";

                array_push(dbutton_options, combined);
                array_push(dbutton_indices, i);
            }

            dmenu_box = 2;
            dbutton_layout = 1;
            break;

        case ""weapons"":
            dmenu_title = ""Liste d'armes"";
            dbutton_options = [];
            dbutton_indices = [];
            var max_len = 40;

            for (var i = drecent_weapon; i <= dweaponcount; i++)
            {
                scr_weaponinfo(i);
                var cleaned_desc = string_replace_all(weapondesctemp, ""#"", "" "");
                var combined = weaponnametemp + "" - "" + cleaned_desc;

                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + ""..."";

                array_push(dbutton_options, combined);
                array_push(dbutton_indices, i);
            }

            var cutoff = min(drecent_weapon - 1, dweaponcount);

            for (var i = 1; i <= cutoff; i++)
            {
                scr_weaponinfo(i);
                var cleaned_desc = string_replace_all(weapondesctemp, ""#"", "" "");
                var combined = weaponnametemp + "" - "" + cleaned_desc;

                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + ""..."";

                array_push(dbutton_options, combined);
                array_push(dbutton_indices, i);
            }

            dmenu_box = 2;
            dbutton_layout = 1;
            break;

        case ""keyitems"":
            dmenu_title = ""Liste d'objets clés (Peut briser le jeu)"";
            dbutton_options = [];
            dbutton_indices = [];
            var max_len = 40;

            for (var i = drecent_keyitem; i <= dkeyitemcount; i++)
            {
                scr_keyiteminfo(i);
                var cleaned_desc = string_replace_all(tempkeyitemdesc, ""#"", "" "");
                var combined = tempkeyitemname + "" - "" + cleaned_desc;

                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + ""..."";

                array_push(dbutton_options, combined);
                array_push(dbutton_indices, i);
            }

            var cutoff = min(drecent_keyitem - 1, dkeyitemcount);

            for (var i = 1; i <= cutoff; i++)
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

        case ""flag_categories"":
            dmenu_title = ""Divers"";
            dbutton_options = [];
            dbutton_indices = [];
            categories_len = array_length(dother_categories);
            var max_len = 40;
            
            if (!global.dreading_custom_flag)
            {
                array_push(dbutton_options, ""Custom"");
                array_push(dbutton_indices, 0);
            }
            else
            {
                array_push(dbutton_options, ""global.flag["" + dcustom_flag_text[0] + ""] = |"" + dcustom_flag_text[1] + ""|"");
                array_push(dbutton_indices, 0);
            }
            
            for (var i = 0; i < categories_len; i++)
            {
                array_push(dbutton_options, dother_categories[i]);
                array_push(dbutton_indices, i);
            }
            
            dmenu_box = 2;
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
                dbutton_selected = 1;
            }

            if (dbutton_selected == 2)
            {
                dmenu_state = ""give"";
                dbutton_selected = 1;
            }

            if (dbutton_selected == 3)
            {
                dmenu_state = ""flag_misc"";
                dbutton_selected = 1;
            }

            break;

        case ""warp"":
            if (dbutton_selected == 1)
            {
                dmenu_state = ""lightworld"";
                dbutton_selected = 1;
            }
            
            if (dbutton_selected == 2)
            {
                dmenu_state = ""darkworld"";
                dbutton_selected = 1;
            }
            
            if (dbutton_selected == 3)
            {
                dmenu_state = ""battles"";
                dbutton_selected = 1;
            }
            
            break;
        
        case ""lightworld"":
            if (dbutton_selected == 1)
                dmenu_state = ""kris_hallway"";
            
            if (dbutton_selected == 2)
                dmenu_state = ""school_hallway"";
            
            if (dbutton_selected == 3)
                dmenu_state = ""school_closet"";
            
            if (dbutton_selected == 4)
                dmenu_state = ""after_darkworld"";
            
            break;
        
        case ""kris_hallway"":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 0;
            snd_free_all();
            room_goto(room_krishallway);
            
            if (dbutton_selected == 2)
                global.plot = 1;
            
            break;
        
        case ""school_hallway"":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 0;
            snd_free_all();
            room_goto(room_schoollobby);
            
            if (dbutton_selected == 2)
                global.plot = 1;
            
            break;
        
        case ""school_closet"":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 0;
            snd_free_all();
            room_goto(room_schooldoor);
            break;
        
        case ""after_darkworld"":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 0;
            snd_free_all();
            room_goto(room_lw_computer_lab);
            break;
        
        case ""darkworld"":
            if (dbutton_selected == 1)
                dmenu_state = ""dw_entrance"";
            
            if (dbutton_selected == 2)
                dmenu_state = ""castletown_entrance"";
            
            if (dbutton_selected == 3)
                dmenu_state = ""field_entrance"";
            
            if (dbutton_selected == 4)
                dmenu_state = ""seam_shop"";
            
            if (dbutton_selected == 5)
                dmenu_state = ""greatboard_entrance"";
            
            if (dbutton_selected == 6)
                dmenu_state = ""forest_entrance"";
            
            if (dbutton_selected == 7)
                dmenu_state = ""bakesale"";
            
            if (dbutton_selected == 8)
                dmenu_state = ""castle_entrance"";
            
            if (dbutton_selected == 9)
                dmenu_state = ""castle_top_floor"";
            
            if (dbutton_selected == 10)
                dmenu_state = ""darkworld_exit"";
            
            break;
        
        case ""dw_entrance"":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dark1);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 0;
                global.char[2] = 0;
                global.plot = 11;
            }
            
            break;
        
        case ""castletown_entrance"":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_castle_outskirts);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 0;
                global.char[2] = 0;
                global.plot = 16;
            }
            
            break;
        
        case ""field_entrance"":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_field1);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 0;
                global.plot = 33;
            }
            
            break;
        
        case ""seam_shop"":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_field_shop1);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 2;
                global.plot = 40;
            }
            
            break;
        
        case ""greatboard_entrance"":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_field_checkers4);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 2;
                global.plot = 50;
            }
            
            break;
        
        case ""forest_entrance"":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_forest_savepoint1);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 0;
                global.plot = 60;
            }
            
            break;
        
        case ""bakesale"":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_forest_savepoint2);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 0;
                global.plot = 75;
            }
            
            break;
        
        case ""castle_entrance"":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_cc_1f);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 165;
            }
            
            break;
        
        case ""castle_top_floor"":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_cc_5f);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 165;
            }
            
            break;
        
        case ""darkworld_exit"":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_cc_prefountain);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 0;
                global.plot = 240;
            }
            
            break;
        
        case ""battles"":
            if (dbutton_selected == 1)
                dmenu_state = ""lancer_battle"";
            
            if (dbutton_selected == 2)
                dmenu_state = ""kround_battle"";
            
            if (dbutton_selected == 3)
                dmenu_state = ""clover_battle"";
            
            if (dbutton_selected == 4)
                dmenu_state = ""susie_battle"";
            
            if (dbutton_selected == 5)
                dmenu_state = ""kround2_battle"";
            
            if (dbutton_selected == 6)
                dmenu_state = ""jevil_battle"";
            
            if (dbutton_selected == 7)
                dmenu_state = ""king_battle"";
            
            break;
        
        case ""lancer_battle"":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_castle_town);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 0;
                global.plot = 16;
            }
            
            break;
        
        case ""kround_battle"":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_field_checkers7);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 2;
                global.plot = 55;
            }
            
            break;
        
        case ""clover_battle"":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_forest_area3);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 0;
                global.plot = 72;
            }
            
            break;
        
        case ""susie_battle"":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_forest_savepoint3);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 0;
                global.plot = 120;
            }
            
            break;
        
        case ""kround2_battle"":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_cc_6f);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.charauto[2] = 0;
                global.plot = 165;
            }
            
            break;
        
        case ""jevil_battle"":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_cc_prison_prejoker);
            
            if (dbutton_selected == 2)
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
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_cc_throneroom);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.charauto[2] = 0;
            }
            
            break;

        case ""give"":
            if (dbutton_selected == 1)
            {
                dmenu_state = ""objects"";
                dbutton_selected = 1;
            }

            if (dbutton_selected == 2)
            {
                dmenu_state = ""armors"";
                dbutton_selected = 1;
            }

            if (dbutton_selected == 3)
            {
                dmenu_state = ""weapons"";
                dbutton_selected = 1;
            }

            if (dbutton_selected == 4)
            {
                dmenu_state = ""keyitems"";
                dbutton_selected = 1;
            }

            break;

        case ""objects"":
            dgiver_menu_state = dmenu_state;
            dbutton_selected = clamp(dbutton_selected, 0, array_length(dbutton_options) - 1);
            dgiver_button_selected = dbutton_selected;
            dmenu_state = ""givertab"";
            dbutton_selected = 1;
            break;

        case ""armors"":
            dgiver_menu_state = dmenu_state;
            dgiver_button_selected = dbutton_selected;
            dmenu_state = ""givertab"";
            dbutton_selected = 1;
            break;

        case ""weapons"":
            dgiver_menu_state = dmenu_state;
            dgiver_button_selected = dbutton_selected;
            dmenu_state = ""givertab"";
            dbutton_selected = 1;
            break;

        case ""keyitems"":
            dgiver_menu_state = dmenu_state;
            dgiver_button_selected = dbutton_selected;
            dmenu_state = ""givertab"";
            dbutton_selected = 1;
            break;

        case ""givertab"":
            if (dgiver_menu_state == ""objects"")
            {
                if (dgiver_amount > 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];

                    for (var i = 0; i < dgiver_amount; i++)
                        scr_itemget(real_index);

                    scr_debug_print(string(dgiver_amount) + "" "" + dgiver_bname + "" ajouté à l'inventaire"");
                }
                else if (dgiver_amount < 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];

                    for (var i = 0; i < abs(dgiver_amount); i++)
                        scr_itemremove(real_index);

                    scr_debug_print(string(abs(dgiver_amount)) + "" "" + dgiver_bname + "" retiré de l'inventaire"");
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
                
                for (var i = 0; i < array_length(dother_all_options); i++)
                {
                    options = dother_all_options[i];
                    
                    if (options[0] == (dbutton_selected - 2))
                        array_push(dother_options, options);
                }
                
                dhorizontal_index = find_subarray_index(dother_options[0][2], dother_options[0][3]);
                dmenu_state = ""flag_misc"";
                dbutton_selected = 1;
            }
            
            break;
        
        case ""flag_misc"":
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

importGroup.QueueFindReplace(obj_darkcontroller.EventHandlerFor(EventType.Step, (uint)0, Data),
"if (scr_debug())", "if (0)");

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

    if (sunkus_kb_check_pressed(83))
        instance_create(0, 0, obj_savemenu);

    if (sunkus_kb_check_pressed(76))
        scr_load();

    if (sunkus_kb_check_pressed(82) && sunkus_kb_check(8))
        game_restart_true();

    if (sunkus_kb_check_pressed(82) && !sunkus_kb_check(8))
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
    
    if (scr_debug_keycheck(vk_f3))
        scr_raise_party();
    
    if (keyboard_check_pressed(ord(""W"")))
    {
        scr_wincombat();
        scr_debug_print(""Combat passé"");
    }
    
    if (scr_debug_keycheck(vk_f6))
        scr_weaken_enemies();
    
    if (scr_debug_keycheck(vk_f8))
        scr_weaken_party(true);
    
    if (scr_debug_keycheck(ord(""M"")))
    {
        if (audio_is_playing(global.batmusic[1]))
        {
            if (!audio_is_paused(global.batmusic[1]))
                audio_pause_sound(global.batmusic[1]);
            else
                audio_resume_sound(global.batmusic[1]);
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

ScriptMessage("Mode Debug du Chapitre 1 " + (enable ? "ajouté" : "désactivé") + ".\r\n" + "Pour activer le Mode Debug en jeu, appuyer sur F10.");

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
dmenu_title = "Menu Debug";
dbutton_options_original = ["Sauts", "Items", "Recrues", "Divers"];
dnumber_litems = [0, 11, 14, 14, 18];
dlight_weapons = [];
dlight_armors = [[3, "Pansement"], [14, "Montre"]];
dlight_objects = [[1, "Chocolat Chaud"], [2, "Crayon"], [3, "Pansement"], [4, "Bouquet"], [5, "Boule de Trucs"], [6, "Crayon Halloween"], [7, "Crayon Fétiche"], [8, "Œuf"], [9, "Cartes"], [10, "Boîte de ChocoCœurs"], [11, "Verre"], [12, "Gomme"], [13, "Critérium"], [14, "Montre"], [15, "Crayon de Noël"], [16, "Épine de Cactus"], [17, "ÉclatNoir"], [18, "Stylo-Plume"]];

if (global.chapter >= 4)
    dtemp_lst = get_lw_dw_weapon_list();
else
    dlight_weapons = [[2, "Crayon"], [6, "Crayon Halloween"], [7, "Crayon Fétiche"], [12, "Gomme"], [13, "Critérium"]];

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
dmenu_state = "debug";
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
ditem_types = ["objects", "armors", "weapons", "keyitems"];
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
SEAM = cate_enum++;
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
dother_categories = ["Séquence Vaisseau", "Superbosses", "Weird Route", "Seam", "Œufs", "Onion San", "Misc chap 1", "Misc chap 2", "Legend of Tenna", "Sword Route", "Misc chap 3", "Misc chap 4", "Mousse", "Roboteur"];
dother_all_options = [];
dother_options = [];

if (global.chapter >= 0)
{
    array_push(dother_all_options, [GONER, "NOURRITURE", 903, [["SUCRÉE", 0], ["TENDRE", 1], ["AMÈRE", 2], ["SALÉE", 3], ["DOULEUR", 4], ["FROIDE", 5]]]);
    array_push(dother_all_options, [GONER, "GROUPE SANGUIN", 904, [["A", 0], ["AB", 1], ["B", 2], ["C", 3], ["D", 4]]]);
    array_push(dother_all_options, [GONER, "COULEUR", 905, [["ROUGE", 0], ["BLEU", 1], ["VERT", 2], ["CYAN", 3]]]);
    array_push(dother_all_options, [GONER, "PRÉSENT", 909, [["GENTILLESSE", -1], ["ESPRIT", 0], ["AMBITION", 1], ["BRAVOURE", 2], ["VOIX", 3]]]);
    array_push(dother_all_options, [GONER, "SENTIMENT ÉPROUVÉ", 906, [["AMOUR", 0], ["ESPOIR", 1], ["DÉGOÛT", 2], ["PEUR", 3]]]);
    array_push(dother_all_options, [GONER, "RÉPONDU HONNÊTEMENT", 907, [["OUI", 0], ["NON", 1]]]);
    array_push(dother_all_options, [GONER, "CONSENTIR AUX CRISES", 908, [["OUI", 0], ["NON", 1]]]);
}

if (global.chapter >= 1)
{
    array_push(dother_all_options, [ROBOTEUR, "Tête Roboteur", 220, [["Laser", 0], ["Épée", 1], ["Flamme", 2], ["Canard", 3]]]);
    array_push(dother_all_options, [ROBOTEUR, "Corps Roboteur", 221, [["Sobre", 0], ["Roue", 1], ["Tank", 2], ["Canard", 3]]]);
    array_push(dother_all_options, [ROBOTEUR, "Jambes Roboteur", 222, [["Baskets", 0], ["Pneus", 1], ["Chaînes", 2], ["Canard", 3]]]);
    array_push(dother_all_options, [MISC1, "Nom du gang", 214, [["Les Types (unused)", 0], ["L'Escouade $?$!$", 1], ["Le Fan Club Lancer", 2], ["Le Fun Gang", 3]]]);
    array_push(dother_all_options, [MISC1, "Prophétie entendu", 203, [["Non", 1], ["Oui", 0]]]);
    array_push(dother_all_options, [MISC1, "Manuel jeté", 207, [["Non", 0], ["A tenté", 1], ["L'a jeté", 2]]]);
    array_push(dother_all_options, [MISC1, "Gâteau rendu", 253, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [MISC1, "Starwalker", 254, [["Pissing me off", 0], ["I will   join", 1]]]);
    array_push(dother_all_options, [MISC1, "Objectif de Donation", 216, [["Non rempli", 0], ["Atteint", 1]]]);
    array_push(dother_all_options, [MISC1, "Fleurs d'Asgore", 262, [["Pas vu", 0], ["Pas données", 2], ["Données", 4]]]);
    array_push(dother_all_options, [MISC1, "Noëlle dehors", 276, [["Pas parlé", 0], ["Pas parlé de Susie", 1], ["A parlé de Susie", 2]]]);
    array_push(dother_all_options, [MISC1, "Évier inspecté (chap 1)", 278, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [EGG, "Œuf obtenu (chap 1)", 911, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [SUPERBOSS, "Jevil vaincu", 241, [["Non", 0], ["Via violence", 6], ["Via clémence", 7]]]);
    array_push(dother_all_options, [ONION_SAN, "Relation (chap 1)", 258, [["Pas vu", 0], ["Amis", 2], ["Pas amis", 3]]]);
    array_push(dother_all_options, [ONION_SAN, "Nom de Kris", 259, [["Pas donné", 0], ["Kris", 1], ["Hippopotame", 2]]]);
    array_push(dother_all_options, [ONION_SAN, "Nom d'Onion", 260, [["Pas donné", 0], ["Onion", 1], ["Beauté", 2], ["Asriel II", 3], ["Dégoûtant", 4]]]);
    array_push(dother_all_options, [MOUSSE, "Mousse mangée (chap 1)", 106, [["Non", 0], ["Oui", 1]]]);
}

if (global.chapter >= 2)
{
    array_push(dother_all_options, [MISC2, "Peluche", 307, [["Pas donnée", 0], ["Ralsei", 1], ["Susie", 2], ["Noëlle", 3], ["Berdly", 4]]]);
    array_push(dother_all_options, [MISC2, "Hacker recruté", 659, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [MISC2, "Bras de Berdly", 457, [["Brûlé", 0], ["Ok", 1]]]);
    array_push(dother_all_options, [WEIRD2, "Avancée", 915, [["Pas fait", 0], ["Nikomercant tué", 3], ["Berdly gelé", 6], ["A parlé a Susie", 9], ["Noëlle vue a l'hôpital", 20]]]);
    array_push(dother_all_options, [WEIRD2, "A cancel la weird route", 916, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [EGG, "Œuf obtenu (chap 2)", 918, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [SUPERBOSS, "Spamton vaincu", 309, [["Non", 0], ["Oui", 9]]]);
    array_push(dother_all_options, [MISC2, "\"Fan\" de Mettaton", 422, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [MISC2, "Statue de Susie récupérée", 393, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [MISC2, "ICE-E récupéré", 394, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [MISC2, "Évier inspecté (chap 2)", 461, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [MISC2, "Scène de l'abri vue", 315, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [ONION_SAN, "Relation (chap 2)", 425, [["Pas vu", 0], ["Amis", 1], ["Plus amis", 2]]]);
    array_push(dother_all_options, [MOUSSE, "Mousse mangée (chap 2)", 920, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [MOUSSE, "... avec Noëlle (chap 2)", 921, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [MOUSSE, "... avec Susie (chap 2)", 922, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [SEAM, "Seam a abandonné la quête", 961, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [SEAM, "Cristal de Jevil donné", 954, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [SEAM, "Cristal de Spamton donné", 353, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [SEAM, "A parlé a Seam tout court", 312, [["Non", 0], ["Oui", 1]]]);
}

if (global.chapter >= 3)
{
    array_push(dother_all_options, [LOT, "LOT Rang Board 1", 1173, [["Z", 0], ["C", 1], ["B", 2], ["A", 3], ["S", 4], ["T", 5]]]);
    array_push(dother_all_options, [LOT, "LOT Rang Board 2", 1174, [["Z", 0], ["C", 1], ["B", 2], ["A", 3], ["S", 4], ["T", 5]]]);
    array_push(dother_all_options, [SWORD, "Avancé", 1055, [["Pas fait", 0], ["Clé de glace obtenue", 1], ["Donjon (plateau 2)", 1.5], ["Elle a été utilisée", 2], ["Clé de l'abri obtenue", 3], ["Donjon (plateau 3)", 4], ["Clé de l'abri utilisée", 5], ["ERAM vaincu", 6]]]);
    array_push(dother_all_options, [SWORD, "Susie attaquée", 1268, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [EGG, "Œuf obtenu (chap 3)", 930, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [SUPERBOSS, "Chevalier vaincu", 1047, [["Non", 2], ["Oui", 1]]]);
    array_push(dother_all_options, [MISC3, "Fontaine", 1144, [["Pas affronté", 0], ["A flirt(pas parlé au rideau)", 1], ["Pas flirté", 2], ["A flirt(a parlé au rideau)", 3]]]);
    array_push(dother_all_options, [MISC3, "Statue de Tenna récupérée", 1222, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [MOUSSE, "Mousse mangée (chap 3)", 1078, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [SEAM, "Cristal du Chevalier donné", 856, [["Non", 0], ["Oui", 1]]]);
}

if (global.chapter >= 4)
{
    array_push(dother_all_options, [EGG, "Œuf obtenu (chap 4)", 931, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [SUPERBOSS, "Gerson vaincu", 1629, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [MOUSSE, "Mousse mangée (chap 4)", 1592, [["Non", 0], ["Oui", 1], ["Refusée", 2]]]);
    array_push(dother_all_options, [MISC4, "QC avec Susie", 701, [["Pas allé", 0], ["Y est allé", 1]]]);
    array_push(dother_all_options, [MISC4, "Prière", 1507, [["Pas prié", 0], ["Pour Susie", 1], ["Pour Noëlle", 2], ["Pour Asriel", 3]]]);
    array_push(dother_all_options, [MISC4, "Tenna donné", 779, [["Non", 0], ["Oui", 2]]]);
    array_push(dother_all_options, [MISC4, "Tel. de Noelle", 714, [["Pas inspecté", 0], ["Pas répondu", 1], ["Allez au festival", 2], ["Wrong number song", 3]]]);
    array_push(dother_all_options, [MISC4, "Prix Susie récupéré", 747, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [MISC4, "Tache retirée", 748, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [MISC4, "Échelle récupérée", 864, [["Non", 0], ["Oui", 1]]]);
    array_push(dother_all_options, [MISC4, "Oreiller récupéré", 865, [["Non", 0], ["Oui", 1]]]);
}

dflag_categories_len = [];

for (i = 0; i < array_length(dother_categories); i++)
    array_push(dflag_categories_len, 0);

for (i = 0; i < array_length(dother_all_options); i++)
    dflag_categories_len[dother_all_options[i][0]] += 1;

global.dreading_custom_flag = 0;
dcustom_flag_text = ["", ""];

find_subarray_index = function(arg0, arg1)
{
    value = global.flag[arg0];
    lst = arg1;
    prev = 0;
    
    for (i = 0; i < array_length(lst); i++)
    {
        if (lst[i][1] > value)
            break;
        
        prev = i;
    }
    
    return prev;
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
    target_room: 1,
    target_plot: global.plot,
    target_is_darkzone: global.darkzone,
    target_member_2: global.char[1],
    target_member_3: global.char[2]
};
dkeyboard_input = "";

for (i = 0; i < array_length(drooms_id); i++)
    array_push(drooms, room_get_name(drooms_id[i].room_index));


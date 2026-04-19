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
dbackspace_timer = 0;
dmenu_title = scr_dmode_get_text("debug_menu");
dbutton_options_original = [
    scr_dmode_get_text("warps"),
    scr_dmode_get_text("items"),
    scr_dmode_get_text("recruits"),
    scr_dmode_get_text("misc")
];
dnumber_litems = [0, 11, 14, 14, 18];
dlight_weapons = [];
dlight_armors = [
    [3, scr_dmode_get_text("bandage")],
    [14, scr_dmode_get_text("wristwatch")]
];
dlight_objects = [
    [1,  scr_dmode_get_text("hot_chocolate")],
    [2,  scr_dmode_get_text("pencil")],
    [3,  scr_dmode_get_text("bandage")],
    [4,  scr_dmode_get_text("bouquet")],
    [5,  scr_dmode_get_text("ball_junk")],
    [6,  scr_dmode_get_text("halloween_pencil")],
    [7,  scr_dmode_get_text("lucky_pencil")],
    [8,  scr_dmode_get_text("egg")],
    [9,  scr_dmode_get_text("cards")],
    [10, scr_dmode_get_text("heart_candy")],
    [11, scr_dmode_get_text("glass")],
    [12, scr_dmode_get_text("eraser")],
    [13, scr_dmode_get_text("mech_pencil")],
    [14, scr_dmode_get_text("wristwatch")],
    [15, scr_dmode_get_text("holiday_pencil")],
    [16, scr_dmode_get_text("cactus_needle")],
    [17, scr_dmode_get_text("black_shard")],
    [18, scr_dmode_get_text("quill_pen")]
];
dhinter_active = false;
itemdescb = "";
armordesctemp = "";
weapondesctemp = "";
tempkeyitemdesc = "";
dhinter_text = "";

if (global.chapter >= 4)
{
    dtemp_lst = get_lw_dw_weapon_list();
    
    for (i = 0; i < array_length(dtemp_lst); i++)
        array_push(dlight_weapons, dlight_objects[dtemp_lst[i].lw_id - 1]);
}
else
{
    dlight_weapons = [
    [2,  scr_dmode_get_text("pencil")],
    [6,  scr_dmode_get_text("halloween_pencil")],
    [7,  scr_dmode_get_text("lucky_pencil")],
    [12, scr_dmode_get_text("eraser")],
    [13, scr_dmode_get_text("mech_pencil")]
];
}

for (i = 0; i < array_length(dlight_objects); i++)
{
    if (dlight_objects[i][0] > dnumber_litems[global.chapter])
        array_delete(dlight_objects, i--, 1);
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

dpop_history = function()
{
	dkeyboard_input = "";
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
ZOEUFS = cate_enum++;
ONION_SAN = cate_enum++;
MISC1 = cate_enum++;
MISC2 = cate_enum++;
LOT = cate_enum++;
SWORD3 = cate_enum++;
MISC3 = cate_enum++;
MISC4 = cate_enum++;
MOUSSE = cate_enum++;
ROBOTEUR = cate_enum++;
dother_categories = [
    scr_dmode_get_text("cat_vessel"),
    scr_dmode_get_text("cat_superboss"),
    scr_dmode_get_text("cat_weird"),
    scr_dmode_get_text("cat_seam"),
    scr_dmode_get_text("cat_eggs"),
    scr_dmode_get_text("cat_onion"),
    scr_dmode_get_text("cat_misc1"),
    scr_dmode_get_text("cat_misc2"),
    scr_dmode_get_text("cat_tenna"),
    scr_dmode_get_text("cat_sword"),
    scr_dmode_get_text("cat_misc3"),
    scr_dmode_get_text("cat_misc4"),
    scr_dmode_get_text("cat_moss"),
    scr_dmode_get_text("cat_thrash")
];
dother_all_options = [];
dother_options = [];

if (global.chapter >= 0)
{
    // FOOD
    array_push(dother_all_options, [GONER, scr_dmode_get_text("g_food"), 903, [
        [scr_dmode_get_text("opt_sweet"), 0], [scr_dmode_get_text("opt_soft"), 1], 
        [scr_dmode_get_text("opt_bitter"), 2], [scr_dmode_get_text("opt_salty"), 3], 
        [scr_dmode_get_text("opt_pain"), 4], [scr_dmode_get_text("opt_cold"), 5]
    ]]);

    // BLOOD TYPE
    array_push(dother_all_options, [GONER, scr_dmode_get_text("g_blood"), 904, [
        ["A", 0], ["AB", 1], ["B", 2], ["C", 3], ["D", 4]
    ]]);

    // COLOR
    array_push(dother_all_options, [GONER, scr_dmode_get_text("g_color"), 905, [
        [scr_dmode_get_text("opt_red"), 0], [scr_dmode_get_text("opt_blue"), 1], 
        [scr_dmode_get_text("opt_green"), 2], [scr_dmode_get_text("opt_cyan"), 3]
    ]]);

    // GIFT
    array_push(dother_all_options, [GONER, scr_dmode_get_text("g_gift"), 909, [
        [scr_dmode_get_text("opt_kindness"), -1], [scr_dmode_get_text("opt_mind"), 0], 
        [scr_dmode_get_text("opt_ambition"), 1], [scr_dmode_get_text("opt_bravery"), 2], 
        [scr_dmode_get_text("opt_voice"), 3]
    ]]);

    // OPINION
    array_push(dother_all_options, [GONER, scr_dmode_get_text("g_feeling"), 906, [
        [scr_dmode_get_text("opt_love"), 0], [scr_dmode_get_text("opt_hope"), 1], 
        [scr_dmode_get_text("opt_disgust"), 2], [scr_dmode_get_text("opt_fear"), 3]
    ]]);

    // HONEST
    array_push(dother_all_options, [GONER, scr_dmode_get_text("g_honest"), 907, [
        [scr_dmode_get_text("opt_yes"), 0], [scr_dmode_get_text("opt_no"), 1]
    ]]);

    // CONSENT
    array_push(dother_all_options, [GONER, scr_dmode_get_text("g_crises"), 908, [
        [scr_dmode_get_text("opt_yes"), 0], [scr_dmode_get_text("opt_no"), 1]
    ]]);
}

if (global.chapter >= 1)
{
    // TRASH MACHINE
    array_push(dother_all_options, [ROBOTEUR, scr_dmode_get_text("thrash_head"), 220, [[scr_dmode_get_text("opt_laser"), 0], [scr_dmode_get_text("opt_sword"), 1], [scr_dmode_get_text("opt_flame"), 2], [scr_dmode_get_text("opt_duck"), 3]]]);
    array_push(dother_all_options, [ROBOTEUR, scr_dmode_get_text("thrash_body"), 221, [[scr_dmode_get_text("opt_simple"), 0], [scr_dmode_get_text("opt_wheel"), 1], [scr_dmode_get_text("opt_tank"), 2], [scr_dmode_get_text("opt_duck"), 3]]]);
    array_push(dother_all_options, [ROBOTEUR, scr_dmode_get_text("thrash_legs"), 222, [[scr_dmode_get_text("opt_sneakers"), 0], [scr_dmode_get_text("opt_tires"), 1], [scr_dmode_get_text("opt_tracks"), 2], [scr_dmode_get_text("opt_duck"), 3]]]);
    
    // MISC 1
    array_push(dother_all_options, [MISC1, scr_dmode_get_text("label_gang"), 214, [[scr_dmode_get_text("gang_guys"), 0], [scr_dmode_get_text("gang_squad"), 1], [scr_dmode_get_text("gang_fanclub"), 2], [scr_dmode_get_text("gang_fungang"), 3]]]);
    array_push(dother_all_options, [MISC1, scr_dmode_get_text("label_prophecy"), 203, [[scr_dmode_get_text("opt_no"), 1], [scr_dmode_get_text("opt_yes"), 0]]]);
    array_push(dother_all_options, [MISC1, scr_dmode_get_text("label_manual"), 207, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_tried"), 1], [scr_dmode_get_text("opt_thrown"), 2]]]);
    array_push(dother_all_options, [MISC1, scr_dmode_get_text("label_cake"), 253, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    array_push(dother_all_options, [MISC1, scr_dmode_get_text("label_starwalker"), 254, [["Pissing me off", 0], ["I will join", 1]]]);
    array_push(dother_all_options, [MISC1, scr_dmode_get_text("label_donation"), 216, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_reached"), 1]]]);
    array_push(dother_all_options, [MISC1, scr_dmode_get_text("label_asgore_flowers"), 262, [[scr_dmode_get_text("opt_notseen"), 0], [scr_dmode_get_text("opt_no"), 2], [scr_dmode_get_text("opt_given"), 4]]]);
    array_push(dother_all_options, [MISC1, scr_dmode_get_text("label_noelle_out"), 276, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_no"), 1], [scr_dmode_get_text("opt_talked_susie"), 2]]]);
    array_push(dother_all_options, [MISC1, scr_dmode_get_text("label_sink"), 278, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    
    // EGGS & SUPERBOSSES
    array_push(dother_all_options, [ZOEUFS, scr_dmode_get_text("label_egg1"), 911, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    array_push(dother_all_options, [SUPERBOSS, scr_dmode_get_text("label_jevil"), 241, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_violence"), 6], [scr_dmode_get_text("opt_mercy"), 7]]]);
    
    // ONION SAN
    array_push(dother_all_options, [ONION_SAN, scr_dmode_get_text("label_onion_rel"), 258, [[scr_dmode_get_text("opt_notseen"), 0], [scr_dmode_get_text("opt_friends"), 2], [scr_dmode_get_text("opt_notfriends"), 3]]]);
    array_push(dother_all_options, [ONION_SAN, scr_dmode_get_text("label_kris_name"), 259, [[scr_dmode_get_text("opt_no"), 0], ["Kris", 1], [scr_dmode_get_text("opt_hippo"), 2]]]);
    array_push(dother_all_options, [ONION_SAN, scr_dmode_get_text("label_onion_name"), 260, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_onyx"), 1], [scr_dmode_get_text("opt_beauty"), 2], [scr_dmode_get_text("opt_asriel2"), 3], [scr_dmode_get_text("opt_stinky"), 4]]]);
    
    // MOSS
    array_push(dother_all_options, [MOUSSE, scr_dmode_get_text("label_moss1"), 106, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
}

if (global.chapter >= 2)
{
    // MISC 2
    array_push(dother_all_options, [MISC2, scr_dmode_get_text("label_plush"), 307, [[scr_dmode_get_text("opt_notgiven"), 0], ["Ralsei", 1], ["Susie", 2], ["Noëlle", 3], ["Berdly", 4]]]);
    array_push(dother_all_options, [MISC2, scr_dmode_get_text("label_hacker"), 659, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    array_push(dother_all_options, [MISC2, scr_dmode_get_text("label_berdly_arm"), 457, [[scr_dmode_get_text("opt_burnt"), 0], [scr_dmode_get_text("opt_ok"), 1]]]);
    array_push(dother_all_options, [MISC2, scr_dmode_get_text("label_mt_fan"), 422, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    array_push(dother_all_options, [MISC2, scr_dmode_get_text("label_susie_statue"), 393, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    array_push(dother_all_options, [MISC2, scr_dmode_get_text("label_icee_statue"), 394, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    array_push(dother_all_options, [MISC2, scr_dmode_get_text("label_sink2"), 461, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    array_push(dother_all_options, [MISC2, scr_dmode_get_text("label_shelter"), 315, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);

    // WEIRD ROUTE
    array_push(dother_all_options, [WEIRD2, scr_dmode_get_text("label_weird_prog"), 915, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_viri_killed"), 3], [scr_dmode_get_text("opt_frozen"), 6], [scr_dmode_get_text("opt_talked_susie"), 9], [scr_dmode_get_text("opt_hospital"), 20]]]);
    array_push(dother_all_options, [WEIRD2, scr_dmode_get_text("label_weird_cancel"), 916, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    
    // Eggs & Superbosses
    array_push(dother_all_options, [ZOEUFS, scr_dmode_get_text("label_egg2"), 918, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    array_push(dother_all_options, [SUPERBOSS, scr_dmode_get_text("label_spamton"), 309, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 9]]]);
    
    // ONION SAN
    array_push(dother_all_options, [ONION_SAN, scr_dmode_get_text("label_onion_rel2"), 425, [[scr_dmode_get_text("opt_notseen"), 0], [scr_dmode_get_text("opt_friends"), 1], [scr_dmode_get_text("opt_notfriends_anymore"), 2]]]);
    
    // MOSS
    array_push(dother_all_options, [MOUSSE, scr_dmode_get_text("label_moss2"), 920, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    array_push(dother_all_options, [MOUSSE, scr_dmode_get_text("label_moss_noelle"), 921, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    array_push(dother_all_options, [MOUSSE, scr_dmode_get_text("label_moss_susie"), 922, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    
    // SEAM
    array_push(dother_all_options, [SEAM, scr_dmode_get_text("label_seam_gaveup"), 961, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    array_push(dother_all_options, [SEAM, scr_dmode_get_text("label_crystal_jevil"), 954, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    array_push(dother_all_options, [SEAM, scr_dmode_get_text("label_crystal_spamton"), 353, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    array_push(dother_all_options, [SEAM, scr_dmode_get_text("label_seam_talk"), 312, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
}

if (global.chapter >= 3)
{
    // LOT
    array_push(dother_all_options, [LOT, scr_dmode_get_text("label_lot_rank1"), 1173, [["Z", 0], ["C", 1], ["B", 2], ["A", 3], ["S", 4], ["T", 5]]]);
    array_push(dother_all_options, [LOT, scr_dmode_get_text("label_lot_rank2"), 1174, [["Z", 0], ["C", 1], ["B", 2], ["A", 3], ["S", 4], ["T", 5]]]);
    
    // SWORD ROUTE
    array_push(dother_all_options, [SWORD3, scr_dmode_get_text("label_sword_prog"), 1055, [
        [scr_dmode_get_text("opt_notseen"), 0], 
        [scr_dmode_get_text("opt_ice_key"), 1], 
        [scr_dmode_get_text("opt_dungeon2"), 1.5], 
        [scr_dmode_get_text("opt_key_used"), 2], 
        [scr_dmode_get_text("opt_shelter_key"), 3], 
        [scr_dmode_get_text("opt_dungeon3"), 4], 
        [scr_dmode_get_text("opt_shelter_used"), 5], 
        [scr_dmode_get_text("opt_eram"), 6]
    ]]);
    array_push(dother_all_options, [SWORD3, scr_dmode_get_text("label_susie_attacked"), 1268, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    
    // Eggs & Superbosses
    array_push(dother_all_options, [ZOEUFS, scr_dmode_get_text("label_egg3"), 930, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    array_push(dother_all_options, [SUPERBOSS, scr_dmode_get_text("label_knight"), 1047, [[scr_dmode_get_text("opt_no"), 2], [scr_dmode_get_text("opt_yes"), 1]]]);
    
    // MISC 3
    array_push(dother_all_options, [MISC3, scr_dmode_get_text("label_fountain"), 1144, [
        [scr_dmode_get_text("opt_notseen"), 0], 
        [scr_dmode_get_text("opt_flirt_no_curtain"), 1], 
        [scr_dmode_get_text("opt_no_flirt"), 2], 
        [scr_dmode_get_text("opt_flirt_curtain"), 3]
    ]]);
    array_push(dother_all_options, [MISC3, scr_dmode_get_text("label_tenna_statue"), 1222, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    
    // MOSS
    array_push(dother_all_options, [MOUSSE, scr_dmode_get_text("label_moss3"), 1078, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    
    // SEAM
    array_push(dother_all_options, [SEAM, scr_dmode_get_text("label_crystal_knight"), 856, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
}

if (global.chapter >= 4)
{
    // Eggs & Superbosses
    array_push(dother_all_options, [ZOEUFS, scr_dmode_get_text("label_egg4"), 931, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    array_push(dother_all_options, [SUPERBOSS, scr_dmode_get_text("label_gerson"), 1629, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    
    // MOSS
    array_push(dother_all_options, [MOUSSE, scr_dmode_get_text("label_moss4"), 1592, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1], [scr_dmode_get_text("opt_refused"), 2]]]);
    
    // MISC 4
    array_push(dother_all_options, [MISC4, scr_dmode_get_text("label_ralsei_room"), 710, [[scr_dmode_get_text("opt_notseen"), 0], [scr_dmode_get_text("opt_seen"), 2]]]);
    array_push(dother_all_options, [MISC4, scr_dmode_get_text("label_qcs_susie"), 701, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_visited"), 1]]]);
    array_push(dother_all_options, [MISC4, scr_dmode_get_text("label_tea_ralsei"), 1514, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    
    // Pray
    array_push(dother_all_options, [MISC4, scr_dmode_get_text("label_prayer"), 1507, [
        [scr_dmode_get_text("opt_no"), 0], 
        [scr_dmode_get_text("opt_for_susie"), 1], 
        [scr_dmode_get_text("opt_for_noelle"), 2], 
        [scr_dmode_get_text("opt_for_asriel"), 3]
    ]]);
    
    array_push(dother_all_options, [MISC4, scr_dmode_get_text("label_tenna_given"), 779, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 2]]]);
    
    // Noelle's phone
    array_push(dother_all_options, [MISC4, scr_dmode_get_text("label_noelle_phone"), 714, [
        [scr_dmode_get_text("opt_not_inspected"), 0], 
        [scr_dmode_get_text("opt_no_answer"), 1], 
        [scr_dmode_get_text("opt_festival"), 2], 
        [scr_dmode_get_text("opt_wrong_number"), 3]
    ]]);
    
    array_push(dother_all_options, [MISC4, scr_dmode_get_text("label_susie_prize"), 747, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    array_push(dother_all_options, [MISC4, scr_dmode_get_text("label_stain"), 748, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    array_push(dother_all_options, [MISC4, scr_dmode_get_text("label_ladder"), 864, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
    array_push(dother_all_options, [MISC4, scr_dmode_get_text("label_pillow"), 865, [[scr_dmode_get_text("opt_no"), 0], [scr_dmode_get_text("opt_yes"), 1]]]);
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
    target_room: ROOM_INITIALIZE,
    target_room: ROOM_INITIALIZE,
    target_plot: global.plot,
    target_is_darkzone: global.darkzone,
    target_member_2: global.char[1],
    target_member_3: global.char[2]
};
dkeyboard_input = "";

for (i = 0; i < array_length(drooms_id); i++)
    array_push(drooms, room_get_name(drooms_id[i].room_index));

function dmenu_state_update()
{
    switch (dmenu_state)
    {
        case "debug":
            dmenu_title = gml_Script_scr_dmode_get_text("menu_debug");
            dbutton_options = dbutton_options_original;
            dmenu_box = 0;
            dbutton_layout = 0;
            break;
        
        case "warp":
            dmenu_title = gml_Script_scr_dmode_get_text("room_list");
            dbutton_options = [gml_Script_scr_dmode_get_text("btn_current_room"), gml_Script_scr_dmode_get_text("btn_search")];
            dbutton_indices = [-1, -1];
            
            if (global.dreading_custom_flag || dkeyboard_input != "")
                dbutton_options[1] = gml_Script_scr_dmode_get_text("ui_contains");
            else
                dbutton_options[1] = gml_Script_scr_dmode_get_text("btn_search");
            
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
        
        case "warp_options":
            dmenu_title = gml_Script_scr_dmode_get_text("warp_options");
            dbutton_options = [gml_Script_scr_dmode_get_text("btn_cancel"), gml_Script_scr_dmode_get_text("ui_is_darkworld"), gml_Script_scr_dmode_get_text("ui_plot_value"), gml_Script_scr_dmode_get_text("ui_teammate2"), gml_Script_scr_dmode_get_text("ui_teammate3"), gml_Script_scr_dmode_get_text("btn_warp")];
            dbutton_indices = [0, 1, 2, 3, 4, 5];
            dbutton_options[1] += drooms_options.target_is_darkzone ? gml_Script_scr_dmode_get_text("opt_yes") : gml_Script_scr_dmode_get_text("opt_no");
            
            if (global.dreading_custom_flag)
                dbutton_options[2] += dkeyboard_input;
            else
                dbutton_options[2] += string(drooms_options.target_plot);
            
            teammates = [gml_Script_scr_dmode_get_text("ui_nobody"), "Kris", "Susie", "Ralsei", "Noëlle"];
            dbutton_options[3] += teammates[drooms_options.target_member_2];
            dbutton_options[4] += teammates[drooms_options.target_member_3];
            break;
        
        case "give":
            dmenu_title = gml_Script_scr_dmode_get_text("item_type");
            dbutton_options_2d = [[gml_Script_scr_dmode_get_text("type_items"), gml_Script_scr_dmode_get_text("type_armors"), gml_Script_scr_dmode_get_text("type_weapons"), gml_Script_scr_dmode_get_text("type_keyitems")]];
            dmenu_box = 0;
            dbutton_layout = 0;
            break;
        
        case "objects":
            dmenu_title = gml_Script_scr_dmode_get_text("item_list");
            dbutton_options = [gml_Script_scr_dmode_get_text("ui_chapter")];
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
                    var cleaned_desc = string_replace_all(itemdescb, "#", " ");
                    var combined = itemnameb;
                    
                    if (string_length(combined) > max_len)
                        combined = string_copy(combined, 1, max_len - 3) + "...";
                    
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
                    var combined = dlight_objects[i][1] + " - " + string(itemcount) + " " + gml_Script_scr_dmode_get_text("ui_held");
                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, dlight_objects[i][0]);
                }
            }
            
            dmenu_box = 2;
            dbutton_layout = 1;
            break;
        
        case "armors":
            dmenu_title = gml_Script_scr_dmode_get_text("armor_list");
            dbutton_options = [gml_Script_scr_dmode_get_text("ui_chapter")];
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
                    var cleaned_desc = string_replace_all(armordesctemp, "#", " ");
                    var combined = armornametemp;
                    
                    if (string_length(combined) > max_len)
                        combined = string_copy(combined, 1, max_len - 3) + "...";
                    
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
                        combined += (" (" + gml_Script_scr_dmode_get_text("ui_equipped") + ")");
                    
                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, i);
                }
            }
            
            dmenu_box = 2;
            dbutton_layout = 1;
            break;
        
        case "weapons":
            dmenu_title = gml_Script_scr_dmode_get_text("weapon_list");
            dbutton_options = [gml_Script_scr_dmode_get_text("ui_chapter")];
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
                    var cleaned_desc = string_replace_all(weapondesctemp, "#", " ");
                    var combined = weaponnametemp;
                    
                    if (string_length(combined) > max_len)
                        combined = string_copy(combined, 1, max_len - 3) + "...";
                    
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
                        combined += (" (" + gml_Script_scr_dmode_get_text("ui_equipped") + ")");
                    
                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, i);
                }
            }
            
            dmenu_box = 2;
            dbutton_layout = 1;
            break;
        
        case "keyitems":
            dmenu_title = gml_Script_scr_dmode_get_text("keyitem_list");
            dbutton_options = [gml_Script_scr_dmode_get_text("ui_chapter")];
            dbutton_indices = [-1];
            dbutton_options[0] += string(ditem_chap);
            var max_len = 33;
            var _data = dkeyitem_index_data(ditem_chap);
            var my_start = _data.start_id;
            var my_count = _data.count;
            
            for (var i = my_start; i < (my_start + my_count); i++)
            {
                scr_keyiteminfo(i);
                var cleaned_desc = string_replace_all(tempkeyitemdesc, "#", " ");
                var combined = tempkeyitemname;
                
                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + "...";
                
                array_push(dbutton_options, combined);
                array_push(dbutton_indices, i);
            }
            
            dmenu_box = 2;
            dbutton_layout = 1;
            break;
        
        case "givertab":
            dmenu_title = gml_Script_scr_dmode_get_text("add_how_many");
            dgiver_amount = 1;
            dmenu_box = 0;
            dbutton_layout = 2;
            break;
        
        case "recruits":
            dmenu_title = gml_Script_scr_dmode_get_text("recruit_list");
            dbutton_options = [gml_Script_scr_dmode_get_text("btn_presets")];
            dbutton_indices = [gml_Script_scr_dmode_get_text("btn_presets")];
            var max_len = 40;
            
            if (dhorizontal_page != 0)
            {
                dbutton_options[0] = " " + dbutton_options[0];
                dbutton_indices[0] = " " + dbutton_indices[0];
            }
            
            if (dhorizontal_page != 0)
            {
                var test_lst = scr_get_chapter_recruit_data(dhorizontal_page);
                
                for (var i = 0; i < array_length(test_lst); i++)
                {
                    var enemy_id = test_lst[i];
                    scr_recruit_info(enemy_id);
                    var combined = _name + " - [" + string(max(floor(global.flag[enemy_id + 600] * _recruitcount), -1)) + "/" + string(_recruitcount) + "]";
                    
                    if (string_length(combined) > max_len)
                        combined = string_copy(combined, 1, max_len - 3) + "...";
                    
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
        
        case "recruit_presets":
            dmenu_title = gml_Script_scr_dmode_get_text("recruit_presets");
            dbutton_options = [gml_Script_scr_dmode_get_text("btn_recruit_all"), gml_Script_scr_dmode_get_text("btn_lose_all")];
            
            if (dhorizontal_page)
            {
                dmenu_title += (" (" + gml_Script_scr_dmode_get_text("ui_chap_short") + " " + string(dhorizontal_page) + ")");
                dbutton_options[0] += " " + gml_Script_scr_dmode_get_text("ui_of_chapter") + " " + string(dhorizontal_page);
                dbutton_options[1] += " " + gml_Script_scr_dmode_get_text("ui_of_chapter") + " " + string(dhorizontal_page);
            }
            
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case "flag_categories":
            dmenu_title = gml_Script_scr_dmode_get_text("misc");
            dbutton_options = [];
            dbutton_indices = [-1];
            categories_len = array_length(dother_categories);
            var max_len = 40;
            
            if (!global.dreading_custom_flag)
                array_push(dbutton_options, gml_Script_scr_dmode_get_text("ui_custom"));
            else
                array_push(dbutton_options, "global.flag[" + dcustom_flag_text[0] + "] = |" + dcustom_flag_text[1] + "|");
            
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
        
        case "flag_misc":
            dmenu_title = gml_Script_scr_dmode_get_text("misc");
            dbutton_options = [];
            dbutton_indices = [];
            other_len = array_length(dother_options);
            var max_len = 40;
            
            for (var i = 0; i < other_len; i++)
            {
                cur_option = dother_options[i];
                flag_number = global.flag[cur_option[2]];
                var combined = cur_option[1] + " - " + gml_Script_scr_dmode_get_text("ui_problem");
                
                if (i == dvertical_index)
                    option_index = dhorizontal_index;
                else
                    option_index = find_subarray_index(cur_option[2], cur_option[3]);
                
                combined = cur_option[1] + " -  " + cur_option[3][option_index][0];
                
                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + "...";
                
                array_push(dbutton_options, combined);
                array_push(dbutton_indices, i);
            }
            
            dmenu_box = 2;
            dbutton_layout = 1;
            break;
        
        case "globals_changer":
            dmenu_title = "Global changer";
            dbutton_options = [];
            dmenu_box = 1;
            dbutton_layout = 1;
            reading_double_flag = dvertical_index == 0 && global.dreading_custom_flag;
            
            for (var i = 0; i < array_length(dglobal_changer_options); i++)
            {
                name = dglobal_changer_options[i][0];
                
                if (i == 0 && reading_double_flag)
                    name = dcustom_flag_text[0];
                
                text = name;
                
                if (i == 0 && reading_double_flag)
                {
                    scr_debug_print(string(dcustom_flag_text));
                    text = "global." + dcustom_flag_text[0];
                }
                
                cur_global_value = "";
                var_exist = variable_global_exists(name);
                
                if (var_exist)
                    cur_global_value = variable_global_get(name);
                
                array_push(dbutton_indices, i);
                limit = dglobal_changer_options[i][2];
                
                if (limit > 1)
                {
                    lookup_index = 0;
                    
                    if (i == dvertical_index)
                        lookup_index = dhorizontal_index;
                    
                    text += ("[" + string(lookup_index) + "]");
                    cur_global_value = cur_global_value[lookup_index];
                }
                
                if (global.dreading_custom_flag && i == dvertical_index)
                {
                    if (i != 0 || dcustom_flag_text[1] != "" || !var_exist)
                        cur_global_value = dcustom_flag_text[reading_double_flag];
                }
                
                if (!(i == 0 && dvertical_index == 0 && !global.dreading_custom_flag))
                    text += (" = |" + string(cur_global_value) + "|");
                
                array_push(dbutton_options, text);
            }
            
            break;
        
        default:
            dmenu_box = 0;
            dbutton_layout = 0;
            dbutton_options = [];
    }
}

function dmenu_state_interact()
{
    selected_name = "";
    
    if (dbutton_layout != 0)
        selected_name = string(dbutton_options[dvertical_index]);
    else
        selected_name = string(dbutton_options_2d[dvertical_index][dhorizontal_index]);
    
    switch (dmenu_state)
    {
        case "debug":
            dvertical_index = 0;
            
            if (selected_name == gml_Script_scr_dmode_get_text("warps"))
            {
                dmenu_state = "warp";
                dhorizontal_index = 0;
                dkeyboard_input = "";
                drooms_options.target_plot = global.plot;
                drooms_options.target_is_darkzone = global.darkzone;
            }
            else if (selected_name == gml_Script_scr_dmode_get_text("items"))
            {
                dmenu_state = "give";
            }
            else if (selected_name == gml_Script_scr_dmode_get_text("recruits"))
            {
                dmenu_state = "recruits";
                dhorizontal_page = 0;
            }
            else if (selected_name == gml_Script_scr_dmode_get_text("misc"))
            {
                dmenu_state = "flag_categories";
            }
            else if (selected_name == "Globals")
            {
                dmenu_state = "globals_changer";
            }
            
            break;
        
        case "warp":
            if (dvertical_index == 1)
            {
                global.dreading_custom_flag = 1;
                keyboard_string = "";
                dkeyboard_input = "";
                dmenu_state_update();
            }
            else
            {
                drooms_options.target_room = -1;
                
                if (dvertical_index != 0)
                    drooms_options.target_room = dbutton_indices[dvertical_index];
                
                drooms_options.target_plot = global.plot;
                drooms_options.target_is_darkzone = global.darkzone;
                drooms_options.target_member_2 = global.char[1];
                drooms_options.target_member_3 = global.char[2];
                dmenu_state = "warp_options";
                dkeyboard_input = "";
            }
            
            break;
        
        case "warp_options":
            if (dvertical_index == 0)
            {
                dkeyboard_input = "";
                dmenu_state = "warp";
            }
            else if (dvertical_index == 1)
            {
                drooms_options.target_is_darkzone ^= 1;
            }
            else if (dvertical_index == 4)
            {
                global.dreading_custom_flag = 1;
                keyboard_string = "";
            }
            else if (dvertical_index == 5)
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
        
        case "give":
            if (dvertical_index == 0)
                dmenu_state = "objects";
            else if (dvertical_index == 1)
                dmenu_state = "armors";
            else if (dvertical_index == 2)
                dmenu_state = "weapons";
            else if (dvertical_index == 3)
                dmenu_state = "keyitems";
            
            dhorizontal_page = !global.darkzone;
            
            if (dvertical_index == 3)
                dhorizontal_page = 0;
            
            dvertical_index = 0;
            break;
        
        case "objects":
            if (dhorizontal_page == 0 && dvertical_index == 0)
            {
                ditem_chap += 1;
                
                if (ditem_chap > global.chapter)
                    ditem_chap = 1;
            }
            else
            {
                dgiver_menu_state = dmenu_state;
                dvertical_index = clamp(dvertical_index, 0, array_length(dbutton_options));
                dgiver_button_selected = dvertical_index;
                dmenu_state = "givertab";
                dvertical_index = 0;
            }
            
            break;
        
        case "armors":
            if (dhorizontal_page == 1)
            {
                global.larmor = dlight_armors[dvertical_index][0];
                break;
            }
            
            if (dhorizontal_page == 0 && dvertical_index == 0)
            {
                ditem_chap += 1;
                
                if (ditem_chap > global.chapter)
                    ditem_chap = 1;
            }
            else
            {
                dgiver_menu_state = dmenu_state;
                dgiver_button_selected = dvertical_index;
                dmenu_state = "givertab";
                dvertical_index = 0;
            }
            
            break;
        
        case "weapons":
            if (dhorizontal_page == 1)
            {
                global.lweapon = dlight_weapons[dvertical_index][0];
                break;
            }
            
            if (dhorizontal_page == 0 && dvertical_index == 0)
            {
                ditem_chap += 1;
                
                if (ditem_chap > global.chapter)
                    ditem_chap = 1;
            }
            else
            {
                dgiver_menu_state = dmenu_state;
                dgiver_button_selected = dvertical_index;
                dmenu_state = "givertab";
                dvertical_index = 0;
            }
            
            break;
        
        case "keyitems":
            if (dvertical_index == 0)
            {
                ditem_chap += 1;
                
                if (ditem_chap > global.chapter)
                    ditem_chap = 1;
            }
            else
            {
                dgiver_menu_state = dmenu_state;
                dgiver_button_selected = dvertical_index;
                dmenu_state = "givertab";
                dvertical_index = 0;
            }
            
            break;
        
        case "givertab":
            if (dgiver_amount == 0)
            {
                scr_debug_print(gml_Script_scr_dmode_get_text("msg_cancelled"));
                break;
            }
            
            if (dgiver_menu_state == "objects")
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
                    scr_debug_print(string(abs(dgiver_amount)) + " " + dgiver_bname + gml_Script_scr_dmode_get_text("msg_removed_inv"));
                else
                    scr_debug_print(string(dgiver_amount) + " " + dgiver_bname + gml_Script_scr_dmode_get_text("msg_added_inv"));
            }
            
            if (dgiver_menu_state == "armors")
            {
                if (dgiver_amount > 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];
                    
                    for (var i = 0; i < dgiver_amount; i++)
                        scr_armorget(real_index);
                    
                    scr_debug_print(string(dgiver_amount) + " " + dgiver_bname + gml_Script_scr_dmode_get_text("msg_added_inv"));
                }
                else if (dgiver_amount < 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];
                    
                    for (var i = 0; i < abs(dgiver_amount); i++)
                        scr_armorremove(real_index);
                    
                    scr_debug_print(string(abs(dgiver_amount)) + " " + dgiver_bname + gml_Script_scr_dmode_get_text("msg_removed_inv"));
                }
            }
            
            if (dgiver_menu_state == "weapons")
            {
                if (dgiver_amount > 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];
                    
                    for (var i = 0; i < dgiver_amount; i++)
                        scr_weaponget(real_index);
                    
                    scr_debug_print(string(dgiver_amount) + " " + dgiver_bname + gml_Script_scr_dmode_get_text("msg_added_inv"));
                }
                else if (dgiver_amount < 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];
                    
                    for (var i = 0; i < abs(dgiver_amount); i++)
                        scr_weaponremove(real_index);
                    
                    scr_debug_print(string(abs(dgiver_amount)) + " " + dgiver_bname + gml_Script_scr_dmode_get_text("msg_removed_inv"));
                }
            }
            
            if (dgiver_menu_state == "keyitems")
            {
                if (dgiver_amount > 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];
                    
                    for (var i = 0; i < dgiver_amount; i++)
                        scr_keyitemget(real_index);
                    
                    scr_debug_print(string(dgiver_amount) + " " + dgiver_bname + gml_Script_scr_dmode_get_text("msg_added_inv"));
                }
                else if (dgiver_amount < 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];
                    
                    for (var i = 0; i < abs(dgiver_amount); i++)
                        scr_keyitemremove(real_index);
                    
                    scr_debug_print(string(abs(dgiver_amount)) + " " + dgiver_bname + gml_Script_scr_dmode_get_text("msg_removed_inv"));
                }
            }
            
            dpop_history();
            dmenu_active = false;
            global.interact = 0;
            break;
        
        case "flag_categories":
            if (dvertical_index > 0)
            {
                dother_options = [];
                real_index = dbutton_indices[dvertical_index];
                
                for (var i = 0; i < array_length(dother_all_options); i++)
                {
                    options = dother_all_options[i];
                    
                    if (options[0] == real_index)
                        array_push(dother_options, options);
                }
                
                dhorizontal_index = find_subarray_index(dother_options[0][2], dother_options[0][3]);
                dmenu_state = "flag_misc";
                dvertical_index = 0;
            }
            
            break;
        
        case "flag_misc":
            break;
        
        case "recruits":
            if (dvertical_index == 0)
                dmenu_state = "recruit_presets";
            
            break;
        
        case "recruit_presets":
            for (var c = 1; c <= global.chapter; c++)
            {
                if (dhorizontal_page != 0)
                    c = dhorizontal_page;
                
                var test_lst = scr_get_chapter_recruit_data(c);
                
                for (var i = 0; i < array_length(test_lst); i++)
                {
                    var enemy_id = test_lst[i];
                    scr_recruit_info(enemy_id);
                    
                    if (dvertical_index == 0)
                        global.flag[enemy_id + 600] = 1;
                    else
                        global.flag[enemy_id + 600] = -1;
                }
                
                if (dhorizontal_page != 0)
                    break;
            }
            
            if (dvertical_index == 0)
                snd_play(snd_pirouette);
            else
                snd_play(snd_weirdeffect);
            
            dpop_history();
            break;
        
        case "globals_changer":
            if (global.dreading_custom_flag)
            {
                var value = 0;
                cur_global_array = dglobal_changer_options[dvertical_index];
                reading_double_flag = dvertical_index == 0;
                glob_name = cur_global_array[0];
                
                if (reading_double_flag)
                    glob_name = dcustom_flag_text[0];
                
                scr_debug_print(string(cur_global_array));
                
                switch (cur_global_array[1])
                {
                    case "string":
                        value = string(dcustom_flag_text[reading_double_flag]);
                        break;
                    
                    case "int":
                    case "uint":
                    case "real":
                        value = real(dcustom_flag_text[reading_double_flag]);
                        break;
                    
                    default:
                        scr_debug_print("Unrecognized type |" + string(cur_global_array[1]) + "|");
                }
                
                if (cur_global_array[2] > 1)
                {
                    base = variable_global_get(glob_name);
                    base[dhorizontal_index] = value;
                    value = base;
                }
                
                variable_global_set(glob_name, value);
                scr_debug_print("Changed global." + string(glob_name) + " to |" + string(value) + "|");
            }
            
            set_keyboard_reader(global.dreading_custom_flag ^ 1);
            break;
        
        default:
            snd_play(snd_error);
            scr_debug_print(gml_Script_scr_dmode_get_text("msg_invalid"));
    }
}

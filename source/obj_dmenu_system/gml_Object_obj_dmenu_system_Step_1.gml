function dmenu_state_update()
{
    switch (dmenu_state)
    {
        case "debug":
            dmenu_title = scr_dmode_get_text("menu_debug");
            dbutton_options_2d = dbutton_options_original;
            dmenu_box = 0;
            dbutton_layout = 0;
            break;
        
        case "test":
            dmenu_title = "Debug save";
            dbutton_options = ["New save", "Search", "Spare route", "Weird route"];
            dbutton_indices = [-2, -2, -1, -1];
            
            if (global.dreading_custom_flag || dkeyboard_input != "")
                dbutton_options[1] = scr_dmode_get_text("ui_contains") + dkeyboard_input;
            else
                dbutton_options[1] = scr_dmode_get_text("btn_search") + dkeyboard_input;
            
            var subs = [];
            subs[2] = ["At Noelle's", "Jackenstein fight"];
            subs[3] = ["Proceed scene", "Titan's fight"];
            dmenu_process_submenus(subs, dkeyboard_input);
            dmenu_box = 1;
            dbutton_layout = 1;
            break;
        
        case "new_debug_save":
            dmenu_title = "New debug save";
            dbutton_options_2d = [["Enter save name"], ["Save", "Cancel"]];
            
            if (global.dreading_custom_flag || dkeyboard_input != "")
                dbutton_options_2d[0][0] = "";
            else
                dbutton_options[0][0] = "Enter save name";
            
            dbutton_options[0][0] += dkeyboard_input;
            dmenu_box = 0;
            dbutton_layout = 3;
            break;
        
        case "debug_load":
            dmenu_title = "Debug Load";
            dbutton_options = [scr_dmode_get_text("btn_search")];
            dbutton_indices = [-1];
            
            if (global.dreading_custom_flag || dkeyboard_input != "")
                dbutton_options[0] = scr_dmode_get_text("ui_contains");
            else
                dbutton_options[0] = scr_dmode_get_text("btn_search");
            
            dbutton_options[0] += dkeyboard_input;
            var my_ids = scr_get_debug_save_list();
            
            for (var i = 0; i < array_length(my_ids); i++)
            {
                var current_id = my_ids[i];
                var current_save_name = debug_save_names[i];
                
                if (!string_pos(string_lower(dkeyboard_input), string_lower(current_save_name)))
                    continue;
                
                array_push(dbutton_options, current_save_name);
                array_push(dbutton_indices, current_id);
            }
            
            dmenu_box = 1;
            dbutton_layout = 1;
            break;
        
        case "debug_load_options":
            dmenu_title = "Debug Load Options";
            dbutton_options = ["With current inventory: ", "Load"];
            dbutton_indices = [0, 1];
            dbutton_options[0] += dload_options.target_with_cur_inv ? scr_dmode_get_text("opt_yes") : scr_dmode_get_text("opt_no");
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case "warp":
            dmenu_title = scr_dmode_get_text("room_list");
            dbutton_options = [scr_dmode_get_text("btn_current_room"), scr_dmode_get_text("btn_search")];
            dbutton_indices = [-1, -1];
            
            if (global.dreading_custom_flag || dkeyboard_input != "")
                dbutton_options[1] = scr_dmode_get_text("ui_contains");
            else
                dbutton_options[1] = scr_dmode_get_text("btn_search");
            
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
            dmenu_title = scr_dmode_get_text("warp_options");
            dbutton_options = [scr_dmode_get_text("btn_cancel"), scr_dmode_get_text("ui_is_darkworld"), scr_dmode_get_text("ui_plot_value"), scr_dmode_get_text("ui_teammate2"), scr_dmode_get_text("ui_teammate3"), scr_dmode_get_text("btn_warp")];
            dbutton_indices = [0, 1, 2, 3, 4, 5];
            dbutton_options[1] += drooms_options.target_is_darkzone ? scr_dmode_get_text("opt_yes") : scr_dmode_get_text("opt_no");
            
            if (global.dreading_custom_flag)
                dbutton_options[2] += dkeyboard_input;
            else
                dbutton_options[2] += string(drooms_options.target_plot);
            
            teammates = [scr_dmode_get_text("ui_nobody"), "Kris", "Susie", "Ralsei", "Noëlle"];
            dbutton_options[3] += teammates[drooms_options.target_member_2];
            dbutton_options[4] += teammates[drooms_options.target_member_3];
            break;
        
        case "give":
            dmenu_title = scr_dmode_get_text("item_type");
            dbutton_options_2d = [[scr_dmode_get_text("type_items"), scr_dmode_get_text("type_armors"), scr_dmode_get_text("type_weapons"), scr_dmode_get_text("type_keyitems")]];
            dmenu_box = 0;
            dbutton_layout = 0;
            break;
        
        case "objects":
            dmenu_title = scr_dmode_get_text("item_list");
            dbutton_options = [scr_dmode_get_text("ui_chapter")];
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
                    var combined = dlight_objects[i][1] + " - " + string(itemcount) + " " + scr_dmode_get_text("ui_held");
                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, dlight_objects[i][0]);
                }
            }
            
            dmenu_box = 2;
            dbutton_layout = 1;
            break;
        
        case "armors":
            dmenu_title = scr_dmode_get_text("armor_list");
            dbutton_options = [scr_dmode_get_text("ui_chapter")];
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
                        combined += (" (" + scr_dmode_get_text("ui_equipped") + ")");
                    
                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, i);
                }
            }
            
            dmenu_box = 2;
            dbutton_layout = 1;
            break;
        
        case "weapons":
            dmenu_title = scr_dmode_get_text("weapon_list");
            dbutton_options = [scr_dmode_get_text("ui_chapter")];
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
                        combined += (" (" + scr_dmode_get_text("ui_equipped") + ")");
                    
                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, i);
                }
            }
            
            dmenu_box = 2;
            dbutton_layout = 1;
            break;
        
        case "keyitems":
            dmenu_title = scr_dmode_get_text("keyitem_list");
            dbutton_options = [scr_dmode_get_text("ui_chapter")];
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
            dmenu_title = scr_dmode_get_text("add_how_many");
            dgiver_amount = 1;
            dmenu_box = 0;
            dbutton_layout = 2;
            break;
        
        case "recruits":
            dmenu_title = scr_dmode_get_text("recruit_list");
            dbutton_options = [scr_dmode_get_text("btn_presets")];
            dbutton_indices = [scr_dmode_get_text("btn_presets")];
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
            dmenu_title = scr_dmode_get_text("recruit_presets");
            dbutton_options = [scr_dmode_get_text("btn_recruit_all"), scr_dmode_get_text("btn_lose_all")];
            
            if (dhorizontal_page)
            {
                dmenu_title += (" (" + scr_dmode_get_text("ui_chap_short") + " " + string(dhorizontal_page) + ")");
                dbutton_options[0] += " " + scr_dmode_get_text("ui_of_chapter") + " " + string(dhorizontal_page);
                dbutton_options[1] += " " + scr_dmode_get_text("ui_of_chapter") + " " + string(dhorizontal_page);
            }
            
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case "flag_categories":
            dmenu_title = scr_dmode_get_text("misc");
            dbutton_options = [];
            dbutton_indices = [-1];
            categories_len = array_length(dother_categories);
            var max_len = 40;
            
            if (!global.dreading_custom_flag)
                array_push(dbutton_options, scr_dmode_get_text("ui_custom"));
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
            dmenu_title = scr_dmode_get_text("misc");
            dbutton_options = [];
            dbutton_indices = [];
            other_len = array_length(dother_options);
            var max_len = 40;
            
            for (var i = 0; i < other_len; i++)
            {
                cur_option = dother_options[i];
                flag_number = global.flag[cur_option[2]];
                var combined = cur_option[1] + " - " + scr_dmode_get_text("ui_problem");
                
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
                array_push(dbutton_indices, i);
                name = dglobal_changer_options[i][0];
                limit = dglobal_changer_options[i][2];
                
                if (i == 0 && reading_double_flag)
                {
                    name = dcustom_flag_text[0];
                    is_good = parse_var_str(name, 0);
                    
                    if (is_good)
                    {
                        name = dtemp_text;
                        limit = dtemp_num;
                    }
                }
                
                text = name;
                
                if (i == 0 && reading_double_flag)
                    text = "global." + name;
                
                cur_global_value = "";
                var_exist = variable_global_exists(name);
                
                if (var_exist)
                    cur_global_value = variable_global_get(name);
                
                if (limit != -1)
                {
                    lookup_index = 0;
                    
                    if (i == dvertical_index && i == 0)
                        lookup_index = limit;
                    else if (i == dvertical_index)
                        lookup_index = dhorizontal_index;
                    
                    text += ("[" + string(lookup_index) + "]");
                    
                    if (typeof(cur_global_value) != "array")
                        cur_global_value = "(Not an array)";
                    else if (lookup_index >= array_length(cur_global_value))
                        cur_global_value = "(Index too high)";
                    else
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
            
            if (selected_name == scr_dmode_get_text("warps"))
            {
                dmenu_state = "warp";
                dhorizontal_index = 0;
                dkeyboard_input = "";
                drooms_options.target_plot = global.plot;
                drooms_options.target_is_darkzone = global.darkzone;
            }
            else if (selected_name == scr_dmode_get_text("items"))
            {
                dmenu_state = "give";
            }
            else if (selected_name == scr_dmode_get_text("recruits"))
            {
                dmenu_state = "recruits";
                dhorizontal_page = 0;
            }
            else if (selected_name == scr_dmode_get_text("misc"))
            {
                dmenu_state = "flag_categories";
            }
            else if (selected_name == "Globals")
            {
                dmenu_state = "globals_changer";
            }
            else if (selected_name == "Test")
            {
                dmenu_state = "test";
            }
            
            break;
        
        case "test":
            if (dvertical_index == 0)
            {
                dmenu_state = "new_debug_save";
                break;
            }
            
            if (dvertical_index == 1)
            {
                dremove_false_history();
                dmenu_skip_reindexing = true;
                global.dreading_custom_flag = 1;
                keyboard_string = "";
                dkeyboard_input = "";
                dmenu_state_update();
                break;
            }
            
            if (dmenu_interact_submenus(selected_name))
                break;
            
            if (selected_name == "- At Noelle's")
                scr_debug_print("Triggered Noelle's Route!");
            else if (selected_name == "- Jackenstein fight")
                scr_debug_print("Triggered Jackenstein!");
            else if (selected_name == "- Proceed scene")
                scr_debug_print("Triggered Proceed scene!");
            else if (selected_name == "- Titan's fight")
                scr_debug_print("Triggered Titan's fight!");
            
            break;
        
        case "new_debug_save":
            if (dvertical_index == 0)
            {
                dremove_false_history();
                dmenu_skip_reindexing = true;
                global.dreading_custom_flag = 1;
                keyboard_string = "";
                dkeyboard_input = "";
                dmenu_state_update();
            }
            else if (dvertical_index == 1 && dhorizontal_index == 0)
            {
                if (dkeyboard_input != "")
                    global.debug_save_name = dkeyboard_input;
                else
                    global.debug_save_name = "Untitled";
                
                dkeyboard_input = "";
                scr_debug_print("Save created: " + string(global.debug_save_name));
                global.debug_saving = 1;
                scr_debug_save();
                dmenu_popup_launch = 0;
                dmenu_state = "debug";
                dbutton_layout = 0;
                dbutton_options = dbutton_options_original;
                dmenu_state_history = [];
                dmenu_vertical_index_history = [];
                dvertical_index = 0;
                dmenu_active = false;
                global.interact = 0;
                snd_play(snd_save);
            }
            else
            {
                dmenu_popup_launch = 0;
                dmenu_state = "debug";
                dbutton_layout = 0;
                dbutton_options = dbutton_options_original;
                dmenu_state_history = [];
                dmenu_vertical_index_history = [];
                dvertical_index = 0;
                dmenu_active = false;
                global.interact = 0;
            }
            
            break;
        
        case "debug_load":
            if (dvertical_index == 0)
            {
                dremove_false_history();
                dmenu_skip_reindexing = true;
                global.dreading_custom_flag = 1;
                keyboard_string = "";
                dkeyboard_input = "";
                dmenu_state_update();
            }
            else
            {
                dload_options.target_save = -1;
                
                if (dvertical_index != 0)
                    dload_options.target_save = dbutton_indices[dvertical_index];
                
                dload_options.target_with_cur_inv = global.dload_cur_inv;
                dmenu_state = "debug_load_options";
                dvertical_index = 1;
                dkeyboard_input = "";
            }
            
            break;
        
        case "debug_load_options":
            if (dvertical_index == 0)
            {
                dremove_false_history();
                dmenu_skip_reindexing = true;
                dload_options.target_with_cur_inv ^= 1;
            }
            else if (dvertical_index == 1)
            {
                var chosen_save_id = dload_options.target_save;
                dmenu_popup_launch = 0;
                dmenu_state = "debug";
                dbutton_options = dbutton_options_original;
                dmenu_state_history = [];
                dmenu_vertical_index_history = [];
                dvertical_index = 0;
                dbutton_layout = 0;
                dmenu_active = false;
                dkeyboard_input = "";
                global.dload_cur_inv = dload_options.target_with_cur_inv;
                global.interact = 0;
                scr_debug_load(chosen_save_id);
            }
            
            break;
        
        case "warp":
            if (dvertical_index == 1)
            {
                dremove_false_history();
                dmenu_skip_reindexing = true;
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
                dremove_false_history();
                dmenu_skip_reindexing = true;
                drooms_options.target_is_darkzone ^= 1;
            }
            else if (dvertical_index >= 2 && dvertical_index <= 4)
            {
                dremove_false_history();
                dmenu_skip_reindexing = true;
                
                if (dvertical_index == 2)
                {
                    global.dreading_custom_flag = 1;
                    keyboard_string = "";
                }
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
            if (dhorizontal_index == 0)
                dmenu_state = "objects";
            else if (dhorizontal_index == 1)
                dmenu_state = "armors";
            else if (dhorizontal_index == 2)
                dmenu_state = "weapons";
            else if (dhorizontal_index == 3)
                dmenu_state = "keyitems";
            
            dhorizontal_page = !global.darkzone;
            
            if (dhorizontal_index == 3)
                dhorizontal_page = 0;
            
            dvertical_index = 0;
            dhorizontal_index = 0;
            break;
        
        case "objects":
            if (dhorizontal_page == 0 && dvertical_index == 0)
            {
                dremove_false_history();
                dmenu_skip_reindexing = true;
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
                dremove_false_history();
                dmenu_skip_reindexing = true;
                global.larmor = dlight_armors[dvertical_index][0];
                break;
            }
            
            if (dhorizontal_page == 0 && dvertical_index == 0)
            {
                dremove_false_history();
                dmenu_skip_reindexing = true;
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
                dremove_false_history();
                dmenu_skip_reindexing = true;
                global.lweapon = dlight_weapons[dvertical_index][0];
                break;
            }
            
            if (dhorizontal_page == 0 && dvertical_index == 0)
            {
                dremove_false_history();
                dmenu_skip_reindexing = true;
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
                dremove_false_history();
                dmenu_skip_reindexing = true;
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
            dremove_false_history();
            
            if (dgiver_amount == 0)
            {
                scr_debug_print(scr_dmode_get_text("msg_cancelled"));
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
                    scr_debug_print(string(abs(dgiver_amount)) + " " + dgiver_bname + scr_dmode_get_text("msg_removed_inv"));
                else
                    scr_debug_print(string(dgiver_amount) + " " + dgiver_bname + scr_dmode_get_text("msg_added_inv"));
            }
            
            if (dgiver_menu_state == "armors")
            {
                if (dgiver_amount > 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];
                    
                    for (var i = 0; i < dgiver_amount; i++)
                        scr_armorget(real_index);
                    
                    scr_debug_print(string(dgiver_amount) + " " + dgiver_bname + scr_dmode_get_text("msg_added_inv"));
                }
                else if (dgiver_amount < 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];
                    
                    for (var i = 0; i < abs(dgiver_amount); i++)
                        scr_armorremove(real_index);
                    
                    scr_debug_print(string(abs(dgiver_amount)) + " " + dgiver_bname + scr_dmode_get_text("msg_removed_inv"));
                }
            }
            
            if (dgiver_menu_state == "weapons")
            {
                if (dgiver_amount > 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];
                    
                    for (var i = 0; i < dgiver_amount; i++)
                        scr_weaponget(real_index);
                    
                    scr_debug_print(string(dgiver_amount) + " " + dgiver_bname + scr_dmode_get_text("msg_added_inv"));
                }
                else if (dgiver_amount < 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];
                    
                    for (var i = 0; i < abs(dgiver_amount); i++)
                        scr_weaponremove(real_index);
                    
                    scr_debug_print(string(abs(dgiver_amount)) + " " + dgiver_bname + scr_dmode_get_text("msg_removed_inv"));
                }
            }
            
            if (dgiver_menu_state == "keyitems")
            {
                if (dgiver_amount > 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];
                    
                    for (var i = 0; i < dgiver_amount; i++)
                        scr_keyitemget(real_index);
                    
                    scr_debug_print(string(dgiver_amount) + " " + dgiver_bname + scr_dmode_get_text("msg_added_inv"));
                }
                else if (dgiver_amount < 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];
                    
                    for (var i = 0; i < abs(dgiver_amount); i++)
                        scr_keyitemremove(real_index);
                    
                    scr_debug_print(string(abs(dgiver_amount)) + " " + dgiver_bname + scr_dmode_get_text("msg_removed_inv"));
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
            else
            {
                dremove_false_history();
                dmenu_skip_reindexing = true;
                global.dreading_custom_flag = 1;
                dhorizontal_index = 0;
                keyboard_string = "";
            }
            
            break;
        
        case "flag_misc":
            dremove_false_history();
            dmenu_skip_reindexing = true;
            break;
        
        case "recruits":
            dremove_false_history();
            
            if (dvertical_index != 0)
                dmenu_skip_reindexing = true;
            
            if (dvertical_index == 0)
                dmenu_state = "recruit_presets";
            
            break;
        
        case "recruit_presets":
            dremove_false_history();
            dmenu_skip_reindexing = true;
            
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
            dremove_false_history();
            dmenu_skip_reindexing = true;
            
            if (global.dreading_custom_flag)
            {
                var value = 0;
                cur_global_array = dglobal_changer_options[dvertical_index];
                reading_double_flag = dvertical_index == 0;
                glob_name = cur_global_array[0];
                glob_index = -1;
                
                if (reading_double_flag)
                {
                    parse_var_str(dcustom_flag_text[0], 1);
                    glob_name = dtemp_text;
                    glob_index = dtemp_num;
                }
                else if (cur_global_array[2] != -1)
                {
                    glob_index = dhorizontal_index;
                }
                
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
                
                if (glob_index != -1)
                {
                    base = variable_global_get(glob_name);
                    base[glob_index] = value;
                    value = base;
                }
                
                variable_global_set(glob_name, value);
                scr_debug_print("Changed global." + string(glob_name) + " to |" + string(value) + "|");
            }
            else if (dvertical_index == 0)
            {
                dhorizontal_index = 0;
            }
            
            set_keyboard_reader(global.dreading_custom_flag ^ 1);
            break;
        
        default:
            snd_play(snd_error);
            scr_debug_print(scr_dmode_get_text("msg_invalid"));
    }
}
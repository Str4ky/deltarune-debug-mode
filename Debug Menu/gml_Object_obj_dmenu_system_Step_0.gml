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
            
            if (dmenu_state == "flag_misc")
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
        dcustom_flag_text = ["", ""];
        return 0;
    }
    
    for (c = 1; c <= string_length(dcustom_flag_text[0]); c++)
    {
        if (!scr_84_is_digit(string_char_at(dcustom_flag_text[0], c)))
        {
            scr_debug_print("Invalid flag |" + dcustom_flag_text[0] + "| because of |" + string_char_at(dcustom_flag_text[0], c) + "|");
            proper_exit = 0;
            break;
        }
    }
    
    if (string_length(dcustom_flag_text[0]) == 0)
    {
        scr_debug_print("Empty flag");
        proper_exit = 0;
    }
    
    if (dmenu_state == "warp_options")
        return proper_exit;
    
    for (c = 1; c <= string_length(dcustom_flag_text[1]); c++)
    {
        if (!scr_84_is_digit(string_char_at(dcustom_flag_text[1], c)) && string_char_at(dcustom_flag_text[1], c) != ".")
        {
            scr_debug_print("Invalid value |" + dcustom_flag_text[1] + "|");
            proper_exit = 0;
            break;
        }
    }
    
    if (string_length(dcustom_flag_text[1]) == 0)
    {
        if (proper_exit)
            scr_debug_print("global.flag[" + string(real(dcustom_flag_text[0])) + "] = |" + string(global.flag[real(dcustom_flag_text[0])]) + "|");
        else
            scr_debug_print("Empty value");
        
        proper_exit = 0;
    }
    
    if (proper_exit)
    {
        scr_debug_print("Updated global.flag[" + string(real(dcustom_flag_text[0])) + "] from |" + string(global.flag[real(dcustom_flag_text[0])]) + "| to |" + dcustom_flag_text[1] + "|");
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
    
    if (dmenu_state == "warp" || dmenu_state == "warp_options")
        dkeyboard_input = dcustom_flag_text[0];
    
    will_exit = keyboard_check_pressed(vk_escape) || keyboard_check_pressed(global.input_k[4]) || keyboard_check_pressed(global.input_k[7]);
    will_exit |= ((dmenu_state == "warp_options" || dmenu_state == "warp") && (keyboard_check_pressed(vk_up) || keyboard_check_pressed(vk_down)));
    
    if (will_exit)
    {
        clean_exit = !keyboard_check_pressed(vk_escape);
        
        if (dmenu_state == "flag_categories" || dmenu_state == "warp_options")
        {
            flags_good = evaluate_custom_flag(clean_exit);
            
            if (flags_good && dmenu_state == "warp_options")
                drooms_options.target_plot = real(dkeyboard_input);
            
            snd_play(array_get([299, 420], flags_good));
        }
        
        if (dmenu_state == "warp" || dmenu_state == "warp_options")
        {
            if (keyboard_check_pressed(vk_down))
                vmove_menu(0, 1);
            else if (keyboard_check_pressed(vk_up))
                vmove_menu(1, 0);
            
            if (dmenu_state == "warp")
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
            dkeyboard_input = "";
        
        dcustom_flag_text = ["", ""];
        will_exit = 1;
    }
    else if (dmenu_state == "flag_categories" && keyboard_check_pressed(vk_left) && dhorizontal_index != 0)
    {
        snd_play(snd_menumove);
        dhorizontal_index--;
    }
    else if (dmenu_state == "flag_categories" && keyboard_check_pressed(vk_right) && dhorizontal_index != 1)
    {
        snd_play(snd_menumove);
        dhorizontal_index++;
    }
    else if (keyboard_check_pressed(vk_backspace))
    {
        dcustom_flag_text[dhorizontal_index] = string_delete(dcustom_flag_text[dhorizontal_index], string_length(dcustom_flag_text[dhorizontal_index]), 1);
        keyboard_string = "";
    }
    else if (keyboard_check_pressed(vk_anykey))
    {
        dcustom_flag_text[dhorizontal_index] += string(keyboard_string);
        keyboard_string = "";
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
        
        if (dmenu_state == "flag_misc")
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
                scr_debug_print("Updated global.flag[" + string(cur_options[2]) + "] to |" + string(cur_options[3][dhorizontal_index][1]) + "|");
                snd_play(snd_menumove);
            }
        }
        
        pressed_right = dmenu_pressed_key(39);
        pressed_left = dmenu_pressed_key(37);
        
        if (pressed_left && pressed_right)
            pressed_right = 0;
        
        if (pressed_right || pressed_left)
        {
            if (dmenu_state == "recruits")
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
            else if (dmenu_state == "warp_options" && (dbutton_selected == 4 || dbutton_selected == 5))
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
            else if ((dmenu_state == "objects" || dmenu_state == "weapons" || dmenu_state == "armors") && (pressed_left + pressed_right) == 1)
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
                case "objects":
                    if (dhorizontal_page == 0)
                        scr_itemcheck(dbutton_indices[dgiver_button_selected - 1]);
                    else
                        scr_litemcheck(dbutton_indices[dgiver_button_selected - 1]);
                    
                    owned_count = itemcount;
                    break;
                
                case "armors":
                    scr_armorcheck_inventory(dbutton_indices[dgiver_button_selected - 1]);
                    owned_count = itemcount;
                    break;
                
                case "weapons":
                    scr_weaponcheck_inventory(dbutton_indices[dgiver_button_selected - 1]);
                    owned_count = itemcount;
                    break;
                
                case "keyitems":
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
                case "objects":
                    if (dhorizontal_page == 0)
                        scr_itemcheck(0);
                    else
                        scr_litemcheck(0);
                    
                    owned_count = itemcount;
                    break;
                
                case "armors":
                    scr_armorcheck_inventory(0);
                    owned_count = itemcount;
                    break;
                
                case "weapons":
                    scr_weaponcheck_inventory(0);
                    owned_count = itemcount;
                    break;
                
                case "keyitems":
                    scr_keyitemcheck(0);
                    owned_count = itemcount;
                    break;
                
                case "recruits":
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
        must_save = dmenu_state != "givertab" && dmenu_state != "recruit_presets" && dmenu_state != "flag_misc" && dmenu_state != "warp_options" && !(dmenu_state == "warp" && dbutton_selected == 2);
        must_save &= ((dmenu_state != "flag_categories" || dbutton_selected != 1) && (!(dmenu_state == "weapons" && dhorizontal_page) && !(dmenu_state == "armors" && dhorizontal_page)));
        must_save &= (dmenu_state != "recruits" || dbutton_selected == 1);
        must_save &= !(scr_array_contains(ditem_types, dmenu_state) && dhorizontal_page == 0 && dbutton_selected == 1);
        snd_play(snd_select);
        
        if (must_save)
        {
            array_push(dmenu_state_history, dmenu_state);
            array_push(dbutton_selected_history, dbutton_selected);
        }
        
        if (dmenu_state == "flag_categories" && dbutton_selected == 1)
        {
            global.dreading_custom_flag = 1;
            dhorizontal_index = 0;
            keyboard_string = "";
        }
        
        if (scr_array_contains(ditem_types, dmenu_state))
        {
            switch (dmenu_state)
            {
                case "objects":
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
                        
                        scr_debug_print(dgiver_bname + " sélectionné !");
                    }
                    
                    break;
                
                case "armors":
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
                        
                        scr_debug_print(string(dgiver_bname) + " sélectionné !");
                    }
                    
                    break;
                
                case "weapons":
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
                        
                        scr_debug_print(string(dgiver_bname) + " sélectionné !");
                    }
                    
                    break;
                
                case "keyitems":
                    if (dbutton_selected > 1)
                    {
                        real_index = dbutton_indices[dbutton_selected - 1];
                        scr_keyiteminfo(real_index);
                        dgiver_bname = tempkeyitemname;
                        scr_debug_print(string(dgiver_bname) + " sélectionné !");
                    }
                    
                    break;
            }
        }
        else if (dmenu_state == "warp" && dbutton_selected == 2)
        {
            scr_debug_print("Recherche sélectionné !");
        }
        else if (dmenu_state != "givertab" && dmenu_state != "flag_misc" && dmenu_state != "warp_options" && (dmenu_state != "recruits" || dbutton_selected == 1))
        {
            scr_debug_print(string(dbutton_options[dbutton_selected - 1]) + " sélectionné !");
        }
        
        if ((dmenu_state == "recruits" && dbutton_selected != 1) || dmenu_state == "warp_options" || dmenu_state == "recruit_presets" || dmenu_state == "warp_options" || dmenu_state == "flag_misc" || ((dmenu_state == "armors" || dmenu_state == "weapons") && dhorizontal_page) || (dmenu_state == "warp" && dbutton_selected == 2))
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
}

if ((dmenu_active == 1 && dmenu_state == "debug" && global.darkzone == 1) || dkeys_helper == 1)
{
    if (keyboard_check_pressed(ord("M")))
    {
        if (dkeys_helper == 0)
            snd_play(snd_select);
        else
            snd_play(snd_smallswing);
        
        dkeys_helper = !dkeys_helper;
    }
}
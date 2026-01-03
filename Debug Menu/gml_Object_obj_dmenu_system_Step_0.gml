global.dmenu_arrow_timer += 1;

if (global.dmenu_active)
{
    if (global.dbutton_layout == 0 && global.dkeys_helper == 0)
    {
        if (keyboard_check_pressed(vk_left))
        {
            global.dbutton_selected -= 1;
            
            if (global.dbutton_selected < 1)
                global.dbutton_selected = array_length(global.dbutton_options);
            
            snd_play(snd_menumove);
        }
        
        if (keyboard_check_pressed(vk_right))
        {
            global.dbutton_selected = (global.dbutton_selected % array_length(global.dbutton_options)) + 1;
            snd_play(snd_menumove);
        }
    }
    
    if (global.dbutton_layout == 1)
    {
        if (keyboard_check_pressed(vk_down))
        {
            snd_play(snd_menumove);
            
            if (global.dbutton_selected < array_length(global.dbutton_options))
            {
                global.dbutton_selected += 1;
                
                if (global.dbutton_selected > (global.dmenu_start_index + global.dbutton_max_visible))
                    global.dmenu_start_index += 1;
            }
        }
        
        if (keyboard_check_pressed(vk_up))
        {
            snd_play(snd_menumove);
            
            if (global.dbutton_selected > 1)
            {
                global.dbutton_selected -= 1;
                
                if (global.dbutton_selected < (global.dmenu_start_index + 1))
                    global.dmenu_start_index -= 1;
            }
        }
        
        if (keyboard_check_pressed(vk_right))
        {
            if (global.dmenu_state == "recruits")
            {
                if (global.dbutton_selected != 1)
                {
                    real_index = global.dbutton_indices[global.dbutton_selected - 1];
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
            }
        }
        
        if (keyboard_check_pressed(vk_left))
        {
            if (global.dmenu_state == "recruits")
            {
                if (global.dbutton_selected != 1)
                {
                    real_index = global.dbutton_indices[global.dbutton_selected - 1];
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
            }
        }
        
        if (keyboard_check(vk_down))
        {
            if (global.dscroll_down_timer >= global.dscroll_delay)
            {
                if ((global.dscroll_down_timer % global.dscroll_speed) == 0)
                {
                    if (global.dbutton_selected < array_length(global.dbutton_options))
                    {
                        global.dbutton_selected += 1;
                        snd_play(snd_menumove);
                        
                        if (global.dbutton_selected > (global.dmenu_start_index + global.dbutton_max_visible))
                            global.dmenu_start_index += 1;
                    }
                }
            }
            
            global.dscroll_down_timer += 1;
        }
        else
        {
            global.dscroll_down_timer = 0;
        }
        
        if (keyboard_check(vk_up))
        {
            if (global.dscroll_up_timer >= global.dscroll_delay)
            {
                if ((global.dscroll_up_timer % global.dscroll_speed) == 0)
                {
                    if (global.dbutton_selected > 1)
                    {
                        global.dbutton_selected -= 1;
                        snd_play(snd_menumove);
                        
                        if (global.dbutton_selected < (global.dmenu_start_index + 1))
                            global.dmenu_start_index -= 1;
                    }
                }
            }
            
            global.dscroll_up_timer += 1;
        }
        else
        {
            global.dscroll_up_timer = 0;
        }
        
        global.dmenu_start_index = clamp(global.dmenu_start_index, 0, max(0, array_length(global.dbutton_options) - global.dbutton_max_visible));
    }
    
    if (global.dbutton_layout == 2)
    {
        if (keyboard_check_pressed(vk_left))
        {
            var owned_count = 0;
            
            switch (global.dgiver_menu_state)
            {
                case "objects":
                    scr_itemcheck(global.dbutton_indices[global.dgiver_button_selected - 1]);
                    owned_count = itemcount;
                    break;
                
                case "armors":
                    scr_armorcheck_inventory(global.dbutton_indices[global.dgiver_button_selected - 1]);
                    owned_count = itemcount;
                    break;
                
                case "weapons":
                    scr_weaponcheck_inventory(global.dbutton_indices[global.dgiver_button_selected - 1]);
                    owned_count = itemcount;
                    break;
                
                case "keyitems":
                    scr_keyitemcheck(global.dbutton_indices[global.dgiver_button_selected - 1]);
                    owned_count = itemcount;
                    break;
                
                default:
                    owned_count = 0;
            }
            
            if (global.dgiver_amount > -owned_count)
            {
                global.dgiver_amount -= 1;
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
            
            switch (global.dgiver_menu_state)
            {
                case "objects":
                    scr_itemcheck(0);
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
                    real_indice = global.dbutton_indices[global.dbutton_selected - 1];
                    recruited_nbr = global.flag[real_indice + 600];
                    global.flag[real_indice + 600] = recruited_nbr + 1;
                    break;
                
                default:
                    owned_count = 0;
            }
            
            if (global.dgiver_amount < owned_count)
            {
                global.dgiver_amount += 1;
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
        
        if (global.dmenu_state != "givertab" && global.dmenu_state != "recruit_presets" && (global.dmenu_state != "recruits" || global.dbutton_selected == 1))
        {
            array_push(global.dmenu_state_history, global.dmenu_state);
            array_push(global.dbutton_selected_history, global.dbutton_selected);
        }
        
        if (global.dmenu_state == "objects" || global.dmenu_state == "armors" || global.dmenu_state == "weapons" || global.dmenu_state == "keyitems")
        {
            switch (global.dmenu_state)
            {
                case "objects":
                    real_index = global.dbutton_indices[global.dbutton_selected - 1];
                    scr_iteminfo(real_index);
                    global.dgiver_bname = itemnameb;
                    scr_debug_print(global.dgiver_bname + " sélectionné !");
                    break;
                
                case "armors":
                    real_index = global.dbutton_indices[global.dbutton_selected - 1];
                    scr_armorinfo(real_index);
                    global.dgiver_bname = armornametemp;
                    scr_debug_print(string(global.dgiver_bname) + " sélectionné !");
                    break;
                
                case "weapons":
                    real_index = global.dbutton_indices[global.dbutton_selected - 1];
                    scr_weaponinfo(real_index);
                    global.dgiver_bname = weaponnametemp;
                    scr_debug_print(string(global.dgiver_bname) + " sélectionné !");
                    break;
                
                case "keyitems":
                    real_index = global.dbutton_indices[global.dbutton_selected - 1];
                    scr_keyiteminfo(real_index);
                    global.dgiver_bname = tempkeyitemname;
                    scr_debug_print(string(global.dgiver_bname) + " sélectionné !");
                    break;
            }
        }
        else if (global.dmenu_state != "givertab" && (global.dmenu_state != "recruits" || global.dbutton_selected == 1))
        {
            scr_debug_print(string(global.dbutton_options[global.dbutton_selected - 1]) + " sélectionné !");
        }
        
        if ((global.dmenu_state == "recruits" && global.dbutton_selected != 1) || global.dmenu_state == "recruit_presets")
        {
            dmenu_state_interact();
            dmenu_state_update();
        }
        else
        {
            dmenu_state_interact();
            global.dmenu_start_index = 0;
            global.dbutton_selected = 1;
            dmenu_state_update();
        }
    }
    
    if (keyboard_check_pressed(global.input_k[5]) || keyboard_check_pressed(global.input_k[8]))
    {
        snd_play(snd_smallswing);
        
        if (array_length(global.dmenu_state_history) > 0)
        {
            global.dmenu_state = global.dmenu_state_history[array_length(global.dmenu_state_history) - 1];
            array_resize(global.dmenu_state_history, array_length(global.dmenu_state_history) - 1);
        }
        else
        {
            global.dmenu_active = !global.dmenu_active;
            global.dmenu_state_history = [];
            global.dbutton_selected_history = [];
            global.interact = 0;
        }
        
        if (array_length(global.dbutton_selected_history) > 0)
        {
            global.dbutton_selected = global.dbutton_selected_history[array_length(global.dbutton_selected_history) - 1];
            array_resize(global.dbutton_selected_history, array_length(global.dbutton_selected_history) - 1);
        }
        
        dmenu_state_update();
        global.dmenu_start_index = clamp(global.dbutton_selected - 1, 0, max(0, array_length(global.dbutton_options) - global.dbutton_max_visible));
    }
}

if (global.dmenu_active == 1 && global.dmenu_state == "debug" && global.darkzone == 1)
{
    if (keyboard_check_pressed(ord("M")))
        global.dkeys_helper = !global.dkeys_helper;
}
else if (global.dkeys_helper == 1)
{
    if (keyboard_check_pressed(ord("M")))
        global.dkeys_helper = !global.dkeys_helper;
}
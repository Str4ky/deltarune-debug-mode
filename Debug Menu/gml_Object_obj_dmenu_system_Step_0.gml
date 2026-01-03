dmenu_arrow_timer += 1;

if (dmenu_active && global.dreading_custom_flag)
{
    update_visu = 1;

    if (keyboard_check_pressed(vk_escape) || keyboard_check_pressed(vk_enter) || keyboard_check_pressed(global.input_k[4]) || keyboard_check_pressed(global.input_k[7]))
    {
        proper_exit = !keyboard_check_pressed(vk_escape);

        if (proper_exit)
        {
            global.flag[real(dcustom_flag_text[0])] = real(dcustom_flag_text[1]);
            scr_debug_print("Updated global.flag[" + string(real(dcustom_flag_text[0])) + "] to |" + dcustom_flag_text[1] + "|");
        }

        global.dreading_custom_flag = 0;
        dcustom_flag_text = ["", ""];

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

        if (dmenu_state == "flag_misc")
        {
            cur_options = dother_options[dbutton_selected - 1];
            cur_options_len = array_length(cur_options[2]);
            playsound = 1;

            if (keyboard_check_pressed(vk_right) && dhorizontal_index < (cur_options_len - 1))
                dhorizontal_index++;
            else if (keyboard_check_pressed(vk_left) && dhorizontal_index != 0)
                dhorizontal_index--;
            else
                playsound = 0;

            if (playsound)
                snd_play(snd_menumove);
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

            if (dmenu_state == "flag_misc")
            {
                new_options = dother_options[dbutton_selected - 1];
                dhorizontal_index = find_subarray_index(new_options[1], new_options[2]);
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

            if (dmenu_state == "flag_misc")
            {
                new_options = dother_options[dbutton_selected - 1];
                dhorizontal_index = find_subarray_index(new_options[1], new_options[2]);
            }
        }

        if (keyboard_check_pressed(vk_right))
        {
            if (dmenu_state == "recruits")
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
            }
        }

        if (keyboard_check_pressed(vk_left))
        {
            if (dmenu_state == "recruits")
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

                        if (dmenu_state == "flag_misc")
                        {
                            new_options = dother_options[dbutton_selected - 1];
                            dhorizontal_index = find_subarray_index(new_options[1], new_options[2]);
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

                        if (dmenu_state == "flag_misc")
                        {
                            new_options = dother_options[dbutton_selected - 1];
                            dhorizontal_index = find_subarray_index(new_options[1], new_options[2]);
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
                case "objects":
                    scr_itemcheck(dbutton_indices[dgiver_button_selected - 1]);
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

        if (dmenu_state != "givertab" && dmenu_state != "recruit_presets" && (dmenu_state != "recruits" || dbutton_selected == 1))
        {
            array_push(dmenu_state_history, dmenu_state);
            array_push(dbutton_selected_history, dbutton_selected);
        }

        if (dmenu_state == "flag_misc" && dbutton_selected == 1)
        {
            global.dreading_custom_flag = 1;
            dhorizontal_index = 0;
            keyboard_string = "";
        }

        if (dmenu_state == "objects" || dmenu_state == "armors" || dmenu_state == "weapons" || dmenu_state == "keyitems")
        {
            switch (dmenu_state)
            {
                case "objects":
                    real_index = dbutton_indices[dbutton_selected - 1];
                    scr_iteminfo(real_index);
                    dgiver_bname = itemnameb;
                    scr_debug_print(dgiver_bname + " sélectionné !");
                    break;

                case "armors":
                    real_index = dbutton_indices[dbutton_selected - 1];
                    scr_armorinfo(real_index);
                    dgiver_bname = armornametemp;
                    scr_debug_print(string(dgiver_bname) + " sélectionné !");
                    break;

                case "weapons":
                    real_index = dbutton_indices[dbutton_selected - 1];
                    scr_weaponinfo(real_index);
                    dgiver_bname = weaponnametemp;
                    scr_debug_print(string(dgiver_bname) + " sélectionné !");
                    break;

                case "keyitems":
                    real_index = dbutton_indices[dbutton_selected - 1];
                    scr_keyiteminfo(real_index);
                    dgiver_bname = tempkeyitemname;
                    scr_debug_print(string(dgiver_bname) + " sélectionné !");
                    break;
            }
        }
        else if (dmenu_state != "givertab" && (dmenu_state != "recruits" || dbutton_selected == 1) && dmenu_state != "flag_misc")
        {
            scr_debug_print(string(dbutton_options[dbutton_selected - 1]) + " sélectionné !");
        }

        if ((dmenu_state == "recruits" && dbutton_selected != 1) || dmenu_state == "recruit_presets")
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

if ((dmenu_active == 1 && dmenu_state == "debug" && global.darkzone == 1) || dkeys_helper == 1)
{
    if (keyboard_check_pressed(ord("M")))
        dkeys_helper = !dkeys_helper;
}

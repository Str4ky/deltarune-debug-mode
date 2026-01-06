if (ditemcount == 0)
{
    ditemcount = 39;
    darmorcount = 27;
    dweaponcount = 26;
    dkeyitemcount = 19;
    drecent_item = 34;
    drecent_armor = 23;
    drecent_weapon = 23;
    drecent_keyitem = 16;
}

function dmenu_state_update()
{
    switch (dmenu_state)
    {
        case "debug":
            dmenu_title = "Menu Debug";
            dbutton_options = dbutton_options_original;
            dmenu_box = 0;
            dbutton_layout = 0;
            break;

        case "warp":
            scr_debug_print("Pas encore disponible !");
            break;

        case "give":
            dmenu_title = "Type d'items";
            dbutton_options = ["Objets", "Armures", "Armes", "Obj Clés"];
            dmenu_box = 0;
            dbutton_layout = 0;
            break;

        case "objects":
            dmenu_title = "Liste d'objets";
            dbutton_options = [];
            dbutton_indices = [];
            var max_len = 40;

            if (dhorizontal_page == 0)
            {
                for (var i = drecent_item; i <= ditemcount; i++)
                {
                    scr_iteminfo(i);
                    var cleaned_desc = string_replace_all(itemdescb, "#", " ");
                    var combined = itemnameb + " - " + cleaned_desc;

                    if (string_length(combined) > max_len)
                        combined = string_copy(combined, 1, max_len - 3) + "...";

                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, i);
                }

                var cutoff = min(drecent_item - 1, ditemcount);

                for (var i = 1; i <= cutoff; i++)
                {
                    scr_iteminfo(i);
                    var cleaned_desc = string_replace_all(itemdescb, "#", " ");
                    var combined = itemnameb + " - " + cleaned_desc;

                    if (string_length(combined) > max_len)
                        combined = string_copy(combined, 1, max_len - 3) + "...";

                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, i);
                }
            }
            else
            {
                for (var i = 0; i < array_length(dlight_objects); i++)
                {
                    scr_litemcheck(dlight_objects[i][0]);
                    var combined = dlight_objects[i][1] + " - " + string(itemcount) + " held";
                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, dlight_objects[i][0]);
                }
            }

            dmenu_box = 2;
            dbutton_layout = 1;
            break;

        case "armors":
            dmenu_title = "Liste d'armures";
            dbutton_options = [];
            dbutton_indices = [];
            var max_len = 40;

            if (dhorizontal_page == 0)
            {
                for (var i = drecent_armor; i <= darmorcount; i++)
                {
                    scr_armorinfo(i);
                    var cleaned_desc = string_replace_all(armordesctemp, "#", " ");
                    var combined = armornametemp + " - " + cleaned_desc;

                    if (string_length(combined) > max_len)
                        combined = string_copy(combined, 1, max_len - 3) + "...";

                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, i);
                }

                var cutoff = min(drecent_armor - 1, darmorcount);

                for (var i = 1; i <= cutoff; i++)
                {
                    scr_armorinfo(i);
                    var cleaned_desc = string_replace_all(armordesctemp, "#", " ");
                    var combined = armornametemp + " - " + cleaned_desc;

                    if (string_length(combined) > max_len)
                        combined = string_copy(combined, 1, max_len - 3) + "...";

                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, i);
                }
            }
            else
            {
                for (var i = 0; i < array_length(dlight_armors); i++)
                {
                    var combined = dlight_armors[i][1];

                    if (global.larmor == dlight_armors[i][0])
                        combined += " (Equiped)";

                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, i);
                }
            }

            dmenu_box = 2;
            dbutton_layout = 1;
            break;

        case "weapons":
            dmenu_title = "Liste d'armes";
            dbutton_options = [];
            dbutton_indices = [];
            var max_len = 40;

            if (dhorizontal_page == 0)
            {
                for (var i = drecent_weapon; i <= dweaponcount; i++)
                {
                    scr_weaponinfo(i);
                    var cleaned_desc = string_replace_all(weapondesctemp, "#", " ");
                    var combined = weaponnametemp + " - " + cleaned_desc;

                    if (string_length(combined) > max_len)
                        combined = string_copy(combined, 1, max_len - 3) + "...";

                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, i);
                }

                var cutoff = min(drecent_weapon - 1, dweaponcount);

                for (var i = 1; i <= cutoff; i++)
                {
                    scr_weaponinfo(i);
                    var cleaned_desc = string_replace_all(weapondesctemp, "#", " ");
                    var combined = weaponnametemp + " - " + cleaned_desc;

                    if (string_length(combined) > max_len)
                        combined = string_copy(combined, 1, max_len - 3) + "...";

                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, i);
                }
            }
            else
            {
                for (var i = 0; i < array_length(dlight_weapons); i++)
                {
                    var combined = dlight_weapons[i][1];

                    if (global.lweapon == dlight_weapons[i][0])
                        combined += " (Equiped)";

                    array_push(dbutton_options, combined);
                    array_push(dbutton_indices, i);
                }
            }

            dmenu_box = 2;
            dbutton_layout = 1;
            break;

        case "keyitems":
            dmenu_title = "Liste d'objets clés (Peut briser le jeu)";
            dbutton_options = [];
            dbutton_indices = [];
            var max_len = 40;

            for (var i = drecent_keyitem; i <= dkeyitemcount; i++)
            {
                scr_keyiteminfo(i);
                var cleaned_desc = string_replace_all(tempkeyitemdesc, "#", " ");
                var combined = tempkeyitemname + " - " + cleaned_desc;

                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + "...";

                array_push(dbutton_options, combined);
                array_push(dbutton_indices, i);
            }

            var cutoff = min(drecent_keyitem - 1, dkeyitemcount);

            for (var i = 1; i <= cutoff; i++)
            {
                scr_keyiteminfo(i);
                var cleaned_desc = string_replace_all(tempkeyitemdesc, "#", " ");
                var combined = tempkeyitemname + " - " + cleaned_desc;

                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + "...";

                array_push(dbutton_options, combined);
                array_push(dbutton_indices, i);
            }

            dmenu_box = 2;
            dbutton_layout = 1;
            break;

        case "givertab":
            dmenu_title = "Ajouter combien à l'inventaire ?";
            dgiver_amount = 1;
            dmenu_box = 0;
            dbutton_layout = 2;
            break;

        case "recruits":
            dmenu_title = "Liste des recrues";
            dbutton_options = ["Préréglages"];
            dbutton_indices = ["Préréglages"];
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

            dmenu_box = 1;
            dbutton_layout = 1;
            break;

        case "recruit_presets":
            dmenu_title = "Préréglages des recrues";
            dbutton_options = ["Recruter tous", "Perdre tous"];

            if (dhorizontal_page)
            {
                dmenu_title += (" (chap " + string(dhorizontal_page) + ")");
                dbutton_options[0] += " le chapitre " + string(dhorizontal_page);
                dbutton_options[1] += " le chapitre " + string(dhorizontal_page);
            }

            dmenu_box = 0;
            dbutton_layout = 1;
            break;

        case "flag_categories":
            dmenu_title = "Divers";
            dbutton_options = [];
            dbutton_indices = [-1];
            categories_len = array_length(dother_categories);
            var max_len = 40;

            if (!global.dreading_custom_flag)
                array_push(dbutton_options, "Custom");
            else
                array_push(dbutton_options, "global.flag[" + dcustom_flag_text[0] + "] = |" + dcustom_flag_text[1] + "|");

            for (var i = 0; i < categories_len; i++)
            {
                if (dflag_categories_len[i] == 0)
                    continue;

                array_push(dbutton_options, dother_categories[i]);
                array_push(dbutton_indices, i);
            }

            dmenu_box = 2;
            dbutton_layout = 1;
            break;

        case "flag_misc":
            dmenu_title = "Divers";
            dbutton_options = [];
            dbutton_indices = [];
            other_len = array_length(dother_options);
            var max_len = 40;

            for (var i = 0; i < other_len; i++)
            {
                cur_option = dother_options[i];
                flag_number = global.flag[cur_option[2]];
                var combined = cur_option[1] + " - problem lol";

                if (i == (dbutton_selected - 1))
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

        default:
            dmenu_title = "Inconnu";
            dbutton_options = [];
            dmenu_box = 0;
            dbutton_layout = 0;
    }
}

function dmenu_state_interact()
{
    switch (dmenu_state)
    {
        case "debug":
            if (dbutton_selected == 1)
            {
                dmenu_state = "warp";
                dbutton_selected = 1;
            }

            if (dbutton_selected == 2)
            {
                dmenu_state = "give";
                dbutton_selected = 1;
            }

            if (dbutton_selected == 3)
            {
                dmenu_state = "flag_categories";
                dbutton_selected = 1;
            }

            break;

        case "warp":
            if (dbutton_selected == 1)
            {
                dmenu_state = "lightworld";
                dbutton_selected = 1;
            }

            if (dbutton_selected == 2)
            {
                dmenu_state = "darkworld";
                dbutton_selected = 1;
            }

            if (dbutton_selected == 3)
            {
                dmenu_state = "battles";
                dbutton_selected = 1;
            }

            break;

        case "give":
            if (dbutton_selected == 1)
                dmenu_state = "objects";
            else if (dbutton_selected == 2)
                dmenu_state = "armors";
            else if (dbutton_selected == 3)
                dmenu_state = "weapons";
            else if (dbutton_selected == 4)
                dmenu_state = "keyitems";

            dhorizontal_page = !global.darkzone;

            if (dbutton_selected == 4)
                dhorizontal_page = 0;

            dbutton_selected = 1;
            break;

        case "objects":
            dgiver_menu_state = dmenu_state;
            dbutton_selected = clamp(dbutton_selected, 0, array_length(dbutton_options) - 1);
            dgiver_button_selected = dbutton_selected;
            dmenu_state = "givertab";
            dbutton_selected = 1;
            break;

        case "armors":
            if (dhorizontal_page == 1)
            {
                global.larmor = dlight_armors[dbutton_selected - 1][0];
                break;
            }

            dgiver_menu_state = dmenu_state;
            dgiver_button_selected = dbutton_selected;
            dmenu_state = "givertab";
            dbutton_selected = 1;
            break;

        case "weapons":
            if (dhorizontal_page == 1)
            {
                global.lweapon = dlight_weapons[dbutton_selected - 1][0];
                break;
            }

            dgiver_menu_state = dmenu_state;
            dgiver_button_selected = dbutton_selected;
            dmenu_state = "givertab";
            dbutton_selected = 1;
            break;

        case "keyitems":
            dgiver_menu_state = dmenu_state;
            dgiver_button_selected = dbutton_selected;
            dmenu_state = "givertab";
            dbutton_selected = 1;
            break;

        case "givertab":
            if (dgiver_menu_state == "objects")
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
                        scr_debug_print(string(abs(dgiver_amount)) + " " + dgiver_bname + " retiré de l'inventaire");
                    else
                        scr_debug_print(string(dgiver_amount) + " " + dgiver_bname + " ajouté à l'inventaire");
                }
                else
                {
                    scr_debug_print("Annulé");
                }
            }

            if (dgiver_menu_state == "armors")
            {
                if (dgiver_amount > 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];

                    for (var i = 0; i < dgiver_amount; i++)
                        scr_armorget(real_index);

                    scr_debug_print(string(dgiver_amount) + " " + dgiver_bname + " ajouté à l'inventaire");
                }
                else if (dgiver_amount < 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];

                    for (var i = 0; i < abs(dgiver_amount); i++)
                        scr_armorremove(real_index);

                    scr_debug_print(string(abs(dgiver_amount)) + " " + dgiver_bname + " retiré de l'inventaire");
                }
                else
                {
                    scr_debug_print("Annulé");
                }
            }

            if (dgiver_menu_state == "weapons")
            {
                if (dgiver_amount > 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];

                    for (var i = 0; i < dgiver_amount; i++)
                        scr_weaponget(real_index);

                    scr_debug_print(string(dgiver_amount) + " " + dgiver_bname + " ajouté à l'inventaire");
                }
                else if (dgiver_amount < 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];

                    for (var i = 0; i < abs(dgiver_amount); i++)
                        scr_weaponremove(real_index);

                    scr_debug_print(string(abs(dgiver_amount)) + " " + dgiver_bname + " retiré de l'inventaire");
                }
                else
                {
                    scr_debug_print("Annulé");
                }
            }

            if (dgiver_menu_state == "keyitems")
            {
                if (dgiver_amount > 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];

                    for (var i = 0; i < dgiver_amount; i++)
                        scr_keyitemget(real_index);

                    scr_debug_print(string(dgiver_amount) + " " + dgiver_bname + " ajouté à l'inventaire");
                }
                else if (dgiver_amount < 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];

                    for (var i = 0; i < abs(dgiver_amount); i++)
                        scr_keyitemremove(real_index);

                    scr_debug_print(string(abs(dgiver_amount)) + " " + dgiver_bname + " retiré de l'inventaire");
                }
                else
                {
                    scr_debug_print("Annulé");
                }
            }

            dmenu_active = false;
            global.interact = 0;
            break;

        case "flag_categories":
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
                dmenu_state = "flag_misc";
                dbutton_selected = 1;
            }

            break;

        case "flag_misc":
            break;

        case "recruits":
            if (dbutton_selected == 1)
            {
                dmenu_state = "recruit_presets";
                dbutton_selected = 1;
            }

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
            scr_debug_print("Sélection invalide !");
    }
}

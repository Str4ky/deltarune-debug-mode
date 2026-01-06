if (ditemcount == 0)
{
    ditemcount = 15;
    darmorcount = 7;
    dweaponcount = 10;
    dkeyitemcount = 7;
    drecent_item = 1;
    drecent_armor = 1;
    drecent_weapon = 1;
    drecent_keyitem = 1;
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
            dmenu_title = "Menu de sauts";
            dbutton_options = ["Lightworld", "Darkworld", "Combats"];
            dmenu_box = 0;
            dbutton_layout = 0;
            break;

        case "lightworld":
            dmenu_title = "Menu de sauts - Lightworld";
            dbutton_options = ["Couloir de Kris", "Hall de l'école", "Placard de l'école", "Après Darkworld"];
            dmenu_box = 1;
            dbutton_layout = 1;
            break;

        case "kris_hallway":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;

        case "school_hallway":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;

        case "school_closet":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;

        case "after_darkworld":
            dmenu_title = "Options de saut";
            dbutton_options = ["Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;

        case "darkworld":
            dmenu_title = "Menu de sauts - Darkworld";
            dbutton_options = ["Entrée du Darkworld", "Entrée de la Citadelle", "Entrée des Plaines", "Bazar de Seam", "Entrée du Grand Plateau", "Entrée de la Forêt", "Vente de Pâtisseries", "Entrée Château", "Dernier étage Château", "Sortie Darkworld"];
            dmenu_box = 1;
            dbutton_layout = 1;
            break;

        case "dw_entrance":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;

        case "castletown_entrance":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;

        case "field_entrance":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;

        case "seam_shop":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;

        case "greatboard_entrance":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;

        case "forest_entrance":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;

        case "bakesale":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;

        case "castle_entrance":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;

        case "castle_top_floor":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;

        case "darkworld_exit":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;

        case "battles":
            dmenu_title = "Menu de sauts - Combats";
            dbutton_options = ["Combat contre Lancer", "Combat contre Kouronné", "Combat contre Tréflette", "Combat contre Susie", "2e Combat contre Kouronné", "Combat contre Jevil", "Combat contre  le Roi"];
            dmenu_box = 1;
            dbutton_layout = 1;
            break;

        case "lancer_battle":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : Combat"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;

        case "kround_battle":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : Combat"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;

        case "clover_battle":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : Combat"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;

        case "susie_battle":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : Combat"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;

        case "kround2_battle":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : Combat"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;

        case "jevil_battle":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : Combat avec Clé"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;

        case "king_battle":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : Combat"];
            dmenu_box = 0;
            dbutton_layout = 1;
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

        case "lightworld":
            if (dbutton_selected == 1)
                dmenu_state = "kris_hallway";

            if (dbutton_selected == 2)
                dmenu_state = "school_hallway";

            if (dbutton_selected == 3)
                dmenu_state = "school_closet";

            if (dbutton_selected == 4)
                dmenu_state = "after_darkworld";

            break;

        case "kris_hallway":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 0;
            snd_free_all();
            room_goto(room_krishallway);

            if (dbutton_selected == 2)
                global.plot = 1;

            break;

        case "school_hallway":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 0;
            snd_free_all();
            room_goto(room_schoollobby);

            if (dbutton_selected == 2)
                global.plot = 1;

            break;

        case "school_closet":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 0;
            snd_free_all();
            room_goto(room_schooldoor);
            break;

        case "after_darkworld":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 0;
            snd_free_all();
            room_goto(room_lw_computer_lab);
            break;

        case "darkworld":
            if (dbutton_selected == 1)
                dmenu_state = "dw_entrance";

            if (dbutton_selected == 2)
                dmenu_state = "castletown_entrance";

            if (dbutton_selected == 3)
                dmenu_state = "field_entrance";

            if (dbutton_selected == 4)
                dmenu_state = "seam_shop";

            if (dbutton_selected == 5)
                dmenu_state = "greatboard_entrance";

            if (dbutton_selected == 6)
                dmenu_state = "forest_entrance";

            if (dbutton_selected == 7)
                dmenu_state = "bakesale";

            if (dbutton_selected == 8)
                dmenu_state = "castle_entrance";

            if (dbutton_selected == 9)
                dmenu_state = "castle_top_floor";

            if (dbutton_selected == 10)
                dmenu_state = "darkworld_exit";

            break;

        case "dw_entrance":
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

        case "castletown_entrance":
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

        case "field_entrance":
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

        case "seam_shop":
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

        case "greatboard_entrance":
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

        case "forest_entrance":
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

        case "bakesale":
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

        case "castle_entrance":
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

        case "castle_top_floor":
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

        case "darkworld_exit":
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

        case "battles":
            if (dbutton_selected == 1)
                dmenu_state = "lancer_battle";

            if (dbutton_selected == 2)
                dmenu_state = "kround_battle";

            if (dbutton_selected == 3)
                dmenu_state = "clover_battle";

            if (dbutton_selected == 4)
                dmenu_state = "susie_battle";

            if (dbutton_selected == 5)
                dmenu_state = "kround2_battle";

            if (dbutton_selected == 6)
                dmenu_state = "jevil_battle";

            if (dbutton_selected == 7)
                dmenu_state = "king_battle";

            break;

        case "lancer_battle":
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

        case "kround_battle":
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

        case "clover_battle":
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

        case "susie_battle":
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

        case "kround2_battle":
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

        case "jevil_battle":
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

        case "king_battle":
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
                    else                        scr_debug_print(string(dgiver_amount) + " " + dgiver_bname + " ajouté à l'inventaire");
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

                    for (var i ; i < dgiver_amount; i++)
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
                        scr_weaponremove(real_dex);

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
                        scr_yitemremove(real_index);

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

        default:
            snd_play(snd_error);
            scr_debug_print("Sélection invalide !");
    }
}


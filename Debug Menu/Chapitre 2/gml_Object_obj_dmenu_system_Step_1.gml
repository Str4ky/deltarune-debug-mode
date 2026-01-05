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
        case "debug":
            dmenu_title = "Menu Debug";
            dbutton_options = dbutton_options_original;
            dmenu_box = 0;
            dbutton_layout = 0;
            break;

        case "warp":
            scr_debug_print("Pas encore disponible !");
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
            dbutton_options = ["Téléporter"];
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
            dbutton_options = ["Citadelle", "Entrée Cyber-Monde", "Entrée Cyber-Plaine", "Boutique de musique", "Zone Poubelle", "Entrée de la Ville", "Pause de la Ville", "Salle de mousse", "Salle des souris pénibles 3", "Cellule de Kris", "Entrée Manoir", "3e étage du Manoir", "Après le Tunnel d'acide", "Sous-sol du Manoir"];
            dmenu_box = 1;
            dbutton_layout = 1;
            break;
        
        case "castletown":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : Avant chercher les autres", "Saut : Après chercher les autres", "Saut : Après le Cyber-Monde"];
            dmenu_box = 1;
            dbutton_layout = 1;
            break;
        
        case "cw_entrance":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case "cyberfield_entrance":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case "music_shop":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case "trash_zone":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case "city_entrance":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case "city_break":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case "moss_room":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case "mouse_game3":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : Après le 3e jeu"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case "prison_cell":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case "mansion_entrance":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case "mansion_3rd_floor":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case "after_acid_tunnel":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case "mansion_basement":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case "battles":
            dmenu_title = "Menu de sauts - Combats";
            dbutton_options = ["Combat contre les DJ", "Combat contre Berdly", "2e Combat contre Berdly", "Combat contre Spamton", "Combat contre Gest. De Tachs", "Combat contre Surimolette", "Combat contre la Reine", "Combat contre Spamton Neo", "Combat contre Giga Queen"];
            dmenu_box = 1;
            dbutton_layout = 1;
            break;
        
        case "dj_battle":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : Combat"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case "berdly_battle":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : Combat"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case "berdly2_battle":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : Combat"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case "spamton_battle":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : Combat"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case "tasque_manager_battle":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : Combat"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case "mauswheel_battle":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : Combat"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case "queen_battle":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : Combat"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case "spamton_neo_battle":
            dmenu_title = "Options de saut";
            dbutton_options = ["Téléporter", "Saut : Combat avec DisqueGravé"];
            dmenu_box = 0;
            dbutton_layout = 1;
            break;
        
        case "giga_queen_battle":
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

            dmenu_box = 2;
            dbutton_layout = 1;
            break;

        case "armors":
            dmenu_title = "Liste d'armures";
            dbutton_options = [];
            dbutton_indices = [];
            var max_len = 40;

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

            dmenu_box = 2;
            dbutton_layout = 1;
            break;

        case "weapons":
            dmenu_title = "Liste d'armes";
            dbutton_options = [];
            dbutton_indices = [];
            var max_len = 40;

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
            dbutton_indices = [];
            categories_len = array_length(dother_categories);
            var max_len = 40;
            
            if (!global.dreading_custom_flag)
            {
                array_push(dbutton_options, "Custom");
                array_push(dbutton_indices, 0);
            }
            else
            {
                array_push(dbutton_options, "global.flag[" + dcustom_flag_text[0] + "] = |" + dcustom_flag_text[1] + "|");
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
                dmenu_state = "recruits";
                dhorizontal_page = 0;
                dbutton_selected = 1;
            }

            if (dbutton_selected == 4)
            {
                dmenu_state = "flag_misc";
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
                dmenu_state = "castletown";
            
            if (dbutton_selected == 2)
                dmenu_state = "cw_entrance";
            
            if (dbutton_selected == 3)
                dmenu_state = "cyberfield_entrance";
            
            if (dbutton_selected == 4)
                dmenu_state = "music_shop";
            
            if (dbutton_selected == 5)
                dmenu_state = "trash_zone";
            
            if (dbutton_selected == 6)
                dmenu_state = "city_entrance";
            
            if (dbutton_selected == 7)
                dmenu_state = "city_break";
            
            if (dbutton_selected == 8)
                dmenu_state = "moss_room";
            
            if (dbutton_selected == 9)
                dmenu_state = "mouse_game3";
            
            if (dbutton_selected == 10)
                dmenu_state = "prison_cell";
            
            if (dbutton_selected == 11)
                dmenu_state = "mansion_entrance";
            
            if (dbutton_selected == 12)
                dmenu_state = "mansion_3rd_floor";
            
            if (dbutton_selected == 13)
                dmenu_state = "after_acid_tunnel";
            
            if (dbutton_selected == 14)
                dmenu_state = "mansion_basement";
            
            break;
        
        case "castletown":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_castle_area_1);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 0;
                global.plot = 7;
                global.flag[34] = 1;
            }
            
            if (dbutton_selected == 3)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 12;
                global.flag[34] = 1;
            }
            
            if (dbutton_selected == 4)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 205;
                global.flag[34] = 0;
            }
            
            break;
        
        case "cw_entrance":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_cyber_intro_1);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 0;
                global.plot = 50;
                global.flag[34] = 1;
            }
            
            break;
        
        case "cyberfield_entrance":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_cyber_savepoint);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 51;
                global.flag[34] = 1;
            }
            
            break;
        
        case "music_shop":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_cyber_musical_shop);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 60;
                global.flag[34] = 0;
            }
            
            break;
        
        case "trash_zone":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_city_intro);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 64;
                global.flag[34] = 0;
            }
            
            break;
        
        case "city_entrance":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_city_entrance);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 0;
                global.char[2] = 0;
                global.plot = 67;
                global.flag[34] = 0;
            }
            
            break;
        
        case "city_break":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_city_savepoint);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 4;
                global.char[2] = 0;
                global.plot = 75;
                global.flag[34] = 0;
            }
            
            break;
        
        case "moss_room":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_city_moss);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 4;
                global.char[2] = 0;
                global.plot = 75;
                global.flag[34] = 0;
            }
            
            break;
        
        case "mouse_game3":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_city_mice3);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 4;
                global.char[2] = 0;
                global.plot = 78;
                global.flag[34] = 0;
            }
            
            break;
        
        case "prison_cell":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_krisroom);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 0;
                global.char[2] = 0;
                global.plot = 100;
                global.flag[34] = 0;
            }
            
            break;
        
        case "mansion_entrance":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_entrance);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 120;
                global.flag[34] = 0;
            }
            
            break;
        
        case "mansion_3rd_floor":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_east_3f);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 0;
                global.plot = 125;
                global.flag[34] = 0;
            }
            
            break;
        
        case "after_acid_tunnel":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_acid_tunnel_exit);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 160;
                global.flag[34] = 0;
            }
            
            break;
        
        case "mansion_basement":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            global.char[0] = 1;
            global.char[1] = 0;
            global.char[2] = 0;
            snd_free_all();
            room_goto(room_dw_mansion_b_central);
            
            if (dbutton_selected == 2)
            {
                global.flag[358] = 1;
                global.flag[309] = 4;
                global.flag[34] = 0;
                
                if (global.plot < 120)
                    global.plot = 120;
            }
            
            break;
        
        case "battles":
            if (dbutton_selected == 1)
                dmenu_state = "dj_battle";
            
            if (dbutton_selected == 2)
                dmenu_state = "berdly_battle";
            
            if (dbutton_selected == 3)
                dmenu_state = "berdly2_battle";
            
            if (dbutton_selected == 4)
                dmenu_state = "spamton_battle";
            
            if (dbutton_selected == 5)
                dmenu_state = "tasque_manager_battle";
            
            if (dbutton_selected == 6)
                dmenu_state = "mauswheel_battle";
            
            if (dbutton_selected == 7)
                dmenu_state = "queen_battle";
            
            if (dbutton_selected == 8)
                dmenu_state = "spamton_neo_battle";
            
            if (dbutton_selected == 9)
                dmenu_state = "giga_queen_battle";
            
            break;
        
        case "dj_battle":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_cyber_music_final);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 52;
                global.flag[34] = 1;
            }
            
            break;
        
        case "berdly_battle":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_cyber_rollercoaster);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 2;
                global.plot = 60;
                global.flag[34] = 0;
            }
            
            break;
        
        case "berdly2_battle":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_city_berdly);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 4;
                global.char[2] = 0;
                global.plot = 78;
                global.flag[34] = 0;
            }
            
            break;
        
        case "spamton_battle":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_city_spamton_alley);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 0;
                global.char[2] = 0;
                global.plot = 80;
                global.flag[34] = 0;
            }
            
            break;
        
        case "tasque_manager_battle":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_tasquePaintings);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 120;
                global.flag[34] = 0;
            }
            
            break;
        
        case "mauswheel_battle":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_kitchen);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 120;
                global.flag[34] = 0;
            }
            
            break;
        
        case "queen_battle":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_east_4f_d);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 160;
                global.flag[34] = 0;
            }
            
            break;
        
        case "spamton_neo_battle":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_b_east_b);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 0;
                global.char[2] = 0;
                global.flag[358] = 2;
                global.flag[373] = 1;
                global.flag[309] = 7;
                scr_keyitemget(11);
                global.flag[34] = 0;
                
                if (global.plot < 120)
                    global.plot = 120;
            }
            
            break;
        
        case "giga_queen_battle":
            dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_east_4f_d);
            
            if (dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 165;
                global.flag[34] = 0;
            }
            
            break;

        case "give":
            if (dbutton_selected == 1)
            {
                dmenu_state = "objects";
                dbutton_selected = 1;
            }

            if (dbutton_selected == 2)
            {
                dmenu_state = "armors";
                dbutton_selected = 1;
            }

            if (dbutton_selected == 3)
            {
                dmenu_state = "weapons";
                dbutton_selected = 1;
            }

            if (dbutton_selected == 4)
            {
                dmenu_state = "keyitems";
                dbutton_selected = 1;
            }

            break;

        case "objects":
            dgiver_menu_state = dmenu_state;
            dbutton_selected = clamp(dbutton_selected, 0, array_length(dbutton_options) - 1);
            dgiver_button_selected = dbutton_selected;
            dmenu_state = "givertab";
            dbutton_selected = 1;
            break;

        case "armors":
            dgiver_menu_state = dmenu_state;
            dgiver_button_selected = dbutton_selected;
            dmenu_state = "givertab";
            dbutton_selected = 1;
            break;

        case "weapons":
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
                if (dgiver_amount > 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];

                    for (var i = 0; i < dgiver_amount; i++)
                        scr_itemget(real_index);

                    scr_debug_print(string(dgiver_amount) + " " + dgiver_bname + " ajouté à l'inventaire");
                }
                else if (dgiver_amount < 0)
                {
                    real_index = dbutton_indices[dgiver_button_selected - 1];

                    for (var i = 0; i < abs(dgiver_amount); i++)
                        scr_itemremove(real_index);

                    scr_debug_print(string(abs(dgiver_amount)) + " " + dgiver_bname + " retiré de l'inventaire");
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

        case "recruits":
            if (dbutton_selected == 1)
            {
                dmenu_state = "recruit_presets";
                dbutton_selected = 1;
            }
            
            break;
        
        case "recruit_presets":
            if (dhorizontal_page == 0)
            {
                for (var c = 1; c <= global.chapter; c++)
                {
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
                }
            }
            else
            {
                var test_lst = scr_get_chapter_recruit_data(dhorizontal_page);
                
                for (var i = 0; i < array_length(test_lst); i++)
                {
                    var enemy_id = test_lst[i];
                    scr_recruit_info(enemy_id);
                    
                    if (dbutton_selected == 1)
                        global.flag[enemy_id + 600] = 1;
                    else
                        global.flag[enemy_id + 600] = -1 / _recruitcount;
                }
            }
            
            if (dbutton_selected == 1)
                snd_play(snd_pirouette);
            else
                snd_play(snd_weirdeffect);
            
            break;
        
        case "flag_categories":
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

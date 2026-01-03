if (global.ditemcount == 0)
{
    global.ditemcount = 63;
    global.darmorcount = 54;
    global.dweaponcount = 54;
    global.dkeyitemcount = 31;
    global.drecent_item = 60;
    global.drecent_armor = 50;
    global.drecent_weapon = 50;
    global.drecent_keyitem = 30;
}

function dmenu_state_update()
{
    switch (global.dmenu_state)
    {
        case "debug":
            global.dmenu_title = "Menu Debug";
            global.dbutton_options = global.dbutton_options_original;
            global.dmenu_box = 0;
            global.dbutton_layout = 0;
            break;
        
        case "warp":
            scr_debug_print("Pas encore disponible !");
            break;
        
        case "give":
            global.dmenu_title = "Type d'items";
            global.dbutton_options = ["Objets", "Armures", "Armes", "Obj Clés"];
            global.dmenu_box = 0;
            global.dbutton_layout = 0;
            break;
        
        case "objects":
            global.dmenu_title = "Liste d'objets";
            global.dbutton_options = [];
            global.dbutton_indices = [];
            var max_len = 40;
            
            for (var i = global.drecent_item; i <= global.ditemcount; i++)
            {
                scr_iteminfo(i);
                var cleaned_desc = string_replace_all(itemdescb, "#", " ");
                var combined = itemnameb + " - " + cleaned_desc;
                
                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + "...";
                
                array_push(global.dbutton_options, combined);
                array_push(global.dbutton_indices, i);
            }
            
            var cutoff = min(global.drecent_item - 1, global.ditemcount);
            
            for (var i = 1; i <= cutoff; i++)
            {
                scr_iteminfo(i);
                var cleaned_desc = string_replace_all(itemdescb, "#", " ");
                var combined = itemnameb + " - " + cleaned_desc;
                
                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + "...";
                
                array_push(global.dbutton_options, combined);
                array_push(global.dbutton_indices, i);
            }
            
            global.dmenu_box = 2;
            global.dbutton_layout = 1;
            break;
        
        case "flag_misc":
            global.dmenu_title = "Divers";
            global.dbutton_options = [];
            global.dbutton_indices = [];
            other_len = array_length(global.dother_options);
            var max_len = 40;
            
            if (!global.dreading_custom_flag)
            {
                array_push(global.dbutton_options, "Custom");
                array_push(global.dbutton_indices, 0);
            }
            else
            {
                array_push(global.dbutton_options, "global.flag[" + global.dcustom_flag_text[0] + "] = |" + global.dcustom_flag_text[1] + "|");
                array_push(global.dbutton_indices, 0);
            }
            
            for (var i = 1; i < other_len; i++)
            {
                cur_option = global.dother_options[i];
                flag_number = global.flag[cur_option[1]];
                var combined = cur_option[0] + " - problem lol";
                
                if (i == (global.dbutton_selected - 1))
                {
                    option_index = global.dhorizontal_index;
                    global.flag[cur_option[1]] = cur_option[2][global.dhorizontal_index][1];
                }
                else
                {
                    option_index = find_subarray_index(cur_option[1], cur_option[2]);
                }
                
                if (option_index != -1)
                    combined = cur_option[0] + " -  " + cur_option[2][option_index][0];
                
                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + "...";
                
                array_push(global.dbutton_options, combined);
                array_push(global.dbutton_indices, i);
            }
            
            global.dmenu_box = 2;
            global.dbutton_layout = 1;
            break;
        
        case "recruits":
            global.dmenu_title = "Liste des recrues";
            global.dbutton_options = [];
            global.dbutton_indices = [];
            var max_len = 40;
            
            for (var c = 1; c <= global.chapter; c++)
            {
                var test_lst = scr_get_chapter_recruit_data(c);
                
                for (var i = 0; i < array_length(test_lst); i++)
                {
                    var enemy_id = test_lst[i];
                    scr_recruit_info(enemy_id);
                    var combined = _name + " - " + string(max(floor(global.flag[enemy_id + 600] * _recruitcount), -1)) + "/" + string(_recruitcount);
                    
                    if (string_length(combined) > max_len)
                        combined = string_copy(combined, 1, max_len - 3) + "...";
                    
                    array_push(global.dbutton_options, combined);
                    array_push(global.dbutton_indices, enemy_id);
                }
            }
            
            global.dmenu_box = 2;
            global.dbutton_layout = 1;
            break;
        
        case "armors":
            global.dmenu_title = "Liste d'armures";
            global.dbutton_options = [];
            global.dbutton_indices = [];
            var max_len = 40;
            
            for (var i = global.drecent_armor; i <= global.darmorcount; i++)
            {
                scr_armorinfo(i);
                var cleaned_desc = string_replace_all(armordesctemp, "#", " ");
                var combined = armornametemp + " - " + cleaned_desc;
                
                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + "...";
                
                array_push(global.dbutton_options, combined);
                array_push(global.dbutton_indices, i);
            }
            
            var cutoff = min(global.drecent_armor - 1, global.darmorcount);
            
            for (var i = 1; i <= cutoff; i++)
            {
                scr_armorinfo(i);
                var cleaned_desc = string_replace_all(armordesctemp, "#", " ");
                var combined = armornametemp + " - " + cleaned_desc;
                
                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + "...";
                
                array_push(global.dbutton_options, combined);
                array_push(global.dbutton_indices, i);
            }
            
            global.dmenu_box = 2;
            global.dbutton_layout = 1;
            break;
        
        case "weapons":
            global.dmenu_title = "Liste d'armes";
            global.dbutton_options = [];
            global.dbutton_indices = [];
            var max_len = 40;
            
            for (var i = global.drecent_weapon; i <= global.dweaponcount; i++)
            {
                scr_weaponinfo(i);
                var cleaned_desc = string_replace_all(weapondesctemp, "#", " ");
                var combined = weaponnametemp + " - " + cleaned_desc;
                
                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + "...";
                
                array_push(global.dbutton_options, combined);
                array_push(global.dbutton_indices, i);
            }
            
            var cutoff = min(global.drecent_weapon - 1, global.dweaponcount);
            
            for (var i = 1; i <= cutoff; i++)
            {
                scr_weaponinfo(i);
                var cleaned_desc = string_replace_all(weapondesctemp, "#", " ");
                var combined = weaponnametemp + " - " + cleaned_desc;
                
                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + "...";
                
                array_push(global.dbutton_options, combined);
                array_push(global.dbutton_indices, i);
            }
            
            global.dmenu_box = 2;
            global.dbutton_layout = 1;
            break;
        
        case "keyitems":
            global.dmenu_title = "Liste d'objets clés (Peut briser le jeu)";
            global.dbutton_options = [];
            global.dbutton_indices = [];
            var max_len = 40;
            
            for (var i = global.drecent_keyitem; i <= global.dkeyitemcount; i++)
            {
                scr_keyiteminfo(i);
                var cleaned_desc = string_replace_all(tempkeyitemdesc, "#", " ");
                var combined = tempkeyitemname + " - " + cleaned_desc;
                
                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + "...";
                
                array_push(global.dbutton_options, combined);
                array_push(global.dbutton_indices, i);
            }
            
            var cutoff = min(global.drecent_keyitem - 1, global.dkeyitemcount);
            
            for (var i = 1; i <= cutoff; i++)
            {
                scr_keyiteminfo(i);
                var cleaned_desc = string_replace_all(tempkeyitemdesc, "#", " ");
                var combined = tempkeyitemname + " - " + cleaned_desc;
                
                if (string_length(combined) > max_len)
                    combined = string_copy(combined, 1, max_len - 3) + "...";
                
                array_push(global.dbutton_options, combined);
                array_push(global.dbutton_indices, i);
            }
            
            global.dmenu_box = 2;
            global.dbutton_layout = 1;
            break;
        
        case "givertab":
            global.dmenu_title = "Ajouter combien à l'inventaire ?";
            global.dgiver_amount = 1;
            global.dmenu_box = 0;
            global.dbutton_layout = 2;
            break;
        
        default:
            global.dmenu_title = "Inconnu";
            global.dbutton_options = [];
            global.dmenu_box = 0;
            global.dbutton_layout = 0;
    }
}

function dmenu_state_interact()
{
    switch (global.dmenu_state)
    {
        case "debug":
            if (global.dbutton_selected == 1)
            {
                global.dmenu_state = "warp";
                global.dbutton_selected = 1;
            }
            
            if (global.dbutton_selected == 2)
            {
                global.dmenu_state = "give";
                global.dbutton_selected = 1;
            }
            
            if (global.dbutton_selected == 3)
            {
                global.dmenu_state = "recruits";
                global.dbutton_selected = 1;
            }
            
            if (global.dbutton_selected == 4)
            {
                global.dmenu_state = "flag_misc";
                global.dbutton_selected = 1;
            }
            
            break;
        
        case "warp":
            if (global.dbutton_selected == 1)
            {
                global.dmenu_state = "lightworld";
                global.dbutton_selected = 1;
            }
            
            if (global.dbutton_selected == 2)
            {
                global.dmenu_state = "darkworld";
                global.dbutton_selected = 1;
            }
            
            if (global.dbutton_selected == 3)
            {
                global.dmenu_state = "battles";
                global.dbutton_selected = 1;
            }
            
            break;
        
        case "give":
            if (global.dbutton_selected == 1)
            {
                global.dmenu_state = "objects";
                global.dbutton_selected = 1;
            }
            
            if (global.dbutton_selected == 2)
            {
                global.dmenu_state = "armors";
                global.dbutton_selected = 1;
            }
            
            if (global.dbutton_selected == 3)
            {
                global.dmenu_state = "weapons";
                global.dbutton_selected = 1;
            }
            
            if (global.dbutton_selected == 4)
            {
                global.dmenu_state = "keyitems";
                global.dbutton_selected = 1;
            }
            
            break;
        
        case "objects":
            global.dgiver_menu_state = global.dmenu_state;
            global.dbutton_selected = clamp(global.dbutton_selected, 0, array_length(global.dbutton_options) - 1);
            global.dgiver_button_selected = global.dbutton_selected;
            global.dmenu_state = "givertab";
            global.dbutton_selected = 1;
            break;
        
        case "armors":
            global.dgiver_menu_state = global.dmenu_state;
            global.dgiver_button_selected = global.dbutton_selected;
            global.dmenu_state = "givertab";
            global.dbutton_selected = 1;
            break;
        
        case "weapons":
            global.dgiver_menu_state = global.dmenu_state;
            global.dgiver_button_selected = global.dbutton_selected;
            global.dmenu_state = "givertab";
            global.dbutton_selected = 1;
            break;
        
        case "keyitems":
            global.dgiver_menu_state = global.dmenu_state;
            global.dgiver_button_selected = global.dbutton_selected;
            global.dmenu_state = "givertab";
            global.dbutton_selected = 1;
            break;
        
        case "givertab":
            if (global.dgiver_menu_state == "objects")
            {
                if (global.dgiver_amount > 0)
                {
                    real_index = global.dbutton_indices[global.dgiver_button_selected - 1];
                    
                    for (var i = 0; i < global.dgiver_amount; i++)
                        scr_itemget(real_index);
                    
                    scr_debug_print(string(global.dgiver_amount) + " " + global.dgiver_bname + " ajouté à l'inventaire");
                }
                else if (global.dgiver_amount < 0)
                {
                    real_index = global.dbutton_indices[global.dgiver_button_selected - 1];
                    
                    for (var i = 0; i < abs(global.dgiver_amount); i++)
                        scr_itemremove(real_index);
                    
                    scr_debug_print(string(abs(global.dgiver_amount)) + " " + global.dgiver_bname + " retiré de l'inventaire");
                }
                else
                {
                    scr_debug_print("Annulé");
                }
            }
            
            if (global.dgiver_menu_state == "armors")
            {
                if (global.dgiver_amount > 0)
                {
                    real_index = global.dbutton_indices[global.dgiver_button_selected - 1];
                    
                    for (var i = 0; i < global.dgiver_amount; i++)
                        scr_armorget(real_index);
                    
                    scr_debug_print(string(global.dgiver_amount) + " " + global.dgiver_bname + " ajouté à l'inventaire");
                }
                else if (global.dgiver_amount < 0)
                {
                    real_index = global.dbutton_indices[global.dgiver_button_selected - 1];
                    
                    for (var i = 0; i < abs(global.dgiver_amount); i++)
                        scr_armorremove(real_index);
                    
                    scr_debug_print(string(abs(global.dgiver_amount)) + " " + global.dgiver_bname + " retiré de l'inventaire");
                }
                else
                {
                    scr_debug_print("Annulé");
                }
            }
            
            if (global.dgiver_menu_state == "weapons")
            {
                if (global.dgiver_amount > 0)
                {
                    real_index = global.dbutton_indices[global.dgiver_button_selected - 1];
                    
                    for (var i = 0; i < global.dgiver_amount; i++)
                        scr_weaponget(real_index);
                    
                    scr_debug_print(string(global.dgiver_amount) + " " + global.dgiver_bname + " ajouté à l'inventaire");
                }
                else if (global.dgiver_amount < 0)
                {
                    real_index = global.dbutton_indices[global.dgiver_button_selected - 1];
                    
                    for (var i = 0; i < abs(global.dgiver_amount); i++)
                        scr_weaponremove(real_index);
                    
                    scr_debug_print(string(abs(global.dgiver_amount)) + " " + global.dgiver_bname + " retiré de l'inventaire");
                }
                else
                {
                    scr_debug_print("Annulé");
                }
            }
            
            if (global.dgiver_menu_state == "keyitems")
            {
                if (global.dgiver_amount > 0)
                {
                    real_index = global.dbutton_indices[global.dgiver_button_selected - 1];
                    
                    for (var i = 0; i < global.dgiver_amount; i++)
                        scr_keyitemget(real_index);
                    
                    scr_debug_print(string(global.dgiver_amount) + " " + global.dgiver_bname + " ajouté à l'inventaire");
                }
                else if (global.dgiver_amount < 0)
                {
                    real_index = global.dbutton_indices[global.dgiver_button_selected - 1];
                    
                    for (var i = 0; i < abs(global.dgiver_amount); i++)
                        scr_keyitemremove(real_index);
                    
                    scr_debug_print(string(abs(global.dgiver_amount)) + " " + global.dgiver_bname + " retiré de l'inventaire");
                }
                else
                {
                    scr_debug_print("Annulé");
                }
            }
            
            global.dmenu_active = false;
            global.interact = 0;
            break;
        
        case "flag_misc":
            break;
        
        default:
            snd_play(snd_error);
            scr_debug_print("Sélection invalide !");
    }
}


if (global.ditemcount == 0)
{
    global.ditemcount = 15;
    global.darmorcount = 7;
    global.dweaponcount = 10;
    global.dkeyitemcount = 7;
    global.drecent_item = 1;
    global.drecent_armor = 1;
    global.drecent_weapon = 1;
    global.drecent_keyitem = 1;
}

function dmenu_state_update()
{
    switch (global.dmenu_state)
    {
        case "debug":
            global.dmenu_title = "Menu Debug";
            global.dbutton_options = ["Sauts", "Items"];
            global.dmenu_box = 0;
            global.dbutton_layout = 0;
            break;
        
        case "warp":
            global.dmenu_title = "Menu de sauts";
            global.dbutton_options = ["Lightworld", "Darkworld", "Combats"];
            global.dmenu_box = 0;
            global.dbutton_layout = 0;
            break;
        
        case "lightworld":
            global.dmenu_title = "Menu de sauts - Lightworld";
            global.dbutton_options = ["Couloir de Kris", "Hall de l'école", "Placard de l'école", "Après Darkworld"];
            global.dmenu_box = 1;
            global.dbutton_layout = 1;
            break;
        
        case "kris_hallway":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "school_hallway":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "school_closet":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "after_darkworld":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "darkworld":
            global.dmenu_title = "Menu de sauts - Darkworld";
            global.dbutton_options = ["Entrée du Darkworld", "Entrée de la Citadelle", "Entrée des Plaines", "Bazar de Seam", "Entrée du Grand Plateau", "Entrée de la Forêt", "Vente de Pâtisseries", "Entrée Château", "Dernier étage Château", "Sortie Darkworld"];
            global.dmenu_box = 1;
            global.dbutton_layout = 1;
            break;
        
        case "dw_entrance":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "castletown_entrance":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "field_entrance":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "seam_shop":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "greatboard_entrance":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "forest_entrance":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "bakesale":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "castle_entrance":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "castle_top_floor":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "darkworld_exit":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "battles":
            global.dmenu_title = "Menu de sauts - Combats";
            global.dbutton_options = ["Combat contre Lancer", "Combat contre Kouronné", "Combat contre Tréflette", "Combat contre Susie", "2e Combat contre Kouronné", "Combat contre Jevil", "Combat contre  le Roi"];
            global.dmenu_box = 1;
            global.dbutton_layout = 1;
            break;
        
        case "lancer_battle":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : Combat"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "kround_battle":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : Combat"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "clover_battle":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : Combat"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "susie_battle":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : Combat"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "kround2_battle":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : Combat"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "jevil_battle":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : Combat avec Clé"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "king_battle":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : Combat"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
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
        
        case "lightworld":
            if (global.dbutton_selected == 1)
                global.dmenu_state = "kris_hallway";
            
            if (global.dbutton_selected == 2)
                global.dmenu_state = "school_hallway";
            
            if (global.dbutton_selected == 3)
                global.dmenu_state = "school_closet";
            
            if (global.dbutton_selected == 4)
                global.dmenu_state = "after_darkworld";
            
            break;
        
        case "kris_hallway":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 0;
            snd_free_all();
            room_goto(room_krishallway);
            
            if (global.dbutton_selected == 2)
                global.plot = 1;
            
            break;
        
        case "school_hallway":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 0;
            snd_free_all();
            room_goto(room_schoollobby);
            
            if (global.dbutton_selected == 2)
                global.plot = 1;
            
            break;
        
        case "school_closet":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 0;
            snd_free_all();
            room_goto(room_schooldoor);
            break;
        
        case "after_darkworld":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 0;
            snd_free_all();
            room_goto(room_lw_computer_lab);
            break;
        
        case "darkworld":
            if (global.dbutton_selected == 1)
                global.dmenu_state = "dw_entrance";
            
            if (global.dbutton_selected == 2)
                global.dmenu_state = "castletown_entrance";
            
            if (global.dbutton_selected == 3)
                global.dmenu_state = "field_entrance";
            
            if (global.dbutton_selected == 4)
                global.dmenu_state = "seam_shop";
            
            if (global.dbutton_selected == 5)
                global.dmenu_state = "greatboard_entrance";
            
            if (global.dbutton_selected == 6)
                global.dmenu_state = "forest_entrance";
            
            if (global.dbutton_selected == 7)
                global.dmenu_state = "bakesale";
            
            if (global.dbutton_selected == 8)
                global.dmenu_state = "castle_entrance";
            
            if (global.dbutton_selected == 9)
                global.dmenu_state = "castle_top_floor";
            
            if (global.dbutton_selected == 10)
                global.dmenu_state = "darkworld_exit";
            
            break;
        
        case "dw_entrance":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dark1);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 0;
                global.char[2] = 0;
                global.plot = 11;
            }
            
            break;
        
        case "castletown_entrance":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_castle_outskirts);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 0;
                global.char[2] = 0;
                global.plot = 16;
            }
            
            break;
        
        case "field_entrance":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_field1);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 0;
                global.plot = 33;
            }
            
            break;
        
        case "seam_shop":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_field_shop1);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 2;
                global.plot = 40;
            }
            
            break;
        
        case "greatboard_entrance":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_field_checkers4);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 2;
                global.plot = 50;
            }
            
            break;
        
        case "forest_entrance":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_forest_savepoint1);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 0;
                global.plot = 60;
            }
            
            break;
        
        case "bakesale":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_forest_savepoint2);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 0;
                global.plot = 75;
            }
            
            break;
        
        case "castle_entrance":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_cc_1f);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 165;
            }
            
            break;
        
        case "castle_top_floor":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_cc_5f);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 165;
            }
            
            break;
        
        case "darkworld_exit":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_cc_prefountain);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 0;
                global.plot = 240;
            }
            
            break;
        
        case "battles":
            if (global.dbutton_selected == 1)
                global.dmenu_state = "lancer_battle";
            
            if (global.dbutton_selected == 2)
                global.dmenu_state = "kround_battle";
            
            if (global.dbutton_selected == 3)
                global.dmenu_state = "clover_battle";
            
            if (global.dbutton_selected == 4)
                global.dmenu_state = "susie_battle";
            
            if (global.dbutton_selected == 5)
                global.dmenu_state = "kround2_battle";
            
            if (global.dbutton_selected == 6)
                global.dmenu_state = "jevil_battle";
            
            if (global.dbutton_selected == 7)
                global.dmenu_state = "king_battle";
            
            break;
        
        case "lancer_battle":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_castle_town);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 0;
                global.plot = 16;
            }
            
            break;
        
        case "kround_battle":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_field_checkers7);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 2;
                global.plot = 55;
            }
            
            break;
        
        case "clover_battle":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_forest_area3);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 0;
                global.plot = 72;
            }
            
            break;
        
        case "susie_battle":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_forest_savepoint3);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 0;
                global.plot = 120;
            }
            
            break;
        
        case "kround2_battle":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_cc_6f);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.charauto[2] = 0;
                global.plot = 165;
            }
            
            break;
        
        case "jevil_battle":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_cc_prison_prejoker);
            
            if (global.dbutton_selected == 2)
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
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_cc_throneroom);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.charauto[2] = 0;
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
        
        default:
            snd_play(snd_error);
            scr_debug_print("Sélection invalide !");
    }
}
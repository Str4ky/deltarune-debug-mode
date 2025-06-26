if (global.ditemcount == 0)
{
    global.ditemcount = 33;
    global.darmorcount = 22;
    global.dweaponcount = 22;
    global.dkeyitemcount = 15;
    global.drecent_item = 16;
    global.drecent_armor = 8;
    global.drecent_weapon = 11;
    global.drecent_keyitem = 8;
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
            global.dbutton_options = ["Téléporter"];
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
            global.dbutton_options = ["Citadelle", "Entrée Cyber-Monde", "Entrée Cyber-Plaine", "Boutique de musique", "Zone Poubelle", "Entrée de la Ville", "Pause de la Ville", "Salle de mousse", "Salle des souris pénibles 3", "Cellule de Kris", "Entrée Manoir", "3e étage du Manoir", "Après le Tunnel d'acide", "Sous-sol du Manoir"];
            global.dmenu_box = 1;
            global.dbutton_layout = 1;
            break;
        
        case "castletown":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : Avant chercher les autres", "Saut : Après chercher les autres", "Saut : Après le Cyber-Monde"];
            global.dmenu_box = 1;
            global.dbutton_layout = 1;
            break;
        
        case "cw_entrance":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "cyberfield_entrance":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "music_shop":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "trash_zone":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "city_entrance":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "city_break":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "moss_room":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "mouse_game3":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : Après le 3e jeu"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "prison_cell":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "mansion_entrance":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "mansion_3rd_floor":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "after_acid_tunnel":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "mansion_basement":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : 1ère apparition"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "battles":
            global.dmenu_title = "Menu de sauts - Combats";
            global.dbutton_options = ["Combat contre les DJ", "Combat contre Berdly", "2e Combat contre Berdly", "Combat contre Spamton", "Combat contre Gest. De Tachs", "Combat contre Surimolette", "Combat contre la Reine", "Combat contre Spamton Neo", "Combat contre Giga Queen"];
            global.dmenu_box = 1;
            global.dbutton_layout = 1;
            break;
        
        case "dj_battle":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : Combat"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "berdly_battle":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : Combat"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "berdly2_battle":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : Combat"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "spamton_battle":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : Combat"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "tasque_manager_battle":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : Combat"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "mauswheel_battle":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : Combat"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "queen_battle":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : Combat"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "spamton_neo_battle":
            global.dmenu_title = "Options de saut";
            global.dbutton_options = ["Téléporter", "Saut : Combat avec DisqueGravé"];
            global.dmenu_box = 0;
            global.dbutton_layout = 1;
            break;
        
        case "giga_queen_battle":
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
                global.dmenu_state = "castletown";
            
            if (global.dbutton_selected == 2)
                global.dmenu_state = "cw_entrance";
            
            if (global.dbutton_selected == 3)
                global.dmenu_state = "cyberfield_entrance";
            
            if (global.dbutton_selected == 4)
                global.dmenu_state = "music_shop";
            
            if (global.dbutton_selected == 5)
                global.dmenu_state = "trash_zone";
            
            if (global.dbutton_selected == 6)
                global.dmenu_state = "city_entrance";
            
            if (global.dbutton_selected == 7)
                global.dmenu_state = "city_break";
            
            if (global.dbutton_selected == 8)
                global.dmenu_state = "moss_room";
            
            if (global.dbutton_selected == 9)
                global.dmenu_state = "mouse_game3";
            
            if (global.dbutton_selected == 10)
                global.dmenu_state = "prison_cell";
            
            if (global.dbutton_selected == 11)
                global.dmenu_state = "mansion_entrance";
            
            if (global.dbutton_selected == 12)
                global.dmenu_state = "mansion_3rd_floor";
            
            if (global.dbutton_selected == 13)
                global.dmenu_state = "after_acid_tunnel";
            
            if (global.dbutton_selected == 14)
                global.dmenu_state = "mansion_basement";
            
            break;
        
        case "castletown":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_castle_area_1);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 0;
                global.plot = 7;
                global.flag[34] = 1;
            }
            
            if (global.dbutton_selected == 3)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 12;
                global.flag[34] = 1;
            }
            
            if (global.dbutton_selected == 4)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 205;
                global.flag[34] = 0;
            }
            
            break;
        
        case "cw_entrance":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_cyber_intro_1);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 0;
                global.plot = 50;
                global.flag[34] = 1;
            }
            
            break;
        
        case "cyberfield_entrance":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_cyber_savepoint);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 51;
                global.flag[34] = 1;
            }
            
            break;
        
        case "music_shop":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_cyber_musical_shop);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 60;
                global.flag[34] = 0;
            }
            
            break;
        
        case "trash_zone":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_city_intro);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 64;
                global.flag[34] = 0;
            }
            
            break;
        
        case "city_entrance":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_city_entrance);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 0;
                global.char[2] = 0;
                global.plot = 67;
                global.flag[34] = 0;
            }
            
            break;
        
        case "city_break":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_city_savepoint);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 4;
                global.char[2] = 0;
                global.plot = 75;
                global.flag[34] = 0;
            }
            
            break;
        
        case "moss_room":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_city_moss);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 4;
                global.char[2] = 0;
                global.plot = 75;
                global.flag[34] = 0;
            }
            
            break;
        
        case "mouse_game3":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_city_mice3);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 4;
                global.char[2] = 0;
                global.plot = 78;
                global.flag[34] = 0;
            }
            
            break;
        
        case "prison_cell":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_krisroom);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 0;
                global.char[2] = 0;
                global.plot = 100;
                global.flag[34] = 0;
            }
            
            break;
        
        case "mansion_entrance":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_entrance);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 120;
                global.flag[34] = 0;
            }
            
            break;
        
        case "mansion_3rd_floor":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_east_3f);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 0;
                global.plot = 125;
                global.flag[34] = 0;
            }
            
            break;
        
        case "after_acid_tunnel":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_acid_tunnel_exit);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 160;
                global.flag[34] = 0;
            }
            
            break;
        
        case "mansion_basement":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            global.char[0] = 1;
            global.char[1] = 0;
            global.char[2] = 0;
            snd_free_all();
            room_goto(room_dw_mansion_b_central);
            
            if (global.dbutton_selected == 2)
            {
                global.flag[358] = 1;
                global.flag[309] = 4;
                global.flag[34] = 0;
                
                if (global.plot < 120)
                    global.plot = 120;
            }
            
            break;
        
        case "battles":
            if (global.dbutton_selected == 1)
                global.dmenu_state = "dj_battle";
            
            if (global.dbutton_selected == 2)
                global.dmenu_state = "berdly_battle";
            
            if (global.dbutton_selected == 3)
                global.dmenu_state = "berdly2_battle";
            
            if (global.dbutton_selected == 4)
                global.dmenu_state = "spamton_battle";
            
            if (global.dbutton_selected == 5)
                global.dmenu_state = "tasque_manager_battle";
            
            if (global.dbutton_selected == 6)
                global.dmenu_state = "mauswheel_battle";
            
            if (global.dbutton_selected == 7)
                global.dmenu_state = "queen_battle";
            
            if (global.dbutton_selected == 8)
                global.dmenu_state = "spamton_neo_battle";
            
            if (global.dbutton_selected == 9)
                global.dmenu_state = "giga_queen_battle";
            
            break;
        
        case "dj_battle":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_cyber_music_final);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 52;
                global.flag[34] = 1;
            }
            
            break;
        
        case "berdly_battle":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_cyber_rollercoaster);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 3;
                global.char[2] = 2;
                global.plot = 60;
                global.flag[34] = 0;
            }
            
            break;
        
        case "berdly2_battle":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_city_berdly);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 4;
                global.char[2] = 0;
                global.plot = 78;
                global.flag[34] = 0;
            }
            
            break;
        
        case "spamton_battle":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_city_spamton_alley);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 0;
                global.char[2] = 0;
                global.plot = 80;
                global.flag[34] = 0;
            }
            
            break;
        
        case "tasque_manager_battle":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_tasquePaintings);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 120;
                global.flag[34] = 0;
            }
            
            break;
        
        case "mauswheel_battle":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_kitchen);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 120;
                global.flag[34] = 0;
            }
            
            break;
        
        case "queen_battle":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_east_4f_d);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 160;
                global.flag[34] = 0;
            }
            
            break;
        
        case "spamton_neo_battle":
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_b_east_b);
            
            if (global.dbutton_selected == 2)
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
            global.dmenu_active = false;
            global.interact = 0;
            global.darkzone = 1;
            snd_free_all();
            room_goto(room_dw_mansion_east_4f_d);
            
            if (global.dbutton_selected == 2)
            {
                global.char[0] = 1;
                global.char[1] = 2;
                global.char[2] = 3;
                global.plot = 165;
                global.flag[34] = 0;
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
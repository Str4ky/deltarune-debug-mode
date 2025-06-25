global.dmenu_arrow_timer += 1;

if (global.dmenu_active)
{
    if (global.dbutton_layout == 0)
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
    
    if (keyboard_check_pressed(global.input_k[4]) || keyboard_check_pressed(global.input_k[7]))
    {
        snd_play(snd_select);
        array_push(global.dmenu_state_history, global.dmenu_state);
        array_push(global.dbutton_selected_history, global.dbutton_selected);
        
        if (global.dmenu_state != "objects" && global.dmenu_state != "armors" && global.dmenu_state != "weapons" && global.dmenu_state != "keyitems")
            scr_debug_print(string(global.dbutton_options[global.dbutton_selected - 1]) + " sélectionné !");
        
        dmenu_state_interact();
        global.dmenu_start_index = 0;
        global.dbutton_selected = 1;
        dmenu_state_update();
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
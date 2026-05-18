function scr_read_keyboard()
{
    var cur_text = global.dkeyboard_text;
    text_changed = 0;
    
    if (keyboard_check(vk_backspace))
    {
        if (keyboard_check_pressed(vk_backspace))
        {
            cur_text = string_delete(cur_text, string_length(cur_text), 1);
            keyboard_string = "";
        }
        
        text_changed = 1;
    }
    else if (keyboard_string != "")
    {
        cur_text += keyboard_string;
        keyboard_string = "";
        text_changed = 1;
    }
    
    global.dkeyboard_text = cur_text;
    return text_changed;
}

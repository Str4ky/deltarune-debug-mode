if (scr_debug())
{
    siner++;
    
    if (mouse_check_button(mb_right) && !i_ex(obj_debug_xy))
    {
        if (sprite_index != -1)
        {
            if (keyboard_check(vk_up))
                y -= 3;
            
            if (keyboard_check(vk_left))
                x -= 3;
            
            if (keyboard_check(vk_down))
                y += 3;
            
            if (keyboard_check(vk_right))
                x += 3;
        }
    }
}
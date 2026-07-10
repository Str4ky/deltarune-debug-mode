if (scr_debug())
{
    siner++;
    
    if (mouse_check_button(mb_right) && !i_ex(obj_debug_xy))
    {
        if (entity_gravity_bk == 0)
            entity_gravity_bk = entity_gravity;
        
        entity_gravity = 0;
        gravity = 0;
        wallcollision = 0;
        vspeed = 0;
        
        if (sprite_index != -1)
        {
            if (keyboard_check(vk_up))
                y -= 6;
            
            if (keyboard_check(vk_left))
                x -= 6;
            
            if (keyboard_check(vk_down))
                y += 6;
            
            if (keyboard_check(vk_right))
                x += 6;
        }
    }
    else
    {
        if (entity_gravity_bk != 0)
        {
            gravity = entity_gravity_bk;
            entity_gravity = entity_gravity_bk;
        }
        
        entity_gravity_bk = 0;
        wallcollision = 1;
    }
}
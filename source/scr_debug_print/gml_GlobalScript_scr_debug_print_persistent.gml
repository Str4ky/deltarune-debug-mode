function scr_debug_print_persistent(arg0, arg1)
{
    if (!scr_debug())
        exit;
    
    if (!instance_exists(obj_debug_gui_persistent))
    {
        instance_create(__view_get(e__VW.XView, 0) + 10, __view_get(e__VW.YView, 0) + 10, obj_debug_gui_persistent);
        obj_debug_gui_persistent.depth = -9999;
    }
    
    var search_key = string(arg0) + ":";
    var final_text = string(arg0) + ": " + string(arg1);
    
    with (obj_debug_gui_persistent)
    {
        var found = false;
        
        for (i = 0; i < messagecount; i++)
        {
            if (string_pos(search_key, message[i]) == 1)
            {
                message[i] = final_text;
                messagetimer[i] = 10;
                found = true;
                break;
            }
        }
        
        if (!found)
        {
            message[messagecount] = final_text;
            messagetimer[messagecount] = 10;
            messagecount++;
        }
        
        debugmessage = message[0];
        
        for (i = 1; i < messagecount; i++)
            debugmessage += ("#" + message[i]);
    }
}

function debug_print_bitmask_persistent(arg0, arg1, arg2)
{
}

function debug_print_persistent(arg0, arg1)
{
    scr_debug_print_persistent(arg0, arg1);
}

function scr_debug_delete_persistent(arg0, arg1 = false)
{
    if (!instance_exists(obj_debug_gui_persistent))
        exit;
    
    var search_key = string(arg0) + ":";
    
    with (obj_debug_gui_persistent)
    {
        var found_index = -1;
        
        for (i = 0; i < messagecount; i++)
        {
            if (string_pos(search_key, message[i]) == 1)
            {
                found_index = i;
                break;
            }
        }
        
        if (found_index != -1)
        {
            for (i = found_index; i < (messagecount - 1); i++)
            {
                message[i] = message[i + 1];
                messagetimer[i] = messagetimer[i + 1];
            }
            
            messagecount--;
            
            if (messagecount <= 0)
            {
                debugmessage = "";
                instance_destroy();
            }
            else
            {
                debugmessage = message[0];
                
                for (i = 1; i < messagecount; i++)
                    debugmessage += ("#" + message[i]);
            }
        }
    }
}

function scr_debug_clear_persistent()
{
}

enum e__VW
{
    XView,
    YView,
    WView,
    HView,
    Angle,
    HBorder,
    VBorder,
    HSpeed,
    VSpeed,
    Object,
    Visible,
    XPort,
    YPort,
    WPort,
    HPort,
    Camera,
    SurfaceID
}
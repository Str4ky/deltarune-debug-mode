if (global.dpause_dialog)
{
    if (button1_p())
    {
        talktimer = talkmax;
        
        with (obj_writer)
            instance_destroy();
        
        global.mnfight = 1.5;
    }
    
    exit;
}

if (arg0 >= 0)
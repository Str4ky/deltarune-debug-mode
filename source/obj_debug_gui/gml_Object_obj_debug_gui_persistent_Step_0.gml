if (messagecount <= 0)
{
    instance_destroy();
}
else
{
    var _rebuild = false;
    i = messagecount - 1;
    
    while (i >= 0)
    {
        messagetimer[i]--;
        
        if (messagetimer[i] <= 0)
        {
            for (var j = i; j < (messagecount - 1); j++)
            {
                message[j] = message[j + 1];
                messagetimer[j] = messagetimer[j + 1];
            }
            
            messagecount--;
            _rebuild = true;
        }
        
        i--;
    }
    
    if (messagecount <= 0)
    {
        instance_destroy();
        exit;
    }
    
    if (_rebuild)
    {
        debugmessage = message[0];
        
        for (i = 1; i < messagecount; i++)
            debugmessage += ("#" + message[i]);
    }
}
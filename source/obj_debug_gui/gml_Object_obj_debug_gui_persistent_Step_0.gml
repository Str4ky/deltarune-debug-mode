if (messagecount <= 0)
{
    instance_destroy();
}
else
{
    for (i = 0; i < (messagecount - 1); i++)
        message[i] = message[i + 1];
    
    messagecount--;
    
    if (messagecount <= 0)
    {
        instance_destroy();
        exit;
    }
    
    debugmessage = message[0];
    
    for (i = 1; i < messagecount; i++)
        debugmessage += ("#" + message[i]);
}
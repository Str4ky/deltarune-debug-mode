UndertaleScript scr_flag_set = Data.Scripts.ByName("scr_flag_set");
importGroup.QueueReplace(scr_flag_set.Code, @"
function scr_flag_set(arg0, arg1)
{
    global.flag[arg0] = arg1;
}

function scr_setflag(arg0, arg1)
{
    scr_flag_set(arg0, arg1);
}
");
ChangeSelection(scr_flag_set);

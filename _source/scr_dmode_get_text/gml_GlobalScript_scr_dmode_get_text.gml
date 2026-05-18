function scr_dmode_get_text(arg0)
{
    var _lang = global.dmode_lang;
    
    if (variable_global_exists("dmode_text"))
    {
        if (variable_struct_exists(global.dmode_text, _lang))
        {
            var _dict = variable_struct_get(global.dmode_text, _lang);
            
            if (variable_struct_exists(_dict, arg0))
                return variable_struct_get(_dict, arg0);
        }
    }
    
    return arg0;
}
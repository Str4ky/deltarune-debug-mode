function scr_qa_export(arg0, arg1)
{
    if (!variable_global_exists("qa_id_dictionary"))
        global.qa_id_dictionary = {};
    
    var _key = string(arg0);
    
    if (_key != "")
        variable_struct_set(global.qa_id_dictionary, _key, arg1);
}
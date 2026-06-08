if (!variable_instance_exists(id, "qa_tracked_string"))
    qa_tracked_string = "";

var _my_text = variable_instance_exists(id, "originalstring") ? originalstring : mystring;

if (_my_text != "" && _my_text != qa_tracked_string)
{
    qa_tracked_string = _my_text;
    
    if (variable_global_exists("qa_id_dictionary"))
    {
        var _current_id = variable_struct_get(global.qa_id_dictionary, _my_text);
        
        if (!is_undefined(_current_id))
        {
            if (!variable_global_exists("qa_last_id"))
                global.qa_last_id = "";
            
            if (_current_id != global.qa_last_id)
            {
                global.qa_last_id = _current_id;
                var _fc = variable_global_exists("fc") ? global.fc : 0;
                var _fe = variable_global_exists("fe") ? global.fe : 0;
                var _darkzone = variable_global_exists("darkzone") ? global.darkzone : 0;
                var _chapter = variable_global_exists("chapter") ? global.chapter : 0;
                var _plot = variable_global_exists("plot") ? global.plot : 0;
                var _flag = variable_global_exists("flag") ? global.flag : [];
                var _export_data = 
                {
                    dialogue_id: _current_id,
                    fc: _fc,
                    fe: _fe,
                    darkzone: _darkzone,
                    chapter: _chapter,
                    plot: _plot,
                    flag: _flag,
                    text_preview: _my_text
                };
                var _file = file_text_open_write("qa_live_context.json");
                
                if (_file != -1)
                {
                    file_text_write_string(_file, json_stringify(_export_data));
                    file_text_close(_file);
                }
            }
        }
    }
}
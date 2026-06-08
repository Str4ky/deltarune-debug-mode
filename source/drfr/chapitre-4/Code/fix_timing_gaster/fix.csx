Patcher.FindReplace(
"gml_Object_obj_credits_ch4_Create_0",
@"glowing_text[3][2] = 89.4;
glowing_text[4][1] = 90;
",
@"glowing_text[3][2] = 90.7;
glowing_text[4][1] = 91;
");

Patcher.FindReplace(
"gml_Object_obj_credits_ch4_Create_0",
"    var _writer = instance_create(70, 80, obj_writer);",
@"    _writer_x = 70;
    if (global.lang == ""en"" && alt_text_enabled && glowing_index == 1)
    {
        _writer_x = 40;
    }
    var _writer = instance_create(_writer_x, 80, obj_writer);
");

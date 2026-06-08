Patcher.FindReplace(
"gml_Object_DEVICE_CHOICE_Step_0",
@"if (TYPE > 0)
{
    if (fadebuffer < 0 && FINISH == 0)
",
@"if (TYPE > 0)
{
    var cmd = """";
    if (TYPE == 3 && string_length(NAME[CURX][CURY]) > 1)
    {
        cmd = string_char_at(NAME[CURX][CURY], 2);
    }
    if (fadebuffer < 0 && FINISH == 0)
"
);

Patcher.FindReplace(
"gml_Object_DEVICE_CHOICE_Step_0",
@"        else if (dy != 0 && YMAX > 0)
        {
            var found = false;
",
@"        else if (dy != 0 && YMAX > 0)
        {
            var found = false;
            if (cmd == ""B"" && global.lang == ""en"")
            {
                CURX++;
            }
"
);

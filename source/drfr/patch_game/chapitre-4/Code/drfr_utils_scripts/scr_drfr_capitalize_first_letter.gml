function scr_drfr_capitalize_first_letter(arg0)
{
    str = arg0;
    str2 = string_char_at(str, 1);
    str2 = string_upper(str2);
    str = string_delete(str, 1, 1);
    str = string_insert(str2, str, 1);
    return (str);
}

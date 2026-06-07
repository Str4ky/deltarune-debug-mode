if (scr_debug())
{
    if (show_debug)
    {
        var _key_data = "[Shift + -/+] Change volume: " + string(main_vol * 100) + "%";
        _key_data += "#[P] Pause song";
        _key_data += "#[R] Restart song";
        _key_data += "#[F5] End song";
        _key_data += "#[F6] Go to end screen";
        _key_data += ("#[I] Autoplay: " + (auto_play ? "on" : "off"));
        _key_data += ("#[U] Swap modes: " + string(tutorial));
        _key_data += "#[N] Toggle rhythm game debug";
        scr_debug_print_persistent("Debug keys", _key_data);
    }
}
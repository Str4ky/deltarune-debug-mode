if (scr_debug())
{
    scr_debug_print_persistent("Debug keys", "#[U] Toggle LOT debug helper: " + (show_debug ? "on" : "off"));

    if (keyboard_check_pressed(ord("U")))
    {
        show_debug = !show_debug;

        if (show_debug)
        {
            debug_print("Debug helper visible");
        }
        else
        {
            debug_print("Debug helper hidden");
        }
    }

    if (show_debug)
    {
        var _key_data = "#[U] Toggle LOT debug helper: " + (show_debug ? "on" : "off");
        _key_data += "#[1/2/3] Play as Kris/Susie/Ralsei";
        _key_data += "#[Shift + <arrow>] Move room";
        _key_data += "#[Ctrl + <arrow>] Move 1 tile";
        _key_data += "#[I] Make invulnerable: " + (invulnerable ? "on" : "off");
        _key_data += "#[Num +] Add 100 pts";
        _key_data += "#[Num -] Remove 100 pts";
        _key_data += "#[H] Drop everyone's PV to 1";
        _key_data += "#[F5] Remove chasing enemies";
        _key_data += "#[M] Stop every current sound";
        _key_data += "#[Backspace] Remove TV filter";
        _key_data += "#[Q] Add 1 Q to inventory (max 2)";
        _key_data += "#[K] Add 1 key to inventory (max 4)";
        _key_data += "#[W + K] Add 4 keys, stone and Lancer to inventory";

        if (room == room_board_1)
        {
            _key_data += "#[Shift + W] Dry desert";
            _key_data += "#[Shift + up] Allow Susie to grab";
            _key_data += "#[W + V] Go to quiz cactus room";
        }
        else if (room == room_board_2)
        {
            _key_data += "#[W + B] Create a boat";
            _key_data += "#[K] Add Lancer to inventory (max 1)";
            _key_data += "#[N] Add friendo to inventory";
            _key_data += "#[Shift + N] Change friendo: " + (global.flag[1017] ? "Lanino" : "Elnina");
            _key_data += "#[W + P] Go to Shuttah room";
            _key_data += "#[W + E] Go to Susie section";
            _key_data += "#[W + V] Go to Susie shop section";
            _key_data += "#[W + F] Go to Pippins fight Susie section";
            _key_data += "#[W + T] Make Tenna leave/come back";
        }
        else if (room == room_board_3)
        {
            _key_data += "#[K] Add Lancer to inventory";
            _key_data += "#[W + V] Go to the other side of water";
            _key_data += "#[W + R] Go to board end";
        }

        scr_debug_print_persistent("Debug keys", _key_data);
    }
}
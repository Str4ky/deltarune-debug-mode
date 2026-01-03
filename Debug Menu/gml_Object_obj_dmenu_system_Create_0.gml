global.dmenu_active = false;
global.dmenu_box = 0;
global.dbutton_layout = 0;
global.dmenu_start_index = 0;
global.dbutton_max_visible = 5;
global.dmenu_arrow_timer = 0;
global.dscroll_up_timer = 0;
global.dscroll_down_timer = 0;
global.dscroll_delay = 15;
global.dscroll_speed = 5;
global.dmenu_title = "Menu Debug";
global.dbutton_options_original = ["Sauts", "Items", "Recrues", "Autre"];
global.dbutton_options = global.dbutton_options_original;
global.dmenu_state = "debug";
global.dbutton_selected = 1;
global.dhorizontal_index = 0;
global.dmenu_state_history = [];
global.dbutton_selected_history = [];
global.dgiver_menu_state = 0;
global.dgiver_button_selected = 0;
global.dgiver_amount = 1;
global.dgiver_bname = 0;
global.ditemcount = 0;
global.darmorcount = 0;
global.dweaponcount = 0;
global.dkeyitemcount = 0;
global.dbutton_indices = [];
global.drecent_item = 0;
global.drecent_armor = 0;
global.drecent_weapon = 0;
global.drecent_keyitem = 0;
global.dother_options = [["Custom", 0, [["", 0]]], ["Nom du gang", 214, [["Les Types (unused)", 0], ["L'Escouade $?$!$", 1], ["Le Fan Club Lancer", 2], ["Le Fun Gang", 3]]], ["Tete Roboteur", 220, [["Laser", 0], ["Epee", 1], ["Flamme", 2], ["Canard", 3]]], ["Corps Roboteur", 221, [["Sobre", 0], ["Roue", 1], ["Tank", 2], ["Canard", 3]]], ["Jambes Roboteur", 222, [["Baskets", 0], ["Pneus", 1], ["Chaines", 2], ["Canard", 3]]], ["Gateau rendu", 253, [["Non", 0], ["Oui", 1]]], ["Starwalker", 254, [["Pissing me off", 0], ["I will   join", 1]]]];

if (global.chapter >= 2)
{
    array_push(global.dother_options, ["En weird route", 915, [["Pas fait", 0], ["A fait le debut", 3], ["IDK", 6], ["A finis", 15]]]);
    array_push(global.dother_options, ["A cancel la weird route", 916, [["Non", 0], ["Oui", 1]]]);
}

if (global.chapter >= 3)
{
    array_push(global.dother_options, ["LOT Rang Board 1", 1173, [["Z", 0], ["C", 1], ["B", 2], ["A", 3], ["S", 4], ["T", 5]]]);
    array_push(global.dother_options, ["LOT Rang Board 2", 1174, [["Z", 0], ["C", 1], ["B", 2], ["A", 3], ["S", 4], ["T", 5]]]);
}

global.dreading_custom_flag = 0;
global.dcustom_flag_text = ["", ""];

find_subarray_index = function(arg0, arg1)
{
    value = global.flag[arg0];
    lst = arg1;
    
    for (i = 0; i < array_length(lst); i++)
    {
        if (value == lst[i][1])
            return i;
    }
    
    return 0;
};

draw_monospace = function(arg0, arg1, arg2)
{
    draw_x = arg0;
    sep = 15;
    
    for (i = 0; i < string_length(arg2); i++)
    {
        draw_text(draw_x, arg1, string_char_at(arg2, i + 1));
        draw_x += sep;
    }
};


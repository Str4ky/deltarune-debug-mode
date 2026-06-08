var jp = variable_global_exists("lang") && global.lang == "ja";
if (pause != 1)
{
    timer += text_speed;
}
if (polite && timer < 30)
{
    timer += 0.3;
}
if (timer > (76 + (60 * (polite == 2))))
{
    instance_destroy();
}
if (!bright && !instance_exists(obj_ghosthouse_jackolantern) && !instance_exists(obj_dw_church_jackenstein))
{
    exit;
}
if (small && timer == (11 - max(ceil(obj_ghosthouse_jackolantern.timer / 11), 10 * (i_ex(obj_jackenstein_enemy) && obj_jackenstein_enemy.scaredycat))) && pause < 1)
{
    if (instance_number(object_index) < 27)
    {
        if ((instance_number(object_index) % 2) == 0)
        {
            doom = true;
        }
        var _x = lengthdir_x(9 + (2.4 * instance_number(object_index)) + random(9), (instance_number(object_index) + 74) * instance_number(object_index));
        var _y = lengthdir_y(7 + (1.7 * instance_number(object_index)), ((3.12 * instance_number(object_index)) + 74) * instance_number(object_index)) - 15;
        with (instance_create_depth(obj_ghosthouse_jackolantern.x + _x, obj_ghosthouse_jackolantern.y + _y, obj_heart.depth - instance_number(object_index), obj_bulletparent))
        {
            sprite_index = spr_small_jackolantern;
            snd_play(snd_small_jackolantern_appear);
            with (instance_create_depth(x - 40, y - 40, obj_heart.depth - 1000, obj_takingtoolong))
            {
                small = true;
            }
        }
    }
}
if (small && timer == 12 && pause == 0)
{
    pause = true;
    timer = 0;
}
if (small && obj_ghosthouse_jackolantern.timer >= 130 && pause == 1)
{
    pause = 2;
    timer = 0;
    fade = 1;
    if (i_ex(obj_jackenstein_enemy) && obj_jackenstein_enemy.scaredycat)
    {
        text_speed = 4;
    }
}
var str = "";
if ((i_ex(obj_jackenstein_enemy) && obj_jackenstein_enemy.scaredycat) && pause > 1 && tutu < 2)
{
    str = stringsetloc("LONG", "obj_takingtoolong_slash_Draw_0_gml_19_0");
}
else if (!jp && bright)
{
    str = "TU";
    if (timer > 8)
    {
        str += " ÉMET";
    }
    if (timer > 20)
    {
        str += " TRO";
    }
    if (timer > 46)
    {
        str += "\nDE";
    }
    if (timer > 55)
    {
        str += " LUMIAIRE";
    }
}
else if (!jp && tutu == 1)
{
    str = "";
    if (timer > 4)
    {
        str += "TU";
    }
    if (timer > 11)
    {
        str += " MET";
    }
    if (timer > 20)
    {
        str += " TON";
    }
    if (timer > 29)
    {
        str += " TU";
    }
    if (timer > 35)
    {
        str += " TU";
    }
}
else if (!jp && tutu == 2)
{
    str = "";
    if (timer > 4)
    {
        str += "TON";
    }
    if (timer > 18)
    {
        str += "       ";
    }
    if (timer > 20)
    {
        str += " TU";
    }
    if (timer > 27)
    {
        str += " TU";
    }
}
var str2 = stringsetloc("TU ", "obj_takingtoolong_slash_Draw_0_gml_25_0");
if (jp && tutu == 1)
{
    str = "カ";
    if (timer > 9)
    {
        str += "　ワ";
    }
    if (timer > 19)
    {
        str += "　イ";
    }
    if (timer > 29)
    {
        str += "　す";
    }
    if (timer > 39)
    {
        str += "ぎ";
    }
}
else if (jp && tutu == 2)
{
    text_speed = 2;
    str = "す";
    if (timer > 15)
    {
        str += "き";
    }
    if (timer > 30)
    {
        str += "　　　　す";
    }
    if (timer > 45)
    {
        str += "き";
    }
}
else if (jp && !bright && !tutu && (!small || (pause > 1 && timer > 0)))
{
    str = "ナ";
    str2 = str;
    if (timer > 12)
    {
        str += "ガ";
    }
    if (timer > 20)
    {
        str += "イ";
    }
    if (timer > 32)
    {
        str += " シ";
    }
    if (timer > 39)
    {
        str += "す";
    }
    if (timer > 46)
    {
        str += "ぎ";
    }
}
else if (jp && bright)
{
    str = "マ";
    if (timer > 10)
    {
        str += " ブ";
    }
    if (timer > 20)
    {
        str += " シ";
    }
    if (timer > 30)
    {
        str += " イ";
    }
    if (timer > 38)
    {
        str += " す";
    }
    if (timer > 46)
    {
        str += "ぎ";
    }
}
else if (!jp && !bright && !tutu && !polite && !small)
{
    str = "";
    if (timer > 6)
    {
        str += "TU";
    }
    if (timer > 12)
    {
        str += " MET";
    }
    if (timer > 20)
    {
        str += " TRO";
    }
    if (timer > 30)
    {
        str += " LONGTEMPS";
    }
}
else if (!jp && small)
{
    str = "";
    if (timer > 4 && pause != 0)
    {
        str += "TU";
    }
    if (timer > 12)
    {
        str += " MET";
    }
    if (timer > 20)
    {
        str += " TRO";
    }
    if (timer > 30)
    {
        str += " LONGTEMPS";
    }
}
else if (!jp && polite)
{
    str = "";
    if (timer > 5)
    {
        str = "TU";
    }
    if (timer > 13)
    {
        str += " MET";
    }
    if (timer > 22)
    {
        str += " TRO";
    }
    if (timer > 28)
    {
        str += " LONGTEMPS";
    }
}
if (!jp && polite == 2)
{
    str2 = "";
    if (timer > 69)
    {
        str2 += "    TRO";
    }
    if (timer > 75)
    {
        str2 += " LONGTEMPS";
    }
}
if (doom && pause == 2)
{
    instance_destroy();
}
draw_set_alpha(1);
draw_set_color(c_white);
if (jp)
{
    if (small)
    {
        draw_set_font(fnt_ja_main);
    }
    else
    {
        draw_set_font(fnt_ja_mainbig);
    }
}
else if (small)
{
    draw_set_font(fnt_main);
}
else
{
    draw_set_font(fnt_mainbig);
}
shake_factor = 6;
if (!jp && tutu == 1)
{
    shake_factor = 7;
}
if (!jp && bright)
{
    shake_factor = 10;
}
var num = (!polite * max(0, (string_length(str) / (shake_factor - (3.5 * jp))) - 1)) / (1 + small);
draw_set_alpha(fade);
y_offset = 0;
x_offset = 0;
for (var pos = 1; pos <= string_length(str); pos++)
{
    x_offset += ((15 - (8 * small)) * (1 + jp));
    if (string_char_at(str, pos) == "\n")
    {
        y_offset += 30;
        x_offset = 0;
    }
    draw_text(x + irandom_range(-num, num) + x_offset, y + y_offset + irandom_range(-num, num), string_char_at(str, pos));
}
if (polite == 2 && timer > (49 + (7 * !jp)))
{
    draw_text((x + 152) - (32 * jp), y + 30, jp ? "ニ" : stringsetloc("MET", "obj_takingtoolong_slash_Draw_0_gml_126_0"));
}
if (polite == 2 && timer > 53)
{
    var pos = 1 + (4 * !jp);
    while (pos <= string_length(str2))
    {
        draw_text(x + (15 * pos * (1 + jp)), y + 60, string_char_at(str2, pos));
        pos++;
    }
}
draw_set_alpha(1);

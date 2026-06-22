if instance_exists(obj_mainchara)
    checkX = (obj_mainchara.x + 20)
timer += 2
c_rainbow = make_color_hsv((timer % 255), 255, 255)
curColor = merge_color(merge_color(c_white, c_rainbow, 0.5), c_black, 0.2)
draw_set_color(curColor)
if (createAndStay == 1)
{
    var i = (array_length(coords) - 1)
    while (i >= 0)
    {
        if (checkX >= coords[i])
        {
            newcount = (i + 1)
            break
        }
        else
        {
            i--
            continue
        }
    }
    if (count < newcount)
        count = newcount
    if (count == 16)
    {
        if (global.plot < 67)
            global.plot = 67
    }
}
for (i = 0; i < array_length(coords); i++)
{
    if (createAndStay == 0 || count > i)
        draw_rectangle(coords[i], 110, (coords[i] + sizes[i]), 220, false)
}
draw_set_color(c_white)

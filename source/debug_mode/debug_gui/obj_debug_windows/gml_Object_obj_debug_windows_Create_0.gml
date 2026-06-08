xx = mouse_x - camerax() - 40;
yy = mouse_y - cameray() - 20;
xx = clamp(xx, -30, 500);
yy = clamp(yy, -5, 340);
type = 0;
button_text[0] = "Drag Window!";
event_user(15);
watchvar = " ";
watchflag = -1;

for (i = 0; i < button_amount; i++)
{
    button_state[i] = 0;
    button_clicked[i] = 0;
}

remmx = mouse_x - camerax();
remmy = mouse_y - cameray();
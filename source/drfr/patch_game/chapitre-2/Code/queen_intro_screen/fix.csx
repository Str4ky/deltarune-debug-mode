Patcher.FindReplace(
"gml_Object_obj_ch2_scene9_Alarm_5",
"    screen[screencount] = instance_create((screenx[screencount] * 2) + camerax() + x_offset, (screeny[screencount] * 2) + cameray(), obj_queenscreen);",
@"    if (global.lang == ""en"" && screencount == 9)
	screen[screencount] = instance_create((((screenx[screencount] * 2) + camerax()) + 10), ((screeny[screencount] * 2) + cameray()), obj_queenscreen);
else
	screen[screencount] = instance_create((((screenx[screencount] * 2) + camerax()) + x_offset), ((screeny[screencount] * 2) + cameray()), obj_queenscreen);
"
);

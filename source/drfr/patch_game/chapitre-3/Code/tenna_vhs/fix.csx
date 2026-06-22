Patcher.FindReplace(
	"gml_Object_obj_ch3_couch_video_Create_0",
	@"""obj_ch3_couch_video_slash_Create_0_gml_12_0""), 0.63 + offset, 0.67 + offset)",
	@"""obj_ch3_couch_video_slash_Create_0_gml_12_0""), 0.63 + offset, 0.68 + offset)"
);

Patcher.FindReplace(
	"gml_Object_obj_ch3_couch_video_Create_0",
	"0.68 + offset, 0.73 + offset)];",
	"0.69 + offset, 0.74 + offset)];"
);

Patcher.FindReplace(
"gml_Object_obj_ch3_couch_video_Create_0",
@"text_index = 0;
house_index = (global.lang == ""ja"") ? 3 : 2;
",
@"else
{
    array_insert(text_list, 1, new scr_video_caption(""Prinspal"", 0.17 + offset, 0.21 + offset));
    array_insert(text_list, 5, new scr_video_caption(""Le Roi de... Unique"", 0.4 + offset, 0.43 + offset));
}
text_index = 0;
house_index = 3;
");


Patcher.FindReplace(
"gml_Object_obj_ch3_couch_video_Draw_0",
@"        else
        {
            scr_84_set_draw_font(""mainbig"");
            draw_set_halign(fa_center);
            draw_set_color(c_black);
            draw_text(camerax() + (view_wport[0] / 2) + 2, ((cameray() + view_hport[0]) - 60) + 2, text_list[text_index].caption_text);
            draw_text(camerax() + (view_wport[0] / 2) + 4, ((cameray() + view_hport[0]) - 60) + 4, text_list[text_index].caption_text);
            draw_set_color(c_yellow);
            draw_text_outline(camerax() + (view_wport[0] / 2), (cameray() + view_hport[0]) - 60, text_list[text_index].caption_text, 8388608);
            draw_set_color(c_white);
            draw_set_halign(fa_left);
        }
",
@"        else
        {
		    drfr_offset = (global.lang == ""en"") ? 75 : 60;
            scr_84_set_draw_font(""mainbig"");
            draw_set_halign(fa_center);
            draw_set_color(c_black);
            draw_text(camerax() + (view_wport[0] / 2) + 2, ((cameray() + view_hport[0]) - drfr_offset) + 2, text_list[text_index].caption_text);
            draw_text(camerax() + (view_wport[0] / 2) + 4, ((cameray() + view_hport[0]) - drfr_offset) + 4, text_list[text_index].caption_text);
            draw_set_color(c_yellow);
            draw_text_outline(camerax() + (view_wport[0] / 2), (cameray() + view_hport[0]) - drfr_offset, text_list[text_index].caption_text, 8388608);
            draw_set_color(c_white);
            draw_set_halign(fa_left);
        }
"
);

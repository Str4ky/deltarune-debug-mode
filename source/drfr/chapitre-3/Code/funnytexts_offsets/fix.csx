//Pre VHS AMAZING
if (1 == 2) {
Patcher.FindReplace(
	"gml_Object_obj_ch3_PGS01F_Step_0",
	@"scr_funnytext_init(0, 8, -58, scr_84_get_sprite(""spr_funnytext_amazing_01""), 0, 0);",
	@"scr_funnytext_init(0, -24, -45, scr_84_get_sprite(""spr_funnytext_amazing_01""), 0, 0);"
);

//Pre VHS TEARS
Patcher.FindReplace(
	"gml_Object_obj_ch3_PGS01F_Step_0",
	@"scr_funnytext_init(1, 0, 0, scr_84_get_sprite(""spr_funnytext_tears""), 0, 0);",
	@"scr_funnytext_init(1, -20, 0, scr_84_get_sprite(""spr_funnytext_tears""), 0, 0);"
);

//Pre VHS TEARS 2 (????)
Patcher.FindReplace(
	"gml_Object_obj_ch3_PGS01F_Step_0",
	@"scr_funnytext_init(2, 0, 0, scr_84_get_sprite(""spr_funnytext_tears""), 0, 0);",
	@"scr_funnytext_init(2, -20, 0, scr_84_get_sprite(""spr_funnytext_tears""), 0, 0);"
);

//Intro Tenna LOVELY
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSA01G_Step_0",
	@"scr_funnytext_init(0, 0, -16, scr_84_get_sprite(""spr_funnytext_lovely""), 0, 0);",
	@"scr_funnytext_init(0, -40, -14, scr_84_get_sprite(""spr_funnytext_lovely""), 0, 0);"
);

//Intro Tenna PHYSICAL CHALLENGE
Patcher.FindReplace(
"gml_Object_obj_ch3_GSA01G_Step_0",
@"    scr_funnytext_init(3, 0, y_offset, scr_84_get_sprite(""spr_funnytext_physical_challenges""), 0, 0);",
@"    var drfr_x_offset = (global.lang == ""ja"") ? 0 : -20;
    scr_funnytext_init(3, drfr_x_offset, y_offset, scr_84_get_sprite(""spr_funnytext_physical_challenges""), 0, 0);"
);

//Intro Tenna PRIZES
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSA01G_Step_0",
	@"scr_funnytext_init(4, 0, -14, scr_84_get_sprite(""spr_funnytext_prizes""), 0, 0);",
	@"scr_funnytext_init(4, 18, -16, scr_84_get_sprite(""spr_funnytext_prizes""), 0, 0);"
);

//Intro Tenna GRAND PRIZE
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSA01G_Step_0",
	@"scr_funnytext_init(5, 0, y_offset, scr_84_get_sprite(""spr_funnytext_grand_prize""), 0, 0);",
	@"scr_funnytext_init(5, 1, y_offset, scr_84_get_sprite(""spr_funnytext_grand_prize""), 0, 0);"
);

//Intro Tenna fix 2nd GRAND PRIZE
Patcher.FindReplace(
"gml_Object_obj_ch3_GSA01G_Step_0",
@"    c_msgsetloc(0, <string>, ""obj_ch3_GSA01G_slash_Step_0_gml_486_0"");",
@"    scr_funnytext_init(6, 1, y_offset - 2, scr_84_get_sprite(""spr_funnytext_grand_prize""), 0, 0);
    c_msgsetloc(0, ""* Exact ^1! Le \\O6du jour est l'objet le PLUS convoité de cette saison.../%"", ""obj_ch3_GSA01G_slash_Step_0_gml_486_0"");"
);

//Intro Tenna WIN
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSA01G_Step_0",
	@"scr_funnytext_init(7, 0, -10, scr_84_get_sprite(""spr_funnytext_win_big""), 0, 0);",
	@"scr_funnytext_init(7, -100, -20, scr_84_get_sprite(""spr_funnytext_win_big""), 0, 0);"
);

//Intro Tenna FUN-O-METER
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSA01G_Step_0",
	@"scr_funnytext_init(8, -10, -10, scr_84_get_sprite(""spr_funnytext_fun_o_meter""), 0, 0);",
	@"scr_funnytext_init(8, -10, -15, scr_84_get_sprite(""spr_funnytext_fun_o_meter""), 0, 0);"
);

//Intro Tenna coffee
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSA01G_Step_0",
	@"scr_funnytext_init(0, 0, 0, scr_84_get_sprite(""spr_funnytext_coffee""), 0, 0);",
	@"scr_funnytext_init(0, -10, 0, scr_84_get_sprite(""spr_funnytext_coffee""), 0, 0);"
);

//Board 1 intro STARS
Patcher.FindReplace(
"gml_Object_obj_ch3_GSA02_Step_0",
@"    var y_offset = (global.lang == ""ja"") ? 0 : -10;
    scr_funnytext_init(0, 0, y_offset, star_text, 0, 0);
",
@"    var y_offset = (global.lang == ""ja"") ? 0 : 2;
    var drfr_x_offset = (global.lang == ""ja"") ? 0 : -10;
    scr_funnytext_init(0, 0, y_offset, star_text, 0, 0);
");

//Board 1 intro NAMEs
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSA02_Step_0",
	@"scr_funnytext_init(1, 0, -10, scr_84_get_sprite(""spr_funnytext_names""), 0, 0);",
	@"scr_funnytext_init(1, 10, -10, scr_84_get_sprite(""spr_funnytext_names""), 0, 0);"
);

//Board 1 intro BOARD
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSA02_Step_0",
	@"scr_funnytext_init(2, 0, 0, scr_84_get_sprite(""spr_funnytext_board""), 0, 0);",
	@"scr_funnytext_init(2, 7, 5, scr_84_get_sprite(""spr_funnytext_board""), 0, 0);"
);

//Board 1 intro LOVE
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSA02_Step_0",
	@"scr_funnytext_init(3, 0, -14, scr_84_get_sprite(""spr_funnytext_love""), 0, 0);",
	@"scr_funnytext_init(3, -10, -11, scr_84_get_sprite(""spr_funnytext_love""), 0, 0);"
);

//Board 1 intro QUIZZES
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSA02_Step_0",
	@"scr_funnytext_init(4, 0, -10, scr_84_get_sprite(""spr_funnytext_quizzes""), 0, 0);",
	@"scr_funnytext_init(4, 35, -5, scr_84_get_sprite(""spr_funnytext_quizzes""), 0, 0);"
);

// "CHALLENGE" offset chef gameover
Patcher.FindReplace(
	"gml_Object_obj_gameover_chef_Step_0",
	@"scr_funnytext_init(0, 0, 0, scr_84_get_sprite(""spr_funnytext_challenge""), 0, 0);",
	@"scr_funnytext_init(0, 0, -10, scr_84_get_sprite(""spr_funnytext_challenge""), 0, 0);"
);

//Green room GREEN ROOM
Patcher.FindReplace(
	"gml_Object_obj_room_green_room_Step_0",
	@"scr_funnytext_init(0, 0, y_offset, scr_84_get_sprite(""spr_funnytext_green_room""), 0, 0);",
	@"scr_funnytext_init(0, 30, y_offset, scr_84_get_sprite(""spr_funnytext_green_room""), 0, 0);"
);

//Green room Enjoy and Relax
Patcher.FindReplace(
	"gml_Object_obj_room_green_room_Step_0",
	@"scr_funnytext_init(1, 0, -20, scr_84_get_sprite(""spr_funnytext_relax""), 0, 0);",
	@"scr_funnytext_init(1, 5, -5, scr_84_get_sprite(""spr_funnytext_relax""), 0, 0);"
);

//Intro board 2 DARK FOUNTAIN
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSB01_Step_0",
	@"scr_funnytext_init(1, -20, -30, scr_84_get_sprite(""spr_funnytext_dark_fountain""), 0, 0);",
	@"scr_funnytext_init(1, -15, -30, scr_84_get_sprite(""spr_funnytext_dark_fountain""), 0, 0);"
);

//Intro board 2 BOARD
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSB01_Step_0",
	@"scr_funnytext_init(2, 0, 0, scr_84_get_sprite(""spr_funnytext_board""), 0, 0);",
	@"scr_funnytext_init(2, -6, -6, scr_84_get_sprite(""spr_funnytext_board""), 0, 0);"
);

//Intro board 2 TAN
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSB01_Step_0",
	@"scr_funnytext_init(3, 0, 0, scr_84_get_sprite(""spr_funnytext_tan""), 0, 0);",
	@"scr_funnytext_init(3, 50, -20, scr_84_get_sprite(""spr_funnytext_tan""), 0, 0);"
);

//Intro board 2 LOVERS
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSB01_Step_0",
	@"scr_funnytext_init(4, 0, -10, scr_84_get_sprite(""spr_funnytext_lovers""), 0, 0);",
	@"scr_funnytext_init(4, -5, -8, scr_84_get_sprite(""spr_funnytext_lovers""), 0, 0);"
);

//Intro board 2 GENTLE
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSB01_Step_0",
	@"scr_funnytext_init(5, 0, 0, scr_84_get_sprite(""spr_funnytext_gentle""), 0, 0);",
	@"scr_funnytext_init(5, -50, 0, scr_84_get_sprite(""spr_funnytext_gentle""), 0, 0);"
);

// Cars dont have feet
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSC07_Step_0",
	@"scr_funnytext_init(0, 0, 0, scr_84_get_sprite(""spr_funnytext_city_feet""), 0, 0);",
	@"scr_funnytext_init(0, -65, 0, scr_84_get_sprite(""spr_funnytext_city_feet""), 0, 0);"
);

//Concert de rock CONCERT
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSB02_Step_0",
	@"scr_funnytext_init(0, 0, -14, scr_84_get_sprite(""spr_funnytext_rock_concert""), 0, 0);",
	@"scr_funnytext_init(0, -40, -30, scr_84_get_sprite(""spr_funnytext_rock_concert""), 0, 0);"
);

//Concert de rock aligator (????????)
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSB02_Step_0",
	@"scr_funnytext_init(1, 0, 0, scr_84_get_sprite(""spr_funnytext_alligator""), 0, 0);",
	@"scr_funnytext_init(1, -20, -5, scr_84_get_sprite(""spr_funnytext_alligator""), 0, 0);"
);

//Susiezilla PHYSICAL CHALLENGE
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSC05_Step_0",
	@"scr_funnytext_init(0, 0, -20, scr_84_get_sprite(""spr_funnytext_physical_challenge""), 0, 0);",
	@"scr_funnytext_init(0, 0, -15, scr_84_get_sprite(""spr_funnytext_physical_challenge""), 0, 0);"
);

//Susiezilla FLAMES
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSC05_Step_0",
	@"scr_funnytext_init(1, 0, -10, scr_84_get_sprite(""spr_funnytext_flames""), 0, 0);",
	@"scr_funnytext_init(1, 0, -30, scr_84_get_sprite(""spr_funnytext_flames""), 0, 0);"
);

//ROUND 1
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSA06_Step_0",
	"scr_funnytext_init(2, 0, -14, 4464, 0, 0);",
	"scr_funnytext_init(2, -10, 0, 4464, 0, 0);"
);

//Il y a 2 MANCHES    ROUND(S)
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSA06_Step_0",
	@"scr_funnytext_init(3, 0, -14, round_sprite, 0, 0);",
	@"scr_funnytext_init(3, -10, 0, round_sprite, 0, 0);"
);

//UNE seule MANCHE   ROUND(S)
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSA06_Step_0",
	"scr_funnytext_init(4, 0, -16, 3055, 0, 0);",
	"scr_funnytext_init(4, -13, -2, 3055, 0, 0);"
);

//Trust me I know TV
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSA06_Step_0",
	@"scr_funnytext_init(5, 0, 0, scr_84_get_sprite(""spr_funnytext_know_tv""), 0, 0);",
	@"scr_funnytext_init(5, -70, 0, scr_84_get_sprite(""spr_funnytext_know_tv""), 0, 0);"
);

//A word from our sponsor
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSA06_Step_0",
	@"scr_funnytext_init(6, 0, -10, scr_84_get_sprite(""spr_funnytext_word""), 0, 0);",
	@"scr_funnytext_init(6, 23, -5, scr_84_get_sprite(""spr_funnytext_word""), 0, 0);"
);

//Your mother LOVE watching chef game
Patcher.FindReplace(
"gml_Object_obj_ch3_GSA04_Step_0",
@"scr_funnytext_init(1, 0, -14, scr_84_get_sprite(""spr_ja_funnytext_daisuki""), 0, 0);",
@"    if (global.lang == ""en"")
    {
        scr_funnytext_init(1, -70, -42, scr_84_get_sprite(""spr_funnytext_adore""), 0, 0);
    }
    else
    {
        scr_funnytext_init(1, 0, -14, scr_84_get_sprite(""spr_ja_funnytext_daisuki""), 0, 0);
    }
"
);

//Remember your BROTHER
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSA04_Step_0",
	@"scr_funnytext_init(2, 0, -10, scr_84_get_sprite(""spr_funnytext_brother""), 0, 0);",
	@"scr_funnytext_init(2, 20, -15, scr_84_get_sprite(""spr_funnytext_brother""), 0, 0);"
);

//BIG chef game
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSA04_Step_0",
	@"scr_funnytext_init(1, 0, -14, 1272, 0, 0);",
	@"scr_funnytext_init(1, -25, -14, 1272, 0, 0);"
);

//WIN chef game
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSA04_Step_0",
	@"scr_funnytext_init(2, 0, -14, scr_84_get_sprite(""spr_funnytext_win""), 0, 0);",
	@"scr_funnytext_init(2, -60, -11, scr_84_get_sprite(""spr_funnytext_win""), 0, 0);"
);

//CHALLENGE rhythm game over
Patcher.FindReplace(
	"gml_Object_obj_gameover_band_Step_0",
	@"scr_funnytext_init(0, 0, 0, scr_84_get_sprite(""spr_funnytext_challenge""), 0, 0);",
	@"scr_funnytext_init(0, 0, -10, scr_84_get_sprite(""spr_funnytext_challenge""), 0, 0);"
);

//Post board 2 BOARD
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSB03_Step_0",
	@"scr_funnytext_init(0, 0, 0, scr_84_get_sprite(""spr_funnytext_board""), 0, 0);",
	@"scr_funnytext_init(0, 0, 7, scr_84_get_sprite(""spr_funnytext_board""), 0, 0);"
);

//Post board 2 BONUS ROUND
Patcher.FindReplace(
	"gml_Object_obj_ch3_GSB03_Step_0",
	"scr_funnytext_init(3, -10, -14, 4487, 0, 0);",
	"scr_funnytext_init(3, -23, 0, 4487, 0, 0);"
);

//HOPE-O-METER
Patcher.FindReplace(
	"gml_Object_obj_dw_tv_funometer_Draw_0",
	"draw_sprite_ext(spr_hopeometer_hope, hope_anim, x - 14, y + 170, image_xscale * 2, image_yscale * 2, image_angle, image_blend, image_alpha);",
	"draw_sprite_ext(spr_hopeometer_hope, hope_anim, x, y + 187, image_xscale * 1.5, image_yscale * 2, image_angle, image_blend, image_alpha);"
);

//Post Tenna figt HALL OF FAME
Patcher.FindReplace(
	"gml_Object_obj_ch3_PTB01_Step_0",
	@"scr_funnytext_init(0, 0, 0, scr_84_get_sprite(""spr_funnytext_hall_of_fame""), 0, 0);",
	@"scr_funnytext_init(0, 24, -3, scr_84_get_sprite(""spr_funnytext_hall_of_fame""), 0, 0);"
);
}

Patcher.FindReplace(
"gml_GlobalScript_scr_monstersetup",
@"""scr_monstersetup_slash_scr_monstersetup_gml_2288_0"");
        global.actsimulsus[myself][0] = 1;
",
@"""scr_monstersetup_slash_scr_monstersetup_gml_2288_0"");
        global.actsimulsus[myself][0] = 0;
");

Patcher.FindReplaceFile(
	"gml_Object_obj_holywatercooler_enemy_Step_0",
	"holy_find",
	"holy_replace"
);

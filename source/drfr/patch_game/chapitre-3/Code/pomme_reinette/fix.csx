Patcher.FindReplace(
"gml_GlobalScript_scr_quiztext",
@"            spritex = 200;
            spritey = 100;
            correctanswer = 3;
            answeroption[0] = stringsetloc(<string>, ""scr_quiztext_slash_scr_quiztext_gml_301_0"");
            answeroption[1] = stringsetloc(<string>, ""scr_quiztext_slash_scr_quiztext_gml_302_0"");
            answeroption[2] = stringsetloc(<string>, ""scr_quiztext_slash_scr_quiztext_gml_303_0"");
            answeroption[3] = stringsetloc(<string>, ""scr_quiztext_slash_scr_quiztext_gml_304_0"");
",
@"            spritex = 250;
            spritey = 100;
            correctanswer = 0;
            answeroption[0] = stringsetloc(""PIPPOINTS"", ""scr_quiztext_slash_scr_quiztext_gml_301_0"");
            answeroption[1] = stringsetloc(""DES"", ""scr_quiztext_slash_scr_quiztext_gml_302_0"");
            answeroption[2] = stringsetloc(""DE-BILE"", ""scr_quiztext_slash_scr_quiztext_gml_303_0"");
            answeroption[3] = stringsetloc(""POMME REINETTE"", ""scr_quiztext_slash_scr_quiztext_gml_304_0"");
");

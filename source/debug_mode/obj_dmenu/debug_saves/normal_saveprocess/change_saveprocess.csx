Patcher.FindReplace(
"gml_GlobalScript_scr_saveprocess",
@"file = ""filech"" + string(global.chapter) + ""_"" + string(arg0);",
@"if (global.debug_saving == 1)
        file = ""debug_save/filech"" + string(global.chapter) + ""_"" + string(arg0);
    else
        file = ""filech"" + string(global.chapter) + ""_"" + string(arg0);
");

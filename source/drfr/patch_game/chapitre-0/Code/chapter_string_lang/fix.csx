Patcher.FindReplace(
"gml_Object_obj_ui_chapter_Create_0",
"    var chapter_text = <string> + string(_chapter);",
@"    var chapter_loc = (global.lang == ""en"") ? ""Chapitre "" : ""Chapter "";
    var chapter_text = chapter_loc + string(_chapter);"
);

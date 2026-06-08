Patcher.WriteGMLFile("gml_Object_obj_writer_Step_0", "obj_writer_step.gml");
Patcher.WriteGMLFile("gml_GlobalScript_scr_qa_export", "scr_qa_export.gml");

Patcher.FindReplace(
	"gml_GlobalScript_msgsetloc",
	"msgset(msg_index, str);",
	"msgset(msg_index, str);scr_qa_export(str, localized_string_id);"
);

Patcher.FindReplace(
	"gml_GlobalScript_msgnextloc",
	"msgnext(str);",
	"msgnext(str);scr_qa_export(str, localized_string_id);"
);

Patcher.FindReplace(
	"gml_GlobalScript_stringsetloc",
	"return stringset(str);",
	"scr_qa_export(str, argument[1]);return stringset(str);"
);

Patcher.FindReplace(
	"gml_GlobalScript_msgsetsubloc",
	"msgset(msg_index, str);",
	"msgset(msg_index, str);scr_qa_export(str, argument[argument_count - 1]);"
);

Patcher.FindReplace(
	"gml_GlobalScript_msgnextsubloc",
	"msgnext(str);",
	"msgnext(str);scr_qa_export(str, argument[argument_count - 1]);"
);

Patcher.FindReplace(
	"gml_GlobalScript_stringsetsubloc",
	"return stringset(str);",
	"scr_qa_export(str, argument[argument_count - 1]);return stringset(str);"
);

Patcher.FindReplace(
	"gml_GlobalScript_c_msgsetloc",
	"c_msgset(msg_index, str);",
	"c_msgset(msg_index, str);scr_qa_export(str, localized_string_id);"
);

Patcher.FindReplace(
	"gml_GlobalScript_c_msgnextloc",
	"c_msgnext(str);",
	"c_msgnext(str);scr_qa_export(str, localized_string_id);"
);

Patcher.FindReplace(
	"gml_GlobalScript_c_msgsetsubloc",
	"c_msgset(msg_index, str);",
	"c_msgset(msg_index, str);scr_qa_export(str, argument[argument_count - 1]);"	
);

Patcher.FindReplace(
	"gml_GlobalScript_c_msgnextsubloc",
	"c_msgnext(str);",
	"c_msgnext(str);scr_qa_export(str, argument[argument_count - 1]);"
);

Patcher.FindReplace(
"gml_Object_obj_npc_sign_Other_10",
@"msgsetsubloc(0, <string>, total_checkmarks, ""obj_npc_sign_slash_Other_10_gml_770_0"");",
@"if (total_checkmarks < 2)
	msgsetsubloc(0, ""* Selon cyber^1, vous avez trouvé ~1 coche bleue sur 3./"", total_checkmarks, ""obj_npc_sign_slash_Other_10_gml_770_0"");
else
	msgsetsubloc(0, ""* Selon cyber^1, vous avez trouvé ~1 coches bleues sur 3./"", total_checkmarks, ""obj_npc_sign_slash_Other_10_gml_770_0"");"
);

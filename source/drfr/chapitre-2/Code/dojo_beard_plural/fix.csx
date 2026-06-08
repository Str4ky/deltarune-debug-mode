Patcher.FindReplace(
"gml_Object_obj_dojo_spareenemy_Step_0",
@"msgsetsubloc(0, <string>, string(beardcount), ""obj_dojo_spareenemy_slash_Step_0_gml_44_0"");",
@"if (beardcount == 1)
	barbe = ""\\M2Ah !! C'est ma&barbe de ~1 jour !/%""
else
	barbe = ""\\M2Ah !! C'est ma&barbe de ~1 jours !/%""
msgsetsubloc(0, barbe, string(beardcount), ""obj_dojo_spareenemy_slash_Step_0_gml_44_0"");"
);

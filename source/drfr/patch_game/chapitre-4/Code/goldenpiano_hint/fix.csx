Patcher.FindReplace(
"gml_Object_obj_dw_church_secretpiano_Step_0",
@"        msgsetloc(0, <string>, ""obj_dw_church_secretpiano_slash_Step_0_gml_209_0"");",
@"        if (global.lang == ""ja"")
        {
            msgsetloc(0, ""* Trois mélodies^1, il y a.&* Deux résident aux tours du sud.&* Une où l'or heurte l'eau./%"", ""obj_dw_church_secretpiano_slash_Step_0_gml_209_0"");
        }
        else
        {
            msgsetloc(0, ""* La mélodie entière^1, est en trois tiers./"", ""obj_dw_church_secretpiano_slash_Step_0_gml_209_0"");
            msgnextloc(""* Deux au sud^1, au sommet de tours escarpées./"", ""obj_dw_church_secretpiano_slash_Step_0_gml_209_0"");
            msgnextloc(""* L'autre où l'eau^1, avec l'or s'est faite frappée./%"", ""obj_dw_church_secretpiano_slash_Step_0_gml_209_0"");
        }
");

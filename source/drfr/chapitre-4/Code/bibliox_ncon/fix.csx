Patcher.FindReplace(
"gml_Object_obj_dw_church_intro_guei_Step_0",
@"            if (talked)
            {
                other.ncon = 1;
",
@"            if (talked)
            {
                other.ncon = (global.lang == ""ja"") ? 1 : 0;
");

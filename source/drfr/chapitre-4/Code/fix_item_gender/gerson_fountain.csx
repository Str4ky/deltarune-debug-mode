Patcher.FindReplace(
"gml_Object_obj_gerson_fountain_Draw_0",
@"    draw_text(camerax() + 460, cameray() + y1_off, string_hash_to_newline(stringsetloc(<string>, ""obj_gerson_fountain_slash_Draw_0_gml_505_0"")));
    draw_text(camerax() + 460, cameray() + y2_off, string_hash_to_newline(stringsetsubloc(<string>, string(sellvalue), ""obj_shop_vending_slash_Draw_0_gml_456_0"")));
",
@"    draw_text(camerax() + 460, cameray() + y1_off, string_hash_to_newline(stringsetsubloc(""~1 jeter"", cap_item_gender, ""obj_gerson_fountain_slash_Draw_0_gml_505_0"")));
    draw_text(camerax() + 460, cameray() + y2_off, string_hash_to_newline(stringsetsubloc(""pour ~1 $ ?"", string(sellvalue), ""obj_shop_vending_slash_Draw_0_gml_456_0"")));
");

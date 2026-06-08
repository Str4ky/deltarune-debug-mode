Patcher.FindReplace(
"gml_GlobalScript_scr_itemget_anytype_text",
@"""obj_treasure_room_slash_Other_10_gml_76_0"");",
@"""obj_treasure_room_slash_Other_10_gml_76_0"");
        if (global.lang == ""en"")
		{
			scr_drfr_get_anyitem_gender(_itemid, _itemtype);
			yourstring = ""vos"";
			if (_itemtype == ""money"" || (_itemtype == ""item"" && _pocketed))
				yourstring = ""votre"";
			itemgetstring = stringsetsubloc(""* (Vous ajoutez ~1\\cY~2\\cW dans ~3 \\cY~4\\cW.)"", safe_apo_name, itemname, yourstring, itemtypename, ""obj_treasure_room_slash_Other_10_gml_76_0"");
		}
");

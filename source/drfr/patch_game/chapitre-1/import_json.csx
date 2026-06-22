FilePatcher.ImportFile("lang_fr.json", "lang/lang_en.json");

var json = JSONPatcher.ReadJSONLangFile("lang/lang_en.json");
JSONPatcher.UpdateKey(json, "DEVICE_CONTACT_slash_Step_0_gml_5_0", "caca");

string temp = JSONPatcher.JSONToString(json);
FilePatcher.ImportFileFromText(temp, "lang/lang_en.json");

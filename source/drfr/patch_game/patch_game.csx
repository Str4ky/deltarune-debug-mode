string fr_file = "strings_fr.txt";
string en_file = "strings_en.txt";
string file_dir = $"chapitre-{curChap}";

string[] fr_lines = File.ReadAllLines($"{file_dir}/{fr_file}");
string[] en_lines = File.ReadAllLines($"{file_dir}/{en_file}");

Hashtable trad_table = new Hashtable();
for (int i = 0; i < fr_lines.Length; i++) {
	en_lines[i] = en_lines[i].Replace("[RETOUR A LA LIGNE]", "\n");
	fr_lines[i] = fr_lines[i].Replace("[RETOUR A LA LIGNE]", "\n");
	trad_table.Add(en_lines[i], fr_lines[i]);
}

foreach (UndertaleString str in Data.Strings) {
	if (trad_table.ContainsKey(str.Content)) {
		str.Content = trad_table[str.Content].ToString();
	}
}

importFontFolder("Fonts");
if (curChap != 0)
	importSoundFolder($"{file_dir}/Sound");

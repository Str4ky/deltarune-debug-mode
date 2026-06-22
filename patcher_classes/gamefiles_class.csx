using System;
using System.IO;
using System.Text.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Unicode;

using Underanalyzer.Decompiler;
using UndertaleModLib;
using UndertaleModLib.Compiler;
using UndertaleModLib.Decompiler;
using UndertaleModLib.Models;

using strDict = System.Collections.Generic.Dictionary<string, string>;

static public class FilePatcher
{
	private static string[] forbidden_suffix = {
		".exe", ".bak", ".win", ".mod"
	};
	public static string[] ignore_suffix = {};
	public readonly static string backup_suffix = ".bak";
	public readonly static string modfiles_suffix = ".mod";
	private static bool _isInit = false;
	private static string baseDir;

	public static void _init(string outputDir)
	{
		if (_isInit)
			throw new Exception("Error, FilePatcher class already init");

		_isInit = true;
		baseDir = outputDir;
	}

	public static void RegisterIgnorableSuffix(string suffix)
	{
	   if (Array.IndexOf(ignore_suffix, suffix) >= 0)
		   throw new Exception($"Suffix '{suffix}' is already registered");

		Array.Resize(ref ignore_suffix, ignore_suffix.Length + 1);
		ignore_suffix[ignore_suffix.Length - 1] = suffix;
	}

	public static void UnregisterIgnorableSuffix(string suffix)
	{
		int index = Array.IndexOf(ignore_suffix, suffix);

		if (index < 0)
			throw new Exception($"Suffix '{suffix}' is not registered");

		string[] newArray = new string[ignore_suffix.Length - 1];
		if (index != 0)
			Array.Copy(ignore_suffix, 0, newArray, 0, index);

		if (index < ignore_suffix.Length - 1)
			Array.Copy(ignore_suffix, index + 1, newArray, index, ignore_suffix.Length - index - 1);

		ignore_suffix = newArray;
	}

	private static string _PathToGameFile(string filepath)
	{
		return (Path.Join(baseDir, filepath));
	}

	public static bool GameFileExist(string filepath)
	{
		return (Path.Exists(_PathToGameFile(filepath)));
	}

	public static string ReadGameFile(string filepath)
	{
		string gamepath = _PathToGameFile(filepath);

		if (!GameFileExist(filepath))
			throw new Exception($"Error, {gamepath} isn't a gamefile");

		return (File.ReadAllText(gamepath));
	}

	private static void _CreateEmptyFile(string filename)
	{
		File.WriteAllText(filename, "");
	}

	private static void _PreCopySecurity(string dest)
	{
		if (Directory.Exists(dest))
			throw new Exception($"Error, forbidden to write text into a folder {dest}");

		string file_suffix = System.IO.Path.GetExtension(dest);
		foreach (string suf in forbidden_suffix) {
			if (suf == file_suffix)
				throw new Exception($"Error, forbidden to import a {suf} file");
		}

		bool fileExists = Path.Exists(dest);
		bool backupExists = Path.Exists(dest + backup_suffix);
		bool modfileExists = Path.Exists(dest + modfiles_suffix);
		bool isVanillaFile = fileExists && !backupExists && !modfileExists;

		if (fileExists && !backupExists && !modfileExists)
			File.Copy(dest, dest + backup_suffix);
		else if (!fileExists && !modfileExists)
			_CreateEmptyFile(dest + modfiles_suffix);
	}

	private static void _CopyTextToFile(string text, string dest)
	{
		dest = Path.Join(baseDir, dest);
		_PreCopySecurity(dest);
		File.WriteAllText(dest, text);
		Console.WriteLine($"Imported {dest} file through text");
	}

	private static void _CopyFile(string file, string dest)
	{
		dest = Path.Join(baseDir, dest);
		_PreCopySecurity(dest);
		File.Copy(file, dest, true);
		Console.WriteLine($"Imported {dest} file through file");
	}

	public static void ImportFileFromText(string text, string path)
		=> _CopyTextToFile(text, path);

	//TODO enable directory creation based on filepath and destpath
	public static void ImportFile(string file, string path)
	{
		if (File.Exists(file) == false) {
			throw new Exception($"Error, {file} doesn't exist");
		}

		//if path is a folder
		if (Directory.Exists(path))
			path = Path.Join(path, Path.GetFileName(file));
		_CopyFile(file, path);
	}

	public static void ImportFile(string file) =>
		ImportFile(file, file);

	private static void _ImportFolder()
	{
		string[] file_list = Directory.GetFiles(".", "*.*", SearchOption.AllDirectories);
		foreach (string f in file_list) {
			string file_suf = Path.GetExtension(f);
			bool ignore_file = false;
			foreach (string suf in ignore_suffix) {
				if (suf == file_suf) {
					ignore_file = true;
					break;
				}
			}
			if (!ignore_file)
				ImportFile(f, f);
		}
	}

	public static void ImportFolder(string folder)
	{
		string temp = System.IO.Directory.GetCurrentDirectory();
		Directory.SetCurrentDirectory(folder);
		_ImportFolder();
		Directory.SetCurrentDirectory(temp);
	}
}

FilePatcher._init(Path.GetDirectoryName(FilePath));

static public class JSONPatcher
{
	public static JsonSerializerOptions SerializeOptions = _defaultSerializeOptions();

	private static JsonSerializerOptions _defaultSerializeOptions()
	{
		var encoderSettings = new TextEncoderSettings();
		encoderSettings.AllowRange(UnicodeRanges.All);
		var options = new JsonSerializerOptions
		{
			Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
			WriteIndented = true,
			AllowDuplicateProperties = false
		};
		return (options);
	}

	public static strDict ReadJSONLangFile(string filepath)
	{
		string json = FilePatcher.ReadGameFile(filepath);
		return (JsonSerializer.Deserialize<strDict>(json));
	}

	public static bool KeyExists(strDict json, string key)
	{
		return (json.ContainsKey(key));
	}

	public static string GetDialogue(strDict json, string key)
	{
		if (!KeyExists(json, key))
			return (null);
		return (json[key]);
	}

	public static void UpdateKey(strDict json, string key, string val)
	{
		if (!KeyExists(json, key))
			throw new Exception($"Error, {key} key doesn't exist in json");
		json[key] = val;
	}

	public static bool WriteKey(strDict json, string key, string val)
	{
		bool res = KeyExists(json, key);
		json[key] = val;
		return (res);
	}

	public static string JSONToString(strDict json)
	{
		return (JsonSerializer.Serialize(json, SerializeOptions));
	}
}

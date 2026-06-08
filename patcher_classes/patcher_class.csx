using System;
using System.IO;
using System.Collections;
using Underanalyzer.Decompiler;
using UndertaleModLib;
using UndertaleModLib.Compiler;
using UndertaleModLib.Decompiler;
using UndertaleModLib.Models;

static public class Patcher
{
    public static UndertaleData Data { get; set; }
    public static CodeImportGroup ImportGroup { get; set; }
    public static GlobalDecompileContext GlobalContext { get; set; }
	private static string BaseDir = System.IO.Directory.GetCurrentDirectory();
	public static string CurrentDir = "";

	public static string[][] RegexTags = {
		new string[2] {"<string>", @"['""]((?:\\.|[^""\\])*)['""]"},
		new string[2] {"<indent>", @" *"}
	};

	//Changing dir logic
	public static void UpdateDir(string dirPath)
	{
		Directory.SetCurrentDirectory(BaseDir);
		CurrentDir = dirPath;
		Directory.SetCurrentDirectory(dirPath);
	}

	//Basic init function
    public static void Initialize(UndertaleData data)
    {
        Console.WriteLine("Initializing patcher...");

        Data = data;
        GlobalContext = new(data);
        ImportGroup = new(data, GlobalContext, data.ToolInfo.DecompilerSettings);
    }


	//Fetch file content with the current directory
	private static string _getFileText(string filePath)
	{
		return (File.ReadAllText(filePath));
	}


	//Regex safe FindReplace cause utmt sucks
	private static void _queueReplaceCode(string gmlName, string find, string replace)
	{
		UndertaleCode codeEntry = Data.Code.ByName(gmlName);
		/*if (codeEntry == null) {
			throw new Exception($"Error, gmlName `{gmlName}' does not exist.");
		}
		string codeText = ImportGroup.DecompileExistingCode(codeEntry);
		if (!Regex.Match(find, replace)) {
			throw new Exception($"Error, code replacement in `{gmlName}' not found");
		}*/
		ImportGroup.QueueRegexFindReplace(codeEntry, find, replace);
	}


	//Find and replace basic strings in gmlName
    public static void FindReplace(string gmlName, string find, string replace)
    {
		if (gmlName == null || gmlName == "") {
			throw new Exception($"Empty or NULL gml_name");
		}

		Console.WriteLine($"Queueing replacement for {gmlName}...");
		string findText = Regex.Escape(find);
		for (int i = 0; i < RegexTags.Length; i++) {
			findText = findText.Replace(RegexTags[i][0], RegexTags[i][1]);
		}
		_queueReplaceCode(gmlName, findText, replace);
    }

	public static void FindReplaceFile(string gmlName, string findPath, string replacePath)
	{
		Console.WriteLine($"Reading files for ${gmlName}");
        FindReplace(gmlName, _getFileText(findPath), _getFileText(replacePath));
	}


	//Append/Prepend replace argument after find
	public static void FindAppend(string gmlName, string find, string replace) =>
		FindReplace(gmlName, find, find + replace);

	public static void FindAppendFile(string gmlName, string findPath, string replacePath) =>
		FindReplace(gmlName, _getFileText(findPath), _getFileText(findPath) + _getFileText(replacePath));

	public static void FindPrepend(string gmlName, string find, string replace) =>
		FindReplace(gmlName, find, replace + find);

	public static void FindPrependFile(string gmlName, string findPath, string replacePath) =>
		FindReplace(gmlName, _getFileText(findPath), _getFileText(replacePath) + _getFileText(findPath));


	//Write content into specified gmlName
    public static void WriteGML(string gmlName, string content)
    {
        Console.WriteLine($"Queueing overwrite for {gmlName}...");
        ImportGroup.QueueReplace(gmlName, content);
    }

    public static void WriteGMLFile(string gmlName, string filePath) =>
        WriteGML(gmlName, _getFileText(filePath));


	//Append changes to gmlName
	public static void AppendGML(string gmlName, string content)
	{
		Console.WriteLine($"Queueing appending for {gmlName}...");
		ImportGroup.QueueAppend(gmlName, content);
	}

	public static void AppendGMLFile(string gmlName, string filePath) =>
		AppendGML(gmlName, _getFileText(filePath));


	//Prepend changes to gmlName
	public static void PrependGML(string gmlName, string content)
	{
		Console.WriteLine($"Queueing prepending for {gmlName}...");
		ImportGroup.QueuePrepend(gmlName, content);
	}

	public static void PrependGMLFile(string gmlName, string filePath) =>
		PrependGML(gmlName, _getFileText(filePath));

	public static UndertaleGameObject RegisterObject(string objName, bool visible = true,
			bool persistent = false, bool awake = true)
	{
		if (Data.GameObjects.ByName(objName) != null)
			throw new Exception($"Error, object `{objName}' already exist.");
		UndertaleGameObject new_obj = new UndertaleGameObject()
		{
			Name = Data.Strings.MakeString(objName),
			Visible = visible,
			Persistent = persistent,
			Awake = awake
		};
		Data.GameObjects.Add(new_obj);
		return (new_obj);
	}


	//Commit all changes made
    public static void CommitChanges()
    {
        Console.WriteLine("Committing changes...");
        ImportGroup.Import();
    }
}

EnsureDataLoaded();
Patcher.Initialize(Data);


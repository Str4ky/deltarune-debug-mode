ADD_SCRIPT="""
var FILE_NAME_name = Data.Strings.MakeString("gml_Script_FILE_NAME");
var FILE_NAME_code = new UndertaleCode()
{
    Name = FILE_NAME_name,
    LocalsCount = 1
};
Data.Code.Add(FILE_NAME_code);

UndertaleCodeLocals.LocalVar FILE_NAME_argsLocal = new UndertaleCodeLocals.LocalVar();
FILE_NAME_argsLocal.Name = Data.Strings.MakeString("arguments");
FILE_NAME_argsLocal.Index = 0;

var FILE_NAME_locals = new UndertaleCodeLocals()
{
    Name = FILE_NAME_name
};
FILE_NAME_locals.Locals.Add(FILE_NAME_argsLocal);
Data.CodeLocals.Add(FILE_NAME_locals);
Data.Functions.EnsureDefined("FILE_NAME", Data.Strings);

UndertaleScript FILE_NAME = new UndertaleScript()
{
    Name = Data.Strings.MakeString("FILE_NAME"),
    Code = FILE_NAME_code
};
Data.Scripts.Add(FILE_NAME);
"""

ADD_OBJECT="""UndertaleGameObject FILE_NAME = new UndertaleGameObject();
FILE_NAME.Name = Data.Strings.MakeString("FILE_NAME");
FILE_NAME.Visible = {};
FILE_NAME.Persistent = {};
FILE_NAME.Awake = {};
Data.GameObjects.Add(FILE_NAME);
"""

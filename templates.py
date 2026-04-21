ADD_SCRIPT="""UndertaleScript FILE_NAME = new UndertaleScript();
FILE_NAME.Name = Data.Strings.MakeString("FILE_NAME");
FILE_NAME.Code = new UndertaleCode();
FILE_NAME.Code.Name = Data.Strings.MakeString("gml_GlobalScript_FILE_NAME");
FILE_NAME.Code.LocalsCount = 1;
Data.Scripts.Add(FILE_NAME);
Data.Code.Add(FILE_NAME.Code);
"""

ADD_OBJECT="""UndertaleGameObject FILE_NAME = new UndertaleGameObject();
FILE_NAME.Name = Data.Strings.MakeString("FILE_NAME");
FILE_NAME.Visible = {};
FILE_NAME.Persistent = {};
FILE_NAME.Awake = {};
Data.GameObjects.Add(FILE_NAME);
"""

EnsureDataLoaded();

bool enable = ScriptQuestion(@"
Activer ou désactiver le mode debug pour le Chapitre 2 ?

Oui = Activer le mode debug
Non = Désactiver le mode debug

(Choissiez bien l'option souhaitée. Si vous avez une erreur, c'est que vous avez choisi la mauvaise.)
");

UndertaleModLib.Compiler.CodeImportGroup importGroup = new(Data)
{
    ThrowOnNoOpFindReplace = true
};

// scr_debug → changer "debug = 0" → "debug = global.debug"
string scrDebug = "gml_GlobalScript_scr_debug";
if (Data.Code.ByName(scrDebug) == null)
    throw new ScriptException("Could not find script: scr_debug");

if (enable)
{
    importGroup.QueueFindReplace(scrDebug, "return 0", "return 1");
}
else
{
    importGroup.QueueFindReplace(scrDebug, "return 1", "return 0");
}


importGroup.Import();
ScriptMessage("Mode debug du Chapitre 2 " + (enable ? "activé" : "désactivé") + ".");

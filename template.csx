// TOP
EnsureDataLoaded();

if (!Data.IsVersionAtLeast(2023, 6))
{
    ScriptError("Erreur 0 : Ce script fonctionne uniquement pour la version payante de Deltarune.");
    return;
}

if (Data?.GeneralInfo?.DisplayName?.Content.ToLower() != "deltarune chapter CHAPTER_NUMBER" &&
    Data?.GeneralInfo?.DisplayName?.Content.ToLower() != "deltarune chapitre CHAPTER_NUMBER")
{
    ScriptError("Erreur 1 : Ce script s'applique seulement au Chapitre CHAPTER_NUMBER.");
    return;
}


bool enable = ScriptQuestion(
"Ajouter le Mode Debug pour le Chapitre CHAPTER_NUMBER ?"
);

if (!enable)
{
    return;
}

GlobalDecompileContext globalDecompileContext = new(Data);
Underanalyzer.Decompiler.IDecompileSettings decompilerSettings = new Underanalyzer.Decompiler.DecompileSettings();
UndertaleModLib.Compiler.CodeImportGroup importGroup = new(Data, globalDecompileContext, decompilerSettings)
{
    ThrowOnNoOpFindReplace = false
};

// BOTTOM
importGroup.Import();

ScriptMessage("Mode Debug du Chapitre CHAPTER_NUMBER " + (enable ? "ajouté" : "désactivé") + ".\r\n" + "Pour activer le Mode Debug en jeu, appuyer sur F10.");

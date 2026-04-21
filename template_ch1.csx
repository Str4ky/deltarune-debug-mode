// TOP
EnsureDataLoaded();

if (!Data.IsVersionAtLeast(2023, 6))
{
    ScriptError("Erreur 0 : Ce script fonctionne uniquement pour la version LTS de la démo et la version payante de Deltarune.");
    return;
}

if (Data?.GeneralInfo?.DisplayName?.Content.ToLower() != "deltarune chapter 1" &&
    Data?.GeneralInfo?.DisplayName?.Content.ToLower() != "deltarune chapitre 1")
{
    ScriptError("Erreur 1 : Ce script s'applique seulement au Chapitre 1.");
    return;
}

bool isFrenchPatch = false;
foreach (var str in Data.Strings)
{
    if (str.Content == "Français")
    {
        isFrenchPatch = true;
        break;
    }
}
string defaultLang = isFrenchPatch ? "fr" : "en";

bool isDemo = false;
foreach (var str in Data.Strings)
{
    if (str.Content == "1.19")
    {
        isDemo = true;
        break;
    }
}

string versionInfo = isDemo ? "\r\n[Version détectée : Démo Itch]" : "\r\n[Version détectée : Payante]";

GlobalDecompileContext globalDecompileContext = new(Data);
Underanalyzer.Decompiler.IDecompileSettings decompilerSettings = new Underanalyzer.Decompiler.DecompileSettings();
UndertaleModLib.Compiler.CodeImportGroup importGroup = new(Data, globalDecompileContext, decompilerSettings)
{
    ThrowOnNoOpFindReplace = false
};

if (isDemo)
{
    // Set scr_debug_print
    UndertaleScript CREATE_scr_debug_print = new UndertaleScript();
    CREATE_scr_debug_print.Name = Data.Strings.MakeString("scr_debug_print");
    CREATE_scr_debug_print.Code = new UndertaleCode();
    CREATE_scr_debug_print.Code.Name = Data.Strings.MakeString("gml_GlobalScript_scr_debug_print");
    CREATE_scr_debug_print.Code.LocalsCount = 1;
    Data.Scripts.Add(CREATE_scr_debug_print);
    Data.Code.Add(CREATE_scr_debug_print.Code);

    // Set obj_debug_gui
    UndertaleGameObject CREATE_obj_debug_gui = new UndertaleGameObject(); 
    CREATE_obj_debug_gui.Name = Data.Strings.MakeString("obj_debug_gui");
    CREATE_obj_debug_gui.Visible = true;
    CREATE_obj_debug_gui.CollisionShape = (CollisionShapeFlags)1;
    CREATE_obj_debug_gui.Awake = true;
    Data.GameObjects.Add(CREATE_obj_debug_gui);
}

// Set scr_debug_fullheal
UndertaleScript CREATE_scr_debug_fullheal = new UndertaleScript();
CREATE_scr_debug_fullheal.Name = Data.Strings.MakeString("scr_debug_fullheal");
CREATE_scr_debug_fullheal.Code = new UndertaleCode();
CREATE_scr_debug_fullheal.Code.Name = Data.Strings.MakeString("gml_GlobalScript_scr_debug_fullheal");
CREATE_scr_debug_fullheal.Code.LocalsCount = 1;

Data.Scripts.Add(CREATE_scr_debug_fullheal);
Data.Code.Add(CREATE_scr_debug_fullheal.Code);

// Set scr_turn_skip
UndertaleScript CREATE_scr_turn_skip = new UndertaleScript();
CREATE_scr_turn_skip.Name = Data.Strings.MakeString("scr_turn_skip");
CREATE_scr_turn_skip.Code = new UndertaleCode();
CREATE_scr_turn_skip.Code.Name = Data.Strings.MakeString("gml_GlobalScript_scr_turn_skip");
CREATE_scr_turn_skip.Code.LocalsCount = 1;

Data.Scripts.Add(CREATE_scr_turn_skip);
Data.Code.Add(CREATE_scr_turn_skip.Code);

// BOTTOM
importGroup.Import();

ScriptMessage("Mode Debug du Chapitre 1 ajouté.\r\n" + "Pour activer le Mode Debug en jeu, appuyer sur F10.");

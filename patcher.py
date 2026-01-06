import os

def read_file(filename):
    f = open(filename, encoding="utf-8")
    dest = f.read()
    f.close()
    return (dest)

src = read_file("./template.csx")
CREATE_0 = read_file("./Debug Menu/gml_Object_obj_dmenu_system_Create_0.gml")
DRAW_0 = read_file("./Debug Menu/gml_Object_obj_dmenu_system_Draw_0.gml")
STEP_0 = read_file("./Debug Menu/gml_Object_obj_dmenu_system_Step_0.gml")

changes = [
        ["CREATE_CODE", CREATE_0.replace('"', '""')],
        ["DRAW_CODE", DRAW_0.replace('"', '""')],
        ["STEP0_CODE", STEP_0.replace('"', '""')]
]

for i in range(1, 5):
    if (i == 1):
        output = read_file("./template_chapter1.csx")
    else:
        output = src.replace("CHAPTER_NUMBER", str(i))
    for change in changes:
        output = output.replace(change[0], change[1])

    chap_step_1 = read_file(f"./Debug Menu/Chapitre {i}/gml_Object_obj_dmenu_system_Step_1.gml")
    output = output.replace("STEP1_CODE", chap_step_1.replace('"', '""'))
    extra = "";
    if (os.path.isfile(f"./Extra (chapitre {i}).csx")):
        extra = read_file(f"./Extra (chapitre {i}).csx")
    output = output.replace("EXTRA_IMPORTS", extra);

    with open(f"Mode Debug (chapitre {i}).csx", "w", encoding="utf-8") as f:
        f.write(output)

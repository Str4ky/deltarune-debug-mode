import os
import sys
import re
import json
import templates

OWD = os.path.abspath('.')
CWD = '.'
EXTRA_FOLDER = "EXTRAS"
SRC_FOLDER = "source"

def set_double_quote(gml_code):
    return gml_code.replace('"', '""')

def get_file_pos(filename):
    return (os.path.join(CWD, filename))

def update_cwd(new_dir):
    global CWD
    if (new_dir == OWD):
        CWD = '.'
    else:
        CWD = os.path.normpath(os.path.join(CWD, new_dir))
    os.chdir(new_dir)

def get_command_variable(config):
    elem_type = config['element_type']
    mode = config['mode']
    obj_name = config['obj_name']
    gml_name = config['gml_name']

    elem_identifier = ""
    queue_op = ""
    if (elem_type == 'scr'):
        elem_identifier = f"{obj_name}.Code"
    elif (elem_type == 'obj'):
        obj_methods = gml_name.split("_")[-2:]
        elem_identifier = f"{obj_name}.EventHandlerFor(EventType.{obj_methods[0]}, (uint){obj_methods[1]}, Data)"

    if (mode == "replace"):
        queue_op = "QueueReplace"
    elif (mode == "append"):
        queue_op = "QueueAppend"
    elif (mode == "prepend"):
        queue_op = "QueuePrepend"

    return (elem_identifier, queue_op)


def init_basic_json(foldername, json_name):
    if (not os.path.exists(json_name)):
        print(f"Warning: `get_file_pos(json_name)' doesn't exist. Skipping", file=sys.stderr)
        return ({})
    with open(json_name, "r", encoding="utf-8") as f:
        try:
            data = json.load(f)
        except json.JSONDecodeError:
            print(f"Error: Malformed config.json in `{CWD}'.", file=sys.stderr)
            return ({})

    res = {}
    defaults = {
        'mode': 'append',
        'actions': {},
        'create_new': False,
        'skip_ch1': False,
        'persistent': True,
        'visible': True,
        'awake': True
    }
    for key, value in defaults.items():
        defaults[key] = data.get(key, value)

    for filename, file in data.items():
        if (defaults.get(filename) != None):
            continue
        if (not os.path.exists(filename)):
            print(f"Error: `{filename}' doesn't exist in {CWD}, skipping", file=sys.stderr)
            continue

        for key, value in defaults.items():
            file[key] = file.get(key, value)

        type_predic = ""
        gml_types = [
                ['gml_GlobalScript_', 'scr'],
                ['gml_Object_', 'obj'], 
                ['gml_RoomCC_', 'room']
        ]

        gml_predic = ""
        if (filename.startswith("gml_")):
            gml_predic = filename
        else:
            for elem, type in gml_types:
                if (filename.startswith(type)):
                    gml_predic = elem + filename

        file['gml_name'] = file.get("gml_name", gml_predic)

        gml_extension = '.gml'
        if (file['gml_name'].endswith(gml_extension)):
            file['gml_name'] = file['gml_name'][:-len(gml_extension)]

        for elem, type in gml_types:
            if (file['gml_name'].startswith(elem)):
                type_predic = type
                break

        file['element_type'] = file.get('element_type', type_predic)
        if (not file['element_type'] in [obj[1] for obj in gml_types]):
            print(f"Error: unknown element type `{file['element_type']}' for file `{filename}' in `{get_file_pos(json_name)}'")
            continue

        left_index = 2
        right_index = -2
        if (file['element_type'] == 'scr'):
            file['obj_name'] = "_".join(file['gml_name'].split("_")[left_index:])
        else:
            file['obj_name'] = "_".join(file['gml_name'].split("_")[left_index:right_index])
        if (file['element_type'] == 'obj' and not file['gml_name'].endswith("Create_0")):
            file['create_new'] = False

        if (file['element_type'] == "scr"):
            file['variable_type'] = "UndertaleScript"
            file['class_id'] = "Scripts"
        elif (file['element_type'] == "obj"):
            file['variable_type'] = "UndertaleGameObject"
            file['class_id'] = "GameObjects"

        res[filename] = file

    return res

def parse_folder(declared_elements=None, current_chapter=0):

    """Process a folder and generate CSX lines while tracking declarations."""
    if declared_elements is None:
        declared_elements = set()
        
    csx_lines = []
    master_config_path = "config.json"
    folder_order = []
    if os.path.exists(master_config_path):
        with open(master_config_path, 'r', encoding='utf-8') as f:
            try:
                master_config = json.load(f)
                folder_order = master_config.get("order", [])
            except json.JSONDecodeError:
                print(f"Error: Malformed master {get_file_pos(master_config_path)}.")

    folders = []
    for fold in folder_order:
        if (not os.path.exists(fold)):
            print(f"Warning: {master_config_path} `{fold}' doesn't exist)", file=sys.stderr)
        elif (not os.path.isdir(fold)):
            print(f"Warning: `{fold}' isn't a folder", file=sys.stderr)

        else:
            folders.append(fold)

    for fold in os.listdir('.'):
        if (fold[0] != '.' and os.path.isdir(fold) and
            not fold in folders and fold != EXTRA_FOLDER):
            print(f"Warning: `{fold}' module not used", file=sys.stderr)

    for folder in folders:
        if folder == EXTRA_FOLDER or folder.startswith('.'):
            continue

        update_cwd(folder)
        folder_config = init_basic_json(folder, master_config_path)

        for filename, config in folder_config.items():
            csx_lines.append("\n")
            with open(filename, 'r', encoding='utf-8') as f:
                gml_code = set_double_quote(f.read())
            create_new = config.get("create_new", False)
            if (config['skip_ch1'] and current_chapter == 1):
                continue
            create_new = config['create_new']
            if (not config['obj_name'] in declared_elements):
                declared_elements.append(config['obj_name'])
                if (create_new):
                    if (config['element_type'] == 'scr'):
                        csx_lines.append((templates.ADD_SCRIPT.replace(
                            "FILE_NAME", config['obj_name']
                        )))
                    elif (config['element_type'] == 'obj'):
                        obj_options = [config['visible'], config['persistent'],
                                    config['awake']]

                        for i in range(len(obj_options)):
                            obj_options[i] = str(obj_options[i]).lower()

                        csx_lines.append((templates.ADD_OBJECT.replace(
                            "FILE_NAME", config['obj_name']
                        ).format(*obj_options)))
                else:
                    csx_lines.append(f'{config["variable_type"]} {config["obj_name"]} = Data.{config["class_id"]}.ByName("{config["obj_name"]}");')

            elem_identifier, queue_op = get_command_variable(config)

            csx_lines.append(f'importGroup.{queue_op}({elem_identifier}, @"\n{gml_code}");')

            for action, params in config['actions'].items():
                if (action == "find_replace"):
                    csx_lines.append(f'importGroup.QueueFindReplace("{config["gml_name"]}", @"{set_double_quote(params["find"])}", @"{set_double_quote(params["replace"])}");')




        # --- SCRIPT PROCESSING ---

        """"""
        update_cwd('..')

    return csx_lines

def compile_utmt_mod(source_folder, template_file):
    """Main build loop for Chapters 1-4."""
    if not os.path.exists(source_folder) or not os.path.exists(template_file):
        print("Error: 'source' folder or 'template.csx' missing.")
        return

    with open(template_file, 'r', encoding='utf-8') as f:
        template_content = f.read()

    if "// BOTTOM" not in template_content:
        print("Error: '// BOTTOM' marker missing in template.csx.")
        return
        
    template_parts = template_content.split("// BOTTOM")
    template_top = template_parts[0].replace("// TOP", "").strip() 
    template_bottom = template_parts[1].strip()

    for i in range(1, 5):
        element_memory = []
        
        output_file = f"Mode Debug (chapitre {i}).csx"
        final_lines = []
        
        current_top = template_top.replace("CHAPTER_NUMBER", str(i))
        current_bottom = template_bottom.replace("CHAPTER_NUMBER", str(i))

        if i == 1 and os.path.exists("template_ch1.csx"):
            with open("template_ch1.csx", 'r', encoding='utf-8') as fch1:
                ch1_content = fch1.read()
                if "// BOTTOM" in ch1_content:
                    parts = ch1_content.split("// BOTTOM")
                    current_top = parts[0].replace("// TOP", "").strip()
                    current_bottom = parts[1].strip()

        if i == 1:
            element_memory = ["scr_debug_print", "obj_debug_gui", "scr_debug_fullheal", "scr_turn_skip"]

        final_lines.append(current_top)
        
        update_cwd(source_folder)
        print(f"Parsing common code for Chapter {i}...")
        common_lines = parse_folder(element_memory, i)
        
        if common_lines:
            final_lines.append(f"\n// --- COMMON CODE ---")
            final_lines.extend(common_lines)
        
        extras_path = os.path.join(EXTRA_FOLDER, f"CH{i}")
        if os.path.exists(extras_path):
            update_cwd(extras_path)
            print(f"Parsing extras for Chapter {i}...")
            specific_lines = parse_folder(element_memory, i) 
            if specific_lines:
                final_lines.append(f"\n// --- EXTRAS CHAPTER {i} ---")
                final_lines.extend(specific_lines)

        update_cwd(OWD)
        final_lines.append("\n" + current_bottom)

        with open(output_file, 'w', encoding='utf-8') as out_file:
            out_file.write('\n'.join(final_lines))
            
        print(f"Success: '{output_file}' generated.\n")

# --- Execution ---
compile_utmt_mod(source_folder="source", template_file="template.csx")

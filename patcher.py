import os
import re
import json

def set_double_quote(gml_code):
    return gml_code.replace('"', '""')

def parse_folder(folder_path, declared_elements=None, current_chapter=0):
    """Process a folder and generate CSX lines while tracking declarations."""
    if declared_elements is None:
        declared_elements = set()
        
    csx_lines = []
    if not os.path.exists(folder_path):
        return csx_lines

    folders = [d for d in os.listdir(folder_path) if os.path.isdir(os.path.join(folder_path, d))]
    
    def get_priority(folder_name):
        match = re.match(r'^(\d+)_', folder_name)
        return int(match.group(1)) if match else float('inf')
        
    folders.sort(key=get_priority)

    for folder in folders:
        if folder.upper() == "EXTRAS":
            continue

        subfolder_path = os.path.join(folder_path, folder)
        folder_match = re.match(r'^(\d+)_([A-Za-z0-9_]+)$', folder)

        if not folder_match:
            continue

        element_name = folder_match.group(2)
        element_type = 'scr' if element_name.startswith('scr_') else 'obj'

        config_path = os.path.join(subfolder_path, "config.json")
        config = {"create_new": False}
        
        if os.path.exists(config_path):
            with open(config_path, 'r', encoding='utf-8') as f:
                try:
                    config = json.load(f)
                except json.JSONDecodeError:
                    print(f"Error: Malformed config.json in '{folder}'.")
        else:
             print(f"Warning: Missing config.json in '{folder}'. Assuming existing.")

        if current_chapter == 1 and config.get("skip_ch1", False):
            continue

        create_new = config.get("create_new", False)

        # --- SCRIPT PROCESSING ---
        if element_type == 'scr':
            gml_file = os.path.join(subfolder_path, f"gml_GlobalScript_{element_name}.gml")
            if os.path.exists(gml_file):
                with open(gml_file, 'r', encoding='utf-8') as f:
                    gml_code = set_double_quote(f.read())
                
                csx_lines.append(f'\n// Script {element_name}')
                
                # Check memory to prevent duplicate C# declarations
                if element_name not in declared_elements:
                    if create_new:
                        csx_lines.append(f'UndertaleScript {element_name} = new UndertaleScript();')
                        csx_lines.append(f'{element_name}.Name = Data.Strings.MakeString("{element_name}");')
                        csx_lines.append(f'Data.Scripts.Add({element_name});')
                    else:
                        csx_lines.append(f'UndertaleScript {element_name} = Data.Scripts.ByName("{element_name}");')
                    declared_elements.add(element_name)

                mode = config.get("mode", "replace").lower()
                target = f"{element_name}.Code"

                for fr in config.get("find_replace", []):
                    f_str = set_double_quote(fr.get("find", ""))
                    r_str = set_double_quote(fr.get("replace", ""))
                    csx_lines.append(f'importGroup.QueueFindReplace({target}, @"{f_str}", @"{r_str}");')

                action = "QueueReplace" if mode == "replace" else "QueueAppend"
                csx_lines.append(f'importGroup.{action}({target}, @"\n{gml_code}\n");')
                csx_lines.append(f'ChangeSelection({element_name});')

        # --- GAME OBJECT PROCESSING ---
        elif element_type == 'obj':
            files = [f for f in os.listdir(subfolder_path) if f.endswith('.gml')]
            events_config = config.get("events", {})
            events_order = list(events_config.keys())

            def sort_files(filename):
                """Sort files based on the order defined in config.json."""
                file_regex = r'^gml_Object_' + re.escape(element_name) + r'_([A-Za-z]+)_(\d+)\.gml$'
                match = re.match(file_regex, filename)
                if match:
                    event_key = f"{match.group(1)}_{match.group(2)}"
                    if event_key in events_order:
                        return events_order.index(event_key)
                return float('inf') 

            files.sort(key=sort_files)
            events_processed = 0
            
            if files:
                csx_lines.append(f'\n// GameObject {element_name}')
                
                if element_name not in declared_elements:
                    if create_new:
                        visible = str(config.get("visible", True)).lower()
                        persistent = str(config.get("persistent", False)).lower()
                        awake = str(config.get("awake", True)).lower()

                        csx_lines.append(f'UndertaleGameObject {element_name} = new UndertaleGameObject();')
                        csx_lines.append(f'{element_name}.Name = Data.Strings.MakeString("{element_name}");')
                        csx_lines.append(f'{element_name}.Visible = {visible};')
                        csx_lines.append(f'{element_name}.Persistent = {persistent};')
                        csx_lines.append(f'{element_name}.Awake = {awake};')
                        csx_lines.append(f'Data.GameObjects.Add({element_name});')
                    else:
                        csx_lines.append(f'UndertaleGameObject {element_name} = Data.GameObjects.ByName("{element_name}");')
                    declared_elements.add(element_name)
            
            for file in files:
                file_regex = r'^gml_Object_' + re.escape(element_name) + r'_([A-Za-z]+)_(\d+)\.gml$'
                file_match = re.match(file_regex, file)
                
                if file_match:
                    event_name = file_match.group(1)
                    subevent_id = file_match.group(2)
                    
                    event_key = f"{event_name}_{subevent_id}"
                    current_event_config = events_config.get(event_key, {})
                    mode = current_event_config.get("mode", "replace").lower()
                    
                    target = f'{element_name}.EventHandlerFor(EventType.{event_name}, (uint){subevent_id}, Data)'

                    for fr in current_event_config.get("find_replace", []):
                        f_str = set_double_quote(fr.get("find", ""))
                        r_str = set_double_quote(fr.get("replace", ""))
                        csx_lines.append(f'importGroup.QueueFindReplace({target}, @"{f_str}", @"{r_str}");')

                    with open(os.path.join(subfolder_path, file), 'r', encoding='utf-8') as f:
                        gml_code = set_double_quote(f.read())
                    
                    action = "QueueReplace" if mode == "replace" else "QueueAppend"
                    csx_lines.append(f'importGroup.{action}({target}, @"\n{gml_code}\n");')
                    events_processed += 1

            if events_processed > 0:
                csx_lines.append(f'ChangeSelection({element_name});')

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
        element_memory = set() # Reset memory for each chapter
        
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

        final_lines.append(current_top)
        
        print(f"Parsing common code for Chapter {i}...")
        common_lines = parse_folder(source_folder, element_memory, i)
        
        if common_lines:
            final_lines.append(f"\n// --- COMMON CODE ---")
            final_lines.extend(common_lines)
        
        extras_path = os.path.join(source_folder, "EXTRAS", f"CH{i}")
        if os.path.exists(extras_path):
            print(f"Parsing extras for Chapter {i}...")
            specific_lines = parse_folder(extras_path, element_memory, i) 
            if specific_lines:
                final_lines.append(f"\n// --- EXTRAS CHAPTER {i} ---")
                final_lines.extend(specific_lines)
        
        final_lines.append("\n" + current_bottom)

        with open(output_file, 'w', encoding='utf-8') as out_file:
            out_file.write('\n'.join(final_lines))
            
        print(f"Success: '{output_file}' generated.\n")

# --- Execution ---
compile_utmt_mod(source_folder="source", template_file="template.csx")
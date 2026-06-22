import os
import sys
import shutil

MAX_CHAP = 4
ALL_CHAPTERS = list(range(0, MAX_CHAP + 1))
OWD = os.path.dirname(os.path.abspath(sys.argv[0]))
CWD = OWD
os.chdir(OWD)

PATCH_FILE_PREFIX = ".csx"

#TODO create stack of chapter_lock for perms
CHAPTER_LOCK_FILE = ".chapter_lock"
CHAPTER_LOCK_STACK = []

SOURCE_DIR = os.path.abspath("source")
OUTPUT_DIR = os.path.abspath("output")
OUTPUT_NAME = "chapter{}.csx"

CURRENT_OUTPUT = ""
CURRENT_CHAPTER = 0

def update_cwd(new_dir):
    global CWD
    CWD = os.path.normpath(os.path.join(CWD, new_dir))
    os.chdir(new_dir)

def set_double_quote(text):
    return (text.replace('"', '""'))

def read_file(filename):
    with open(filename) as f:
        content = f.read()
    return (content)

def save_output(modname, chapter, content):
    os.chdir('..')
    os.makedirs(OUTPUT_DIR, exist_ok=True)
    os.makedirs(f"{OUTPUT_DIR}/{modname}", exist_ok=True)
    with open(f"{OUTPUT_DIR}/{modname}/{OUTPUT_NAME.format(chapter)}", "w") as f:
        f.write(content)
    os.chdir(SOURCE_DIR)

def analyze_chapter_lock(depth_level):
    global CHAPTER_LOCK_STACK

    if (depth_level == 0):
        default_return = ALL_CHAPTERS[1:]
    else:
        default_return = CHAPTER_LOCK_STACK[-1]

    if (not os.path.exists(CHAPTER_LOCK_FILE)):
        return (default_return)

    file_loc = f"{SOURCE_DIR}/{CUR_MOD_NAME}/{CURRENT_LOOKUP_DIR}/{CHAPTER_LOCK_FILE}"
    chapter_lock_content = read_file(CHAPTER_LOCK_FILE).strip()
    if (chapter_lock_content == ''):
        print(f"Warning: empty {CHAPTER_LOCK_FILE} at `{file_loc}', ignoring file")
        return (default_return)
    elif (chapter_lock_content == 'all'):
        return (ALL_CHAPTERS)

    chapter_lock_content = chapter_lock_content.replace("\t", " ")
    parsed = ""
    for i in range(len(chapter_lock_content)):
        c = chapter_lock_content[i]
        if (c.isdigit() or c == '-'):
            parsed += c
        elif (c == ',' or c == ' '):
            parsed += ' '
        else:
            print(f"Error: unrecognized token `{c}' in {file_loc}:1:{i}, ignoring file")
            return (default_return)

    lookup_type = 0
    for i in range(len(parsed)):
        c = parsed[i]
        if (lookup_type == 0):
            if (c == '-'):
                lookup_type = 1
            elif (c.isdigit()):
                lookup_type = 2
    
        elif (lookup_type == 1):
            if (not c.isdigit()):
                print(f"Error: excepting digit after '-' sign at {file_loc}:1:{i}, ignoring file")
                return (default_return)
            lookup_type = 2

        elif (lookup_type == 2):
            if (c == '-'):
                print(f"Error: '-' sign not a start of a number at {file_loc}:1:{i}, ignoring file")
                return (default_return)
            if (c == ' '):
                lookup_type = 0

    splitted = parsed.split(' ')
    splitted = [sp for sp in splitted if sp.strip()]
    chap_list = list(set(list(map(int, splitted))))
    if (CURRENT_CHAPTER == 0 and min(chap_list) < 0):
        return ([])

    if (min(chap_list) < 0):
        if (max(chap_list) >= 0):
            print(f"Warning: invalid chapter values in `{file_loc}', ignore file")
            print("Rules: only positive or negative numbers but no mix")
            return (default_return)
        parsed = ALL_CHAPTERS.copy()
        for chap in chap_list:
            parsed.remove(-chap)
        chap_list = parsed

    return (chap_list)

def respect_chapter_lock(cur_chap, stack):
    for lock in stack:
        if (not cur_chap in lock):
            return (False)
    return (True)


if (os.path.isdir(OUTPUT_DIR)):
    shutil.rmtree(OUTPUT_DIR)

mod_list = []
update_cwd(SOURCE_DIR)
if (len(sys.argv) == 1):
    mod_list_temp = os.listdir(SOURCE_DIR)

    for mod in mod_list_temp:
        if (os.path.isdir(mod)):
            mod_list.append(mod)
        else:
            print(f"Warning: non folder element `{mod}' in source folder", file=sys.stderr)
else:
    mod_list = sys.argv[1:]

def array_to_csharp_array(lst):
    if (lst == []):
        return ("")
    dest = '{@"'
    dest += '", @"'.join(lst)
    dest += '"}'
    return (dest)

def replace_globals(cur_cont):
    global_list = [
        ["PATCHER_CURRENT_CHAPTER", str(CURRENT_CHAPTER)],
        ["PATCHER_MODS_LIST", array_to_csharp_array(MOD_LIST)],
        ["PATCHER_MODS_LOADED", array_to_csharp_array(MODS_LOADED[CURRENT_CHAPTER])]
    ]
    for glob, rep in global_list:
        cur_cont = cur_cont.replace(glob, rep)
    return (cur_cont)

def apply_patch_file(filename):
    global CURRENT_OUTPUT, CURRENT_CHAPTER

    patch_content = read_file(filename)
    patch_content = replace_globals(patch_content)
    if (CURRENT_LOOKUP_DIR == ""):
        CURRENT_OUTPUT += patch_content
    else:
        #CURRENT_OUTPUT += f'Patcher.UpdateFile("{filename}");'
        CURRENT_OUTPUT += "\ndo {\n" + patch_content
        if (patch_content[-1] != '\n'):
            CURRENT_OUTPUT += '\n'
        CURRENT_OUTPUT += "} while (false);\n\n"

CURRENT_LOOKUP_DIR = ""
def search_folder(folder, depth_level):
    global CURRENT_LOOKUP_DIR, CURRENT_OUTPUT, CHAPTER_LOCK_STACK
    cur_dir = os.path.abspath('.')
    update_cwd(folder)
    CURRENT_LOOKUP_DIR = CWD.removeprefix(CUR_MOD)[1:]
    CHAPTER_LOCK_STACK.append(analyze_chapter_lock(depth_level))
    if (respect_chapter_lock(CURRENT_CHAPTER, CHAPTER_LOCK_STACK)):
        dir_content = os.listdir('.')
        files_list = []
        dir_list = []

        for elem in dir_content:
            if (os.path.isdir(elem)):
                dir_list.append(elem)
            elif (os.path.isfile(elem) and elem.endswith(PATCH_FILE_PREFIX)):
                files_list.append(elem)

        if (files_list and depth_level):
            CURRENT_OUTPUT += f'Patcher.UpdateDir("{CURRENT_LOOKUP_DIR}");'

        for file in files_list:
            apply_patch_file(file)

        for fold in dir_list:
            search_folder(fold, depth_level + 1)

    CHAPTER_LOCK_STACK.pop()
    update_cwd(cur_dir)

CUR_MOD = ""
OUTPUT_BASE = '#load "../../patcher_classes/patcher_class.csx"\n'
MOD_LIST = mod_list
MODS_LOADED = [[]] * len(ALL_CHAPTERS)

for mod in MOD_LIST:
    CUR_MOD = os.path.join(OWD, SOURCE_DIR, mod)
    CUR_MOD_NAME = mod
    for i in ALL_CHAPTERS:
        CURRENT_CHAPTER = i
        CURRENT_OUTPUT = OUTPUT_BASE
        search_folder(mod, 0)
        CURRENT_OUTPUT += "\nPatcher.CommitChanges();\n"
        save_output(mod, i, CURRENT_OUTPUT)
        MODS_LOADED[CURRENT_CHAPTER].append(mod)

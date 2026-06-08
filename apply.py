import os
import sys
import shutil

def str_is_num(s):
    if (s[0] == '-'):
        return (s[1:].isdigit())
    return (s.isdigit())

def parse_argv(argv, chap_list):
    global PATCH_MAP

    PATCH_MAP = [0] * len(chap_list)
    treating_neg_index = False
    mod_lst = []
    for av in argv:
        if (not str_is_num(av)):
            mod_lst.append(av)
            continue
        new_chap = int(av)
        if (new_chap >= 0 and treating_neg_index or
            new_chap < 0 and max(PATCH_MAP) != 0):
            print("Error: cannot treat all but and specific chapters")
            exit(1)

        if (new_chap < 0 and not treating_neg_index):
            treating_neg_index = True
            PATCH_MAP = [1] * len(chap_list)

        PATCH_MAP[new_chap] = not treating_neg_index

    if (max(PATCH_MAP) == 0):
        PATCH_MAP = [1] * len(chap_list)
    return (mod_lst)


PATCH_MAP = []

DELTARUNE_FOLDER = os.path.abspath("/home/gabio/drfr/DELTARUNE")
if (not os.path.isdir(DELTARUNE_FOLDER)):
    print("Error: DELTARUNE game folder doesn't exist", file=sys.stderr)
    exit(1)

UTMT_CLI_PATH = os.path.abspath("/home/gabio/drfr/utmt_cli/UndertaleModCli")
if (not os.path.isfile(UTMT_CLI_PATH)):
    print("Error: UTMT CLI executable doesn't exist", file=sys.stderr)
    exit(1)


SOURCE_DIR = os.path.abspath("source")
if (not os.path.isdir(SOURCE_DIR)):
    print(f"Error: The {SOURCE_DIR} directory doesn't exist", file=sys.stderr)
    exit(1)

OUTPUT_DIR = os.path.abspath("output")

chap_list = sorted(os.listdir(DELTARUNE_FOLDER))

CHAPTER_LIST = [0]
CHAPTER_FOLDER_SUFFIX = "chapter"
CHAPTER_FOLDER_PREFIX = "_windows"
for elem in chap_list:
    if (elem.startswith(CHAPTER_FOLDER_SUFFIX) and elem.endswith(CHAPTER_FOLDER_PREFIX)):
        CHAPTER_LIST.append(int(elem[len(CHAPTER_FOLDER_SUFFIX):-len(CHAPTER_FOLDER_PREFIX)]))


if (len(sys.argv) != 1):
    MODS_LIST = parse_argv(sys.argv[1:], CHAPTER_LIST)
    if (MODS_LIST == []):
        MODS_LIST = os.listdir(SOURCE_DIR)
else:
    MODS_LIST = os.listdir(SOURCE_DIR)
    if (len(CHAPTER_LIST) > len(PATCH_MAP)):
        PATCH_MAP += [1] * (len(CHAPTER_LIST) - len(PATCH_MAP))


BACKUP_PREFIX = ".bak"
DATA_FILE_NAME = "data.win"

UTMT_COMMAND = f"{UTMT_CLI_PATH} load {DELTARUNE_FOLDER}/{{}}/{DATA_FILE_NAME} -s {{}} -o {DELTARUNE_FOLDER}/{{}}/{DATA_FILE_NAME}"

for chapter in CHAPTER_LIST:
    if (PATCH_MAP[chapter] == 0):
        continue
    subfolder = ""
    if (chapter != 0):
        subfolder = CHAPTER_FOLDER_SUFFIX + str(chapter) + CHAPTER_FOLDER_PREFIX
    file_path = f"{DELTARUNE_FOLDER}/{subfolder}/{DATA_FILE_NAME}"
    copy_name = file_path + BACKUP_PREFIX
    if (not os.path.exists(copy_name)):
        shutil.copyfile(file_path, copy_name)
    else:
        shutil.copyfile(copy_name, file_path)

OWD = os.path.abspath('.')
error_code = 0

for mod in MODS_LIST:
    for chapter in CHAPTER_LIST:
        if (PATCH_MAP[chapter] == 0):
            continue
        script_path = f"{OUTPUT_DIR}/{mod}/chapter{chapter}.csx"
        subfolder = ""
        if (chapter != 0):
            subfolder = CHAPTER_FOLDER_SUFFIX + str(chapter) + CHAPTER_FOLDER_PREFIX
        if (not os.path.exists(script_path)):
            continue
        cur_cmd = UTMT_COMMAND.format(subfolder, script_path, subfolder)
        os.chdir(f"{SOURCE_DIR}/{mod}")
        error_code += os.system(cur_cmd)
        os.chdir(OWD)
        if (error_code):
            exit(1)
        print(f"Applied {mod} to chapter {chapter}")

print(MODS_LIST)

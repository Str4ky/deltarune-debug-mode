import os
import sys

#Patcher runtime files
MODS_LIST_FILE = ".patcher_mod_order"
CUR_MOD_INDEX_FILE = ".patcher_cur_mod_index"
CUR_CHAPTER_FILE = ".patcher_cur_chapter"


#Important paths
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

OUTPUT_DIR_NAME = "output"
OUTPUT_DIR = os.path.abspath(OUTPUT_DIR_NAME)
if (not os.path.isdir(OUTPUT_DIR)):
    exit(1)


#List game's chapters
chap_list = sorted(os.listdir(DELTARUNE_FOLDER))
CHAPTER_LIST = [0]
CHAPTER_FOLDER_SUFFIX = "chapter"
CHAPTER_FOLDER_PREFIX = "_windows"
for elem in chap_list:
    if (elem.startswith(CHAPTER_FOLDER_SUFFIX) and elem.endswith(CHAPTER_FOLDER_PREFIX)):
        CHAPTER_LIST.append(int(elem[len(CHAPTER_FOLDER_SUFFIX):-len(CHAPTER_FOLDER_PREFIX)]))


#Backup stuff
BACKUP_PREFIX = ".bak"
MODFILE_PREFIX = ".mod"
DATA_FILE_NAME = "data.win"


#Patching stuff
PATCH_MAP = []  # list of chapters to patch
UTMT_COMMAND = f"{UTMT_CLI_PATH} load {{}} {{}} -o {{}}"  # patch command

#Directory init
OWD = os.path.dirname(os.path.abspath(sys.argv[0]))  # Original Working Directory
CWD = OWD  # Current Working Directory
os.chdir(OWD)

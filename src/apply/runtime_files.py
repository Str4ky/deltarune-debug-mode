from .config import *
import os

def delete_config_files():
    lst = [
        MODS_LIST_FILE,
        CUR_MOD_INDEX_FILE,
        CUR_CHAPTER_FILE
    ]
    for file in lst:
        os.unlink(file)

def setup_config_files(cur_chap, mods_to_load):
    with open(MODS_LIST_FILE, "w") as f:
        f.write("|".join(mods_to_load))

    with open(CUR_MOD_INDEX_FILE, "w") as f:
        f.write("0");

    with open(CUR_CHAPTER_FILE, "w") as f:
        f.write(str(cur_chap))

import os
import sys
import shutil

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

error_code = 0

reset_gamefiles(DELTARUNE_FOLDER)

for chapter in CHAPTER_LIST:
    if (PATCH_MAP[chapter] == 0):
        continue
    subfolder = ""
    if (chapter != 0):
        subfolder = CHAPTER_FOLDER_SUFFIX + str(chapter) + CHAPTER_FOLDER_PREFIX
    mods_to_load = []
    utmt_cmd_scripts = []
    for mod in MODS_LIST:
        script_path = f"{OUTPUT_DIR}/{mod}/chapter{chapter}.csx"
        if (not os.path.exists(script_path)):
            continue
        mods_to_load.append(mod)
        utmt_cmd_scripts.append("-s " + script_path)

    if (mods_to_load == []):
        continue

    os.chdir(SOURCE_DIR)
    setup_config_files(chapter, mods_to_load)
    cur_cmd = UTMT_COMMAND.format(subfolder, " ".join(utmt_cmd_scripts), subfolder)
    error_code += os.system(cur_cmd)
    delete_config_files()
    os.chdir(OWD)
    if (error_code):
        exit(1)
    print(f"Applied mods to chapter {chapter}")

from . import config
from . import runtime_files
import os

def execute_command(subfolder, cmd_scripts):
    output_file = os.path.join(config.DELTARUNE_FOLDER, subfolder, config.DATA_FILE_NAME)
    patch_cmd = config.UTMT_COMMAND.format(output_file, " ".join(cmd_scripts), output_file)

    error_code = os.system(patch_cmd)
    if (error_code):
        exit(1)

def patch_chapter(chapter):
    subfolder = ""
    if (chapter != 0):
        subfolder = config.CHAPTER_FOLDER_SUFFIX + str(chapter) + config.CHAPTER_FOLDER_PREFIX

    mods_to_load = []
    utmt_cmd_scripts = []
    for mod in config.MODS_LIST:
        script_path = os.path.join(config.OUTPUT_DIR, mod, f"chapter{chapter}.csx")
        if (not os.path.exists(script_path)):
            continue
        mods_to_load.append(mod)
        utmt_cmd_scripts.append("-s " + script_path)

    os.chdir(config.SOURCE_DIR)
    runtime_files.setup_config_files(chapter, mods_to_load)

    execute_command(subfolder, utmt_cmd_scripts)

    runtime_files.delete_config_files()
    os.chdir(config.OWD)
    print(f"Applied mods to chapter {chapter}")

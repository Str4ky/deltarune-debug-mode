import os
import shutil
from . import config

def scan_dir_files(path) -> list:
    res = []
    temp = os.listdir(path)
    for elem in temp:
        joined = os.path.join(path, elem)
        if (os.path.isfile(joined)):
            res.append(joined)
        elif (os.path.isdir(joined)):
            res += scan_dir_files(joined)
    return (res)

def reset_gamefiles(gamepath):
    files = scan_dir_files(gamepath)
    for file in files:
        if (file.endswith(config.BACKUP_PREFIX)):
            shutil.copyfile(file, file[:-len(config.BACKUP_PREFIX)])
            os.unlink(file)
        elif (file.endswith(config.MODFILE_PREFIX)):
            os.unlink(file[:-len(config.MODFILE_PREFIX)])
            os.unlink(file)

from . import config
import os

def str_is_num(s):
    if (s[0] == '-'):
        return (s[1:].isdigit())
    return (s.isdigit())

def parse_argv(argv, chap_list):
    config.PATCH_MAP = [False] * len(chap_list)
    treating_neg_index = False
    mod_lst = []
    for av in argv:
        if (not str_is_num(av)):
            mod_lst.append(av)
            continue
        new_chap = int(av)
        if (new_chap >= 0 and treating_neg_index or
            new_chap < 0 and max(config.PATCH_MAP) != 0):
            print("Error: cannot treat all but and specific chapters")
            exit(1)

        if (new_chap < 0 and not treating_neg_index):
            treating_neg_index = True
            config.PATCH_MAP = [True] * len(chap_list)

        config.PATCH_MAP[new_chap] = not treating_neg_index

    if (max(config.PATCH_MAP) == 0):
        config.PATCH_MAP = [True] * len(chap_list)
    return (mod_lst)

def handle_argv(argv, chap_list):
    if (len(argv) == 1):
        config.MODS_LIST = os.listdir(config.SOURCE_DIR)
        if (len(chap_list) > len(config.PATCH_MAP)):
            config.PATCH_MAP += [True] * (len(chap_list) - len(config.PATCH_MAP))
    else:
        config.MODS_LIST = parse_argv(argv, chap_list)
        if (config.MODS_LIST == []):
            config.MODS_LIST = os.listdir(config.SOURCE_DIR)

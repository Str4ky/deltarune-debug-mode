from . import config
from .reset_gamefiles import reset_gamefiles
from .handle_argv import handle_argv
from .patch_chapter import patch_chapter

def main(argv) -> None:
    reset_gamefiles(config.DELTARUNE_FOLDER)
    handle_argv(argv, config.CHAPTER_LIST)
    for chapter in config.CHAPTER_LIST:
        if (config.PATCH_MAP[chapter] == 0):
            continue

    for chapter in config.CHAPTER_LIST:
        patch_chapter(chapter)

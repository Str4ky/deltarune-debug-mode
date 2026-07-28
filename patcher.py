import sys
from patcher_modules.compiler import compile_utmt_mod

if __name__ == "__main__":
    source_fol = "source"
    if len(sys.argv) == 2:
        source_fol = sys.argv[1]
        
    compile_utmt_mod(
        source_folder=source_fol, 
        template_file="patcher_modules/templates/debug_mode_template.csx"
    )
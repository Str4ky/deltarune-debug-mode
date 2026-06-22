Patcher.FindReplace(
    "gml_GlobalScript_scr_charbox",
    @"draw_sprite(scr_84_get_sprite(""spr_bnamenoelle""), 0, xx + 51 + xchunk, bpoff + b_offset + 3 + mmy[c]);",
    @"draw_sprite(scr_84_get_sprite(""spr_bnamenoelle""), 0, xx + 51 + xchunk, bpoff + b_offset + 0 + mmy[c]);"
);

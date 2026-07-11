if (scr_debug())
{
    if (keyboard_check_pressed(ord("W")))
    {
        scr_debug_print(dstr("Fight skipped", "Combat passé"));
        var _y_miniboss = asset_get_index("obj_plat_enm_yellow_miniboss");
        var _pun_gun = asset_get_index("obj_plat_enm_yellow_punishmentgun");
        var _y_combat   = asset_get_index("obj_dw_fcastle_yellow_combat");
        var _orange_gauntlet = asset_get_index("obj_dw_fcastle_orange_gauntlet");
        
        with (all)
        {
            if (id != obj_plat_player.id && variable_instance_exists(id, "get_hurt"))
            {
                hp = 0;
                
                if (object_index != _y_miniboss && object_index != _pun_gun && object_index != _orange_gauntlet)
                {
                    hit = 1;
                    instance_destroy();
                }
            }
        }
        
        with (obj_plat_bullet)
            instance_destroy();
        
        with (obj_plat_combatstarter)
        {
            enemy_wave_extflags = [];
            
            if (con == 1 || con == 2)
            {
                con = 2;
                endcombat();
            }
        }
        
        with (obj_enemy_wave)
        {
            if (con == 1)
                end_wave();
        }
        
        if (_y_combat != -1)
        {
            with (_y_combat)
                con = 11;
        }
    }
}
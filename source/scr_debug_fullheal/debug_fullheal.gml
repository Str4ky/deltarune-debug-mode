function scr_debug_fullheal()
{
    var _in_battle = false;
    
    if (variable_global_exists("charinstance") && is_array(global.charinstance))
    {
        if (array_length(global.charinstance) > 0 && instance_exists(global.charinstance[0]))
        {
            if (variable_instance_exists(global.charinstance[0], "myself"))
                _in_battle = true;
        }
    }
    
    if (_in_battle)
    {
        with (obj_dmgwriter)
        {
            if (delaytimer >= 1)
                killactive = 1;
        }
        
        scr_healallitemspell(999);
        
        for (var i = 0; i < 3; i++)
        {
            if (instance_exists(global.charinstance[i]))
            {
                with (global.charinstance[i])
                {
                    if (variable_instance_exists(id, "tu"))
                        tu--;
                }
            }
        }
    }
    else
    {
        with (obj_dmgwriter)
        {
            if (delaytimer >= 1)
                killactive = 1;
        }
        
        scr_healall(999);
        var _targets = [];
        var _plat_player_idx = asset_get_index("obj_plat_player");
        var _mainchara_idx = asset_get_index("obj_mainchara");
        
        if (_plat_player_idx != -1 && instance_exists(_plat_player_idx))
        {
            with (_plat_player_idx)
                array_push(_targets, id);
            
            var _plat_follower_idx = asset_get_index("obj_plat_follower");
            
            if (_plat_follower_idx != -1)
            {
                with (_plat_follower_idx)
                    array_push(_targets, id);
            }
        }
        else if (_mainchara_idx != -1 && instance_exists(_mainchara_idx))
        {
            with (_mainchara_idx)
                array_push(_targets, id);
            
            var _caterpillar_idx = asset_get_index("obj_caterpillarchara");
            
            if (_caterpillar_idx != -1)
            {
                with (_caterpillar_idx)
                    array_push(_targets, id);
            }
        }
        
        for (var i = 0; i < array_length(_targets); i++)
        {
            var _t = _targets[i];
            
            if (instance_exists(_t))
            {
                var healanim = instance_create(_t.x, _t.y, obj_healanim);
                healanim.target = _t;
                var dmgwr = instance_create(_t.x, _t.y - 30, obj_dmgwriter);
                
                with (dmgwr)
                {
                    delay = 4;
                    specialmessage = 3;
                    image_speed = 0;
                }
            }
        }
    }
}
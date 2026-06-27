if (scr_debug() && i_ex(obj_mainchara_board))
{
    xx = 0;
    yy = 0;
    tile_width = 32;
    if (left_p())
    {
        xx = -tile_width;
    }
    else if (right_p())
    {
        xx = tile_width;
    }
    else if (down_p())
    {
        yy = tile_width;
    }
    else if (up_p())
    {
        yy = -tile_width;
    }
    if (keyboard_check(vk_control))
    {
        for (i = 0; i < instance_number(obj_mainchara_board); i++)
        {
            hero = instance_find(obj_mainchara_board, i);
            hero.x += xx;
            hero.y += yy;
        }
    }
}
if (scr_debug() && i_ex(obj_board_solid))
{
    xx = 0;
    yy = 0;
    board_width = 384;
    board_height = 256;
    if (left_p())
    {
        xx = -board_width;
    }
    else if (right_p())
    {
        xx = board_width;
    }
    else if (down_p())
    {
        yy = board_height;
    }
    else if (up_p())
    {
        yy = -board_height;
    }
    if ((xx + yy) != 0 && keyboard_check(vk_shift))
    {
        obj_index = -1;
        for (i = 0; i < instance_number(obj_board_solid); i++)
        {
            temp = instance_find(obj_board_solid, i);
            if (object_get_name(temp.object_index) == "obj_board_solid")
            {
                obj_index = i;
                break;
            }
        }
        if (obj_index != -1)
        {
            solid_base = instance_find(obj_board_solid, obj_index);
            solid_x = solid_base.x;
            solid_y = solid_base.y;
            solid_origin_x = -1;
            if (room == room_board_1 || room == room_board_1_sword)
            {
                solid_origin_x = 896;
                solid_origin_y = 64;
            }
            else if (room == room_board_2 || room == room_board_2_sword)
            {
                solid_origin_x = 1664;
                solid_origin_y = 3136;
            }
            else if (room == room_board_3)
            {
                solid_origin_x = 736;
                solid_origin_y = 1152;
            }
            else if (room == room_board_3_sword)
            {
                solid_origin_x = 608;
                solid_origin_y = 1152;
            }
            else if (room == room_board_dungeon_2)
            {
                solid_origin_x = 1696;
                solid_origin_y = 800;
            }
            else if (room == room_board_dungeon_3)
            {
                solid_origin_x = 896;
                solid_origin_y = 64;
            }
            else if (room == room_board_prepostshadowmantle)
            {
                solid_origin_x = 128;
                solid_origin_y = 224;
            }
            middle_h_offset = floor(solid_origin_x / board_width);
            middle_v_offset = floor(solid_origin_y / board_height);
            horiz_offset = (solid_origin_x % board_width) - 64;
            vert_offset = (solid_origin_y % board_height) - 64;
            xx += (horiz_offset + -(solid_x - (middle_h_offset * board_width) - (board_width / 2)));
            yy += (vert_offset + -(solid_y - (middle_v_offset * board_height) - (board_height / 2)));
            scr_quickwarp(xx, yy, xx + 192, (yy + board_height) - 128);
        }
    }
}

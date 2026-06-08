function scr_drfr_get_anyitem_gender(arg0, arg1)
{
    item_article_au = "au";
    item_article_du = "du";
    item_article_un = "un";
    safe_apo_name = "";

    if (arg1 == "money")
    {
        exit;
    }

    switch (arg1)
    {
        case "item":
            scr_drfr_get_item_gender(arg0);
            break;
        case "litem":
            scr_drfr_get_litem_gender(arg0);
            break;
        case "weapon":
            scr_drfr_get_weapon_gender(arg0);
            break;
        case "armor":
            scr_drfr_get_armor_gender(arg0);
            break;
        case "key":
            scr_drfr_get_keyitem_gender(arg0);
            break;
        default:
            item_gender = "le";
            use_apo = 0;
            break;
    }

    if (item_gender == "le")
    {
        item_article_au = "au";
        item_article_du = "du";
        item_article_un = "un";
    }
    else if (item_gender == "la")
    {
        item_article_au = "à la";
        item_article_du = "de la";
        item_article_un = "une";
    }
    else if (item_gender == "les")
    {
        item_article_au = "aux";
        item_article_du = "des";
        item_article_un = "des";
    }
    if (use_apo)
    {
        item_article_au = "à l'";
        item_article_au = "de l'";
        item_article_un += " ";
        safe_apo_name = "l'";
    }
    else
    {
        item_article_au += " ";
        item_article_du += " ";
        item_article_un += " ";
        safe_apo_name = item_gender + " ";
    }
    cap_item_gender = scr_drfr_capitalize_first_letter(item_gender);
    cap_safe_apo_name = scr_drfr_capitalize_first_letter(safe_apo_name);
    cap_item_article_du = scr_drfr_capitalize_first_letter(item_article_du);
    cap_item_article_au = scr_drfr_capitalize_first_letter(item_article_au);
    cap_item_article_un = scr_drfr_capitalize_first_letter(item_article_un);
}

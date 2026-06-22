function scr_drfr_get_weapon_gender(arg0)
{
    use_apo = 0;
    item_gender = "le";
    remainder = "";

    switch (arg0)
    {
        case 1:
            remainder = "Boisilame";
            use_apo = 0;
            item_gender = "la";
            break;

        case 2:
            remainder = "CriniHache";
            use_apo = 0;
            item_gender = "la";
            break;

        case 3:
            remainder = "ÉcharpeRouge";
            use_apo = 1;
            item_gender = "la";
            break;

        case 4:
            remainder = "ArmeDeTous";
            use_apo = 1;
            item_gender = "la";
            break;

        case 5:
            remainder = "Épée Frousse";
            use_apo = 1;
            item_gender = "la";
            break;

        case 6:
            remainder = "BravHache";
            use_apo = 0;
            item_gender = "la";
            break;

        case 7:
            remainder = "FauxDémon.";
            use_apo = 0;
            item_gender = "la";
            break;

        case 8:
            remainder = "Trefoil";
            use_apo = 0;
            item_gender = "la";
            break;

        case 9:
            remainder = "Lamebeau";
            use_apo = 0;
            item_gender = "la";
            break;

        case 10:
            remainder = "ÉcharpeChic";
            use_apo = 1;
            item_gender = "la";
            break;

        case 11:
            remainder = "ÉpéeTordue";
            use_apo = 1;
            item_gender = "la";
            break;

        case 12:
            remainder = "AnneauNeige";
            use_apo = 1;
            item_gender = "le";
            break;

        case 13:
            remainder = "AnneauÉpines";
            use_apo = 1;
            item_gender = "le";
            break;

        case 14:
            remainder = "Rebondilame";
            use_apo = 0;
            item_gender = "la";
            break;

        case 15:
            remainder = "ÉcharpeSupp.";
            use_apo = 1;
            item_gender = "la";
            break;

        case 16:
            remainder = "SabreMéca";
            use_apo = 0;
            item_gender = "le";
            break;

        case 17:
            remainder = "HacheAuto";
            use_apo = 0;
            item_gender = "la";
            break;

        case 18:
            remainder = "ÉcharpeFibre";
            use_apo = 1;
            item_gender = "la";
            break;

        case 19:
            remainder = "Lamebeau2";
            use_apo = 0;
            item_gender = "la";
            break;

        case 20:
            remainder = "ÉpéePétée";
            use_apo = 1;
            item_gender = "la";
            break;

        case 21:
            remainder = "ÉcharPantin";
            use_apo = 1;
            item_gender = "la";
            break;

        case 22:
            remainder = "AnneauGelé";
            use_apo = 1;
            item_gender = "le";
            break;

        case 23:
            remainder = "Sabre10";
            use_apo = 0;
            item_gender = "le";
            break;

        case 24:
            remainder = "Toxic'Hache";
            use_apo = 0;
            item_gender = "la";
            break;

        case 25:
            remainder = "ÉcharpeFlex";
            use_apo = 1;
            item_gender = "la";
            break;

        case 26:
            remainder = "ÉclatNoir";
            use_apo = 1;
            item_gender = "le";
            break;

        case 50:
            remainder = "SucreD'Arme";
            use_apo = 0;
            item_gender = "le";
            break;

        case 51:
            remainder = "ÉcharpePage";
            use_apo = 1;
            item_gender = "la";
            break;

        case 52:
            remainder = "HacheJustice";
            use_apo = 0;
            item_gender = "la";
            break;

        case 53:
            remainder = "Épailée";
            use_apo = 1;
            item_gender = "la";
            break;

        case 54:
            remainder = "Absorb'Hache";
            use_apo = 1;
            item_gender = "la";
            break;
    }
}

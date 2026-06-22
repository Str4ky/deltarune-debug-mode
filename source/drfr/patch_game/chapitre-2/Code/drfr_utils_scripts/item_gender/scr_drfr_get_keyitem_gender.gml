function scr_drfr_get_keyitem_gender(arg0)
{
    use_apo = 0;
    item_gender = "le";
    remainder = "";

    switch (arg0)
    {
        case 1:
            remainder = "Téléphone";
            use_apo = 0;
            item_gender = "le";
            break;

        case 2:
            remainder = "Œuf";
            use_apo = 1;
            item_gender = "le";
            break;

        case 3:
            remainder = "GâteauBrisé";
            use_apo = 0;
            item_gender = "le";
            break;

        case 4:
            remainder = "Clé Cassée A";
            use_apo = 0;
            item_gender = "la";
            break;

        case 5:
            remainder = "Clé de Porte";
            use_apo = 0;
            item_gender = "la";
            break;

        case 6:
            remainder = "Clé Cassée B";
            use_apo = 0;
            item_gender = "la";
            break;

        case 7:
            remainder = "Lancer";
            use_apo = 0;
            item_gender = "le";
            break;

        case 8:
            remainder = "Lancer";
            use_apo = 0;
            item_gender = "le";
            break;

        case 9:
            remainder = "Rouxls Kaard";
            use_apo = 0;
            item_gender = "le";
            break;

        case 10:
            remainder = "DisqueVierge";
            use_apo = 0;
            item_gender = "le";
            break;

        case 11:
            remainder = "DisqueGravé";
            use_apo = 0;
            item_gender = "le";
            break;

        case 12:
            remainder = "GenClé";
            use_apo = 0;
            item_gender = "la";
            break;

        case 13:
            remainder = "Crist.D'Ombre";
            use_apo = 0;
            item_gender = "le";
            break;

        case 14:
            remainder = "Starwalker";
            use_apo = 0;
            item_gender = "le";
            break;

        case 15:
            remainder = "CristalPur";
            use_apo = 0;
            item_gender = "le";
            break;

        case 16:
            remainder = "ManetÉtrange";
            use_apo = 0;
            item_gender = "la";
            break;

        case 17:
            remainder = "PassVIP";
            use_apo = 0;
            item_gender = "le";
            break;

        case 18:
            remainder = "TicketVoyage";
            use_apo = 0;
            item_gender = "le";
            break;

        case 19:
            remainder = "ManetLancer";
            use_apo = 0;
            item_gender = "la";
            break;

        case 30:
            remainder = "Partition";
            use_apo = 0;
            item_gender = "la";
            break;

        case 31:
            remainder = "AiscaGriffes";
            use_apo = 1;
            item_gender = "les";
            break;
    }
}

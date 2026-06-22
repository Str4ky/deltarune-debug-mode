function scr_drfr_get_litem_gender(arg0)
{
    use_apo = 0;
    item_gender = "le";
    remainder = "";

	switch (arg0)
	{
		case 1:
			remainder = "Chocolat Chaud";
			use_apo = 0;
			item_gender = "le";
			break;

		case 2:
			remainder = "Crayon";
			use_apo = 0;
			item_gender = "le";
			break;

		case 3:
			remainder = "Pansement";
			use_apo = 0;
			item_gender = "le";
			break;

		case 4:
			remainder = "Bouquet";
			use_apo = 0;
			item_gender = "le";
			break;

		case 5:
			remainder = "Boule de Trucs";
			use_apo = 0;
			item_gender = "la";
			break;

		case 6:
			remainder = "Crayon Halloween";
			use_apo = 0;
			item_gender = "le";
			break;

		case 7:
			remainder = "Crayon Fétiche";
			use_apo = 0;
			item_gender = "le";
			break;

		case 8:
			remainder = "Œuf";
			use_apo = 1;
			item_gender = "le";
			break;

		case 9:
			remainder = "Cartes";
			use_apo = 0;
			item_gender = "les";
			break;

		case 10:
			remainder = "Boîte de ChocoCœurs";
			use_apo = 0;
			item_gender = "la";
			break;

		case 11:
			remainder = "Verre";
			use_apo = 0;
			item_gender = "le";
			break;

		case 12:
			remainder = "Gomme";
			use_apo = 0;
			item_gender = "la";
			break;

		case 13:
			remainder = "Critérium";
			use_apo = 0;
			item_gender = "le";
			break;

		case 14:
			remainder = "Montre";
			use_apo = 0;
			item_gender = "la";
			break;

		case 15:
			remainder = "Crayon de Noël";
			use_apo = 0;
			item_gender = "le";
			break;

		case 16:
			remainder = "Épine de Cactus";
			use_apo = 1;
			item_gender = "la";
			break;

		case 17:
			remainder = "ÉclatNoir";
			use_apo = 1;
			item_gender = "le";
			break;

		case 18:
			remainder = "Stylo-Plume";
			use_apo = 0;
			item_gender = "le";
			break;
	}
}

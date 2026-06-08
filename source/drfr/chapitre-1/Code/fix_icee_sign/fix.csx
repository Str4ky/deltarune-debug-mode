{

UndertaleRoom room_town_mid = Data.Rooms.ByName("room_town_mid");

var layer = room_town_mid.Layers[5];
foreach (var Tuile in layer.AssetsData.LegacyTiles) {
	if (Tuile.InstanceID == 10002278) {
		Tuile.X = 272;
		Tuile.Y = 4;
		Tuile.Width = 171;
	}
}

}

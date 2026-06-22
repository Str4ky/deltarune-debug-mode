{
	string base_name = "gml_RoomCC_room_dw_cyber_keyboard_puzzle_2";
	for (int i = 1; i <= 16; i++) {
		Patcher.WriteGMLFile(
				$"{base_name}_{i}_PreCreate",
				$"{base_name}_{i}_PreCreate.gml"
		);
	}

	base_name = $"gml_RoomCC_room_dw_city_monologue";
	for (int i = 19; i <= 20; i++) {
		Patcher.WriteGMLFile(
				$"{base_name}_{i}_PreCreate",
				$"{base_name}_{i}_PreCreate.gml"
		);
	}
}

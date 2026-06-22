{
	string base_name = "gml_RoomCC_room_dw_cyber_keyboard_puzzle_1";
	 
	string[] letter_changes = {"M", "O", "E", "M"};
	for (int i = 0; i < letter_changes.Length; i++) {
		Patcher.WriteGML(
			$"{base_name}_{i + 1}_PreCreate",
			$"myString = \"{letter_changes[i]}\""
		);
	}

	UndertaleRoom cur_room = Data.Rooms.ByName(@"room_dw_cyber_keyboard_puzzle_1");
	foreach (var Object in cur_room.GameObjects) {
		if (Object.InstanceID != 101899 && Object.InstanceID != 101903)
			continue ;
		string pre_create_name = $"gml_RoomCC_{cur_room.Name.Content}_{Object.InstanceID}_PreCreate";
		//Object.PreCreateCode = Patcher.CreateNewCodeFile(pre_create_name, "drfr_change_puzzle_to_p");
		//TODO reimplement
	}
}

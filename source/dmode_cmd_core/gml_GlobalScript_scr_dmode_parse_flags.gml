function scr_dmode_get_argv_flags(arg0)
{
	var argv = arg0;
	var argc = array_length(argv);

	var flags = []
	var flag_index = 0;

	for (var i = 1; i < argc; i++)
	{
		var arg = argv[i];
		
		if (arg == "--")
			break;
		
		var arg_len = string_length(arg);
		if (arg_len >= 2 && (string_copy(arg, 1, 2) == "--"))
			flags[flag_index++] = string_copy(arg, 3, arg_len - 2);
	}

	return (flags);
}

function scr_dmode_remove_argv_flags(arg0)
{
	var argv = arg0;
	var argc = array_length(argv);

	var flags_saw = 0;

	for (var i = 1; i < argc; i++)
	{
		var arg = argv[i];
		
		if (arg == "--")
		{
			flags_saw++;
			break;
		}
		
		var arg_len = string_length(arg);
		if (arg_len >= 2 && (string_copy(arg, 1, 2) == "--"))
		{
			flags_saw++;
			for (var j = i; j < (argc - flags_saw); j++)
				argv[j] = argv[j + 1];
		}
	}
	array_resize(argv, argc - flags_saw);
	return (argv);
}

function scr_dmode_flags_flag_used(arg0, arg1)
{
	var flag_list = arg0;
	var target = arg1;

	for (var i = 0; i < array_length(flag_list); i++)
		if (flag_list[i] == target)
			return (1);

	return (0);
}

function scr_dmode_flags_get_value(arg0, arg1)
{
	var flag_list = arg0;
	var target = arg1;

	for (var i = 0; i < array_length(flag_list); i++)
	{
		if (flag_list[i] != target)
			continue ;

		var flag = flag_list[i];

		for (var j = 1; j < string_length(flag); j++)
			if (string_char_at(flag, j) == "=")
				break;

		if (j == string_length(flag))
			return ("");
		return (string_copy(flag, j + 1, string_length(flag) - j));
	}
	return ("");
}

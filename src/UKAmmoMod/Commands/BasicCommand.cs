using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using GameConsole;

namespace UKAmmoMod.Commands;

public abstract class BasicCommand : ICommand {
	public string Name {
		get {
			string res = GetType().Name;
			if(res.EndsWith("Command")) res = res.Remove(res.Length - "Command".Length);
				return Regex.Replace(res, @"(?<!^)(?=[A-Z])", "_");
		}
	}
	public abstract string Description { get; }
	public string Command => Name;

	public abstract void Execute(GameConsole.Console console, string[] args);
}

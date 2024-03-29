﻿using System;
using System.Collections.Generic;
using System.Text;
using UKAmmoMod.Utilities;

namespace UKAmmoMod.Commands; 

public sealed class YesAmmoCommand : BasicCommand {
	public override string Description => "Gives all ammo.";

	public override void Execute(GameConsole.Console console, string[] args) {
		GuardAgainst.LengthOutOfRange(args, nameof(args), 0, 0);
		if(AmmoInventory.Instance == null) {
			// BepInEx logs do not appear in the in-game console, sometime soon i will probably make a mod that fixes this, or make a PR to UMM
			MainPlugin.logger.LogError("Cannot give ammo because ammo inventory does not exist.");
			return;
		}

		AmmoInventory.Instance.ReplenishAll();
	}
}

﻿using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace UKAmmoMod.Patches.ShotgunAmmo; 

[HarmonyPatch(typeof(Shotgun), nameof(Shotgun.Shoot))]
file static class ShotgunUseAmmoWhenFiringPatch {
	static bool Prerequisite => AmmoInventory.UseShells;

	static bool Prefix() {
		if(AmmoInventory.Instance.Shells < 1) return false;
		AmmoInventory.Instance.Shells--;

		return true;
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace UKAmmoMod.Patches.ShotgunAmmo; 

[HarmonyPatch(typeof(Shotgun), nameof(Shotgun.Pump))]
file static class ShotgunNoPumpWhenOutOfAmmoPatch {
	static bool Prerequisite => AmmoInventory.UseShells;

	static bool Prefix(Shotgun __instance) {
		if(AmmoInventory.Instance.Shells < 1) return false;
		return true;
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace UKAmmoMod.Patches.ShotgunAmmo; 

[HarmonyPatch(typeof(Shotgun), nameof(Shotgun.ShootSinks))]
file static class ShotgunUseAmmoWhenAltFiringPatch {
	static bool Prerequisite => AmmoInventory.UseShells;

	static bool Prefix() {
		if(AmmoInventory.Instance.Shells < 2) return false;
		AmmoInventory.Instance.Shells -= 2;

		return true;
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace UKAmmoMod.Patches {
	[HarmonyPatch(typeof(Shotgun), nameof(Shotgun.ShootSinks))]
	static class ShotgunUseAmmoWhenAltFiringPatch {
		static bool Prefix() {
			if(AmmoInventory.Instance.Shells < 2) return false;
			AmmoInventory.Instance.Shells -= 2;

			return true;
		}
	}
}

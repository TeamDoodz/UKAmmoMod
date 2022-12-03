using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace UKAmmoMod.Patches.RocketLauncherAmmo {
	[HarmonyPatch(typeof(RocketLauncher), nameof(RocketLauncher.Shoot))]
	static class RocketLauncherUseAmmoWhenFiringPatch {
		static bool Prefix() {
			if(AmmoInventory.Instance.Rockets < 1) return false;
			AmmoInventory.Instance.Rockets--;

			return true;
		}
	}
}

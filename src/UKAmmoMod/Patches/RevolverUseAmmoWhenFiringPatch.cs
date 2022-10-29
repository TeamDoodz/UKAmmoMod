using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace UKAmmoMod.Patches {
	[HarmonyPatch(typeof(Revolver), nameof(Revolver.Shoot))]
	static class RevolverUseAmmoWhenFiringPatch {
		const int FIRE_NORMAL = 1;
		const int FIRE_SUPER = 2;

		static bool Prefix(int shotType) {
			if(shotType == FIRE_SUPER) {
				if(AmmoInventory.Instance.Cells < 6) return false;
				AmmoInventory.Instance.Cells -= 6;
			} else {
				if(AmmoInventory.Instance.Cells < 1) return false;
				AmmoInventory.Instance.Cells--;
			}
			return true;
		}
	}
}

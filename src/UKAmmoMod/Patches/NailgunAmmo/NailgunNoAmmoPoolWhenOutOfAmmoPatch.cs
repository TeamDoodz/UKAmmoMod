using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace UKAmmoMod.Patches.NailgunAmmo; 

[HarmonyPatch(typeof(Nailgun), nameof(Nailgun.Update))]
file static class NailgunNoAmmoPoolWhenOutOfAmmoPatch {
	static void Postfix(Nailgun __instance) {
		int amount = __instance.altVersion ? 10 : 1;

		if (AmmoInventory.Instance.Nails >= amount) {
			return;
		}

		__instance.wc.naiAmmo = 0f;
		__instance.wc.naiSaws = 0f;
	}
}

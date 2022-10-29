using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace UKAmmoMod.Patches {
	[HarmonyPatch(typeof(NewMovement), nameof(NewMovement.Start))]
	static class InitAmmoPatch {
		static void Prefix(NewMovement __instance) {
			__instance.gameObject.AddComponent<AmmoInventory>();
		}
	}
}

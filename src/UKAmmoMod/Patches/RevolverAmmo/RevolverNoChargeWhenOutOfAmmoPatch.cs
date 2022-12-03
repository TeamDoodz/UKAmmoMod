using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace UKAmmoMod.Patches.RevolverAmmo; 

[HarmonyPatch(typeof(Revolver), nameof(Revolver.Update))]
static class RevolverNoChargeWhenOutOfAmmoPatch {
	static void Prefix(Revolver __instance) {
		if (AmmoInventory.Instance.Cells < 6) __instance.pierceCharge = 0f;
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace UKAmmoMod.Patches {
	[HarmonyPatch(typeof(NewMovement), nameof(NewMovement.Respawn))]
	static class ResetAmmoOnRespawnPatch {
		static void Prefix() {
			AmmoInventory.Instance.ReplenishAll();
		}
	}
}

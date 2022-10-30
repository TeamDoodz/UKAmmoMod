using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace UKAmmoMod.Patches {
	[HarmonyPatch(typeof(Punch), nameof(Punch.Parry))]
	static class GiveAmmoOnParryPatch {
		static void Prefix() {
			AmmoInventory.Instance.ReplenishAll();
		}
	}
}

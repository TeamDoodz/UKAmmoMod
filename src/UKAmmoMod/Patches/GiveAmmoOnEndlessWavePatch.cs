using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace UKAmmoMod.Patches; 

[HarmonyPatch(typeof(EndlessGrid), nameof(EndlessGrid.NextWave))]
static class GiveAmmoOnEndlessWavePatch {
	static void Prefix() {
		AmmoInventory.Instance.HalfReplenishAll();
	}
}

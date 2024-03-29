﻿using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace UKAmmoMod.Patches.ShotgunAmmo; 

[HarmonyPatch(typeof(Shotgun), nameof(Shotgun.UpdateMeter))]
file static class ShotgunNoChargeWhenOutOfAmmoPatch {
	static bool Prerequisite => AmmoInventory.UseShells;

	static void Prefix(Shotgun __instance) {
		if(AmmoInventory.Instance.Shells < 2) __instance.grenadeForce = 0f;
	}
}

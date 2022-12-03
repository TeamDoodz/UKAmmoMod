using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace UKAmmoMod.Patches; 

[HarmonyPatch(typeof(StatsManager), nameof(StatsManager.HideShit))]
static class HideAmmoUIPatch {
	static void Prefix() {
		foreach(Transform obj in AmmoCounters.Instance.transform) {
			obj.gameObject.SetActive(false);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace UKAmmoMod.Patches; 

[HarmonyPatch(typeof(PlayerActivator), nameof(PlayerActivator.OnTriggerEnter))]
static class ShowAmmoUIPatch {
	static GameObject? ammoCountersPrefab;
	static void Prefix(PlayerActivator __instance) {
		if(__instance.activated) return;

		if(ammoCountersPrefab == null) ammoCountersPrefab = ModAssets.MainBundle.LoadAsset<GameObject>("assets/prefabs/ammocounts.prefab");
		GameObject.Instantiate(ammoCountersPrefab);
	}
}

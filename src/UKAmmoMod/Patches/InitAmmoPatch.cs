using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.SceneManagement;
using HarmonyLib;
using UnityEngine;

namespace UKAmmoMod.Patches {
	[HarmonyPatch(typeof(NewMovement), nameof(NewMovement.Start))]
	static class InitAmmoPatch {
		static GameObject? ammoCountersPrefab;
		static void Prefix(NewMovement __instance) {
			if(SceneManager.GetActiveScene().name.StartsWith("Main")) return;
			__instance.gameObject.AddComponent<AmmoInventory>();
			if(ammoCountersPrefab == null) ammoCountersPrefab = ModAssets.MainBundle.LoadAsset<GameObject>("assets/prefabs/ammocounts.prefab");
			GameObject.Instantiate(ammoCountersPrefab);
		}
	}
}

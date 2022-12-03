using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UKAmmoMod.Patches; 

[HarmonyPatch(typeof(EnemyIdentifier), nameof(EnemyIdentifier.Death))]
static class EnemiesDropAmmoPatch {
	static Dictionary<EnemyType, int> enemyDropAmnts = new Dictionary<EnemyType, int>() {
		{EnemyType.Filth, 1},
		{EnemyType.Stray, 1},
		{EnemyType.Drone, 1},
		{EnemyType.Schism, 1},
		{EnemyType.Streetcleaner, 1},
		{EnemyType.Soldier, 1},

		{EnemyType.MaliciousFace, 2},
		{EnemyType.Idol, 2},

		{EnemyType.Turret, 3},

		{EnemyType.Swordsmachine, 4},
		{EnemyType.Cerberus, 4},
		{EnemyType.Mindflayer, 4},
		{EnemyType.Virtue, 4},

		{EnemyType.HideousMass, 5},
		{EnemyType.Ferryman, 5},

		{EnemyType.V2, 10},
		{EnemyType.Minos, 10},
		{EnemyType.Gabriel, 10},
		{EnemyType.V2Second, 10},
		{EnemyType.Leviathan, 10},
		{EnemyType.GabrielSecond, 10},

		{EnemyType.FleshPrison, 25},
		{EnemyType.CancerousRodent, 25},

		{EnemyType.VeryCancerousRodent, 50},
		{EnemyType.MinosPrime, 50},
	};

	static void Prefix(EnemyIdentifier __instance) {
		if(__instance.dead) return;

		if(__instance.gameObject.GetComponent<CancerousRodent>() != null) __instance.enemyType = EnemyType.CancerousRodent;

		PickupPrefab[] prefabs = GetPrefabs(__instance);

		Collider col = __instance.GetComponent<Collider>() ?? __instance.GetComponentInChildren<Collider>();
		foreach(var prefab in prefabs) {
			AmmoPickupFactory.Instance.SpawnPickup(prefab, col.ClosestPointOnBounds(col.transform.position + Random.insideUnitSphere * 100f), Vector3.zero);
		}
	}

	private static PickupPrefab[] GetPrefabs(EnemyIdentifier __instance) {
		PickupPrefab[] prefabs;
		int amount = enemyDropAmnts.ContainsKey(__instance.enemyType) ? enemyDropAmnts[__instance.enemyType] : 2;
		prefabs = new PickupPrefab[amount];

		Array possible = Enum.GetValues(typeof(PickupPrefab));
		for(int i = 0; i < amount; i++) {
			PickupPrefab chosen = GetRandomPrefab(possible);
			if(AmmoInventory.Instance.IsFullOf(chosen)) chosen = GetRandomPrefab(possible); // less likely to get an ammo type you are already full of
			prefabs[i] = chosen;
		}

		return prefabs;
	}

	private static PickupPrefab GetRandomPrefab(Array possible) {
		return (PickupPrefab)possible.GetValue(Random.Range(0, possible.Length));
	}
}

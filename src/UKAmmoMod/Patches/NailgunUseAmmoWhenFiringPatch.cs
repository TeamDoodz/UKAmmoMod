using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using HarmonyLib;

namespace UKAmmoMod.Patches {
	[HarmonyPatch]
	static class NailgunUseAmmoWhenFiringPatch {
		static IEnumerable<MethodInfo> TargetMethods() {
			// jesse
			static MethodInfo getMeth(string name) {
				return typeof(Nailgun).GetMethod(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			}

			yield return getMeth(nameof(Nailgun.Shoot));
			yield return getMeth(nameof(Nailgun.BurstFire));
			yield return getMeth(nameof(Nailgun.SuperSaw));
		}

		static bool Prefix(Nailgun __instance) {
			int amount = __instance.altVersion ? 4 : 1;

			if(AmmoInventory.Instance.Nails < amount) {
				__instance.burstAmount = 0;
				return false;
			}
			AmmoInventory.Instance.Nails -= amount;

			return true;
		}
	}
}

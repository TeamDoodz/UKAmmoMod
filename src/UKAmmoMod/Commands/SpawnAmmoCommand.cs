using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace UKAmmoMod.Commands {
	public sealed class SpawnAmmoCommand : BasicCommand {
		public override string Description => "Spawns one of every ammo type.";

		public override void Execute(GameConsole.Console console, string[] args) {
			for(int i = 0; i < 10; i++) {
				Vector3 posToSpawn = Camera.main.transform.position + Vector3.right * i;
				foreach(PickupPrefab ammo in Enum.GetValues(typeof(PickupPrefab))) {
					posToSpawn += Camera.main.transform.forward * (AmmoPickup.DistanceToPickUp * 1.2f);
					AmmoPickupFactory.Instance.SpawnPickup(ammo, posToSpawn, Vector3.up);
				}
			}
		}
	}
}

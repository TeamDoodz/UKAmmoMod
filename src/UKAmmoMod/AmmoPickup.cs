using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using BepInEx.Configuration;

namespace UKAmmoMod {
	public sealed class AmmoPickup : MonoBehaviour {
		static float MinMultiplier = MainPlugin.cfg.Bind(
			new ConfigDefinition(
				"Pickups",
				"MinMultiplier"
			),
			1.0f,
			new ConfigDescription("The multiplier for ammo pickups at no style rank.")
		).Value;
		static float MaxMultiplier = MainPlugin.cfg.Bind(
			new ConfigDefinition(
				"Pickups",
				"MaxMultiplier"
			),
			6.0f,
			new ConfigDescription("The multiplier for ammo pickups at ULTRAKILL style rank.")
		).Value;

		public int GiveCells;
		public int GiveShells;
		public int GiveNails;
		public int GiveRockets;

		public const float DistanceToPickUp = 10f;

		public AnimationCurve StyleLevelScaling = new();

		private float pickupTimer = 0.1f;
		private float despawnTimer = 10f;

		private Rigidbody rb;
		private SphereCollider col;

		private void Awake() {
			StyleLevelScaling.keys[0].value = MinMultiplier;
			StyleLevelScaling.keys[1].value = MaxMultiplier;
			rb = GetComponent<Rigidbody>();
			col = GetComponent<SphereCollider>();
			int styleMultiplier = (int)StyleLevelScaling.Evaluate(StyleHUD.Instance.rankIndex / 7);
			GiveCells *= styleMultiplier;
			GiveShells *= styleMultiplier;
			GiveNails *= styleMultiplier;
			GiveRockets *= styleMultiplier;
		}

		private void Update() {
			pickupTimer -= Time.deltaTime;
			despawnTimer -= Time.deltaTime;
			if(despawnTimer < 0f) Destroy(gameObject);
			if(pickupTimer > 0f) return;

			Vector3 playerPos = NewMovement.Instance.GetComponent<Collider>().ClosestPoint(transform.position);
			float dist = Vector3.Distance(transform.position, playerPos);
			if(dist <= DistanceToPickUp) {
				rb.useGravity = false;
				rb.velocity = (playerPos - transform.position).normalized * 15f;
			} else {
				rb.useGravity = true;
			}
			if(dist <= col.radius) {
				int styleMultiplier = (int)StyleLevelScaling.Evaluate(StyleHUD.Instance.rankIndex / 7);
				if(
					AmmoInventory.Instance.TryAddCells(GiveCells * styleMultiplier) ||
					AmmoInventory.Instance.TryAddShells(GiveShells * styleMultiplier) ||
					AmmoInventory.Instance.TryAddNails(GiveNails * styleMultiplier) ||
					AmmoInventory.Instance.TryAddRockets(GiveRockets * styleMultiplier)
				) {
					Destroy(gameObject);
				}
			}
		}

		private void OnDrawGizmos() {
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(transform.position, DistanceToPickUp);
		}
	}
}

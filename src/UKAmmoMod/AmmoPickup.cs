using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace UKAmmoMod {
	public sealed class AmmoPickup : MonoBehaviour {
		public int GiveCells;
		public int GiveShells;
		public int GiveNails;
		public int GiveRockets;

		public const float DistanceToPickUp = 15f;

		public AnimationCurve StyleLevelScaling = new();

		private float pickupTimer = 0.1f;

		private Rigidbody rb;
		private SphereCollider col;

		private void Awake() {
			rb = GetComponent<Rigidbody>();
			col = GetComponent<SphereCollider>();
		}

		private void Update() {
			pickupTimer -= Time.deltaTime;
			if(pickupTimer > 0f) return;

			Vector3 playerPos = NewMovement.Instance.GetComponent<Collider>().ClosestPoint(transform.position);
			float dist = Vector3.Distance(transform.position, playerPos);
			if(dist <= DistanceToPickUp) {
				rb.useGravity = false;
				rb.AddForce(Vector3.ClampMagnitude((playerPos - transform.position), 10f) * 10f, ForceMode.Acceleration);
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

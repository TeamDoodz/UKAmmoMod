using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace UKAmmoMod; 

public sealed class DontCollideWithPlayer : MonoBehaviour {
	void Awake() {
		Physics.IgnoreCollision(GetComponent<Collider>(), NewMovement.Instance.GetComponent<Collider>());
	}
}

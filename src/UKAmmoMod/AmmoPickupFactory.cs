using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace UKAmmoMod; 

[ConfigureSingleton(SingletonFlags.PersistAutoInstance)]
public class AmmoPickupFactory : MonoSingleton<AmmoPickupFactory> {
	private Dictionary<PickupPrefab, GameObject> prefabs = new Dictionary<PickupPrefab, GameObject>();

	// MonoSingleton.Awake is public instead of protected for some reason. Kinda cringe tbh
	public override void Awake() {
		base.Awake();
		prefabs.Add(PickupPrefab.Cells,   ModAssets.MainBundle.LoadAsset<GameObject>("assets/prefabs/cellspickup.prefab"));
		prefabs.Add(PickupPrefab.Shells,  ModAssets.MainBundle.LoadAsset<GameObject>("assets/prefabs/shellspickup.prefab"));
		prefabs.Add(PickupPrefab.Nails,   ModAssets.MainBundle.LoadAsset<GameObject>("assets/prefabs/nailspickup.prefab"));
		prefabs.Add(PickupPrefab.Rockets, ModAssets.MainBundle.LoadAsset<GameObject>("assets/prefabs/rocketspickup.prefab"));
	}

	public AmmoPickup SpawnPickup(PickupPrefab prefab, Vector3 position, Vector3 velocity) {
		GameObject obj = GameObject.Instantiate(prefabs[prefab]);
		obj.transform.position = position;
		obj.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.VelocityChange);
		return obj.GetComponent<AmmoPickup>();
	}
}

using UnityEngine;
using UnityEngine.Rendering;

namespace UKAmmoMod; 

///<summary>
/// Makes an object point to whatever camera is currently rendering.
///</summary>
[ExecuteInEditMode]
public class Billboard : MonoBehaviour {

	public enum Mode {
		X,
		XY
	}

	[field: SerializeField]
	public Mode CurrentMode { get; set; } = Mode.X;

	protected Transform T {
		get {
			if(t == null) t = transform;
			return t;
		}
	}
	private Transform? t;


	private void Update() {
		var quaternion = Quaternion.LookRotation(T.position - Camera.main.transform.position);
		if(CurrentMode == Mode.X) quaternion = Quaternion.Euler(T.rotation.x, quaternion.eulerAngles.y, quaternion.eulerAngles.z);
		T.rotation = quaternion;
	}

}

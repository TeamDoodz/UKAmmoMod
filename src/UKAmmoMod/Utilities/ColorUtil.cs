using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace UKAmmoMod.Utilities; 

internal static class ColorUtil {
	public static Color MoveTowards(Color current, Color target, float maxDelta) {
		Vector4 outp = Vector4.MoveTowards(current, target, maxDelta);
		return outp;
	}
}

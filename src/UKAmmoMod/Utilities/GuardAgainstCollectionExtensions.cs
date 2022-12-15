using System;
using System.Collections.Generic;
using System.Text;

namespace UKAmmoMod.Utilities; 

internal static class GuardAgainst {
	public static T[] LengthOutOfRange<T>(T[] array, string parameterName, int min, int max) {
		if(array.Length < min || array.Length > max) throw new ArgumentException(parameterName);
		return array;
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.GuardClauses;

namespace UKAmmoMod.Utilities {
	internal static class GuardAgainstCollectionExtensions {
		public static T[] LengthOutOfRange<T>(this IGuardClause guardClause, T[] array, string parameterName, int min, int max) {
			if(array.Length < min || array.Length > max) throw new ArgumentException(parameterName);
			return array;
		}
	}
}

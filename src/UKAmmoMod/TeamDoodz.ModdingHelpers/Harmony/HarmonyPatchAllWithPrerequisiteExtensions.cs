using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using HarmonyLib;

namespace TeamDoodz.ModdingHelpers.Harmony;

internal static class HarmonyPatchAllWithPrerequisiteExtensions {
	const string PREREQUSITE_PROP_NAME = "Prerequisite";

	/// <summary>
	/// Performs patching. If a patch class implements a static bool property called <c>Prerequisite</c> or marked with <see cref="HarmonyPrerequisiteAttribute"/>, it will only be patched if it returns true.
	/// </summary>
	public static void PatchAllWithPrerequisite(this HarmonyLib.Harmony harmony, Type type) {
		if(TryGetPrerequisite(type, out PropertyInfo prop)) {
			if(!(bool)prop.GetValue(null)) {
				// Using Harmony logger here so that this code doesn't depend on BepInEx and can be used anywhere
				FileLog.Log($"Not patching {type} because its prerequisite returned false.");
			}
		}
		harmony.PatchAll(type);
	}

	/// <inheritdoc cref="PatchAllWithPrerequisite(HarmonyLib.Harmony, Type)"/>
	public static void PatchAllWithPrerequisite(this HarmonyLib.Harmony harmony, Assembly ass) {
		AccessTools.GetTypesFromAssembly(ass).Do(harmony.PatchAllWithPrerequisite);
	}

	/// <inheritdoc cref="PatchAllWithPrerequisite(HarmonyLib.Harmony, Assembly)"/>
	/// <remarks>This method can fail to use the correct assembly when being inlined. It calls StackTrace.GetFrame(1) which can point to the wrong method/assembly. If you are unsure or run into problems, use <code>PatchAll(Assembly.GetExecutingAssembly())</code> instead.</remarks>
	public static void PatchAllWithPrerequisite(this HarmonyLib.Harmony harmony) {
		var method = new StackTrace().GetFrame(1).GetMethod();
		var assembly = method.ReflectedType.Assembly;
		harmony.PatchAllWithPrerequisite(assembly);
	}

	private static bool TryGetPrerequisite(Type type, out PropertyInfo prop) {
		prop = type.GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
			.Where((prop) => prop.Name == PREREQUSITE_PROP_NAME || prop.GetCustomAttribute<HarmonyPrerequisiteAttribute>() != null)
			.FirstOrDefault((prop) => prop.PropertyType == typeof(bool));
		return prop != null;
	}
}

[AttributeUsage(AttributeTargets.Property)]
internal sealed class HarmonyPrerequisiteAttribute : Attribute {

}

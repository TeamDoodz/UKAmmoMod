using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace UKAmmoMod; 

[BepInPlugin(GUID, Name, Version)]
public sealed class MainPlugin : BaseUnityPlugin {
	public const string GUID = "io.github.TeamDoodz.UKAmmoMod";
	public const string Name = "Ammo Mod";
	public const string Version = "1.0.0";

	private static MainPlugin? instance;
	internal static MainPlugin Instance {
		get {
			if(instance == null) throw new Exception("Instance is null.");
			return instance;
		}
	}
	internal static ManualLogSource logger => Instance.Logger;
	internal static ConfigFile cfg => Instance.Config;

	private void Awake() {
		instance = this;
		new Harmony(GUID).PatchAll();
		logger.LogMessage($"{Name} v{Version} loaded!");
	}
}

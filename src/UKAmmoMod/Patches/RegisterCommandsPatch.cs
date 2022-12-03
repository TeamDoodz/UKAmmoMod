using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using GameConsole;
using HarmonyLib;
using Console = GameConsole.Console;

namespace UKAmmoMod.Patches; 

[HarmonyPatch(typeof(Console), nameof(Console.Awake))]
static class RegisterCommandsPatch {
	static void Postfix(Console __instance) {
		foreach(Type type in Assembly.GetExecutingAssembly().GetTypes()) {
			if(type.IsAbstract) continue; // no abstract types
			if(type.GetInterface(nameof(ICommand)) == null) continue; // must implement ICommand
			if(!type.GetConstructors().Any((x) => x.GetParameters().Length == 0)) continue; // must have a parameterless constructor

			__instance.RegisterCommand((ICommand)Activator.CreateInstance(type));
		}
	}
}

using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using BepInEx.Configuration;
using Ardalis.GuardClauses;

namespace UKAmmoMod {
	/// <summary>
	/// Represents the player's inventory. This resets at the start of each level. <br/>
	/// <br/>
	/// <b>Cells</b>:  The Revolver consumes 1 per normal shot, and 6 per charged shot.<br/>
	/// <b>Shells</b>: The Shotgun consumes 1 per normal shot, and 2 per core eject.<br/>
	/// <b>Nails</b>: The Nailgun consumes 1 per normal shot. The Sawblade Launcher consumes 4 per shot. <br/>
	/// <b>Rockets</b>: The Rocket Launcher consumes 1 per normal shot, and 1 per boulder shot.
	/// </summary>
	public sealed class AmmoInventory : MonoSingleton<AmmoInventory> {
		public delegate void AmmoChangedEvent(int before, int after);

		private static int BindMaxAmmo(string ammoType, int defaultVal) {
			return MainPlugin.cfg.Bind(
				new ConfigDefinition(
					"MaxAmmo",
					$"Max{ammoType}"
				),
				defaultVal,
				new ConfigDescription($"Maximum for {ammoType}.")
			).Value;
		}

		public static int MaxCells { get; } = BindMaxAmmo("Cells", 60);
		public static int MaxShells { get; } = BindMaxAmmo("Shells", 20);
		public static int MaxNails { get; } = BindMaxAmmo("Nails", 350);
		public static int MaxRockets { get; } = BindMaxAmmo("Rockets", 5);

		private int _cells = Mathf.FloorToInt(MaxCells * 0.75f);
		private int _shells = Mathf.FloorToInt(MaxShells * 0.75f);
		private int _nails = Mathf.FloorToInt(MaxNails * 0.75f);
		private int _rockets = Mathf.FloorToInt(MaxRockets * 0.75f);

		public event AmmoChangedEvent? OnCellsChanged;
		public event AmmoChangedEvent? OnShellsChanged;
		public event AmmoChangedEvent? OnNailsChanged;
		public event AmmoChangedEvent? OnRocketsChanged;

		public int Cells {
			get => _cells;
			set {
				int before = _cells;
				_cells = Guard.Against.OutOfRange(value, nameof(value), 0, MaxCells);
				OnCellsChanged?.Invoke(before, _cells);
			}
		}
		public int Shells {
			get => _shells;
			set {
				int before = _shells;
				_shells = Guard.Against.OutOfRange(value, nameof(value), 0, MaxShells);
				OnShellsChanged?.Invoke(before, _cells);
			}
		}
		public int Nails {
			get => _nails;
			set {
				int before = _nails;
				_nails = Guard.Against.OutOfRange(value, nameof(value), 0, MaxNails);
				OnNailsChanged?.Invoke(before, _cells);
			}
		}
		public int Rockets {
			get => _shells;
			set {
				int before = _rockets;
				_rockets = Guard.Against.OutOfRange(value, nameof(value), 0, MaxRockets);
				OnRocketsChanged?.Invoke(before, _rockets);
			}
		}

#if DEBUG
		private void OnGUI() {
			static void logAmmo(string ammoName, int amount) {
				GUILayout.Label($"{ammoName}: {amount}");
			}

			GUI.skin = MainPlugin.DebugSkin;

			logAmmo("Cells", Cells);
			logAmmo("Shells", Shells);
			logAmmo("Nails", Nails);
			logAmmo("Rockets", Rockets);
		}
#endif
	}
}

using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using BepInEx.Configuration;

namespace UKAmmoMod; 

/// <summary>
/// Represents the player's inventory. This resets at the start of each level.
/// </summary>
[ConfigureSingleton(SingletonFlags.NoAutoInstance)]
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

	public static int MaxCells { get; } = BindMaxAmmo("Cells", 20);
	public static int MaxShells { get; } = BindMaxAmmo("Shells", 10);
	public static int MaxNails { get; } = BindMaxAmmo("Nails", 125);
	public static int MaxRockets { get; } = BindMaxAmmo("Rockets", 5);

	private int _cells = MaxCells;
	private int _shells = MaxShells;
	private int _nails = MaxNails;
	private int _rockets = MaxRockets;

	public event AmmoChangedEvent? OnCellsChanged;
	public event AmmoChangedEvent? OnShellsChanged;
	public event AmmoChangedEvent? OnNailsChanged;
	public event AmmoChangedEvent? OnRocketsChanged;

	public bool TryAddCells(int amount) {
		int before = Cells;
		Cells += amount;
		return Cells != before;
	}
	public bool TryAddShells(int amount) {
		int before = Shells;
		Shells += amount;
		return Shells != before;
	}
	public bool TryAddNails(int amount) {
		int before = Nails;
		Nails += amount;
		return Nails != before;
	}
	public bool TryAddRockets(int amount) {
		int before = Rockets;
		Rockets += amount;
		return Rockets != before;
	}

	public int Cells {
		get => _cells;
		set {
			int before = _cells;
			_cells = Mathf.Clamp(value, 0, MaxCells);
			if(_cells == before) return;
			OnCellsChanged?.Invoke(before, _cells);
		}
	}
	public int Shells {
		get => _shells;
		set {
			int before = _shells;
			_shells = Mathf.Clamp(value, 0, MaxShells);
			if(_shells == before) return;
			OnShellsChanged?.Invoke(before, _shells);
		}
	}
	public int Nails {
		get => _nails;
		set {
			int before = _nails;
			_nails = Mathf.Clamp(value, 0, MaxNails);
			if(_nails == before) return;
			OnNailsChanged?.Invoke(before, _nails);
		}
	}
	public int Rockets {
		get => _rockets;
		set {
			int before = _rockets;
			_rockets = Mathf.Clamp(value, 0, MaxRockets);
			if(_rockets == before) return;
			OnRocketsChanged?.Invoke(before, _rockets);
		}
	}

	public void ReplenishAll() {
		Cells = MaxCells;
		Shells = MaxShells;
		Nails = MaxNails;
		Rockets = MaxRockets;
	}

	public void HalfReplenishAll() {
		Cells = Mathf.Max(Cells, MaxCells / 2);
		Shells = Mathf.Max(Shells, MaxShells / 2);
		Nails = Mathf.Max(Nails, MaxNails / 2);
		Rockets = Mathf.Max(Cells, MaxRockets / 2);
	}

	public bool IsFullOf(PickupPrefab prefab) {
		return prefab switch {
			PickupPrefab.Cells => Cells >= MaxCells,
			PickupPrefab.Shells => Shells >= MaxShells,
			PickupPrefab.Nails => Nails >= MaxNails,
			PickupPrefab.Rockets => Rockets >= MaxRockets,
			_ => false,
		};
	}
}

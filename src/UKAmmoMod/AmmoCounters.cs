using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace UKAmmoMod {
	public sealed class AmmoCounters : MonoBehaviour {
		[SerializeField] private Text? cellsLabel;
		[SerializeField] private Text? shellsLabel;
		[SerializeField] private Text? nailsLabel;
		[SerializeField] private Text? rocketsLabel;

		private void Start() {
			AmmoInventory.Instance.OnCellsChanged += OnCellsChanged;
			AmmoInventory.Instance.OnShellsChanged += OnShellsChanged;
			AmmoInventory.Instance.OnNailsChanged += OnNailsChanged;
			AmmoInventory.Instance.OnRocketsChanged += OnRocketsChanged;

			OnCellsChanged(0, AmmoInventory.Instance.Cells);
			OnShellsChanged(0, AmmoInventory.Instance.Shells);
			OnNailsChanged(0, AmmoInventory.Instance.Nails);
			OnRocketsChanged(0, AmmoInventory.Instance.Rockets);
		}

		private void OnCellsChanged(int before, int after) {
			if(cellsLabel == null) return;
			cellsLabel.text = after.ToString();
		}

		private void OnShellsChanged(int before, int after) {
			if(shellsLabel == null) return;
			shellsLabel.text = after.ToString();
		}

		private void OnNailsChanged(int before, int after) {
			if(nailsLabel == null) return;
			nailsLabel.text = after.ToString();
		}

		private void OnRocketsChanged(int before, int after) {
			if(rocketsLabel == null) return;
			rocketsLabel.text = after.ToString();
		}
	}
}

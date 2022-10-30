using System;
using System.Collections.Generic;
using System.Text;
using UKAmmoMod.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace UKAmmoMod {
	public sealed class AmmoCounters : MonoBehaviour {
		[SerializeField] private Text? cellsLabel;
		[SerializeField] private Text? shellsLabel;
		[SerializeField] private Text? nailsLabel;
		[SerializeField] private Text? rocketsLabel;

		private Color cellsOriginalCol;
		private Color shellsOriginalCol;
		private Color nailsOriginalCol;
		private Color rocketsOriginalCol;

		private void Start() {
			cellsOriginalCol = cellsLabel?.color ?? Color.white;
			shellsOriginalCol = shellsLabel?.color ?? Color.white;
			nailsOriginalCol = nailsLabel?.color ?? Color.white;
			rocketsOriginalCol = rocketsLabel?.color ?? Color.white;

			AmmoInventory.Instance.OnCellsChanged += OnCellsChanged;
			AmmoInventory.Instance.OnShellsChanged += OnShellsChanged;
			AmmoInventory.Instance.OnNailsChanged += OnNailsChanged;
			AmmoInventory.Instance.OnRocketsChanged += OnRocketsChanged;

			OnCellsChanged(0, AmmoInventory.Instance.Cells);
			OnShellsChanged(0, AmmoInventory.Instance.Shells);
			OnNailsChanged(0, AmmoInventory.Instance.Nails);
			OnRocketsChanged(0, AmmoInventory.Instance.Rockets);
		}

		private void Update() {
			cellsLabel.color = ColorUtil.MoveTowards(cellsLabel.color, cellsOriginalCol, Time.deltaTime * 2f);
			shellsLabel.color = ColorUtil.MoveTowards(shellsLabel.color, shellsOriginalCol, Time.deltaTime * 2f);
			nailsLabel.color = ColorUtil.MoveTowards(nailsLabel.color, nailsOriginalCol, Time.deltaTime * 2f);
			rocketsLabel.color = ColorUtil.MoveTowards(rocketsLabel.color, rocketsOriginalCol, Time.deltaTime * 2f);
		}

		private void OnCellsChanged(int before, int after) {
			if(cellsLabel == null) return;
			cellsLabel.text = after.ToString();
			if(after > before) cellsLabel.color += new Color(0.3f, 0.3f, 0.3f);
		}

		private void OnShellsChanged(int before, int after) {
			if(shellsLabel == null) return;
			shellsLabel.text = after.ToString();
			if(after > before) shellsLabel.color += new Color(0.3f, 0.3f, 0.3f);
		}

		private void OnNailsChanged(int before, int after) {
			if(nailsLabel == null) return;
			nailsLabel.text = after.ToString();
			if(after > before) nailsLabel.color += new Color(0.3f, 0.3f, 0.3f);
		}

		private void OnRocketsChanged(int before, int after) {
			if(rocketsLabel == null) return;
			rocketsLabel.text = after.ToString();
			if(after > before) rocketsLabel.color += new Color(0.3f, 0.3f, 0.3f);
		}
	}
}

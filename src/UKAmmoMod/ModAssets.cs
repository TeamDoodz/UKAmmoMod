using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace UKAmmoMod; 

public static class ModAssets {
	private static AssetBundle? mainBundle;
	public static AssetBundle MainBundle {
		get {
			if(mainBundle == null) {
				mainBundle = AssetBundle.LoadFromFile(Path.Combine(Path.GetDirectoryName(MainPlugin.Instance.Info.Location),$"{nameof(UKAmmoMod).ToLower()}_assets"));
				foreach(var asset in mainBundle.GetAllAssetNames()) {
					MainPlugin.logger.LogDebug(asset);
				}
			}
			return mainBundle;
		}
	}
}

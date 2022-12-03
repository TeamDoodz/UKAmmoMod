using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace UKAmmoMod.Patches.RevolverAmmo
{
    [HarmonyPatch(typeof(Revolver), nameof(Revolver.Shoot))]
    static class RevolverUseAmmoWhenFiringPatch
    {
        const int FIRE_NORMAL = 1;
        const int FIRE_SUPER = 2;

        static bool Prefix(Revolver __instance, int shotType)
        {
            int normalAmnt = __instance.altVersion ? 2 : 1;

            if (shotType == FIRE_SUPER)
            {
                if (AmmoInventory.Instance.Cells < 6) return false;
                AmmoInventory.Instance.Cells -= 6;
            }
            else
            {
                if (AmmoInventory.Instance.Cells < normalAmnt) return false;
                AmmoInventory.Instance.Cells--;
            }
            return true;
        }
    }
}

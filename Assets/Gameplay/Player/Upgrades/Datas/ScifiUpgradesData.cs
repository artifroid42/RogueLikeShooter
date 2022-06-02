using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Player.Upgrades
{
    [CreateAssetMenu(fileName = "ScifiUpgradeData", menuName = "RLS/Gameplay/Upgrades/Scifi Upgrade Data")]
    public class ScifiUpgradesData : ACommonUpgradesData
    {
        [Serializable]
        public struct SficiPowerUpgrade
        {
            public float PowerShotLoadingTime;
            public float PowerShotDamages;
        }

        public List<SficiPowerUpgrade> SficiPowerUpgrades;
    }
}


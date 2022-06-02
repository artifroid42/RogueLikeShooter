using System;
using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Player.Upgrades
{
    [CreateAssetMenu(fileName = "PirateUpgradeData", menuName = "RLS/Gameplay/Upgrades/Pirate Upgrade Data")]
    public class PirateUpgradesData : ACommonUpgradesData
    {
        [Serializable]
        public struct PiratePowerUpgrade
        {
            public float BarilCouldown;
            public int BarilDamages;
        }

        public List<PiratePowerUpgrade> PiratePowerUpgrades;
    }
}

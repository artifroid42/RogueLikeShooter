using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Player.Upgrades
{
    [CreateAssetMenu(fileName = "NinjaUpgradeData", menuName = "RLS/Gameplay/Upgrades/Ninja Upgrade Data")]
    public class NinjaUpgradesData : ACommonUpgradesData
    {
        [Serializable]
        public struct NinjaPowerUpgrade
        {
            public float DashCouldown;
            public float DashRange;
        }

        public List<NinjaPowerUpgrade> NinjaPowerUpgrades;
    }
}


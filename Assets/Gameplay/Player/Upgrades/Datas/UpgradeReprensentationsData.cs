using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Player.Upgrades
{
    [CreateAssetMenu(fileName = "UpgradeReprensentationsData", menuName = "RLS/Gameplay/Upgrades/Upgrade Reprensentations Data")]
    public class UpgradeReprensentationsData : ScriptableObject
    {
        [Serializable]
        public struct UpgradeRepresentation
        {
            public EUpgrade Upgrade;
            public Color UpgradeColor;
            public Sprite UpgradeSprite;
        }

        public List<UpgradeRepresentation> UpgradeRepresentations;
    }
}

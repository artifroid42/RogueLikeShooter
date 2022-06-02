using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Player.Upgrades
{
    public abstract class ACommonUpgradesData : ScriptableObject
    {
        public EClass Class;

        public List<int> HealthLevels;
        public List<int> DamageLevels;
        public List<float> AttackSpeedLevels;
        public List<float> MoveSpeedLevels;
    }
}

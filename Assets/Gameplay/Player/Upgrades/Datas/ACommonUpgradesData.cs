using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Player.Upgrades
{
    public abstract class ACommonUpgradesData : ScriptableObject
    {
        public EClass Class;

        public List<float> HealthLevels;
        public List<float> DamageLevels;
        public List<float> AttackSpeedLevels;
        public List<float> MoveSpeedLevels;
    }
}

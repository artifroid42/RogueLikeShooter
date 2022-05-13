using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Player.Upgrades
{
    [CreateAssetMenu(fileName = "PlayerStatsData", menuName = "RLS/Gameplay/Upgrades/Player Stats Data")]
    public class PlayerStatsData : ScriptableObject
    {
        [SerializeField]
        private ClassStatsData m_ninjaStatsData;
        [SerializeField]
        private ClassStatsData m_pirateStatsData;
        [SerializeField]
        private ClassStatsData m_scifiStatsData;
        [SerializeField]
        private AnimationCurve m_experienceLevelProgression;
        [SerializeField]
        private int m_maxLevel = 30;
        [SerializeField]
        private float m_expAmountForLevelMax = 10000f;

        public ClassStatsData NinjaStatsData => m_ninjaStatsData;
        public ClassStatsData PirateStatsData => m_pirateStatsData;
        public ClassStatsData ScifiStatsData => m_scifiStatsData;
        public AnimationCurve ExperienceLevelProgression => m_experienceLevelProgression;
        public float ExpAmountForLevelMax => m_expAmountForLevelMax; 
        public int MaxLevel => m_maxLevel;


    }
}
using UnityEngine;

namespace RLS.Gameplay.Ennemy.LongRange
{
    public class LongRangeMonsterAI : MonsterAI
    {
        [Header("Long Range Refs")]
        [SerializeField]
        private Transform m_projectileSource = null;
        public Transform ProjectileSource => m_projectileSource;

        [SerializeField]
        private float m_damageImprovementOffset = 0.2f;
        public float DamageImprovementOffset => m_damageImprovementOffset;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RLS.Gameplay.Player.Upgrades
{
    public enum EClass
    {
        Ninja,
        Pirate,
        SciFi
    }

    [CreateAssetMenu(fileName = "ClassStatsData", menuName = "RLS/Gameplay/Upgrades/Class Stats Data")]
    public class ClassStatsData : ScriptableObject
    {
        [SerializeField]
        private int m_maxUpgradesCount = 10;

        [Header("Start stats")]
        [SerializeField]
        private float m_startHealth = 10;
        [SerializeField]
        private float m_startDamage = 10f;
        [SerializeField]
        private float m_startAttackSpeed = 10f;
        [SerializeField]
        private float m_startMoveSpeed = 10f;
        [SerializeField]
        private int m_powerLevel = 1;

        [Header("Upgrade increments")]
        [SerializeField]
        private float m_healthIncrement = 0.2f;
        [SerializeField]
        private float m_attackIncrement = 0.2f;
        [SerializeField]
        private float m_attackSpeedIncrement = 0.2f;
        [SerializeField]
        private float m_moveSpeedIncrement = 0.2f;

        [Header("Ressources")]
        [SerializeField]
        private EClass m_class;
        [SerializeField]
        private Sprite m_classSprite;

        public float StartHealth => m_startHealth;
        public float StartDamage => m_startDamage;
        public float AttackSpeed => m_startAttackSpeed;
        public float MoveSpeed => m_startMoveSpeed;
        public int PowerLevel => m_powerLevel;

        public float HealthIncrement => m_healthIncrement;
        public float AttackIncrement => m_attackIncrement;
        public float AttackSpeedIncrement => m_attackSpeedIncrement;
        public float MoveSpeedIncrement => m_moveSpeedIncrement;

        public EClass Class => m_class;
        public Sprite ClassSprite => m_classSprite;

        public int MaxUpgradesCount => m_maxUpgradesCount; 
    }
}

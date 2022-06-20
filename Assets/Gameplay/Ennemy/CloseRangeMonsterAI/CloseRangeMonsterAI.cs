using RLS.Gameplay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Ennemy.CloseRange
{
    public class CloseRangeMonsterAI : MonsterAI
    {
        [SerializeField]
        private DamageDealer m_weaponDamageDealer = null;
        public DamageDealer WeaponDamageDealer => m_weaponDamageDealer;

        [SerializeField]
        private float m_damageImprovementOffset = 0.1f;

        internal override void EnterStateMachine()
        {
            base.EnterStateMachine();
            float damageFactor = MOtter.MOtt.GM.GetCurrentMainStateMachine<DungeonFlow.DungeonGameMode>().LevelManager.LevelsCount * m_damageImprovementOffset;
            m_weaponDamageDealer.SetDamageToDeal((int)(m_weaponDamageDealer.DamageToDeal + m_weaponDamageDealer.DamageToDeal * damageFactor));
        }
    }
}
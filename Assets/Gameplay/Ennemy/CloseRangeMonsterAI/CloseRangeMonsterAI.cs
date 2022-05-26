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
    }
}
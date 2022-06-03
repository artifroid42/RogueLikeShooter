using RLS.Utils;
using System;
using UnityEngine;

namespace RLS.Gameplay.Combat
{
    public class CombatController : MonoBehaviour
    {
        [SerializeField]
        private ushort m_teamIndex = 0;
        public ushort TeamIndex => m_teamIndex;

        [SerializeField]
        protected int m_maxLifePoints = 100;
        protected int m_lifePoints = 0;
        public int MaxLifePoints => m_maxLifePoints;
        public int LifePoints => m_lifePoints;

        public Action<CombatController> OnLifeChanged = null;
        public Action<CombatController> OnDamageTaken = null;
        public Action<CombatController> OnDied = null;
        public Action<CombatController> OnDamageGiven = null;

        public CombatController LastControllerToHitMe { protected set; get; } = null;
        public float LastTimeControllerHitMe { protected set; get; } = 0f;
        public bool IsAlive { private set; get; } = true;

        protected virtual void Start()
        {
            m_lifePoints = m_maxLifePoints;
        }

        public void SetTeam(ushort a_teamIndex)
        {
            m_teamIndex = a_teamIndex;
        }

        public void SetMaxLifePoints(int a_maxLifePoints, ECurrentLifeBehaviourWhenChangingMaxLife a_lifeBehaviour = ECurrentLifeBehaviourWhenChangingMaxLife.ChangeProportionally)
        {
            switch (a_lifeBehaviour)
            {
                case ECurrentLifeBehaviourWhenChangingMaxLife.StayStill:
                    break;
                case ECurrentLifeBehaviourWhenChangingMaxLife.ChangeProportionally:
                    m_lifePoints = Mathf.RoundToInt(((float)m_lifePoints).Map(0f, m_maxLifePoints, 0f, a_maxLifePoints));
                    break;
                case ECurrentLifeBehaviourWhenChangingMaxLife.SetToMaxLife:
                    m_lifePoints = a_maxLifePoints;
                    break;
                default:
                    break;
            }
            m_maxLifePoints = a_maxLifePoints;
            OnLifeChanged?.Invoke(this);
        }

        public void TakeDamage(int a_damageToDeal, CombatController a_source)
        {
            if (!IsAlive) return;

            if(a_source == null || a_source.TeamIndex != m_teamIndex)
            {
                m_lifePoints -= a_damageToDeal;
                OnDamageTaken?.Invoke(this);
                OnLifeChanged?.Invoke(this);

                if(a_source != null)
                {
                    LastControllerToHitMe = a_source;
                    LastTimeControllerHitMe = Time.time;
                    a_source.HandleDamageGiven(this);
                    a_source.OnDamageGiven?.Invoke(a_source);
                }

                if(m_lifePoints <= 0)
                {
                    IsAlive = false;
                    HandleDeath();
                    OnDied?.Invoke(this);
                }
            }
        }

        protected virtual void HandleDamageGiven(CombatController a_target)
        {

        }

        protected virtual void HandleDeath()
        {
            
        }
    }

    public enum ECurrentLifeBehaviourWhenChangingMaxLife
    {
        StayStill,
        ChangeProportionally,
        SetToMaxLife
    }
}
using RLS.Utils;
using UnityEngine;

namespace RLS.Gameplay.Combat
{
    public class CombatController : MonoBehaviour
    {
        [SerializeField]
        private ushort m_teamIndex = 0;
        public ushort TeamIndex => m_teamIndex;

        [SerializeField]
        private int m_maxLifePoints = 100;
        private int m_lifePoints = 0;

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
            
        }

        public void TakeDamage(int a_damageToDeal, CombatController a_source)
        {
            if(a_source == null || a_source.TeamIndex != m_teamIndex)
            {
                m_lifePoints -= a_damageToDeal;
                if(m_lifePoints < 0)
                {
                    HandleDeath();
                }
            }
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
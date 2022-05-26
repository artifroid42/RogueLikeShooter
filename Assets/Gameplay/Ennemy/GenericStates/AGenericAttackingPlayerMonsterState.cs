using UnityEngine;

namespace RLS.Gameplay.Ennemy
{
    public abstract class AGenericAttackingPlayerMonsterState<T> : AIs.TreeBehaviourState<T> where T : MonsterAI
    {
        [Header("Gameplay Params")]
        [SerializeField]
        private float m_attackCooldown = 1f;
        private float m_timeOfLastAttack = 0f;

        [Header("Config Params")]
        [SerializeField]
        private float m_rotationSmoothness = 5f;

        private Vector3 m_targettedForward = default;

        private void Start()
        {
            m_isContinuousState = false;
        }

        public override void EnterState()
        {
            base.EnterState();
            Owner.Agent.SetDestination(Owner.transform.position);
        }

        public override void UpdateState()
        {
            base.UpdateState();
            update_rotation();
            if (Time.time - m_timeOfLastAttack > m_attackCooldown)
            {
                TryToAttack();
                m_timeOfLastAttack = Time.time;
            }
        }

        private void update_rotation()
        {
            m_targettedForward = (Owner.ClosestPlayer.transform.position - Owner.transform.position);
            m_targettedForward.y = 0;
            m_targettedForward.Normalize();
            Owner.transform.forward = Vector3.Lerp(Owner.transform.forward, m_targettedForward, m_rotationSmoothness * Time.deltaTime);
        }

        protected virtual void TryToAttack()
        {

        }
    }
    public abstract class AGenericAttackingPlayerMonsterState : AGenericAttackingPlayerMonsterState<MonsterAI>
    {

    }
}
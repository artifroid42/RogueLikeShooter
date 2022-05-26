using RLS.Gameplay.AIs;
using UnityEngine;

namespace RLS.Gameplay.Ennemy
{
    public class RotatingTowardLastControllerThatHitMe : TreeBehaviourState<MonsterAI>
    {
        private Vector3 m_originForward = default;
        private Vector3 m_targettedForward = default;
        private float m_rotationInterpolationValue = 0f;
        public override void EnterState()
        {
            base.EnterState();
            set_up_rotation();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            m_rotationInterpolationValue += (1f / Owner.DurationToLookAtAfterBeingHit) * Time.deltaTime;

            Owner.transform.forward = Vector3.Lerp(m_originForward, m_targettedForward, m_rotationInterpolationValue);
            //Debug.Log($"interValue: {m_rotationInterpolationValue} | forwardToApply: {m_targettedForward}");
            if (m_rotationInterpolationValue > 1f)
            {
                set_up_rotation();
            }
        }

        private void set_up_rotation()
        {
            m_rotationInterpolationValue = 0;
            m_originForward = Owner.transform.forward;
            m_targettedForward = Owner.CombatController.LastControllerToHitMe.transform.position - Owner.transform.position;
        }
    }
}
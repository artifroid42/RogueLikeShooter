using UnityEngine;

namespace RLS.Gameplay.Ennemy
{
    public class GettingCloserToPlayerMonsterState : AIs.TreeBehaviourState<MonsterAI>
    {
        private const float UPDATE_PATH_DELAY = 0.5f;

        private float m_lastTimeOfPathUpdate = 0;
        public override void UpdateState()
        {
            base.UpdateState();
            if (Time.time - m_lastTimeOfPathUpdate > UPDATE_PATH_DELAY && Owner.ClosestPlayer != null)
            {
                Owner.Agent.SetDestination(Owner.ClosestPlayer.transform.position);
                Owner.LastPlayerPositionKnown = Owner.ClosestPlayer.transform.position;
                m_lastTimeOfPathUpdate = Time.time;
            }
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}
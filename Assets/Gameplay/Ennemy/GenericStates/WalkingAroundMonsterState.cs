using UnityEngine;

namespace RLS.Gameplay.Ennemy
{
    public class WalkingAroundMonsterState : AIs.TreeBehaviourState<MonsterAI>
    {
        private const float UPDATE_PATH_DELAY = 0.5f;

        private float m_lastTimeOfPathUpdate = 0;
        public override void UpdateState()
        {
            base.UpdateState();
            if (Time.time - m_lastTimeOfPathUpdate > UPDATE_PATH_DELAY)
            {
                Owner.Agent.SetDestination(Owner.SpawnPosition);
                m_lastTimeOfPathUpdate = Time.time;
            }
        }
    }
}
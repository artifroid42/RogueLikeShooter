using UnityEngine;
using UnityEngine.AI;

namespace RLS.Gameplay.Ennemy
{
    public class GoingToLastPlayerPositionKnownState : AIs.TreeBehaviourState<MonsterAI>
    {
        Vector3 m_lastPlayerPositionKnownOnNavMesh = default;
        public override void EnterState()
        {
            base.EnterState();
            if(NavMesh.FindClosestEdge(Owner.LastPlayerPositionKnown, out NavMeshHit hit, NavMesh.AllAreas))
            {
                m_lastPlayerPositionKnownOnNavMesh = hit.position;
                Owner.Agent.SetDestination(m_lastPlayerPositionKnownOnNavMesh);
            }
            else
            {
                Owner.HasReachedLastPlayerPositionKnown = true;
            }
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if ((m_lastPlayerPositionKnownOnNavMesh - Owner.transform.position).sqrMagnitude <= Owner.Agent.velocity.sqrMagnitude * Time.deltaTime)
            {
                Owner.HasReachedLastPlayerPositionKnown = true;
            }
        }
    }
}
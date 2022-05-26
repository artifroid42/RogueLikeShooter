using RLS.Gameplay.AIs;
using UnityEngine;
using UnityEngine.AI;

namespace RLS.Gameplay.Ennemy
{
    public class MonsterAI : TreeBehaviour
    {
        [Header("Components Refs")]
        [SerializeField]
        private NavMeshAgent m_agent = null;
        public NavMeshAgent Agent => m_agent;

        [Header("States Refs")]
        [SerializeField]
        private AttackingPlayerMonsterState m_attackingPlayerState = null;
        [SerializeField]
        private GettingCloserToPlayerMonsterState m_gettingCloseToPlayerState = null;
        [SerializeField]
        private WalkingAroundMonsterState m_walkingAroundState = null;

        [Header("Sight")]
        [SerializeField]
        private Transform m_sightSource = null;
        [SerializeField]
        private LayerMask m_sightLayerMask = default;
        [SerializeField]
        private float m_sightMaxDistance = 30f;
        [SerializeField]
        private float m_ratioWidthAngle = 0.5f;

        [Header("Params")]
        [SerializeField]
        private float m_distanceToAttack = 3f;

        private DungeonFlow.DungeonGameMode m_gamemode = null;
        private Player.Player m_closestPlayer = null;

        internal override void EnterStateMachine()
        {
            base.EnterStateMachine();
            m_gamemode = MOtter.MOtt.GM.GetCurrentMainStateMachine<DungeonFlow.DungeonGameMode>();
        }

        protected override void ProcessChoice()
        {
            base.ProcessChoice();
            if(is_seeing_a_player())
            {
                if(is_close_enough_to_attack())
                {
                    RequestState(m_attackingPlayerState);
                }
                else
                {
                    RequestState(m_gettingCloseToPlayerState);
                }
            }
            else
            {
                RequestState(m_walkingAroundState);
            }
        }

        protected bool is_seeing_a_player()
        {
            m_closestPlayer = null;
            float minDistanceToAPlayer = float.MaxValue;
            for (int i = 0; i < m_gamemode.Players.Count; ++i)
            {
                Vector3 localPlayerDir = transform.InverseTransformPoint(m_gamemode.Players[i].transform.position).normalized;
                RaycastHit hitInfo;
                float distanceToThisPlayer = Vector3.Distance(m_gamemode.Players[i].transform.position, transform.position);
                if (distanceToThisPlayer < m_sightMaxDistance
                    && distanceToThisPlayer < minDistanceToAPlayer
                    && localPlayerDir.z > 0 
                    && Mathf.Abs(localPlayerDir.x) < m_ratioWidthAngle)
                {
                    for(int j = 0; j < m_gamemode.Players[i].SeeablePositions.Length; ++j)
                    {
                        var sightDir = (m_gamemode.Players[i].SeeablePositions[j].transform.position - m_sightSource.position).normalized;
                        if (Physics.Raycast(m_sightSource.position, sightDir, out hitInfo, m_sightMaxDistance, m_sightLayerMask))
                        {
                            if(hitInfo.collider.GetComponent<Player.Player>() == m_gamemode.Players[i])
                            {
                                m_closestPlayer = m_gamemode.Players[i];
                                minDistanceToAPlayer = distanceToThisPlayer;
                            }
                        }
                    }
                    if(m_closestPlayer == m_gamemode.Players[i])
                    {
                        continue;
                    }
                }
            }
            return m_closestPlayer != null;
        }

        protected bool is_close_enough_to_attack()
        {
            return Vector3.Distance(m_closestPlayer.transform.position, transform.position) < m_distanceToAttack; 
        }
    }
}
using RLS.Gameplay.AIs;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace RLS.Gameplay.Ennemy
{
    public class MonsterAI : TreeBehaviour
    {
        [Header("Components Refs")]
        [SerializeField]
        private NavMeshAgent m_agent = null;
        [SerializeField]
        private MonsterCombatController m_combatController = null;
        public NavMeshAgent Agent => m_agent;
        public MonsterCombatController CombatController => m_combatController;

        [Header("States Refs")]
        [SerializeField]
        private TreeBehaviourState m_attackingPlayerState = null;
        [SerializeField]
        private TreeBehaviourState m_gettingCloseToPlayerState = null;
        [SerializeField]
        private TreeBehaviourState m_walkingAroundState = null;
        [SerializeField]
        private TreeBehaviourState m_rotatingTowardLastControllerThatHitMe = null;

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
        [SerializeField]
        private float m_durationToStopFollowingPlayerAfterLosingSightOnHim = 2f;
        private float m_lastTimePlayerSeen = 0f;
        [SerializeField]
        private float m_durationToLookAtAfterBeingHit = 1f;
        public float DurationToLookAtAfterBeingHit => m_durationToLookAtAfterBeingHit;

        private DungeonFlow.DungeonGameMode m_gamemode = null;
        public Player.Player ClosestPlayer { private set; get; } = null;

        public Vector3 SpawnPosition { private set; get; } = default;

        internal override void EnterStateMachine()
        {
            base.EnterStateMachine();
            m_gamemode = MOtter.MOtt.GM.GetCurrentMainStateMachine<DungeonFlow.DungeonGameMode>();
            SpawnPosition = transform.position;
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
                if(has_been_hit_recently())
                {
                    RequestState(m_rotatingTowardLastControllerThatHitMe);
                }
                else
                {
                    RequestState(m_walkingAroundState);
                }
            }
        }

        private bool has_been_hit_recently()
        {
            return Time.time - CombatController.LastTimeControllerHitMe < DurationToLookAtAfterBeingHit;
        }

        protected bool is_seeing_a_player()
        {
            ClosestPlayer = null;
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
                                ClosestPlayer = m_gamemode.Players[i];
                                minDistanceToAPlayer = distanceToThisPlayer;
                                m_lastTimePlayerSeen = Time.time;
                            }
                        }
                    }
                    if(ClosestPlayer == m_gamemode.Players[i])
                    {
                        continue;
                    }
                }
            }
            return ClosestPlayer != null || Time.time - m_lastTimePlayerSeen < m_durationToStopFollowingPlayerAfterLosingSightOnHim;
        }

        protected bool is_close_enough_to_attack()
        {
            if (ClosestPlayer != null)
                return Vector3.Distance(ClosestPlayer.transform.position, transform.position) < m_distanceToAttack;
            else
                return false;
        }
    }
}
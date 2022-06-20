using RLS.Gameplay.AIs;
using RLS.Gameplay.Combat;
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
        [SerializeField]
        private EnnemyAnimationsHandler m_animationsHandler = null;
        [SerializeField]
        private Outline m_outline = null;
        [SerializeField]
        private HPLostFeedback m_HPLostFeedback = null;
        public NavMeshAgent Agent => m_agent;
        public MonsterCombatController CombatController => m_combatController;
        public EnnemyAnimationsHandler AnimationsHandler => m_animationsHandler;
        public Outline Outline => m_outline;
        public HPLostFeedback HPLostFeedback => m_HPLostFeedback;

        [Header("States Refs")]
        [SerializeField]
        private TreeBehaviourState m_attackingPlayerState = null;
        [SerializeField]
        private TreeBehaviourState m_gettingCloseToPlayerState = null;
        [SerializeField]
        private TreeBehaviourState m_walkingAroundState = null;
        [SerializeField]
        private TreeBehaviourState m_rotatingTowardLastControllerThatHitMe = null;
        [SerializeField]
        private TreeBehaviourState m_goingToLastPlayerPositionKnown = null;

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

        private float m_lastTimePlayerSeen = 0f;
        [SerializeField]
        private float m_durationToLookAtAfterBeingHit = 1f;
        public float DurationToLookAtAfterBeingHit => m_durationToLookAtAfterBeingHit;

        [SerializeField]
        private float m_expReward = 500f;

        private DungeonFlow.DungeonGameMode m_gamemode = null;
        public Player.Player ClosestPlayer { private set; get; } = null;
        public Vector3 LastPlayerPositionKnown { set; get; } = default;
        public Vector3 SpawnPosition { private set; get; } = default;

        public bool HasReachedLastPlayerPositionKnown = true;

        private bool m_isWatched = false;

        internal override void EnterStateMachine()
        {
            base.EnterStateMachine();
            m_gamemode = MOtter.MOtt.GM.GetCurrentMainStateMachine<DungeonFlow.DungeonGameMode>();
            SpawnPosition = transform.position;

            m_combatController.OnDied += HandleMonsterDied;
        }

        internal override void ExitStateMachine()
        {
            m_combatController.OnDied -= HandleMonsterDied;

            base.ExitStateMachine();
        }

        private void Awake()
        {
            m_outline.OutlineWidth = 0f;
        }

        private void HandleMonsterDied(CombatController obj)
        {
            m_gamemode.Players[0].PlayerExpManager.EarnExp(m_expReward);
        }

        internal void SetWatched(bool a_isWatched)
        {
            if(a_isWatched == !m_isWatched)
            {
                m_outline.OutlineWidth = a_isWatched ? 4f : 0f;
                m_isWatched = a_isWatched;
            }
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
                HasReachedLastPlayerPositionKnown = false;
            }
            else
            {
                if(HasReachedLastPlayerPositionKnown)
                {
                    if (has_been_hit_recently())
                    {
                        RequestState(m_rotatingTowardLastControllerThatHitMe);
                    }
                    else
                    {
                        RequestState(m_walkingAroundState);
                    }
                }
                else
                {
                    RequestState(m_goingToLastPlayerPositionKnown);
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
                Vector3 localPlayerDir = transform.InverseTransformPoint(m_gamemode.Players[i].CurrentPlayer.transform.position).normalized;
                RaycastHit hitInfo;
                float distanceToThisPlayer = Vector3.Distance(m_gamemode.Players[i].CurrentPlayer.transform.position, transform.position);
                if (distanceToThisPlayer < m_sightMaxDistance
                    && distanceToThisPlayer < minDistanceToAPlayer
                    && localPlayerDir.z > 0 
                    && Mathf.Abs(localPlayerDir.x) < m_ratioWidthAngle)
                {
                    for(int j = 0; j < m_gamemode.Players[i].CurrentPlayer.SeeablePositions.Length; ++j)
                    {
                        var sightDir = (m_gamemode.Players[i].CurrentPlayer.SeeablePositions[j].transform.position - m_sightSource.position).normalized;
                        if (Physics.Raycast(m_sightSource.position, sightDir, out hitInfo, m_sightMaxDistance, m_sightLayerMask))
                        {
                            if(hitInfo.collider.GetComponent<Player.Player>() == m_gamemode.Players[i].CurrentPlayer)
                            {
                                ClosestPlayer = m_gamemode.Players[i].CurrentPlayer;
                                minDistanceToAPlayer = distanceToThisPlayer;
                                m_lastTimePlayerSeen = Time.time;
                            }
                        }
                    }
                    if(ClosestPlayer == m_gamemode.Players[i].CurrentPlayer)
                    {
                        continue;
                    }
                }
            }
            return ClosestPlayer != null;
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
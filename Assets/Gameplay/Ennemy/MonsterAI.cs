using RLS.Gameplay.AIs;
using UnityEngine;

namespace RLS.Gameplay.Ennemy
{
    public class MonsterAI : TreeBehaviour
    {
        [Header("States Refs")]
        [SerializeField]
        private AttackingPlayerMonsterState m_attackingPlayerState = null;
        [SerializeField]
        private GettingCloserToPlayerMonsterState m_gettingCloseToPlayerState = null;
        [SerializeField]
        private WalkingAroundMonsterState m_walkingAroundState = null;

        private Player.Player m_closestPlayer = null;


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

        private bool is_seeing_a_player()
        {
            return false;
        }

        private bool is_close_enough_to_attack()
        {
            return false;
        }
    }
}
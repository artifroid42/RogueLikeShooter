using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Ennemy
{
    public class AttackingPlayerMonsterState : AIs.TreeBehaviourState<MonsterAI>
    {
        private void Start()
        {
            m_isContinuousState = false;
        }
    }
}
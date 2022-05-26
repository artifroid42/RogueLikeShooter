using UnityEngine;

namespace RLS.Gameplay.Ennemy
{
    public class EnnemyAnimationsHandler : MonoBehaviour
    {
        private int ATTACK_TRIGGER = Animator.StringToHash("Attack");

        [SerializeField]
        private Animator m_animator = null;

        public void Attack()
        {
            m_animator.SetTrigger(ATTACK_TRIGGER);
        }


    }
}
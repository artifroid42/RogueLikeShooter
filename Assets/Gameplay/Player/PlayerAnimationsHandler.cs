using UnityEngine;

namespace RLS.Gameplay.Player
{
    public class PlayerAnimationsHandler : Character.Animations.CharacterAnimationsHandler
    {
        private int SHURIKEN_THROW_TRIGGER = Animator.StringToHash("ThrowShuriken");
        private int SWORD_ATTACK_TRIGGER = Animator.StringToHash("SwordAttack");

        public void ThrowShuriken()
        {
            m_animator.SetTrigger(SHURIKEN_THROW_TRIGGER);
        }

        public void SwordAttack()
        {
            m_animator.SetTrigger(SWORD_ATTACK_TRIGGER);
        }
    }
}
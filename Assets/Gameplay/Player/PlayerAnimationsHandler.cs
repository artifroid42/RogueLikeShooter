using UnityEngine;

namespace RLS.Gameplay.Player
{
    public class PlayerAnimationsHandler : Character.Animations.CharacterAnimationsHandler
    {
        private int SHURIKEN_THROW_TRIGGER = Animator.StringToHash("ThrowShuriken");
        private int SWORD_ATTACK_TRIGGER = Animator.StringToHash("SwordAttack");
        private int GUN_SHOOT_TRIGGER = Animator.StringToHash("GunShoot");

        public void ThrowShuriken()
        {
            m_animator.SetTrigger(SHURIKEN_THROW_TRIGGER);
        }

        public void SwordAttack()
        {
            m_animator.SetTrigger(SWORD_ATTACK_TRIGGER);
        }

        public void ThrowExplosiveBarrel()
        {
            m_animator.SetTrigger(SHURIKEN_THROW_TRIGGER);
        }

        public void GunShoot()
        {
            //m_animator.SetTrigger(GUN_SHOOT_TRIGGER);
        }
    }
}
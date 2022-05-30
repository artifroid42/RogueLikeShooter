using UnityEngine;

namespace RLS.Gameplay.Player
{
    public class PlayerAnimationsHandler : Character.Animations.CharacterAnimationsHandler
    {
        private int SHURIKEN_THROW_TRIGGER = Animator.StringToHash("ThrowShuriken");

        public void ThrowShuriken()
        {
            m_animator.SetTrigger(SHURIKEN_THROW_TRIGGER);
        }
    }
}
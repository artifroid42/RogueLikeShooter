using UnityEngine;

namespace RLS.Gameplay.Ennemy
{
    public class EnnemyAnimationsHandler : Character.Animations.CharacterAnimationsHandler
    {
        private int SWORD_ATTACK_TRIGGER = Animator.StringToHash("SwordAttack");
        private int SPELL_ATTACK_TRIGGER = Animator.StringToHash("SpellAttack");

        public void SwordAttack()
        {
            m_animator.SetTrigger(SWORD_ATTACK_TRIGGER);
        }

        public void CastSpell()
        {
            m_animator.SetTrigger(SPELL_ATTACK_TRIGGER);
        }

    }
}
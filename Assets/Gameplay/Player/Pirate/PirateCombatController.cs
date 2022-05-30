using UnityEngine;

namespace RLS.Gameplay.Player.Pirate
{
    public class PirateCombatController : PlayerCombatController
    {
        [Header("Refs")]
        [SerializeField]
        private Combat.Weapon.PirateSword m_pirateSword = null;
        [SerializeField]
        private PlayerAnimationsHandler m_animationHandler = null;

        public Combat.Weapon.PirateSword PirateSword => m_pirateSword;

        [Header("Params")]
        [SerializeField]
        private float m_attackCooldown = 1.5f;
        private float m_timeOfLastAttack = 0f;
        public override void HandleAttackStartedInput()
        {
            base.HandleAttackStartedInput();
            if (Time.time - m_timeOfLastAttack < m_attackCooldown) return;
            m_timeOfLastAttack = Time.time;


            m_animationHandler.SwordAttack();
            m_pirateSword.CanDoDamage = true;
        }
    }
}
using UnityEngine;

namespace RLS.Gameplay.Player.Pirate
{
    public class PirateAnimationsEventsHandler : MonoBehaviour
    {
        [SerializeField]
        private PirateCombatController m_combatController = null;
        public void StopAttacking()
        {
            m_combatController.PirateSword.CanDoDamage = false;
        }
    }
}
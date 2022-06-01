using RLS.Gameplay.Combat;
using UnityEngine;

namespace RLS.Gameplay.Player
{
    public class PlayerCombatInfosManager : MonoBehaviour
    {
        [SerializeField]
        private UI.PlayerUIManagersManager m_UIManager = null;

        private PlayerCombatController m_combatController = null;

        public void SetCombatControllerRef(PlayerCombatController a_combatController)
        {
            if(m_combatController != null)
            {
                m_combatController.OnDamageTaken -= HandleDamageTaken;
                m_combatController.OnDied -= HandleDied;
            }

            m_combatController = a_combatController;
            m_UIManager.PlayerPanel.HealthBar.SetHealthSliderValue((float)a_combatController.LifePoints
                / (float)a_combatController.MaxLifePoints);
            m_combatController.OnDamageTaken += HandleDamageTaken;
            m_combatController.OnDied += HandleDied;
        }

        private void HandleDied(CombatController a_combatController)
        {
            
        }

        private void HandleDamageTaken(CombatController a_combatController)
        {
            m_UIManager.PlayerPanel.HealthBar.SetHealthSliderValue((float) a_combatController.LifePoints 
                / (float) a_combatController.MaxLifePoints);
        }
    }
}
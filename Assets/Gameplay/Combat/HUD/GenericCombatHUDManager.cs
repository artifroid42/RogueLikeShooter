using RLS.Gameplay.Player.UI;
using UnityEngine;
using UnityEngine.UI;

namespace RLS.Gameplay.Combat.HUD
{
    [RequireComponent(typeof(CombatController))]
    public class GenericCombatHUDManager : MonoBehaviour
    {
        private CombatController m_combatController = null;
        [SerializeField]
        private HealthBar m_healthBar = null;

        private void Awake()
        {
            m_combatController = GetComponent<CombatController>();
            m_combatController.OnDamageTaken += HandleDamageTaken;
        }

        private void OnDestroy()
        {
            m_combatController.OnDamageTaken -= HandleDamageTaken;
        }

        private void HandleDamageTaken(CombatController a_combatController)
        {
            m_healthBar.SetHealthSliderValue((float) a_combatController.LifePoints / (float)a_combatController.MaxLifePoints);
        }
    }
}
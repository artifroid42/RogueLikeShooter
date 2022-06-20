using UnityEngine;
using TMPro;
using RLS.Gameplay.Player.Upgrades;

namespace RLS.Gameplay.Player.UI
{
    public class PlayerPanel : Panel
    {
        [SerializeField]
        private ExpBar m_expBar = null;
        [SerializeField]
        private TextMeshProUGUI m_playerLevelText = null;
        [SerializeField]
        private ClassUpgradesModule m_classUpgradesModule = null;
        [SerializeField]
        private HealthBar m_healthBar = null;
        [SerializeField]
        private Cursor m_cursor = null;
        [SerializeField]
        private PowerCouldownWidget m_powerCouldownWidget = null;
        [SerializeField]
        private TextMeshProUGUI m_currentStageDisplay = null;
        [SerializeField]
        private PopUp.PopUpManager m_popUpManager = null;

        public ExpBar ExpBar => m_expBar;
        public TextMeshProUGUI PlayerLevelText => m_playerLevelText;
        public ClassUpgradesModule ClassUpgradesModule => m_classUpgradesModule;
        public HealthBar HealthBar => m_healthBar;
        public Cursor Cursor => m_cursor;
        public PowerCouldownWidget PowerCouldownWidget => m_powerCouldownWidget;
        public PopUp.PopUpManager PopUpManager => m_popUpManager;

        public void SetStageDisplay(int a_stageNumber)
        {
            m_currentStageDisplay.text = a_stageNumber.ToString();
        }
    }
}

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

        public ExpBar ExpBar => m_expBar;
        public TextMeshProUGUI PlayerLevelText => m_playerLevelText;
        public ClassUpgradesModule ClassUpgradesModule => m_classUpgradesModule;
        public HealthBar HealthBar => m_healthBar;
        public Cursor Cursor => m_cursor;
        public PowerCouldownWidget PowerCouldownWidget => m_powerCouldownWidget; 
    }
}

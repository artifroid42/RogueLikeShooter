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
        private PlayerHealthBar m_healthBar = null;
        [SerializeField]
        private Cursor m_cursor = null;

        public ExpBar ExpBar => m_expBar;
        public TextMeshProUGUI PlayerLevelText => m_playerLevelText;
        public ClassUpgradesModule ClassUpgradesModule => m_classUpgradesModule;
        public PlayerHealthBar HealthBar => m_healthBar;
        public Cursor Cursor => m_cursor; 
    }
}

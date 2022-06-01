using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RLS.Gameplay.Player.Upgrades;

namespace RLS.Gameplay.Player.UI
{
    public class PlayerPanel : Panel
    {
        [SerializeField]
        private ExpBar m_expBar;
        [SerializeField]
        private TextMeshProUGUI m_playerLevelText;
        [SerializeField]
        private ClassUpgradesModule m_classUpgradesModule;

        public ExpBar ExpBar => m_expBar;
        public TextMeshProUGUI PlayerLevelText => m_playerLevelText;
        public ClassUpgradesModule ClassUpgradesModule => m_classUpgradesModule; 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RLS.Gameplay.Player.UI
{
    public class PlayerPanel : Panel
    {
        [SerializeField]
        private ExpBar m_expBar;
        [SerializeField]
        private TextMeshProUGUI m_playerLevelText;

        public ExpBar ExpBar => m_expBar;
        public TextMeshProUGUI PlayerLevelText => m_playerLevelText; 
    }
}

using UnityEngine;
using UnityEngine.UI;

namespace RLS.MainMenu.ModeSelectionScreen
{
    public class ModeSelectionScreenPanel : Panel
    {
        [SerializeField]
        private Button m_playEasyButton = null;
        [SerializeField]
        private Button m_playHardButton = null;
        [SerializeField]
        private Button m_backButton = null;

        public Button PlayEasyButton => m_playEasyButton;
        public Button PlayHardButton => m_playHardButton;
        public Button BackButton => m_backButton;
    }
}
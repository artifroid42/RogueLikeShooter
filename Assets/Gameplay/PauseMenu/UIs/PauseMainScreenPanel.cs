using UnityEngine;
using UnityEngine.UI;

namespace RLS.Gameplay.PauseMenu
{
    public class PauseMainScreenPanel : Panel
    {
        [SerializeField]
        private Button m_keepPlayingButton = null;
        [SerializeField]
        private Button m_optionsButton = null;
        [SerializeField]
        private Button m_backToMenuButton = null;
        [SerializeField]
        private Button m_quitGameButton = null;
        public Button KeepPlayingButton => m_keepPlayingButton;
        public Button OptionsButton => m_optionsButton;
        public Button BackToMenuButton => m_backToMenuButton;
        public Button QuitGameButton => m_quitGameButton;
    }
}
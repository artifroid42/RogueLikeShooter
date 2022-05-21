using UnityEngine;
using UnityEngine.UI;

namespace RLS.MainMenu.MainScreen
{
    public class MainScreenPanel : Panel
    {
        [SerializeField]
        private Button m_playButton = null;
        [SerializeField]
        private Button m_optionsButton = null;
        [SerializeField]
        private Button m_quitButton = null;

        public Button PlayButton => m_playButton;
        public Button OptionsButton => m_optionsButton;
        public Button QuitButton => m_quitButton;
    }
}
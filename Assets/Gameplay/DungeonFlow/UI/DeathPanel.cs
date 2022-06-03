using UnityEngine;
using UnityEngine.UI;

namespace RLS.Gameplay.DungeonFlow.UI
{
    public class DeathPanel : Panel
    {
        [SerializeField]
        private Button m_replayButton = null;
        [SerializeField]
        private Button m_quitButton = null;
        public Button ReplayButton => m_replayButton;
        public Button QuitButton => m_quitButton;
    }
}
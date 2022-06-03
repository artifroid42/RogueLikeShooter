using TMPro;
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
        [SerializeField]
        private TextMeshProUGUI m_stagesDoneText = null;
        public Button ReplayButton => m_replayButton;
        public Button QuitButton => m_quitButton;

        public void SetStagesDone(int a_stagesDone)
        {
            m_stagesDoneText.text = string.Format($"You survived {a_stagesDone} stages !");
        }
    }
}
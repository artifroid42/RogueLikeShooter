using MOtter.LevelData;
using UnityEngine;

namespace RLS.Gameplay.PauseMenu
{
    public class PauseMainScreenState : PauseMenuState
    {
        [SerializeField]
        private LevelData m_mainMenuLevelData = null;

        private PauseMainScreenPanel m_panel = null;

        internal override void RegisterReferences()
        {
            base.RegisterReferences();
            m_panel = GetPanel<PauseMainScreenPanel>();
        }

        internal override void RegisterEvents()
        {
            base.RegisterEvents();
            m_panel.KeepPlayingButton.onClick.AddListener(ResumeGame);
            m_panel.BackToMenuButton.onClick.AddListener(GoBackToMenu);
            m_panel.QuitGameButton.onClick.AddListener(QuitGame);
        }

        internal override void UnregisterEvents()
        {
            base.UnregisterEvents();
        }

        public void ResumeGame()
        {
            MOtter.MOtt.GM.GetCurrentMainStateMachine<DungeonFlow.DungeonGameMode>().Unpause();
        }

        public void GoBackToMenu()
        {

        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
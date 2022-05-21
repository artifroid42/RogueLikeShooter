using MOtter.LevelData;
using UnityEngine;

namespace RLS.MainMenu.MainScreen
{
    public class MainScreenState : MainMenuState
    {
        private MainScreenPanel m_panel = null;

        [SerializeField]
        private LevelData m_dungeonLevelData;

        internal override void SetUpDependencies()
        {
            base.SetUpDependencies();
            m_panel = GetPanel<MainScreenPanel>();
        }

        internal override void RegisterEvents()
        {
            base.RegisterEvents();
            m_panel.PlayButton.onClick.AddListener(PlayGame);
            m_panel.QuitButton.onClick.AddListener(QuitGame);
        }

        internal override void UnregisterEvents()
        {
            m_panel.PlayButton.onClick.RemoveListener(PlayGame);
            m_panel.QuitButton.onClick.RemoveListener(QuitGame);
            base.UnregisterEvents();
        }

        public void PlayGame()
        {
            m_dungeonLevelData.LoadLevel();
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
using MOtter.LevelData;
using UnityEngine;

namespace RLS.MainMenu.MainScreen
{
    public class MainScreenState : MainMenuState
    {
        private MainScreenPanel m_panel = null;

        internal override void SetUpDependencies()
        {
            base.SetUpDependencies();
            m_panel = GetPanel<MainScreenPanel>();
        }

        internal override void RegisterEvents()
        {
            base.RegisterEvents();
            m_panel.PlayButton.onClick.AddListener(PlayGame);
            m_panel.OptionsButton.onClick.AddListener(SeeOptions);
            m_panel.QuitButton.onClick.AddListener(QuitGame);
        }

        internal override void UnregisterEvents()
        {
            m_panel.PlayButton.onClick.RemoveListener(PlayGame);
            m_panel.OptionsButton.onClick.RemoveListener(SeeOptions);
            m_panel.QuitButton.onClick.RemoveListener(QuitGame);
            base.UnregisterEvents();
        }

        public void PlayGame()
        {
            MOtter.MOtt.SOUND.Play2DSound(SFXManager.Instance.Menu);
            m_gamemode.SwitchToModeSelectionScreen();
        }

        public void SeeOptions()
        {
            MOtter.MOtt.SOUND.Play2DSound(SFXManager.Instance.Menu);
            m_gamemode.SwitchToOptions();
        }

        public void QuitGame()
        {
            MOtter.MOtt.SOUND.Play2DSound(SFXManager.Instance.Menu);
            Application.Quit();
        }
    }
}
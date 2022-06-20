using MOtter.LevelData;
using RLS.Generic.ModeSelection;
using UnityEngine;

namespace RLS.MainMenu.ModeSelectionScreen
{
    public class ModeSelectionScreenState : MainMenuState
    {
        private ModeSelectionScreenPanel m_panel = null;

        [SerializeField]
        private LevelData m_dungeonLevelData;

        internal override void RegisterReferences()
        {
            base.RegisterReferences();
            m_panel = GetPanel<ModeSelectionScreenPanel>();
        }

        internal override void RegisterEvents()
        {
            base.RegisterEvents();
            m_panel.PlayEasyButton.onClick.AddListener(HandlePlayEasyButtonClicked);
            m_panel.PlayHardButton.onClick.AddListener(HandlePlayHardButtonClicked);
            m_panel.BackButton.onClick.AddListener(HandleBackButtonClicked);
        }

        internal override void UnregisterEvents()
        {
            base.UnregisterEvents();
            m_panel.PlayEasyButton.onClick.RemoveListener(HandlePlayEasyButtonClicked);
            m_panel.PlayHardButton.onClick.RemoveListener(HandlePlayHardButtonClicked);
            m_panel.BackButton.onClick.RemoveListener(HandleBackButtonClicked);
        }

        private void HandleBackButtonClicked()
        {
            MOtter.MOtt.SOUND.Play2DSound(SFXManager.Instance.Menu);
            m_gamemode.SwitchToPreviousState();
        }

        private void HandlePlayHardButtonClicked()
        {
            ModeSelectedData modeSelectedData = new ModeSelectedData();
            modeSelectedData.IsEasyMode = false;
            MOtter.MOtt.DATACONVEY.RegisterContainer(modeSelectedData);
            MOtter.MOtt.SOUND.Play2DSound(SFXManager.Instance.Menu);
            PlayHardcoreGame();
        }

        private void HandlePlayEasyButtonClicked()
        {
            ModeSelectedData modeSelectedData = new ModeSelectedData();
            modeSelectedData.IsEasyMode = true;
            MOtter.MOtt.DATACONVEY.RegisterContainer(modeSelectedData);
            MOtter.MOtt.SOUND.Play2DSound(SFXManager.Instance.Menu);
            PlayEasyGame();
        }

        public void PlayEasyGame()
        {
            m_dungeonLevelData.LoadLevel();
        }

        public void PlayHardcoreGame()
        {
            m_dungeonLevelData.LoadLevel();
        }
    }
}
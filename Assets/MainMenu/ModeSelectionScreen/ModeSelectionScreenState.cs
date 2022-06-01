using MOtter.LevelData;
using System;
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
            m_gamemode.SwitchToPreviousState();
        }

        private void HandlePlayHardButtonClicked()
        {
            PlayHardcoreGame();
        }

        private void HandlePlayEasyButtonClicked()
        {
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
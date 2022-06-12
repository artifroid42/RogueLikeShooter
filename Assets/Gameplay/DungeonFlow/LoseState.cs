using MOtter.LevelData;
using RLS.Gameplay.DungeonFlow.UI;
using RLS.Generic.ModeSelection;
using UnityEngine;

namespace RLS.Gameplay.DungeonFlow
{
    public class LoseState : DungeonGameState
    {
        private DeathPanel m_panel;

        [SerializeField]
        private LevelData m_dungeonLevelData = null;
        [SerializeField]
        private LevelData m_menuLevelData = null;

        internal override void RegisterReferences()
        {
            base.RegisterReferences();
            m_panel = GetPanel<DeathPanel>();
        }

        internal override void RegisterEvents()
        {
            base.RegisterEvents();
            m_panel.QuitButton.onClick.AddListener(QuitToMenu);
            m_panel.ReplayButton.onClick.AddListener(Retry);
        }

        internal override void SetUpDependencies()
        {
            base.SetUpDependencies();
            m_panel.SetStagesDone(MOtter.MOtt.GM.GetCurrentMainStateMachine<DungeonGameMode>().LevelManager.LevelsCount - 1);
        }
        internal override void UnregisterEvents()
        {
            base.UnregisterEvents();
            m_panel.QuitButton.onClick.RemoveListener(QuitToMenu);
            m_panel.ReplayButton.onClick.RemoveListener(Retry);
        }

        private bool m_choiceMade = false;

        public void Retry()
        {
            if (m_choiceMade) return;
            m_choiceMade = true;

            ModeSelectedData modeSelectedData = MOtter.MOtt.GM.GetCurrentMainStateMachine<DungeonFlow.DungeonGameMode>().ModeSelectedData;
            MOtter.MOtt.DATACONVEY.RegisterContainer(modeSelectedData);
            m_dungeonLevelData.LoadLevel();
        }

        public void QuitToMenu()
        {
            if (m_choiceMade) return;
            m_choiceMade = true;

            m_menuLevelData.LoadLevel();
        }
    }
}
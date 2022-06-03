using MOtter.StatesMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.DungeonFlow
{
    public class DungeonGameMode : PauseableFlowMachine
    {
        [Header("States Refs")]
        [SerializeField]
        private LoadingStageState m_loadingStageState = null;
        [SerializeField]
        private GameState m_gameState = null;
        [SerializeField]
        private WonState m_wonState = null;
        [SerializeField]
        private LoseState m_loseState = null;

        [Header("Scene Refs")]
        [SerializeField]
        private Levels.LevelManager m_levelManager = null;
        public Levels.LevelManager LevelManager => m_levelManager;

        [SerializeField]
        private Player.PlayerSpirit m_playerSpirit = null;

        public Generic.ModeSelection.ModeSelectedData ModeSelectedData { private set; get; }
        public List<Player.PlayerSpirit> Players { private set; get; } = null;

        public override IEnumerator LoadAsync()
        {
            m_levelManager.LoadNextStage();
            ModeSelectedData = MOtter.MOtt.DATACONVEY.GetFirstContainer<Generic.ModeSelection.ModeSelectedData>();
            MOtter.MOtt.DATACONVEY.UnregisterContainer(ModeSelectedData);
            yield return StartCoroutine(base.LoadAsync());
        }
        internal override void EnterStateMachine()
        {
            base.EnterStateMachine();
            m_levelManager.OnLoadingEnded += OnLevelLoadingEnded;
            m_levelManager.OnLoadingStarted += OnLevelLoadingStarted;

            m_playerSpirit.InitPlayer();

            Players = new List<Player.PlayerSpirit>();
            Players.Add(m_playerSpirit);
        }

        internal override void ExitStateMachine()
        {
            m_levelManager.OnLoadingEnded -= OnLevelLoadingEnded;
            m_levelManager.OnLoadingStarted -= OnLevelLoadingStarted;

            if(ModeSelectedData.IsEasyMode)
            {
                MOtter.MOtt.SAVE.MaximumLevelReached = m_playerSpirit.PlayerExpManager.CurrentLevel;
                MOtter.MOtt.SAVE.SaveSaveDataManager();
            }

            base.ExitStateMachine();
        }

        private void OnDestroy()
        {
            if (ModeSelectedData.IsEasyMode)
            {
                MOtter.MOtt.SAVE.MaximumLevelReached = m_playerSpirit.PlayerExpManager.CurrentLevel;
                MOtter.MOtt.SAVE.SaveSaveDataManager();
            }
        }

        public void Lose()
        {
            SwitchToState(m_loseState);
        }
        private void OnLevelLoadingStarted()
        {
            SwitchToState(m_loadingStageState);
        }

        private void OnLevelLoadingEnded()
        {
            SwitchToState(m_gameState);
        }
    }
}
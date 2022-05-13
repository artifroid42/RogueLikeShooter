using MOtter.StatesMachine;
using System.Collections;
using UnityEngine;

namespace RLS.Gameplay.DungeonFlow
{
    public class DungeonGameMode : MainFlowMachine
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

        [Header("Manager Refs")]
        [SerializeField]
        private Levels.LevelManager m_levelManager = null;
        public Levels.LevelManager LevelManager => m_levelManager;

        private Player.Player m_currentPlayer = null;

        public override IEnumerator LoadAsync()
        {
            m_levelManager.LoadNextStage();

            yield return StartCoroutine(base.LoadAsync());
        }
        internal override void EnterStateMachine()
        {
            base.EnterStateMachine();
            m_levelManager.OnLoadingEnded += OnLevelLoadingEnded;
            m_levelManager.OnLoadingStarted += OnLevelLoadingStarted;

            m_currentPlayer = FindObjectOfType<Player.Player>();
            m_currentPlayer.InitPlayer();
        }

        internal override void ExitStateMachine()
        {
            m_levelManager.OnLoadingEnded -= OnLevelLoadingEnded;
            m_levelManager.OnLoadingStarted -= OnLevelLoadingStarted;
            base.ExitStateMachine();
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
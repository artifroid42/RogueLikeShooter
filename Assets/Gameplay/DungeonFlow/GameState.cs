using RLS.Gameplay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RLS.Gameplay.DungeonFlow
{
    public class GameState : DungeonGameState, Player.IPlayerInputsObserver
    {
        private Player.Player m_currentPlayer = null;
        private Levels.Stage m_currentStage = null;

        private List<Ennemy.MonsterAI> m_ennemies = new List<Ennemy.MonsterAI>();

        private Player.UI.PlayerPanel m_playerPanel;
        private Player.UI.Debug.PlayerDebugPanel m_debugPlayerPanel;

        #region Life Cycle
        internal override void RegisterReferences()
        {
            base.RegisterReferences();
            m_currentPlayer = FindObjectOfType<Player.Player>();
            m_currentStage = FindObjectOfType<Levels.Stage>();
            m_playerPanel = GetPanel<Player.UI.PlayerPanel>();
            m_debugPlayerPanel = GetPanel<Player.UI.Debug.PlayerDebugPanel>();           
        }

        internal override void RegisterEvents()
        {
            base.RegisterEvents();
            m_currentStage.EndPortal.OnPlayerEnteredPortal += OnPlayerEnteredEndPortal;
            m_gamemode.Players[0].RegisterEvents();
            m_currentPlayer.GetComponent<Player.PlayerInputsHandler>().ActivateInputs();
            m_currentPlayer.GetComponent<Player.PlayerInputsHandler>().RegisterNewObserver(this);
            Cursor.lockState = CursorLockMode.Locked;
        }

        internal override void SetUpDependencies()
        {
            base.SetUpDependencies();
            m_gamemode.Players[0].HandleGameLevelChanged();
            m_reposingCoroutine = StartCoroutine(ReposingPlayer(m_currentPlayer.GetComponent<Player.PlayerMovementController>()));
            m_playerPanel.SetStageDisplay(m_gamemode.LevelManager.LevelsCount);
        }
        public override void EnterState()
        {
            base.EnterState();
            var playerExpManager = m_gamemode.Players[0].PlayerExpManager;
            m_gamemode.Players[0].PlayerUIManagersManager.ExpUIManager.RefreshPlayerInfos(playerExpManager.CurrentLevel, playerExpManager.CurrentExpAmount, playerExpManager.ExpAmountForNextLevel);

            spawning_ennemies();
            m_ennemies?.ForEach(x => x.EnterStateMachine());
        }

        public override void UpdateState()
        {
            base.UpdateState();
            //Debug.LogError($"{m_currentPlayer.transform.position}");
            if (m_currentPlayer != null && m_currentPlayer.transform.position.y < -100f)
            {
                if(m_reposingCoroutine == null)
                {
                    m_reposingCoroutine = StartCoroutine(ReposingPlayer(m_currentPlayer.GetComponent<Player.PlayerMovementController>()));
                }
            }

            m_ennemies?.ForEach(x => x.DoUpdate());
        }

        internal override void UnregisterEvents()
        {
            Cursor.lockState = CursorLockMode.None;
            m_currentPlayer.GetComponent<Player.PlayerInputsHandler>().UnregisterObserver(this);
            m_currentPlayer.GetComponent<Player.PlayerInputsHandler>().DeactivateInputs();
            m_currentStage.EndPortal.OnPlayerEnteredPortal -= OnPlayerEnteredEndPortal;
            m_gamemode.Players[0].UnregisterEvents();
            base.UnregisterEvents();
        }

        public override void ExitState()
        {
            m_ennemies?.ForEach(x => x.ExitStateMachine());

            m_currentPlayer.GetComponent<Player.PlayerMovementController>().DeactivateMovement();
            m_currentPlayer.GetComponent<Player.PlayerInputsHandler>().DeactivateInputs();
            base.ExitState();
        }
        #endregion

        #region Utils
        #region Reposing Players
        private Coroutine m_reposingCoroutine = null;
        private IEnumerator ReposingPlayer(Player.PlayerMovementController a_player)
        {
            a_player.DeactivateMovement();
            a_player.GetComponent<Player.PlayerInputsHandler>().DeactivateInputs();
            yield return null;
            a_player.transform.position = m_currentStage.SpawningPosition.position;
            a_player.transform.rotation = m_currentStage.SpawningPosition.rotation;
            Debug.LogError(m_currentPlayer.transform.position);
            yield return null;
            a_player.ActivateMovement();
            a_player.GetComponent<Player.PlayerInputsHandler>().ActivateInputs();
            m_reposingCoroutine = null;
        }

        private void OnPlayerEnteredEndPortal()
        {
            m_currentStage.EndPortal.OnPlayerEnteredPortal -= OnPlayerEnteredEndPortal;
            m_gamemode.LevelManager.LoadNextStage();

        }
        #endregion
        #region Handling Pause
        public void HandlePauseInput()
        {
            StartCoroutine(WaitAFrameToPause());
        }

        private IEnumerator WaitAFrameToPause()
        {
            yield return null;
            if (!m_gamemode.IsPaused)
                m_gamemode.Pause();
        }
        #endregion
        #region Ennemies Management
        private void spawning_ennemies()
        {
            m_ennemies.Clear();

            var spawners = FindObjectsOfType<Ennemy.Spawning.EnnemySpawner>();
            for (int i = 0; i < spawners.Length; ++i)
            {
                spawners[i].InstantiateRandomEnnemy();
                m_ennemies.Add(spawners[i].Ennemy);
                spawners[i].Ennemy.GetComponent<CombatController>().OnDied += OnEnnemyDied;
            }
        }

        private void OnEnnemyDied(CombatController a_combatController)
        {
            a_combatController.OnDied -= OnEnnemyDied;
            m_ennemies.Remove(a_combatController.GetComponent<Ennemy.MonsterAI>());

            if(m_ennemies.Count == 0)
            {
                m_currentStage.EndPortal.AllowAccessToNextStage();
            }
        }
        #endregion
        #endregion
    }
}
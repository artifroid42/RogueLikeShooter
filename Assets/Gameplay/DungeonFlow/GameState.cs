using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RLS.Gameplay.DungeonFlow
{
    public class GameState : DungeonGameState, Player.IPlayerInputsObserver
    {
        private Player.Player m_currentPlayer = null;
        private Levels.Stage m_currentStage = null;

        internal override void RegisterReferences()
        {
            base.RegisterReferences();
            m_currentPlayer = FindObjectOfType<Player.Player>();
            m_currentStage = FindObjectOfType<Levels.Stage>();

            var playerPanel = GetPanel<Player.UI.PlayerPanel>();
            var debugPlayerPanel = GetPanel<Player.UI.Debug.PlayerDebugPanel>();
            m_currentPlayer.UIManager.Init(playerPanel, debugPlayerPanel);
            
        }

        internal override void RegisterEvents()
        {
            base.RegisterEvents();
            m_currentStage.EndPortal.OnPlayerEnteredPortal += OnPlayerEnteredEndPortal;
            m_currentPlayer.UIManager.RegisterEvents();
            m_currentPlayer.GetComponent<Player.PlayerInputsHandler>().ActivateInputs();
            m_currentPlayer.GetComponent<Player.PlayerInputsHandler>().RegisterNewObserver(this);
            Cursor.lockState = CursorLockMode.Locked;
        }

        internal override void SetUpDependencies()
        {
            base.SetUpDependencies();
            m_reposingCoroutine = StartCoroutine(ReposingPlayer(m_currentPlayer.GetComponent<Player.PlayerMovementController>()));
        }
        public override void EnterState()
        {
            base.EnterState();
            m_currentPlayer.UIManager.RefreshPlayerInfos();
            
            Debug.LogError("ENTER GAME STATE");

            for(int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Debug.LogError($"{SceneManager.GetSceneAt(i).name}");
            }
        }

        public override void UpdateState()
        {
            base.UpdateState();
            //Debug.LogError($"{m_currentPlayer.transform.position}");
            if (m_currentPlayer.transform.position.y < -100f)
            {
                if(m_reposingCoroutine == null)
                {
                    m_reposingCoroutine = StartCoroutine(ReposingPlayer(m_currentPlayer.GetComponent<Player.PlayerMovementController>()));
                }
            }
            
        }

        internal override void UnregisterEvents()
        {
            Cursor.lockState = CursorLockMode.None;
            m_currentPlayer.GetComponent<Player.PlayerInputsHandler>().UnregisterObserver(this);
            m_currentPlayer.GetComponent<Player.PlayerInputsHandler>().DeactivateInputs();
            m_currentStage.EndPortal.OnPlayerEnteredPortal -= OnPlayerEnteredEndPortal;
            m_currentPlayer.UIManager.UnregisterEvents();
            base.UnregisterEvents();
        }

        public override void ExitState()
        {
            m_currentPlayer.GetComponent<Player.PlayerMovementController>().DeactivateMovement();
            m_currentPlayer.GetComponent<Player.PlayerInputsHandler>().DeactivateInputs();
            Debug.LogError("EXIT GAME STATE");
            base.ExitState();
        }

        #region Utils
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
        #endregion
    }
}
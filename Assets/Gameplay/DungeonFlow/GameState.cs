

using UnityEngine;

namespace RLS.Gameplay.DungeonFlow
{
    public class GameState : DungeonGameState
    {
        private Player.Player m_currentPlayer = null;
        private Levels.Stage m_currentStage = null;

        internal override void RegisterReferences()
        {
            base.RegisterReferences();
            m_currentPlayer = FindObjectOfType<Player.Player>();
            m_currentStage = FindObjectOfType<Levels.Stage>();

            var playerPanel = GetPanel<Player.UI.PlayerPanel>();
            m_currentPlayer.UIManager.Init(playerPanel);
        }

        internal override void RegisterEvents()
        {
            base.RegisterEvents();
            m_currentStage.EndPortal.OnPlayerEnteredPortal += OnPlayerEnteredEndPortal;
            m_currentPlayer.UIManager.RegisterEvents();
        }

        internal override void SetUpDependencies()
        {
            base.SetUpDependencies();
            m_currentPlayer.transform.SetPositionAndRotation(m_currentStage.SpawningPosition.position, m_currentStage.SpawningPosition.rotation);
        }
        public override void EnterState()
        {
            base.EnterState();
            m_currentPlayer.GetComponent<Player.PlayerInputsHandler>().ActivateInputs();
            m_currentPlayer.UIManager.RefreshPlayerInfos();

            Debug.LogError("ENTER GAME STATE");
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if(m_currentPlayer.transform.position.y < -100f)
            {
                m_currentPlayer.transform.SetPositionAndRotation(m_currentStage.SpawningPosition.position, m_currentStage.SpawningPosition.rotation);
            }
        }

        private void OnPlayerEnteredEndPortal()
        {
            m_currentStage.EndPortal.OnPlayerEnteredPortal -= OnPlayerEnteredEndPortal;
            m_gamemode.LevelManager.LoadNextStage();
            
        }

        internal override void UnregisterEvents()
        {
            m_currentStage.EndPortal.OnPlayerEnteredPortal -= OnPlayerEnteredEndPortal;
            m_currentPlayer.UIManager.UnregisterEvents();
            base.UnregisterEvents();
        }

        public override void ExitState()
        {
            m_currentPlayer.GetComponent<Player.PlayerInputsHandler>().DeactivateInputs();
            Debug.LogError("EXIT GAME STATE");
            base.ExitState();
        }
    }
}
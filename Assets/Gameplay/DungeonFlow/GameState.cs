

namespace RLS.Gameplay.DungeonFlow
{
    public class GameState : DungeonGameState
    {
        private Player.PlayerMovementController m_currentPlayer = null;
        private Levels.Stage m_currentStage = null;

        internal override void RegisterReferences()
        {
            base.RegisterReferences();
            m_currentPlayer = FindObjectOfType<Player.PlayerMovementController>();
            m_currentStage = FindObjectOfType<Levels.Stage>();
            
        }

        internal override void RegisterEvents()
        {
            base.RegisterEvents();
            m_currentStage.EndPortal.OnPlayerEnteredPortal += OnPlayerEnteredEndPortal;
        }

        internal override void SetUpDependencies()
        {
            base.SetUpDependencies();
            m_currentPlayer.transform.position = m_currentStage.SpawningPosition.position;
            m_currentPlayer.transform.rotation = m_currentStage.SpawningPosition.rotation;
        }
        public override void EnterState()
        {
            base.EnterState();
            m_currentPlayer.GetComponent<Player.PlayerInputsHandler>().ActivateInputs();
        }

        private void OnPlayerEnteredEndPortal()
        {
            m_currentStage.EndPortal.OnPlayerEnteredPortal -= OnPlayerEnteredEndPortal;
            m_gamemode.LevelManager.LoadNextStage();
            
        }

        internal override void UnregisterEvents()
        {
            m_currentStage.EndPortal.OnPlayerEnteredPortal -= OnPlayerEnteredEndPortal;
            base.UnregisterEvents();
        }

        public override void ExitState()
        {
            m_currentPlayer.GetComponent<Player.PlayerInputsHandler>().DeactivateInputs();
            base.ExitState();
        }
    }
}
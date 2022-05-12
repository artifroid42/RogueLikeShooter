

namespace RLS.Gameplay.DungeonFlow
{
    public class GameState : DungeonGameState
    {
        private PlayerController.PlayerMovementController m_currentPlayer = null;
        private Levels.Stage m_currentStage = null;

        public override void EnterState()
        {
            base.EnterState();
            m_currentPlayer = FindObjectOfType<PlayerController.PlayerMovementController>();
            m_currentStage = FindObjectOfType<Levels.Stage>();

            m_currentPlayer.transform.position = m_currentStage.SpawningPosition.position;
            m_currentPlayer.transform.rotation = m_currentStage.SpawningPosition.rotation;

            m_currentStage.EndPortal.OnPlayerEnteredPortal += OnPlayerEnteredEndPortal;
        }

        private void OnPlayerEnteredEndPortal()
        {
            m_currentStage.EndPortal.OnPlayerEnteredPortal -= OnPlayerEnteredEndPortal;
            m_gamemode.LevelManager.LoadNextStage();
            
        }
    }
}
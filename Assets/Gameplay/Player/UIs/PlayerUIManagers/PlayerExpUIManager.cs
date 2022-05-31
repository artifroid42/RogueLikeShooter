using UnityEngine;

namespace RLS.Gameplay.Player.UI
{
    public class PlayerExpUIManager : APlayerUIManager
    {
        public void RefreshPlayerInfos(int a_currentLevel, float a_currentExpAmount, float a_expAmountForNextLevel)
        {
            m_playerPanel.PlayerLevelText.text = a_currentLevel.ToString();
            m_playerPanel.ExpBar.SetValue(a_currentExpAmount / a_expAmountForNextLevel);
        }

        public void SetLevelText(int a_level)
        {
            if(m_playerPanel != null)
            {
                m_playerPanel.PlayerLevelText.text = a_level.ToString();
            }
        }

        public void SetExpBarValue(float a_expRatio)
        {
            m_playerPanel.ExpBar.SetValue(a_expRatio);
        }

        private void Update()
        {
            m_debugPanel?.UpdatePositionDisplay(transform.position);
        }
    }
}

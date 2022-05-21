using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Player.UI
{
    public class PlayerUIManager : MonoBehaviour
    {
        private PlayerPanel m_playerPanel;
        private Player m_player;

        private Debug.PlayerDebugPanel m_debugPanel = null;


        public void Init(PlayerPanel a_playerPanel, Debug.PlayerDebugPanel a_debugPanel)
        {
            m_playerPanel = a_playerPanel;
            m_debugPanel = a_debugPanel;
        }

        public void RefreshPlayerInfos()
        {
            m_playerPanel.PlayerLevelText.text = m_player.CurrentLevel.ToString();
            m_playerPanel.ExpBar.SetValue(m_player.CurrentExpAmount / m_player.ExpAmountForNextLevel);
        }

        public void RegisterEvents()
        {
            m_player = FindObjectOfType<Player>();

            m_player.OnExpChanged += HandleExpChanged;
            m_player.OnLevelChanged += HandleLevelChanged;
        }

        public void UnregisterEvents()
        {
            m_player.OnExpChanged -= HandleExpChanged;
            m_player.OnLevelChanged -= HandleLevelChanged;
        }

        private void HandleLevelChanged(int a_level)
        {
            if(m_playerPanel != null)
            {
                m_playerPanel.PlayerLevelText.text = a_level.ToString();
            }
        }

        private void HandleExpChanged(float a_expAmount)
        {
            SetExpBarValue(a_expAmount);
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

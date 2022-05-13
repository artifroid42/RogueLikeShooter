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


        public void Init(PlayerPanel a_playerPanel)
        {
            m_playerPanel = a_playerPanel;
            m_player = FindObjectOfType<Player>();

            m_player.OnExpChanged += HandleExpChanged;
        }

        private void HandleExpChanged(float a_expAmount)
        {
            SetExpBarValue(a_expAmount);
        }

        public void SetExpBarValue(float a_expRatio)
        {
            m_playerPanel.ExpBar.SetValue(a_expRatio);
        }
    }
}

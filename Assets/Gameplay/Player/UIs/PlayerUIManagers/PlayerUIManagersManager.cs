using RLS.Gameplay.Player.UI.Debug;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Player.UI
{
    public class PlayerUIManagersManager : MonoBehaviour
    {
        [SerializeField] private PlayerPanel m_playerPanel;
        [SerializeField] private Debug.PlayerDebugPanel m_playerDebugPanel;

        [SerializeField] private PlayerUpgradesManager m_upgradesManager;
        [SerializeField] private PlayerExpManager m_expManager;

        [SerializeField] private PlayerUpgradesUIManager m_upgradesUIManager;
        [SerializeField] private PlayerExpUIManager m_expUIManager;

        public PlayerUpgradesUIManager UpgradesUIManager => m_upgradesUIManager;
        public PlayerExpUIManager ExpUIManager => m_expUIManager;
        public PlayerUpgradesManager UpgradesManager => m_upgradesManager;
        public PlayerExpManager ExpManager => m_expManager;
        public PlayerPanel PlayerPanel => m_playerPanel;
        public PlayerDebugPanel PlayerDebugPanel => m_playerDebugPanel;



        public void RegisterEvents()
        {

        }

        public void UnregisterEvents()
        {

        }

        internal void Init()
        {
            m_expUIManager.Init(m_playerPanel, m_playerDebugPanel);
            m_upgradesUIManager.Init(m_playerPanel, m_playerDebugPanel);
        }
    }
}

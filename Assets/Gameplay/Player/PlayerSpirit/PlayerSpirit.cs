using System;
using UnityEngine;

namespace RLS.Gameplay.Player
{
    public class PlayerSpirit : MonoBehaviour, IPlayerInputsObserver
    {
        [Header("Components Refs")]
        [SerializeField]
        private Upgrades.PlayerStatsData m_playerStatsData;
        [SerializeField]
        private UI.PlayerUIManagersManager m_playerUIManagersManager;
        [SerializeField]
        private PlayerExpManager m_playerExpManager;
        [SerializeField]
        private PlayerUpgradesManager m_playerUpgradesManager;

        public UI.PlayerUIManagersManager PlayerUIManagersManager { get => m_playerUIManagersManager; set => m_playerUIManagersManager = value; }

        public PlayerExpManager PlayerExpManager => m_playerExpManager;
        public PlayerUpgradesManager PlayerUpgradesManager => m_playerUpgradesManager;

        public void InitPlayer()
        {
            m_playerUIManagersManager.Init();
            m_playerExpManager.Init();
        }

        public void RegisterEvents()
        {
            PlayerUIManagersManager.RegisterEvents();
        }

        public void UnregisterEvents()
        {
            PlayerUIManagersManager.UnregisterEvents();
        }

        public Player CurrentPlayer { private set; get; } = null;

        public void HandleGameLevelChanged()
        {
            CurrentPlayer = FindObjectOfType<Player>();
            CurrentPlayer.GetComponent<PlayerInputsHandler>().RegisterNewObserver(this);
        }

        public void HandleUpgradeOneInput()
        {

        }

        public void HandleUpgradeTwoInput()
        {

        }

        public void HandleUpgradeThreeInput()
        {

        }

        public void HandleUpgradeFourInput()
        {

        }

        public void HandleUpgradeFiveInput()
        {

        }

        public void HandleUpgradeSixInput()
        {

        }
    }
}
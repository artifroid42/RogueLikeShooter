using RLS.Gameplay.Player.Upgrades;
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

        private EClass m_currentClass;
        public EClass CurrentClass => m_currentClass;

        public void InitPlayer()
        {
            m_playerUIManagersManager.Init();
            m_playerExpManager.Init();
            m_playerUpgradesManager.Init();
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
            m_currentClass = CurrentPlayer.Class;
        }

        public void HandleUpgradeOneInput()
        {
            PlayerUpgradesManager.UpgradeInputDown(1);
        }

        public void HandleUpgradeTwoInput()
        {
            PlayerUpgradesManager.UpgradeInputDown(2);
        }

        public void HandleUpgradeThreeInput()
        {
            PlayerUpgradesManager.UpgradeInputDown(3);
        }

        public void HandleUpgradeFourInput()
        {
            PlayerUpgradesManager.UpgradeInputDown(4);
        }

        public void HandleUpgradeFiveInput()
        {
            PlayerUpgradesManager.UpgradeInputDown(5);
        }

        public void HandleUpgradeSixInput()
        {
            PlayerUpgradesManager.UpgradeInputDown(6);
        }
    }
}
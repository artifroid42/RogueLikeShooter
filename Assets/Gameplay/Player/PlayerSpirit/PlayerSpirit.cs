using RLS.Gameplay.Combat;
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
        [SerializeField]
        private PlayerCombatInfosManager m_combatInfosManager = null;

        public UI.PlayerUIManagersManager PlayerUIManagersManager { get => m_playerUIManagersManager; set => m_playerUIManagersManager = value; }

        public PlayerExpManager PlayerExpManager => m_playerExpManager;
        public PlayerUpgradesManager PlayerUpgradesManager => m_playerUpgradesManager;

        private EClass m_currentClass;
        public EClass CurrentClass => m_currentClass;

        private bool m_isInitialized = false;

        private void Update()
        {
            if (m_isInitialized)
            {
                float cooldown = 0f;
                switch (m_currentClass)
                {
                    case EClass.Ninja:
                        if(CurrentPlayer != null)
                            cooldown = CurrentPlayer.PlayerMovementController.PowerCooldownRatio;
                        break;
                    case EClass.Pirate:
                        cooldown = m_combatInfosManager.CombatController.PowerCooldownRatio;
                        break;
                    case EClass.SciFi:
                        cooldown = m_combatInfosManager.CombatController.PowerCooldownRatio;
                        break;
                }
                m_playerUIManagersManager.PlayerPanel.PowerCouldownWidget.SetCouldownValue(cooldown);
            }
        }

        public void InitPlayer()
        {
            m_playerUIManagersManager.Init();
            m_playerExpManager.Init();
            m_playerUpgradesManager.Init();
            m_isInitialized = true;
        }

        public void RegisterEvents()
        {
            PlayerUIManagersManager.RegisterEvents();
            m_playerUpgradesManager.OnPlayerUpgraded += HandlePlayerUpgraded;
            m_combatInfosManager.CombatController.OnDamageGiven += HandleDamageGiven;
        }

        public void UnregisterEvents()
        {
            PlayerUIManagersManager.UnregisterEvents();
            m_playerUpgradesManager.OnPlayerUpgraded -= HandlePlayerUpgraded;
            m_combatInfosManager.CombatController.OnDamageGiven -= HandleDamageGiven;
        }

        public Player CurrentPlayer { private set; get; } = null;

        private void HandleDamageGiven(CombatController obj)
        {
            m_playerUIManagersManager.PlayerPanel.Cursor.PlayHitFeedback();
        }

        public void HandleGameLevelChanged()
        {
            CurrentPlayer = FindObjectOfType<Player>();
            CurrentPlayer.GetComponent<PlayerInputsHandler>().RegisterNewObserver(this);
            m_currentClass = CurrentPlayer.Class;
            m_combatInfosManager.SetCombatControllerRef(CurrentPlayer.GetComponent<PlayerCombatController>());
            m_playerUpgradesManager.SetPlayerRef(CurrentPlayer);
        }

        private void HandlePlayerUpgraded()
        {
            m_playerUpgradesManager.RefreshStats();
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
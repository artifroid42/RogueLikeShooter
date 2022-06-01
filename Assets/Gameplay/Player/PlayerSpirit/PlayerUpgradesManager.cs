using RLS.Gameplay.Player.Upgrades;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Player
{
    public enum EUpgradeState
    {
        Idle,
        ClassSelection,
        ClassUpgrade
    }

    public class PlayerUpgradesManager : MonoBehaviour
    {
        [SerializeField]
        private Upgrades.PlayerStatsData m_playerStatsData;
        [SerializeField]
        private UI.PlayerUIManagersManager m_playerUIManagersManager;

        private EUpgradeState m_upgradeState = EUpgradeState.Idle;

        private EClass m_classToUpgrade;
        private ClassStatsData m_currentStatsData;

        public int HealthLevel = 1;
        public int DamagesLevel = 1;
        public int AttackSpeedLevel = 1;
        public int MoveSpeedLevel = 1;
        public int PowerLevel = 1;

        public int m_upgradesAppliedCount = 0;

        internal void Init()
        {
            m_upgradeState = EUpgradeState.Idle;
            m_playerUIManagersManager.PlayerPanel.ClassUpgradesModule.Init();
        }

        internal void UpgradeInputDown(int a_inputNumber)
        {
            switch (m_upgradeState)
            {
                case EUpgradeState.Idle:
                    break;
                case EUpgradeState.ClassSelection:
                    switch (a_inputNumber)
                    {
                        case 1:
                            m_classToUpgrade = EClass.Ninja;
                            m_currentStatsData = m_playerStatsData.NinjaStatsData;
                            break;
                        case 2:
                            m_classToUpgrade = EClass.Pirate;
                            m_currentStatsData = m_playerStatsData.PirateStatsData;
                            break;
                        case 3:
                            m_classToUpgrade = EClass.SciFi;
                            m_currentStatsData = m_playerStatsData.ScifiStatsData;
                            break;
                    }

                    ShowClassUpgrades();
                    break;

                case EUpgradeState.ClassUpgrade:

                    switch (a_inputNumber)
                    {
                        case 1:
                            UpgradeHealth();
                            break;
                        case 2:
                            UpgradeDamages();
                            break;
                        case 3:
                            UpgradeAttackSpeed();
                            break;
                        case 4:
                            UpgradeMoveSpeed();
                            break;
                        case 5:
                            UpgradePower();
                            break;
                        case 6:
                            BackToClassSelection();
                            break;
                    }
                    if(m_upgradesAppliedCount == m_playerUIManagersManager.ExpManager.CurrentLevel - 1) // -1 car on ne veut pas upgrade au niveau 1
                    {
                        Hide();
                    }
                    break;
            }
        }

        private void BackToClassSelection()
        {
            if (m_upgradeState == EUpgradeState.ClassUpgrade)
            {
                ShowClassSelection();
            }
        }

        private void UpgradePower()
        {
            if (m_currentStatsData != null && PowerLevel < m_currentStatsData.MaxUpgradesCount)
            {
                Debug.Log("PowerLevel");
                m_upgradesAppliedCount++;
            }
        }

        private void UpgradeMoveSpeed()
        {
            if (m_currentStatsData != null && MoveSpeedLevel < m_currentStatsData.MaxUpgradesCount)
            {
                Debug.Log("MoveSpeedLevel");
                m_upgradesAppliedCount++;
            }
        }

        private void UpgradeAttackSpeed()
        {
            if (m_currentStatsData != null && AttackSpeedLevel < m_currentStatsData.MaxUpgradesCount)
            {
                Debug.Log("AttackSpeedLevel");
                m_upgradesAppliedCount++;
            }
        }

        private void UpgradeDamages()
        {
            if (m_currentStatsData != null && DamagesLevel < m_currentStatsData.MaxUpgradesCount)
            {
                Debug.Log("DamagesLevel");
                m_upgradesAppliedCount++;
            }
        }

        private void UpgradeHealth()
        {
            if (m_currentStatsData != null && HealthLevel < m_currentStatsData.MaxUpgradesCount)
            {
                Debug.Log("HealthLevel");
                m_upgradesAppliedCount++;
            }
        }

        public void ShowClassSelection()
        {
            if (m_upgradeState != EUpgradeState.ClassSelection)
            {
                m_playerUIManagersManager.PlayerPanel.ClassUpgradesModule.ShowClassSelection();
                m_upgradeState = EUpgradeState.ClassSelection;
            }
        }

        public void ShowClassUpgrades()
        {
            if (m_upgradeState != EUpgradeState.ClassUpgrade)
            {
                m_playerUIManagersManager.PlayerPanel.ClassUpgradesModule.ShowClassUpgrades(m_currentStatsData.ClassSprite);
                m_upgradeState = EUpgradeState.ClassUpgrade;
            }
        }

        public void Hide()
        {
            if(m_upgradeState != EUpgradeState.Idle)
            {
                m_playerUIManagersManager.PlayerPanel.ClassUpgradesModule.Hide();
                m_upgradeState = EUpgradeState.Idle;
            }
        }
    }
}


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
        private Player m_currentPlayer = null;

        public int m_upgradesAppliedCount = 0;

        public Action OnPlayerUpgraded;

        [SerializeField]
        private List<ClassUpgrades> m_classesUpgrades;

        [Serializable]
        internal class ClassUpgrades
        {
            public EClass Class;
            public List<UpgradeLevel> UpgradeLevels;
        }

        [Serializable]
        internal class UpgradeLevel
        {
            public EUpgrade Type;
            public int Level;
        }

        internal void Init()
        {
            m_upgradeState = EUpgradeState.Idle;
            m_playerUIManagersManager.PlayerPanel.ClassUpgradesModule.Init();
        }

        public void SetPlayerRef(Player a_player)
        {
            m_currentPlayer = a_player;
        }

        internal void RefreshStats()
        {
            throw new NotImplementedException();
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
                    UpdateUpgradesSliders();
                    if(m_upgradesAppliedCount == m_playerUIManagersManager.ExpManager.CurrentLevel - 1) // -1 car on ne veut pas upgrade au niveau 1
                    {
                        Hide();
                    }
                    break;
            }
        }



        private void Upgrade(EClass a_class, EUpgrade a_upgrade)
        {
            var upgradeLevel = m_classesUpgrades.Find(x => x.Class == a_class).UpgradeLevels.Find(x => x.Type == a_upgrade);
            upgradeLevel.Level++;
            m_upgradesAppliedCount++;
            OnPlayerUpgraded?.Invoke();
        }

        private void UpdateUpgradesSliders()
        {
            foreach (var lineWidget in m_playerUIManagersManager.PlayerPanel.ClassUpgradesModule.UpgradeLineWidgets)
            {
                var classUpgrades = m_classesUpgrades.Find(x => x.Class == m_classToUpgrade);
                var upgradeLevel = classUpgrades.UpgradeLevels.Find(x => x.Type == lineWidget.Upgrades).Level;
                lineWidget.SetUpgradeSliderValue((float)upgradeLevel / (float)m_currentStatsData.MaxUpgradesCount);
                Debug.Log("" + (float)upgradeLevel / (float)m_currentStatsData.MaxUpgradesCount);
            }
        }

        private bool CheckUpgrade(EClass a_class, EUpgrade a_upgrade)
        {
            return m_classesUpgrades.Find(x => x.Class == a_class).UpgradeLevels.Find(x => x.Type == a_upgrade).Level < m_currentStatsData.MaxUpgradesCount;
        }

        private void BackToClassSelection()
        {
            if (m_upgradeState == EUpgradeState.ClassUpgrade)
            {
                m_upgradeState = EUpgradeState.Idle; 
                ShowClassSelection();
            }
        }

        private void UpgradePower()
        {
            if (m_currentStatsData != null && CheckUpgrade(m_currentStatsData.Class, EUpgrade.Power))
            {
                Upgrade(m_currentStatsData.Class, EUpgrade.Power);
            }
        }

        private void UpgradeMoveSpeed()
        {
            if (m_currentStatsData != null && CheckUpgrade(m_currentStatsData.Class, EUpgrade.MoveSpeed))
            {
                Upgrade(m_currentStatsData.Class, EUpgrade.MoveSpeed);
            }
        }

        private void UpgradeAttackSpeed()
        {
            if (m_currentStatsData != null && CheckUpgrade(m_currentStatsData.Class, EUpgrade.AttackSpeed))
            {
                Upgrade(m_currentStatsData.Class, EUpgrade.AttackSpeed);
            }
        }

        private void UpgradeDamages()
        {
            if (m_currentStatsData != null && CheckUpgrade(m_currentStatsData.Class, EUpgrade.Damage))
            {
                Upgrade(m_currentStatsData.Class, EUpgrade.Damage);
            }
        }

        private void UpgradeHealth()
        {
            if (m_currentStatsData != null && CheckUpgrade(m_currentStatsData.Class, EUpgrade.Health))
            {
                Upgrade(m_currentStatsData.Class, EUpgrade.Health);
            }
        }

        public void ShowClassSelection()
        {
            if (m_upgradeState == EUpgradeState.Idle)
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
                UpdateUpgradesSliders();
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


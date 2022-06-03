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
        
        [Header("Upgrades Data")]
        [SerializeField]
        private NinjaUpgradesData m_ninjaUpgradesData = null;
        [SerializeField]
        private PirateUpgradesData m_pirateUpgradesData = null;
        [SerializeField]
        private ScifiUpgradesData m_scifiUpgradesData = null;

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
            m_playerUIManagersManager.PlayerPanel.ClassUpgradesModule.Init();
        }

        public void SetPlayerRef(Player a_player)
        {
            m_currentPlayer = a_player;
            RefreshStats();
        }

        internal void RefreshStats()
        {
            SetUpHealth();
            SetUpMoveSpeed();
            SetUpAttackSpeed();
            SetUpDamages();
            SetUpPower();
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
                    if(a_inputNumber == 6)
                    {
                        BackToClassSelection();
                    }
                    else
                    {
                        if (m_currentStatsData != null && CheckUpgrade(m_currentStatsData.Class, (EUpgrade)(a_inputNumber + 1)))
                        {
                            IncreaseLevelUpgrade(m_currentStatsData.Class, (EUpgrade)(a_inputNumber + 1));
                        }
                    }     
                    
                    UpdateUpgradesSliders();
                    if(m_upgradesAppliedCount == m_playerUIManagersManager.ExpManager.CurrentLevel - 1) // -1 car on ne veut pas upgrade au niveau 1
                    {
                        Hide();
                    }
                    break;
            }
        }



        private UpgradeLevel IncreaseLevelUpgrade(EClass a_class, EUpgrade a_upgrade)
        {
            var upgradeLevel = m_classesUpgrades.Find(x => x.Class == a_class).UpgradeLevels.Find(x => x.Type == a_upgrade);
            upgradeLevel.Level++;
            m_upgradesAppliedCount++;
            OnPlayerUpgraded?.Invoke();
            return upgradeLevel;
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


        private void SetUpPower()
        {
            var upgradeLevel = m_classesUpgrades.Find(x => x.Class == m_currentPlayer.Class).
                UpgradeLevels.Find(x => x.Type == EUpgrade.Power);
            switch (m_currentPlayer.Class)
            {
                case EClass.Ninja:
                    m_currentPlayer.GetComponent<Ninja.NinjaPlayerMovement>().DashCooldown = 
                        m_ninjaUpgradesData.NinjaPowerUpgrades[upgradeLevel.Level - 1].DashCouldown;
                    m_currentPlayer.GetComponent<Ninja.NinjaPlayerMovement>().
                        SetDashRange(m_ninjaUpgradesData.NinjaPowerUpgrades[upgradeLevel.Level - 1].DashRange);
                    break;
                case EClass.Pirate:
                    m_currentPlayer.GetComponent<Pirate.PirateCombatController>().BarrelAttackCooldown = 
                        m_pirateUpgradesData.PiratePowerUpgrades[upgradeLevel.Level - 1].BarilCouldown;
                    m_currentPlayer.GetComponent<Pirate.PirateCombatController>().BarrelExplosionDamage =
                        m_pirateUpgradesData.PiratePowerUpgrades[upgradeLevel.Level - 1].BarilDamages;
                    break;
                case EClass.SciFi:
                    m_currentPlayer.GetComponent<SciFi.SciFiCombatController>().PowerShotDamage =
                        m_scifiUpgradesData.SficiPowerUpgrades[upgradeLevel.Level - 1].PowerShotDamages;
                    m_currentPlayer.GetComponent<SciFi.SciFiCombatController>().PowerShotLoadingDuration =
                        m_scifiUpgradesData.SficiPowerUpgrades[upgradeLevel.Level - 1].PowerShotLoadingTime;
                    break;
            }
        }

        private void SetUpMoveSpeed()
        {
            var upgradeLevel = m_classesUpgrades.Find(x => x.Class == m_currentPlayer.Class).
                UpgradeLevels.Find(x => x.Type == EUpgrade.MoveSpeed);
            switch (m_currentPlayer.Class)
            {
                case EClass.Ninja:
                    m_currentPlayer.GetComponent<PlayerMovementController>().BaseSpeed = m_ninjaUpgradesData.
                        MoveSpeedLevels[upgradeLevel.Level - 1];
                    break;
                case EClass.Pirate:
                    m_currentPlayer.GetComponent<PlayerMovementController>().BaseSpeed = m_pirateUpgradesData.
                        MoveSpeedLevels[upgradeLevel.Level - 1];
                    break;
                case EClass.SciFi:
                    m_currentPlayer.GetComponent<PlayerMovementController>().BaseSpeed = m_scifiUpgradesData.
                        MoveSpeedLevels[upgradeLevel.Level - 1];
                    break;
            }
        }

        private void SetUpAttackSpeed()
        {
            var upgradeLevel = m_classesUpgrades.Find(x => x.Class == m_currentPlayer.Class).
                UpgradeLevels.Find(x => x.Type == EUpgrade.AttackSpeed);
            switch (m_currentPlayer.Class)
            {
                case EClass.Ninja:
                    m_currentPlayer.GetComponent<Ninja.NinjaCombatController>().AttackCooldown =
                        1f / m_ninjaUpgradesData.AttackSpeedLevels[upgradeLevel.Level - 1];
                    break;
                case EClass.Pirate:
                    m_currentPlayer.GetComponent<Pirate.PirateCombatController>().SwordAttackCooldown =
                        1f / m_pirateUpgradesData.AttackSpeedLevels[upgradeLevel.Level - 1];
                    break;
                case EClass.SciFi:
                    m_currentPlayer.GetComponent<SciFi.SciFiCombatController>().AttackSpeed =
                        m_scifiUpgradesData.AttackSpeedLevels[upgradeLevel.Level - 1];
                    break;
            }
        }

        private void SetUpDamages()
        {
            var upgradeLevel = m_classesUpgrades.Find(x => x.Class == m_currentPlayer.Class).
                UpgradeLevels.Find(x => x.Type == EUpgrade.Damage);
            switch (m_currentPlayer.Class)
            {
                case EClass.Ninja:
                    m_currentPlayer.GetComponent<Ninja.NinjaCombatController>().ShurikenDamage =
                        m_ninjaUpgradesData.DamageLevels[upgradeLevel.Level - 1];
                    break;
                case EClass.Pirate:
                    m_currentPlayer.GetComponent<Pirate.PirateCombatController>().PirateSword.
                        SetDamageToDeal(m_pirateUpgradesData.DamageLevels[upgradeLevel.Level - 1]);
                    break;
                case EClass.SciFi:
                    m_currentPlayer.GetComponent<SciFi.SciFiCombatController>().LaserBulletDamage =
                        m_scifiUpgradesData.DamageLevels[upgradeLevel.Level - 1];
                    break;
            }
        }

        private void SetUpHealth()
        {
            var upgradeLevel = m_classesUpgrades.Find(x => x.Class == m_currentPlayer.Class).
                UpgradeLevels.Find(x => x.Type == EUpgrade.Health);
            switch (m_currentPlayer.Class)
            {
                case EClass.Ninja:
                        m_currentPlayer.GetComponent<Combat.CombatController>().
                            SetMaxLifePoints(m_ninjaUpgradesData.HealthLevels[upgradeLevel.Level - 1],
                            Combat.ECurrentLifeBehaviourWhenChangingMaxLife.ChangeProportionally);
                    break;

                case EClass.Pirate:
                        m_currentPlayer.GetComponent<Combat.CombatController>().
                            SetMaxLifePoints(m_ninjaUpgradesData.HealthLevels[upgradeLevel.Level - 1],
                            Combat.ECurrentLifeBehaviourWhenChangingMaxLife.ChangeProportionally);
                    break;

                case EClass.SciFi:
                        m_currentPlayer.GetComponent<Combat.CombatController>().
                            SetMaxLifePoints(m_ninjaUpgradesData.HealthLevels[upgradeLevel.Level - 1],
                            Combat.ECurrentLifeBehaviourWhenChangingMaxLife.ChangeProportionally);
                    break;
            }
            
        }

        public void ShowClassSelection()
        {
            if (m_upgradeState != EUpgradeState.ClassUpgrade)
            {
                m_playerUIManagersManager.PlayerPanel.ClassUpgradesModule.ShowClassSelection();
                m_upgradeState = EUpgradeState.ClassSelection;
            }
        }

        private IEnumerator ShowClassSelection_Routine()
        {
            yield return null;
            m_playerUIManagersManager.PlayerPanel.ClassUpgradesModule.ShowClassSelection();
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

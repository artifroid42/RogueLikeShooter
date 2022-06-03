using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Player
{
    public class PlayerExpManager : MonoBehaviour
    {
        [SerializeField]
        private Upgrades.PlayerStatsData m_playerStatsData;
        [SerializeField]
        private UI.PlayerUIManagersManager m_playerUIManagersManager;

        [Header("Params")]
        [SerializeField]
        private float m_currentExpAmount;
        private float m_expAmountForNextLevel;

        [Header("EASY MODE ONLY")]
        [SerializeField]
        private float m_startingLevelRatio = 0.5f;

        private int m_currentLevel;

        public int CurrentLevel
        {
            get => m_currentLevel;
            set
            {
                m_currentLevel = value;
                m_playerUIManagersManager.ExpUIManager.SetLevelText(m_currentLevel);
            }
        }

        public float ExpAmountForNextLevel => m_expAmountForNextLevel;

        public float CurrentExpAmount
        {
            get => m_currentExpAmount;
            set
            {
                m_currentExpAmount = value;
                CheckForNextLevel();
                m_playerUIManagersManager.ExpUIManager.SetExpBarValue(m_currentExpAmount / m_expAmountForNextLevel);
            }
        }

        public void Init()
        {
            int startLevel = GetStartLevel();
            CurrentExpAmount = 0;
            for (int i = 0; i < startLevel; i++)
            {
                LevelUp();
            }
            if(startLevel == 1)
            {
                m_playerUIManagersManager.PlayerPanel.ClassUpgradesModule.Hide();
            }
        } 

        #region Experience
        public void EarnExp(float a_expAmount)
        {
            CurrentExpAmount += a_expAmount;
        }

        private void CheckForNextLevel()
        {
            float levelRatio = (float)CurrentLevel / (float)m_playerStatsData.MaxLevel;
            m_expAmountForNextLevel = m_playerStatsData.ExperienceLevelProgression.Evaluate(levelRatio) * m_playerStatsData.ExpAmountForLevelMax;

            if (CurrentExpAmount >= m_expAmountForNextLevel)
            {
                CurrentExpAmount -= m_expAmountForNextLevel;
                LevelUp();
                CheckForNextLevel();
            }
        }

        private void LevelUp()
        {
            CurrentLevel++;
            m_playerUIManagersManager.UpgradesManager.ShowClassSelection();  
        }


        private int GetStartLevel()
        {
            var modeData = MOtter.MOtt.GM.GetCurrentMainStateMachine<DungeonFlow.DungeonGameMode>().ModeSelectedData;
            int startLevel = 1;
            if (modeData.IsEasyMode)
            {
                startLevel = Mathf.RoundToInt(MOtter.MOtt.SAVE.MaximumLevelReached * m_startingLevelRatio);
                if (startLevel < 1) startLevel = 1;
            }
            startLevel = 1;
            return startLevel;
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                EarnExp(500f);
                Debug.Log("EARN EXP");
            }
        }
#endif
        #endregion
    }

}

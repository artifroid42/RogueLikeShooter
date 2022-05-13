using RLS.Gameplay.Player.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private Upgrades.PlayerStatsData m_playerStatsData;
        [SerializeField]
        private UI.PlayerUIManager m_UIManager;

        private int m_currentLevel;

        [SerializeField]
        private float m_currentExpAmount;
        private float m_expAmountForNextLevel;

        public PlayerUIManager UIManager { get => m_UIManager; set => m_UIManager = value; }

        public Action<float> OnExpChanged;

        public float CurrentExpAmount 
        { 
            get => m_currentExpAmount; 
            set 
            { 
                m_currentExpAmount = value;
                CheckForNextLevel();
                OnExpChanged?.Invoke(m_currentExpAmount / m_expAmountForNextLevel);
            }
        }

        public void InitPlayer()
        {
            m_currentLevel = GetStartLevel();

            

            CurrentExpAmount = 0;
        }

        /// <summary>
        /// Last level reached / 2
        /// </summary>
        private int GetStartLevel()
        {
            return 1; //TODO : return Last level reached / 2
        }

        #region Experience
        public void EarnExp(float a_expAmount)
        {
            CurrentExpAmount += a_expAmount;
        }

        private void CheckForNextLevel()
        {
            float levelRatio = (float)m_currentLevel / (float)m_playerStatsData.MaxLevel;
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
            m_currentLevel++;
            CurrentExpAmount = 0;
            CheckForNextLevel();
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                EarnExp(100f);
                Debug.Log("EARN EXP");
            }
        }
#endif
        #endregion
    }
}

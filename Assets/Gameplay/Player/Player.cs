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

        private int m_currentLevel;
        private float m_currentExpAmount;
        private float m_expAmountForNextLevel;

        private void Start()
        {
            m_currentExpAmount = 0;
            m_currentLevel = GetStartLevel();
        }

        /// <summary>
        /// Last level reached / 2
        /// </summary>
        private int GetStartLevel()
        {
            return 1;
        }

        #region Experience
        public void EarnExp(float a_expAmount)
        {
            m_currentExpAmount += a_expAmount;
            CheckForNextLevel();
        }

        private void CheckForNextLevel()
        {
            if(m_currentExpAmount >= m_expAmountForNextLevel)
            {
                m_currentExpAmount -= m_expAmountForNextLevel;
                LevelUp();
                CheckForNextLevel();
            }   
            //m_panel.bar 
        }

        private void LevelUp()
        {
            m_currentLevel++;
            m_currentExpAmount = 0;

            float levelRatio = (float)m_currentLevel / (float)m_playerStatsData.MaxLevel;
            m_expAmountForNextLevel = m_playerStatsData.ExperienceLevelProgression.Evaluate(levelRatio);
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                EarnExp(100f);
            }
        }
#endif
        #endregion
    }
}

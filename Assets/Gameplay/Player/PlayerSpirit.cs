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
        private UI.PlayerUIManager m_UIManager;

        private int m_currentLevel;

        [Header("Params")]
        [SerializeField]
        private float m_currentExpAmount;
        private float m_expAmountForNextLevel;

        public UI.PlayerUIManager UIManager { get => m_UIManager; set => m_UIManager = value; }

        public Action<float> OnExpChanged;
        public Action<int> OnLevelChanged;

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

        public int CurrentLevel
        {
            get => m_currentLevel;
            set
            {
                m_currentLevel = value;
                OnLevelChanged?.Invoke(m_currentLevel);
            }
        }

        public float ExpAmountForNextLevel => m_expAmountForNextLevel;

        public void InitPlayer()
        {
            CurrentLevel = GetStartLevel();
            CurrentExpAmount = 0;
        }

        /// <summary>
        /// Last level reached / 2
        /// </summary>
        private int GetStartLevel()
        {
            return 1; //TODO : return Last level reached / 2
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
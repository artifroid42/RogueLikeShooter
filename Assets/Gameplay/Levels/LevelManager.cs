using System;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace RLS.Gameplay.Levels
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private Data.GameConfigurationData m_gameConfiguration = null;

        public Action OnLoadingStarted = null;
        public Action OnLoadingEnded = null;

        private Data.StageData m_currentStageData = null;
        private AsyncOperationHandle<SceneInstance> m_currentSceneHandle = default;
        private AsyncOperationHandle<SceneInstance> m_currentCamSceneHandle = default;

        private bool m_isLoadingAStage = false;

        public void LoadNextStage()
        {
            Debug.Log("Asking to load next stage");
            if (m_isLoadingAStage)
            {
                Debug.LogError("Already loading a stage !");
                return;
            }
            OnLoadingStarted?.Invoke();
            m_isLoadingAStage = true;
            unload_current_stage();

        }

        private void unload_current_stage()
        {
            if (m_currentStageData != null)
            {
                Debug.Log("Unloading Current Stage");
                m_currentStageData.OnStageUnloaded += HandleCurrentStageUnloaded;
                m_currentStageData.UnloadStage(m_currentSceneHandle, m_currentCamSceneHandle);
            }
            else
            {
                load_next_stage();
            }
        }

        private void HandleCurrentStageUnloaded()
        {
            Debug.Log("Current Stage Unloaded");
            m_currentStageData.OnStageUnloaded -= HandleCurrentStageUnloaded;
            load_next_stage();
        }

        private void load_next_stage()
        {
            Debug.Log("Loading Next Stage");
            m_currentStageData = m_gameConfiguration.StageListData.GetRandomStageData();

            m_currentStageData.OnStageLoaded += HandleStageLoaded;
            m_currentStageData.LoadStage();
        }

        private void HandleStageLoaded(AsyncOperationHandle<SceneInstance> obj, AsyncOperationHandle<SceneInstance> obj2)
        {
            m_currentStageData.OnStageLoaded -= HandleStageLoaded;
            Debug.Log("Stage Loaded");
            m_currentSceneHandle = obj;
            m_currentCamSceneHandle = obj2;
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(m_currentSceneHandle.Result.Scene);
            m_isLoadingAStage = false;
            OnLoadingEnded?.Invoke();
        }
    }
}